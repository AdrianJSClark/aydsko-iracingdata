<img src="images/Aydsko iRacing Data API ReadMe Logo.png" alt="Aydsko iRacing Data API Logo, which contains the characters &quot;{&quot;, &quot;0&quot;, &quot;1&quot;, and &quot;}&quot; on a red & blue field with &quot;Data API&quot; written below." width="100" />

# Aydsko iRacing Data API

[iRacing](https://www.iracing.com) is the leading online racing simulation for PC. During events hosted via the iRacing service there is a large amount of data created related to race results and member participation in events.

This library allows access via .NET to the iRacing "Data API". These APIs allow a properly authenticated user a supported method of accessing data from the service.

# Getting Started

## Install the Package

[![Nuget](https://img.shields.io/nuget/dt/Aydsko.iRacingData?color=004880&label=NuGet&logo=NuGet)](https://www.nuget.org/packages/Aydsko.iRacingData)

The library is distributed as the `Aydsko.iRacingData` NuGet package. Install using your package manager of choice.

To install using the `dotnet` command:

```pwsh
dotnet add package Aydsko.iRacingData
```

## Register the Client

Register the iRacing Data API client classes with the service provider.

```csharp
services.AddIRacingDataApi(options =>
{
    options.UserAgentProductName = "MyApplicationName";
    options.UserAgentProductVersion = new Version(1, 0);
});
```


## Use the Client

Use the injected `IDataClient` to authenticate and request data.

```csharp
public class ExampleService
{
    private readonly IDataClient dataClient;

    public ExampleService(IDataClient dataClient)
    {
        this.dataClient = dataClient;
    }

    public async Task<MemberInfo> GetMyInfoAsync(string iRacingUsername,
                                                 string iRacingPassword,
                                                 CancellationToken cancellationToken = default)
    {
        dataClient.UseUsernameAndPassword(iRacingUsername, iRacingPassword);

        var infoResponse = await dataClient.GetMyInfoAsync(cancellationToken);
        return infoResponse.Data;
    }
}
```

# Contributing

Before you begin a contribution, please read and ensure you are comfortable with this project's [Code of Conduct](https://github.com/AdrianJSClark/aydsko-iracingdata/blob/main/CODE_OF_CONDUCT.md).

[![Contributor Covenant](https://img.shields.io/badge/Contributor%20Covenant-2.1-4baaaa.svg)](https://github.com/AdrianJSClark/aydsko-iracingdata/blob/main/CODE_OF_CONDUCT.md)

To build & develop on the codebase you'll need:

 - An active [iRacing membership](https://www.iracing.com/membership/)
 - [.NET 6 (or later)](https://dot.net)

To get started, clone [`AdrianJSClark/aydsko-iracingdata`](https://github.com/AdrianJSClark/aydsko-iracingdata) then open and build `src\iRacing Data API.sln`.
