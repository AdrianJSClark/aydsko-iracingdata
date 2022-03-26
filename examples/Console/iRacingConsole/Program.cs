using Microsoft.Extensions.DependencyInjection;
using Aydsko.iRacingData;

Console.WriteLine("Aydsko iRacing Data API Example Console Application");

Console.WriteLine();
Console.Write("iRacing Username: ");
var username = Console.ReadLine();

Console.WriteLine();
Console.Write("iRacing Password: ");
var password = Console.ReadLine();

if (username is null || password is null)
{
    Console.WriteLine("Username or password was not supplied. Exiting...");
    return;
}

var services = new ServiceCollection();
services.AddIRacingDataApi(options =>
{
    options.Username = username;
    options.Password = password;
});

using var provider = services.BuildServiceProvider();
using var appScope = provider.CreateScope();

var iRacingClient = provider.GetRequiredService<IDataClient>();
var myInfo = await iRacingClient.GetMyInfoAsync();

Console.WriteLine();
Console.WriteLine("Request successful!");
Console.WriteLine($@"Driver name: {myInfo.Data.DisplayName}
Customer ID: {myInfo.Data.CustomerId}
Club: {myInfo.Data.ClubName}");
