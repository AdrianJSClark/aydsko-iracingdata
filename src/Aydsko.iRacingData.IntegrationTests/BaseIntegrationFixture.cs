// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Net;
using Aydsko.iRacingData.IntegrationTests.Member;
using Microsoft.Extensions.Configuration;

namespace Aydsko.iRacingData.IntegrationTests;

[Category("Integration")]
internal abstract class BaseIntegrationFixture<TClient> : IDisposable
    where TClient : IDataClient
{
    protected IConfigurationRoot Configuration { get; set; } = default!;
    protected CookieContainer CookieContainer { get; set; } = default!;
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

        CookieContainer = new CookieContainer();
        Handler = new HttpClientHandler()
        {
            CookieContainer = CookieContainer,
            UseCookies = true,
            CheckCertificateRevocationList = true
        };
        HttpClient = new HttpClient(Handler);

        return new iRacingDataClientOptions
        {
            UserAgentProductName = "Aydsko.iRacingData.IntegrationTests",
            UserAgentProductVersion = typeof(MemberInfoTest).Assembly.GetName().Version,
            Username = Configuration["iRacingData:Username"],
            Password = Configuration["iRacingData:Password"]
        };
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
