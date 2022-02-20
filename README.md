<img src="Aydsko iRacing Data API ReadMe Logo.png" alt="Aydsko iRacing Data API Logo, which contains the characters &quot;{&quot;, &quot;0&quot;, &quot;1&quot;, and &quot;}&quot; on a red & blue field with &quot;Data API&quot; written below." width="100" />

# Aydsko iRacing Data API

[iRacing](https://www.iracing.com) is the leading online racing simulation for PC. During events hosted via the iRacing service there is a large amount of data created related to race results and member participation in events.

This library allows access via .NET to the iRacing "Data API". These APIs allow a properly authenticated user a supported method of accessing data from the service.

# Getting Started

## Install the Package

The library is distributed as a NuGet package. Install using your package manager of choice.

To install using the `dotnet` command:

```pwsh
dotnet add package Aydsko.iRacingData
```

## Register the Client

Register the iRacing Data API client classes with the service provider.

```csharp
services.AddiRacingDataApi();
```

## Use the Client

Use the injected `iRacingDataClient` to authenticate and request data.

```csharp
public class ExampleService
{
    private readonly iRacingDataClient dataClient;

    public ExampleService(iRacingDataClient dataClient)
    {
        this.dataClient = dataClient;
    }

    public Task<MemberInfo> GetMyInfoAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        await dataClient.LoginAsync(username, password, cancellationToken);
        var infoResponse = await dataClient.GetMyInfoAsync(cancellationToken);

        return infoResponse.Data;
    }
}
```

# Contributing

Before you begin a contribution, please read and ensure you are comfortable with this project's [Code of Conduct](CODE_OF_CONDUCT.md).

[![Contributor Covenant](https://img.shields.io/badge/Contributor%20Covenant-2.1-4baaaa.svg)](CODE_OF_CONDUCT.md)

To build & develop on the codebase you'll need:

 - An active iRacing membership
 - .NET 6 (or later)

To get started open and build `src\iRacing Data API.sln`.
