// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests;

internal abstract class DataClientIntegrationFixture
{
    protected IDataClient Client { get; private set; }
    private ApiClient? _apiClientBase;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _apiClientBase = new(BaseIntegrationFixture.TokenSource, BaseIntegrationFixture.DataClientOptions, new TestLogger<ApiClient>());
        Client = new DataClient(_apiClientBase, BaseIntegrationFixture.DataClientOptions, new TestLogger<DataClient>(), TimeProvider.System);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        (_apiClientBase as IDisposable)?.Dispose();
        _apiClientBase = null;
    }
}
