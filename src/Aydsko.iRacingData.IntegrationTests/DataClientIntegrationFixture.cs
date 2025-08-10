// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests;

internal abstract class DataClientIntegrationFixture : BaseIntegrationFixture<DataClient>
{
    private LegacyUsernamePasswordApiClient? _legacyApiClient;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var options = BaseSetUp();
        _legacyApiClient = new(HttpClient, options, CookieContainer, new TestLogger<LegacyUsernamePasswordApiClient>());
        Client = new DataClient(_legacyApiClient, options);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _legacyApiClient?.Dispose();
        }
        base.Dispose(disposing);
    }
}
