// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Time.Testing;

namespace Aydsko.iRacingData.IntegrationTests;

internal abstract class CachingIntegrationFixture
    : BaseIntegrationFixture<DataClient>
{
    protected IMemoryCache MemoryCache { get; private set; } = default!;
    protected FakeTimeProvider FakeTimeProvider { get; private set; } = new FakeTimeProvider(DateTimeOffset.UtcNow);

    private LegacyUsernamePasswordApiClient? _legacyApiClient;
    private CachingApiClient? _cachingApiClientBase;

    [SetUp]
    public void SetUp()
    {
        var options = BaseSetUp();

        MemoryCache = new MemoryCache(new MemoryCacheOptions() { TrackStatistics = true });

        _legacyApiClient = new(HttpClient, options, CookieContainer, new TestLogger<LegacyUsernamePasswordApiClient>());
        _cachingApiClientBase = new(_legacyApiClient,
                                    options,
                                    MemoryCache,
                                    new TestLogger<CachingApiClient>(),
                                    new TestLogger<ApiClient>(),
                                    FakeTimeProvider);

        Client = new DataClient(_cachingApiClientBase, options, new TestLogger<DataClient>(), FakeTimeProvider);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _legacyApiClient?.Dispose();
            _cachingApiClientBase?.Dispose();
        }

        base.Dispose(disposing);
    }
}
