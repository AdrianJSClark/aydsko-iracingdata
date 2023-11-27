<img src="Aydsko iRacing Data API ReadMe Logo.png" alt="Aydsko iRacing Data API Logo, which contains the characters &quot;{&quot;, &quot;0&quot;, &quot;1&quot;, and &quot;}&quot; on a red & blue field with &quot;Data API&quot; written below." width="100" />

# Aydsko iRacing Data API

[iRacing](https://www.iracing.com) is the leading online racing simulation for PC. During events hosted via the iRacing service there is a large amount of data created related to race results and member participation in events.

This library allows access via .NET to the iRacing "Data API". These APIs allow a properly authenticated user a supported method of accessing data from the service.

[![Nuget](https://img.shields.io/nuget/dt/Aydsko.iRacingData?color=004880&label=NuGet&logo=NuGet)](https://www.nuget.org/packages/Aydsko.iRacingData)

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

# Versioning

Ideally you should always use the latest version of the library that is available. This is because iRacing will sometimes introduce API changes during a release which make the old code incompatible.

This library will use version numbers will be in the format:

    [YY][SS].[R]

Where:
 - YY = two digit year
 - SS = the iRacing season the library release was made in as a zero-padded number (i.e. 01, 02, 03, 04)
 - R  = release number, which increments when changes are made although it may not be sequential

Example:

 - 2303.1 = changes compatible with iRacing 2023 Season 3 or later, and is release 1 during this season

# Contributing

Before you begin a contribution, please read and ensure you are comfortable with this project's [Code of Conduct](CODE_OF_CONDUCT.md).

[![Contributor Covenant](https://img.shields.io/badge/Contributor%20Covenant-2.1-4baaaa.svg)](CODE_OF_CONDUCT.md)

To build & develop on the codebase you'll need:

 - An active iRacing membership
 - .NET 8 (or later)

To get started open and build `src\iRacing Data API.sln`.
