// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.IntegrationTests.Member;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aydsko.iRacingData.IntegrationTests;

[Category("Integration")]
internal abstract class ServiceProviderIntegrationFixture
    : IDisposable
{
    private bool disposedValue;

    protected ServiceProvider ServiceProvider { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var config = new ConfigurationBuilder()
                        .SetBasePath(TestContext.CurrentContext.TestDirectory)
                        .AddJsonFile("appsettings.json", false)
                        .AddUserSecrets(typeof(MemberInfoTest).Assembly)
                        .AddEnvironmentVariables("IRACINGDATA_")
                        .Build();

        var services = new ServiceCollection();

        services.AddSingleton<IConfiguration>(config);

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddProvider(new NUnitLoggerProvider());
            loggingBuilder.SetMinimumLevel(LogLevel.Trace);
        });

        services.AddIRacingDataApi(options =>
        {
            options.UserAgentProductName = "Aydsko.iRacingData.IntegrationTests";
            options.UserAgentProductVersion = typeof(ServiceProviderIntegrationFixture).Assembly.GetName().Version;
            options.Username = config["iRacingData:Username"];
            options.Password = config["iRacingData:Password"];
            options.ClientId = config["iRacingData:ClientId"];
            options.ClientSecret = config["iRacingData:ClientSecret"];
        });

        ServiceProvider = services.BuildServiceProvider();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~ServiceProviderIntegrationFixture()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
