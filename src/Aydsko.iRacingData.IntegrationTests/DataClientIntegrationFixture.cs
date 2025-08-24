// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests;

internal abstract class DataClientIntegrationFixture : BaseIntegrationFixture<DataClient>
{
    private LegacyUsernamePasswordApiClient? _legacyApiClient;
    private ApiClient? _apiClientBase;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var options = BaseSetUp();

        _legacyApiClient = new(HttpClient, options, CookieContainer, new TestLogger<LegacyUsernamePasswordApiClient>());
        _apiClientBase = new(_legacyApiClient, options, new TestLogger<ApiClient>());
        Client = new DataClient(_apiClientBase, options, new TestLogger<DataClient>());
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
