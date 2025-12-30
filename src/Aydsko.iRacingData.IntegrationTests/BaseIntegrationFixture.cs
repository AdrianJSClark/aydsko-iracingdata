// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.Configuration;

namespace Aydsko.iRacingData.IntegrationTests;

[Category("Integration"), NonParallelizable]
internal abstract class BaseIntegrationFixture<TClient> : IDisposable
    where TClient : IDataClient
{
    protected IConfigurationRoot Configuration { get; set; } = default!;
    protected HttpClientHandler Handler { get; set; } = default!;
    protected HttpClient HttpClient { get; set; } = default!;

    internal TClient Client { get; set; } = default!;

    protected virtual iRacingDataClientOptions BaseSetUp()
    {
        Configuration = new ConfigurationBuilder()
                                .SetBasePath(TestContext.CurrentContext.TestDirectory)
                                .AddJsonFile("appsettings.json", false)
                                .AddUserSecrets(typeof(MemberInfoTest).Assembly)
                                .AddEnvironmentVariables("IRACINGDATA_")
                                .Build();

        Handler = new HttpClientHandler()
        {
            UseCookies = true,
            CheckCertificateRevocationList = true
        };
        HttpClient = new HttpClient(Handler);

        var dataClientOptions = new iRacingDataClientOptions();
        dataClientOptions.UseProductUserAgent("Aydsko.iRacingData.IntegrationTests", typeof(MemberInfoTest).Assembly.GetName().Version!);
        dataClientOptions.UsePasswordLimitedOAuth(Configuration["iRacingData:Username"],
                                                  Configuration["iRacingData:Password"],
                                                  Configuration["iRacingData:ClientId"],
                                                  Configuration["iRacingData:ClientSecret"]);
        return dataClientOptions;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            (HttpClient as IDisposable)?.Dispose();
            HttpClient = null!;

            (Handler as IDisposable)?.Dispose();
            Handler = null!;
        }
    }
}
