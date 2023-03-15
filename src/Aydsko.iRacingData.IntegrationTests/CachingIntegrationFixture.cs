// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.Caching.Memory;

namespace Aydsko.iRacingData.IntegrationTests;

internal class CachingIntegrationFixture : BaseIntegrationFixture<CachingDataClient>
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var options = BaseSetUp();

        _memoryCache = new MemoryCache(new MemoryCacheOptions());

        Client = new CachingDataClient(_httpClient, new TestLogger<CachingDataClient>(), options, _cookieContainer, _memoryCache);
    }
}
