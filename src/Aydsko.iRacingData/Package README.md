# Aydsko iRacing Data API

[iRacing](https://www.iracing.com) is the leading online racing simulation for PC. During events hosted via the iRacing service there is a large amount of data created related to race results and member participation in events.

This library allows access via .NET to the iRacing "Data API". These APIs allow a properly authenticated user a supported method of accessing data from the service.

## Authentication Types

### Legacy Authentication (Username / Password)

**This authentication is no longer supported.** Legacy authentication was removed 9 Dec 2025. See: https://forums.iracing.com/discussion/84226/legacy-authentication-removal-dec-9-2025

One of the OAuth-based authentication methods is now required.

### iRacing OAuth Authentication

To use this authentication method you need to contact iRacing who will allocate a "Client ID" and "Client Secret".

- ["Authorization Code Flow"](https://oauth.iracing.com/oauth2/book/authorization_code_flow.html) is intended for interactive applications, either client apps or web-based apps. It allows users to authenticate with iRacing.
- ["Password Limited Flow"](https://oauth.iracing.com/oauth2/book/password_limited_flow.html) is authentication intended for scripts or back-end processes that do not need to access the details of specific users.

Full information is available from the [iRacing.com Auth Service Documentation](https://oauth.iracing.com/oauth2/book/introduction.html).

## Getting Started

**1. Install the Aydsko iRacing Data API library.**

The library is distributed as the `Aydsko.iRacingData` NuGet package. Install using your package manager of choice.

```pwsh
dotnet add package Aydsko.iRacingData
```

**2. Register the client.**

Register the iRacing Data API client classes with the service provider.

Using iRacing OAuth "Authentication Code Grant" authentication:
```csharp

public class MyTokenSource : IOAuthTokenSource
{
    public async Task<OAuthTokenValue> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        // TODO - Implement retrieval of the iRacing authentication token
        //        via the "/authorize" and "/token" endpoints or retrieve
        //        a previous token from secure storage.
    }
}

services.AddIRacingDataApi(options =>
{
    options.UseProductUserAgent("MyApplicationName", new Version(1, 0));

    // Supply the "IOAuthTokenSource" implementation.
    options.UseOAuthTokenSource(sp => sp.GetRequiredService<MyTokenSource>());
});
```

OR

Using iRacing OAuth "Password Limited Grant" authentication:
```csharp
services.AddIRacingDataApi(options =>
{
    options.UseProductUserAgent("MyApplicationName", new Version(1, 0));

    // Supply your iRacing username, password, client id, and client secret.
    options.UsePasswordLimitedOAuth(iRacingUsername, iRacingPassword, iRacingClientId, iRacingClientSecret);
});
```

**3. Use the client.**

Use the `IDataClient` from the service provider to authenticate and request data.

```csharp
// Retrieve an instance from the service provider.
var dataClient = serviceProvider.GetRequiredService<IDataClient>();

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
    // [... normal configuration for your chosen authentication type here]
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
