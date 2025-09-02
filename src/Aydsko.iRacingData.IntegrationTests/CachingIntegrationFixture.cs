// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.Caching.Memory;

namespace Aydsko.iRacingData.IntegrationTests;
internal abstract class CachingIntegrationFixture
    : BaseIntegrationFixture<DataClient>
{
    protected IMemoryCache MemoryCache { get; private set; } = default!;
    private LegacyUsernamePasswordApiClient? _legacyApiClient;
    private ApiClient? _apiClientBase;
    private CachingApiClient? _cachingApiClientBase;

    [SetUp]
    public void SetUp()
    {
        var options = BaseSetUp();

        MemoryCache = new MemoryCache(new MemoryCacheOptions() { TrackStatistics = true });

        _legacyApiClient = new(HttpClient, options, CookieContainer, new TestLogger<LegacyUsernamePasswordApiClient>());
        _apiClientBase = new(_legacyApiClient, options, new TestLogger<ApiClient>());
        _cachingApiClientBase = new(_apiClientBase, MemoryCache, new TestLogger<CachingApiClient>());

        Client = new DataClient(_cachingApiClientBase, options, new TestLogger<DataClient>());
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _legacyApiClient?.Dispose();
            _apiClientBase?.Dispose();
        }

        base.Dispose(disposing);
    }
}
