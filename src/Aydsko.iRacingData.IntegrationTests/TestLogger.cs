// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.Logging;

namespace Aydsko.iRacingData.IntegrationTests;

internal sealed class TestLogger<TCategoryName>()
    : TestLogger(nameof(TCategoryName)), ILogger<TCategoryName>
{
}

internal class TestLogger(string categoryName)
    : ILogger
{
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        ArgumentNullException.ThrowIfNull(formatter);

        TestContext.Out.WriteLine($"[{logLevel,-11} | {eventId.Id} | {eventId.Name,-26}] {categoryName}: {formatter(state, exception)}");
    }
}
