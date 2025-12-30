// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.Caching.Memory;

namespace Aydsko.iRacingData.IntegrationTests;

internal abstract class CachingIntegrationFixture
{
    protected IMemoryCache MemoryCache { get; private set; } = default!;
    protected IDataClient Client { get; private set; }

    private CachingApiClient? _cachingApiClientBase;

    [SetUp]
    public void SetUp()
    {
        MemoryCache = new MemoryCache(new MemoryCacheOptions() { TrackStatistics = true });

        _cachingApiClientBase = new(BaseIntegrationFixture.TokenSource,
                                    BaseIntegrationFixture.DataClientOptions,
                                    MemoryCache,
                                    new TestLogger<CachingApiClient>(),
                                    new TestLogger<ApiClient>(),
                                    TimeProvider.System);

        Client = new DataClient(_cachingApiClientBase, BaseIntegrationFixture.DataClientOptions, new TestLogger<DataClient>(), TimeProvider.System);
    }

    [TearDown]
    public void TearDown()
    {
        (_cachingApiClientBase as IDisposable)?.Dispose();
        _cachingApiClientBase = null;

        (MemoryCache as IDisposable)?.Dispose();
        MemoryCache = null!;
    }
}
