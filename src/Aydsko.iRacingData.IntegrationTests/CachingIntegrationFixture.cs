// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.Caching.Memory;

namespace Aydsko.iRacingData.IntegrationTests;

internal abstract class CachingIntegrationFixture : BaseIntegrationFixture<CachingDataClient>
{
    protected IMemoryCache MemoryCache { get; private set; } = default!;

    [SetUp]
    public void SetUp()
    {
        var options = BaseSetUp();

        MemoryCache = new MemoryCache(new MemoryCacheOptions() { TrackStatistics = true });

        Client = new CachingDataClient(HttpClient, new TestLogger<CachingDataClient>(), options, CookieContainer, MemoryCache);
    }

    [TearDown]
    public void TearDown()
    {
        MemoryCache.Dispose();
    }
}
