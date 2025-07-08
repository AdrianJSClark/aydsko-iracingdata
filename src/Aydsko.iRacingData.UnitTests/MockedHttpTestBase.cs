// © 2023-2025 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Net;
using System.Net.Http;

namespace Aydsko.iRacingData.UnitTests;

internal abstract class MockedHttpTestBase : IDisposable
{
    protected static readonly int[] TestCustomerIds = [123456];

    protected CookieContainer CookieContainer { get; set; } = null!;
    protected MockedHttpMessageHandler MessageHandler { get; set; } = null!;
    protected HttpClient HttpClient { get; set; } = null!;
    private bool disposedValue;

    // NUnit will ensure that "SetUp" runs before each test so these can all be forced to "null".
    protected DataClient testDataClient = null!;

    [SetUp]
    public void SetUp()
    {
        CookieContainer = new CookieContainer();
        MessageHandler = new MockedHttpMessageHandler(CookieContainer);
        HttpClient = new HttpClient(MessageHandler);

        testDataClient = new DataClient(HttpClient,
                                        new TestLogger<DataClient>(),
                                        new iRacingDataClientOptions()
                                        {
                                            Username = "test.user@example.com",
                                            Password = "SuperSecretPassword",
                                            CurrentDateTimeSource = () => new DateTimeOffset(2022, 04, 05, 0, 0, 0, TimeSpan.Zero)
                                        },
                                        new System.Net.CookieContainer());
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                MessageHandler?.Dispose();
                HttpClient?.Dispose();
                testDataClient?.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
