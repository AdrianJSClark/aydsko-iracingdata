// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.Logging;

namespace Aydsko.iRacingData.IntegrationTests;

public class TestLogger<TCategoryName> : ILogger<TCategoryName>
{
    public IDisposable BeginScope<TState>(TState state)
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (formatter is null)
        {
            throw new ArgumentNullException(nameof(formatter));
        }

        TestContext.Out.WriteLine($"[{logLevel,-11} | {eventId.Id} | {eventId.Name,-26}] {formatter(state, exception)}");
    }
}
