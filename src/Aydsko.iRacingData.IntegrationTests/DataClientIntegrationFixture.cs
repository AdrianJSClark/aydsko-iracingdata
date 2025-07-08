// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests;

internal abstract class DataClientIntegrationFixture : BaseIntegrationFixture<DataClient>
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var options = BaseSetUp();
        Client = new DataClient(HttpClient, new TestLogger<DataClient>(), options, CookieContainer);
    }
}
