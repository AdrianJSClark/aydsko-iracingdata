// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Net;
using Aydsko.iRacingData.IntegrationTests.Member;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Aydsko.iRacingData.IntegrationTests;

[Category("Integration")]
public abstract class BaseIntegrationFixture<TClient> : IDisposable
    where TClient : IDataClient
{
    protected IConfigurationRoot _configuration;
    protected CookieContainer _cookieContainer;
    protected HttpClientHandler _handler;
    protected HttpClient _httpClient;
    protected MemoryCache _memoryCache;

    internal TClient Client { get; set; }

    protected IConfigurationRoot Configuration => _configuration;

    protected iRacingDataClientOptions BaseSetUp()
    {
        _configuration = new ConfigurationBuilder()
                                .SetBasePath(TestContext.CurrentContext.TestDirectory)
                                .AddJsonFile("appsettings.json", false)
                                .AddUserSecrets(typeof(MemberInfoTest).Assembly)
                                .AddEnvironmentVariables("IRACINGDATA_")
                                .Build();

        _cookieContainer = new CookieContainer();
        _handler = new HttpClientHandler()
        {
            CookieContainer = _cookieContainer,
            UseCookies = true,
            CheckCertificateRevocationList = true
        };
        _httpClient = new HttpClient(_handler);

        return new iRacingDataClientOptions
        {
            UserAgentProductName = "Aydsko.iRacingData.IntegrationTests",
            UserAgentProductVersion = typeof(MemberInfoTest).Assembly.GetName().Version,
            Username = _configuration["iRacingData:Username"],
            Password = _configuration["iRacingData:Password"]
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
            (_httpClient as IDisposable)?.Dispose();
            _httpClient = null!;

            (_handler as IDisposable)?.Dispose();
            _handler = null!;

            (_memoryCache as IDisposable)?.Dispose();
            _memoryCache = null!;
        }
    }
}
