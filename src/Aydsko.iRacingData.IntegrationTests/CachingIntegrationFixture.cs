// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.Caching.Memory;

namespace Aydsko.iRacingData.IntegrationTests;

internal abstract class CachingIntegrationFixture
    : BaseIntegrationFixture<DataClient>
{
    protected IMemoryCache MemoryCache { get; private set; } = default!;

    private PasswordLimitedOAuthAuthenticatingHttpClient? _passwordLimitedApiClient;
    private CachingApiClient? _cachingApiClientBase;

    [SetUp]
    public void SetUp()
    {
        var options = BaseSetUp();

        MemoryCache = new MemoryCache(new MemoryCacheOptions() { TrackStatistics = true });

        _passwordLimitedApiClient = new(HttpClient, options, TimeProvider.System);
        _cachingApiClientBase = new(_passwordLimitedApiClient,
                                    options,
                                    MemoryCache,
                                    new TestLogger<CachingApiClient>(),
                                    new TestLogger<ApiClient>(),
                                    TimeProvider.System);

        Client = new DataClient(_cachingApiClientBase, options, new TestLogger<DataClient>(), TimeProvider.System);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _passwordLimitedApiClient?.Dispose();
            _cachingApiClientBase?.Dispose();
        }

        base.Dispose(disposing);
    }
}
