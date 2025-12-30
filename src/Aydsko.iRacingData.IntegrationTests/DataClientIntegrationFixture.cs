// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests;

internal abstract class DataClientIntegrationFixture
    : BaseIntegrationFixture<DataClient>
{
    private PasswordLimitedOAuthAuthenticatingHttpClient? _passwordLimitedApiClient;
    private ApiClient? _apiClientBase;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var options = BaseSetUp();

        _passwordLimitedApiClient = new(HttpClient, options, TimeProvider.System);
        _apiClientBase = new(_passwordLimitedApiClient, options, new TestLogger<ApiClient>());
        Client = new DataClient(_apiClientBase, options, new TestLogger<DataClient>(), TimeProvider.System);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _passwordLimitedApiClient?.Dispose();
            _apiClientBase?.Dispose();
        }
        base.Dispose(disposing);
    }
}
