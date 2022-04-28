# Aydsko iRacing Data API

[iRacing](https://www.iracing.com) is the leading online racing simulation for PC. During events hosted via the iRacing service there is a large amount of data created related to race results and member participation in events.

This library allows access via .NET to the iRacing "Data API". These APIs allow a properly authenticated user a supported method of accessing data from the service.

## Getting Started

### Register Services

Register the iRacing Data API client classes with the service provider.

```csharp
services.AddiRacingDataApi(options =>
{
    options.Username = "your-iracing-user@example.com";
    options.Password = "Your-iRacing-Password";
    options.UserAgentProductName = "MyApplicationName";
    options.UserAgentProductVersion = new Version(1, 0);
});
```

#### 2022 Season 3 Change in Login

There is a change in the login algorithm due to be released with iRacing's 2022 Season 3 update. If this update has been released and authentication stops working enable the `Use2022Season3Login` flag on the options.

```csharp
services.AddiRacingDataApi(options =>
{
    options.Use2022Season3Login = true; // Add this line

    options.Username = "your-iracing-user@example.com";
    options.Password = "Your-iRacing-Password";
    options.UserAgentProductName = "MyApplicationName";
    options.UserAgentProductVersion = new Version(1, 0);
});
```

### Use the Client

Use the injected `iRacingDataClient` to authenticate and request data.

```csharp
public class ExampleService
{
    private readonly IDataClient dataClient;

    public ExampleService(IDataClient dataClient)
    {
        this.dataClient = dataClient;
    }

    public async Task<MemberInfo> GetMyInfoAsync(CancellationToken cancellationToken = default)
    {
        var infoResponse = await dataClient.GetMyInfoAsync(cancellationToken);
        return infoResponse.Data;
    }
}
```
