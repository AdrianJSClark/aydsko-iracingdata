using Aydsko.iRacingData.IntegrationTests.Member;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Aydsko.iRacingData.IntegrationTests;

[Category("Integration")]
public class BaseIntegrationFixture : IDisposable
{
    private IConfigurationRoot _configuration;
    private CookieContainer _cookieContainer;
    private HttpClientHandler _handler;
    private HttpClient _httpClient;

    internal DataClient Client { get; private set; }
    protected IConfigurationRoot Configuration => _configuration;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _configuration = new ConfigurationBuilder()
                                .SetBasePath(TestContext.CurrentContext.TestDirectory)
                                .AddJsonFile("appsettings.json", false)
                                .AddUserSecrets(typeof(MemberInfoTest).Assembly)
                                .Build();

        _cookieContainer = new CookieContainer();
        _handler = new HttpClientHandler() { CookieContainer = _cookieContainer, UseCookies = true };
        _httpClient = new HttpClient(_handler);

        var options = new iRacingDataClientOptions
        {
            UserAgentProductName = "Aydsko.iRacingData.IntegrationTests",
            UserAgentProductVersion = typeof(MemberInfoTest).Assembly.GetName().Version,
            Username = _configuration["iRacingData:Username"],
            Password = _configuration["iRacingData:Password"]
        };

        Client = new DataClient(_httpClient, new TestLogger<DataClient>(), options, _cookieContainer);
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
        }
    }
}
