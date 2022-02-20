# Aydsko iRacing Data API

[iRacing](https://www.iracing.com) is the leading online racing simulation for PC. During events hosted via the iRacing service there is a large amount of data created related to race results and member participation in events.

This library allows access via .NET to the iRacing "Data API". These APIs allow a properly authenticated user a supported method of accessing data from the service.

## Getting Started

### Register Services

Register the iRacing Data API client classes with the service provider.

```csharp
services.AddiRacingDataApi();
```

### Use the Client

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
