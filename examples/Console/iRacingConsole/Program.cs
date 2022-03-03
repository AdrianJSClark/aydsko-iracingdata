using Microsoft.Extensions.DependencyInjection;
using Aydsko.iRacingData;

using var provider = new ServiceCollection().AddIRacingDataApi()
                                            .BuildServiceProvider();

using var appScope = provider.CreateScope();

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

var iRacingClient = provider.GetRequiredService<iRacingDataClient>();
await iRacingClient.LoginAsync(username, password);

Console.WriteLine();
Console.WriteLine("Login successful!");

var myInfo = await iRacingClient.GetMyInfoAsync();

Console.WriteLine();
Console.WriteLine($@"Driver name: {myInfo.Data.DisplayName}
Customer ID: {myInfo.Data.CustomerId}
Club: {myInfo.Data.ClubName}");
