using Microsoft.Extensions.DependencyInjection;
using Aydsko.iRacingData;

Console.WriteLine("Aydsko iRacing Data API Example Console Application");

/*
 * Collect the username and password to use later to access the iRacing Data API.
 */
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

/*
 * Create a service collection and add the iRacing Data API services to it.
 */
var services = new ServiceCollection();
services.AddIRacingDataApi(options =>
{
    options.UserAgentProductName = "Aydsko.iRacing Example";
    options.UserAgentProductVersion = typeof(Program).Assembly.GetName().Version;
});

using var provider = services.BuildServiceProvider();
using var appScope = provider.CreateScope();

/*
 * Retrieve an instance of the "IDataClient" from the service provider.
 */
var iRacingClient = provider.GetRequiredService<IDataClient>();

/*
 * Let the client know our username and password.
 */
iRacingClient.UseUsernameAndPassword(username, password);

/*
 * Retrieve information about our account.
 *
 * Of course, at this point you can use any of the API methods to retrieve data.
 */
var myInfoResponse = await iRacingClient.GetMyInfoAsync();

Console.WriteLine();
Console.WriteLine("Request successful!");

/*
 * Print out some information from our account.
 * 
 * Note that we get back a standard response object which contains a "Data"
 * property which has the data returned for the given request.
 */
Console.WriteLine($@"Driver name: {myInfoResponse.Data.DisplayName}
Customer ID: {myInfoResponse.Data.CustomerId}
Club: {myInfoResponse.Data.ClubName}");
