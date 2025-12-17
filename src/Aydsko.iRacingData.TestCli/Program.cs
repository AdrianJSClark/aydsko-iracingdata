using Aydsko.iRacingData;
using Aydsko.iRacingData.TestCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(true)
    .Build();

var services = new ServiceCollection();
services.AddSingleton<IConfiguration>(configuration);
services.AddHttpClient<OAuthService>(client =>
{
    client.BaseAddress = new Uri("https://oauth.iracing.com/oauth2");
});

//services.AddIRacingDataApi()

using var serviceProvider = services.BuildServiceProvider();

var oaService = serviceProvider.GetService<OAuthService>();
var result = await oaService.GetTokenAsync();
