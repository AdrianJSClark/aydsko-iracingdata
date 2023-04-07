# Aydsko iRacing Data API

[iRacing](https://www.iracing.com) is the leading online racing simulation for PC. During events hosted via the iRacing service there is a large amount of data created related to race results and member participation in events.

This library allows access via .NET to the iRacing "Data API". These APIs allow a properly authenticated user a supported method of accessing data from the service.

## Getting Started

**1. Install the Aydsko iRacing Data API library.**

The library is distributed as the `Aydsko.iRacingData` NuGet package. Install using your package manager of choice.

```pwsh
dotnet add package Aydsko.iRacingData
```

**2. Register the client.**

Register the iRacing Data API client classes with the service provider.

```csharp
services.AddIRacingDataApi(options =>
{
    options.UserAgentProductName = "MyApplicationName";
    options.UserAgentProductVersion = new Version(1, 0);
});
```

**3. Use the client.**

Use the `IDataClient` from the service provider to authenticate and request data.

```csharp
// Retrieve an instance from the service provider.
var dataClient = serviceProvider.GetRequiredService<IDataClient>();

// Supply your iRacing username and password.
dataClient.UseUsernameAndPassword(iRacingUsername, iRacingPassword);

// Retrieve information about our own account.
var infoResponse = await dataClient.GetMyInfoAsync(cancellationToken);

// Write that information to the console.
Console.WriteLine($"Driver name: {infoResponse.Data.DisplayName}");
Console.WriteLine($"Customer ID: {infoResponse.Data.CustomerId}");
Console.WriteLine($"Club: {infoResponse.Data.ClubName}");
```

## Enable Caching of Results

**1. Add the Microsoft memory caching package.**

```powershell
dotnet add package Microsoft.Extensions.Caching.Memory
```

**2. Register the memory caching library with the service provider.**

```csharp
services.AddMemoryCache();
```

**3. Register the caching client.**

Register the iRacing Data API caching client classes with the service provider.

```csharp
services.AddIRacingDataApiWithCaching(options =>
{
    options.UserAgentProductName = "MyApplicationName";
    options.UserAgentProductVersion = new Version(1, 0);
});
```

Then simply use the `IDataClient` as before.
