using Aydsko.iRacingData;
using Aydsko.iRacingData.TestCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/**********************************************************************************************************************
 * WARNING: The "Aydsko.iRacingData.TestCli" code is untested and not intended for any use.
 * 
 * It very likely will not work and, in a worst case, may cause damage or problems to your system.
 * 
 * Use at your own risk.
 */

Console.WriteLine("WARNING: The \"Aydsko.iRacingData.TestCli\" code is untested and not intended for any use.");
Console.WriteLine("This app will now exit.");
return 0;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(true)
    .Build();

var services = new ServiceCollection();
services.AddSingleton<IConfiguration>(configuration);
services.AddHttpClient<OAuthService>(client =>
{
    client.BaseAddress = new Uri("https://oauth.iracing.com");
});
services.AddSingleton(TimeProvider.System);

services.AddIRacingDataApi(options =>
{
    var assemblyName = typeof(Program).Assembly.GetName();
    options.UseProductUserAgent(assemblyName!.Name ?? typeof(Program).FullName!, assemblyName!.Version ?? new());
    options.UseOAuthTokenSource(sp => sp.GetRequiredService<OAuthService>());
});

using var serviceProvider = services.BuildServiceProvider();

try
{
    var dataApi = serviceProvider.GetRequiredService<IDataClient>();
    var myProfile = (await dataApi.GetMemberProfileAsync()).Data;

    Console.WriteLine("================================================================================");
    Console.WriteLine($"  Customer ID: {myProfile.CustomerId}");
    Console.WriteLine($" Display Name: {myProfile.Info.DisplayName}");
    Console.WriteLine($"Index 0 Award: {myProfile.RecentAwards[0].Name} ({myProfile.RecentAwards[0].GroupName})");
    Console.WriteLine("================================================================================");

    return 0;
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex.ToString());
    return -1;
}
