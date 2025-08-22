// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using System.Net;
using System.Net.Http;

namespace Aydsko.iRacingData.UnitTests;

internal abstract class MockedHttpTestBase
    : IDisposable
{
    protected static readonly int[] TestCustomerIds = [123456];

    protected CookieContainer CookieContainer { get; set; } = null!;
    protected MockedHttpMessageHandler MessageHandler { get; set; } = null!;
    protected HttpClient HttpClient { get; set; } = null!;
    private bool disposedValue;

    // NUnit will ensure that "SetUp" runs before each test so these can all be forced to "null".
    protected TestLegacyUsernamePasswordApiClient apiClient = null!;
    protected ApiClientBase apiClientBase = null!;
    protected DataClient testDataClient = null!;

    [SetUp]
    public void SetUp()
    {
        CookieContainer = new CookieContainer();
        MessageHandler = new MockedHttpMessageHandler(CookieContainer);
        HttpClient = new HttpClient(MessageHandler);

        var options = new iRacingDataClientOptions()
        {
            Username = "test.user@example.com",
            Password = "SuperSecretPassword",
            CurrentDateTimeSource = () => new DateTimeOffset(2022, 04, 05, 0, 0, 0, TimeSpan.Zero)
        };
        apiClient = new TestLegacyUsernamePasswordApiClient(HttpClient,
                                                            options,
                                                            CookieContainer,
                                                            new TestLogger<LegacyUsernamePasswordApiClient>());
        apiClientBase = new ApiClientBase(apiClient, options, new TestLogger<ApiClientBase>());
        testDataClient = new DataClient(apiClientBase, options, new TestLogger<DataClient>());
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                MessageHandler?.Dispose();
                HttpClient?.Dispose();
                apiClient?.Dispose();
                apiClientBase?.Dispose();
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
