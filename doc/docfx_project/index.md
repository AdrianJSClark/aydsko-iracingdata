<img src="images/Aydsko iRacing Data API ReadMe Logo.png" alt="Aydsko iRacing Data API Logo, which contains the characters &quot;{&quot;, &quot;0&quot;, &quot;1&quot;, and &quot;}&quot; on a red & blue field with &quot;Data API&quot; written below." width="100" />

# Aydsko iRacing Data API

[iRacing](https://www.iracing.com) is the leading online racing simulation for PC. During events hosted via the iRacing service there is a large amount of data created related to race results and member participation in events.

This library allows access via .NET to the iRacing "Data API". These APIs allow a properly authenticated user a supported method of accessing data from the service.

[![Nuget](https://img.shields.io/nuget/dt/Aydsko.iRacingData?color=004880&label=NuGet&logo=NuGet)](https://www.nuget.org/packages/Aydsko.iRacingData)

## ⚠ Multi-Factor Authentication Requirement

iRacing has announced that they now require multi-factor authentication (MFA) for all users by default. This will affect the ability to use the iRacing Data API by logging in with a username or password.

In the immediate future, use of the Data API will be supported by enabling "Legacy Authentication" in your iRacing account settings. iRacing will advise next steps for the authentication of applications for the Data API later on.

To continue to use this library you **must** [enable "Legacy Authentication" in your iRacing account settings](https://support.iracing.com/support/solutions/articles/31000173894-enabling-or-disabling-legacy-read-only-authentication) before attempting to authenticate. Please **do not** enable this setting unless you require it, as it may reduce the security on your iRacing account.

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

# Contributing

Before you begin a contribution, please read and ensure you are comfortable with this project's [Code of Conduct](https://github.com/AdrianJSClark/aydsko-iracingdata/blob/main/CODE_OF_CONDUCT.md).

[![Contributor Covenant](https://img.shields.io/badge/Contributor%20Covenant-2.1-4baaaa.svg)](https://github.com/AdrianJSClark/aydsko-iracingdata/blob/main/CODE_OF_CONDUCT.md)

To build & develop on the codebase you'll need:

 - An active [iRacing membership](https://www.iracing.com/membership/)
 - [.NET 8 (or later)](https://dot.net)

To get started, clone [`AdrianJSClark/aydsko-iracingdata`](https://github.com/AdrianJSClark/aydsko-iracingdata) then open and build `src\iRacing Data API.sln`.
