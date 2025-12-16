using Aydsko.iRacingData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(true)
    .Build();

var services = new ServiceCollection();
services.AddSingleton<IConfiguration>(configuration);

//services.AddIRacingDataApi()

using var serviceProvider = services.BuildServiceProvider();


