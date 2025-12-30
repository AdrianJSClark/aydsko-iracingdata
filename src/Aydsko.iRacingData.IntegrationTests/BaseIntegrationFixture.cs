// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.Configuration;

[assembly: Category("Integration")]
[assembly: NonParallelizable]

namespace Aydsko.iRacingData.IntegrationTests;

#pragma warning disable CA2201 // Do not raise reserved exception types

[SetUpFixture]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Maintainability", "CA1515:Consider making public types internal", Justification = "NUnit needs to find this.")]
public class BaseIntegrationFixture
{
    internal static IConfigurationRoot Configuration { get; set; } = default!;
    internal static HttpClientHandler Handler { get; set; } = default!;
    internal static HttpClient HttpClient { get; set; } = default!;
    internal static TestOAuthTokenSource TokenSource { get; set; } = default!;
    internal static iRacingDataClientOptions DataClientOptions { get; set; } = default!;

    [OneTimeSetUp]
    public void SetUp()
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

        var userName = Configuration["iRacingData:Username"] ?? throw new Exception("Configuration missing \"Username\" value.");
        var password = Configuration["iRacingData:Password"] ?? throw new Exception("Configuration missing \"Password\" value.");
        var clientId = Configuration["iRacingData:ClientId"] ?? throw new Exception("Configuration missing \"ClientId\" value.");
        var clientSecret = Configuration["iRacingData:ClientSecret"] ?? throw new Exception("Configuration missing \"ClientSecret\" value.");

        var dataClientOptions = new iRacingDataClientOptions
        {
            Username = userName,
            Password = password,
            ClientId = clientId,
            ClientSecret = clientSecret
        };

        dataClientOptions.UseProductUserAgent("Aydsko.iRacingData.IntegrationTests", typeof(MemberInfoTest).Assembly.GetName().Version!);

        TokenSource = new TestOAuthTokenSource(HttpClient, dataClientOptions, TimeProvider.System);

        dataClientOptions.UseOAuthTokenSource(_ => TokenSource);

        DataClientOptions = dataClientOptions;
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        (TokenSource as IDisposable).Dispose();
        TokenSource = null!;

        (HttpClient as IDisposable)?.Dispose();
        HttpClient = null!;

        (Handler as IDisposable)?.Dispose();
        Handler = null!;
    }
}
#pragma warning restore CA2201 // Do not raise reserved exception types
