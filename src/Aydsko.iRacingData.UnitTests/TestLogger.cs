// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.Logging;

namespace Aydsko.iRacingData.UnitTests;

/// <summary>A fake logger implementation for testing which writes to the <see cref="Console"/>.</summary>
/// <typeparam name="T">Type context to include in the logging.</typeparam>
public class TestLogger<T> : ILogger<T>
{
    private static readonly string ForTypeName = typeof(T).FullName ?? typeof(T).Name;

    /// <inheritdoc/>
    public IDisposable BeginScope<TState>(TState state)
    {
        return new TestScope();
    }

    /// <inheritdoc/>
    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    /// <inheritdoc/>
    public void Log<TState>(LogLevel logLevel,
                            EventId eventId,
                            TState state,
                            Exception? exception,
                            Func<TState, Exception?, string> formatter)
    {
        if (formatter is null)
        {
            throw new ArgumentNullException(nameof(formatter));
        }

        var message = formatter(state, exception);

        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }

        message = $"[{logLevel} | {ForTypeName}] : {message}";

        if (exception is not null)
        {
            message += Environment.NewLine + Environment.NewLine + exception.ToString();
        }

        Console.WriteLine(message);
    }

    private sealed class TestScope : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
