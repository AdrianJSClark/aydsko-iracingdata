// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.Caching.Memory;

namespace Aydsko.iRacingData.IntegrationTests;

internal class CachingIntegrationFixture : BaseIntegrationFixture<CachingDataClient>
{
    protected IMemoryCache MemoryCache { get; private set; } = default!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var options = BaseSetUp();

        MemoryCache = new MemoryCache(new MemoryCacheOptions() { TrackStatistics = true });

        Client = new CachingDataClient(HttpClient, new TestLogger<CachingDataClient>(), options, CookieContainer, MemoryCache);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        MemoryCache.Dispose();
    }
}
