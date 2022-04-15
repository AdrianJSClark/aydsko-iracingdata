#### [Aydsko.iRacingData](index.md 'index')
### [Aydsko.iRacingData.Converters](index.md#Aydsko.iRacingData.Converters 'Aydsko.iRacingData.Converters')

## TenThousandthSecondDurationConverter Class

The raw iRacing API results use a number type which carries duration values to the ten-thousandth of a second.  
So, for example, a lap which was displayed in the iRacing results page as "1:23.456" would be returned as "834560".

```csharp
public class TenThousandthSecondDurationConverter
```

Inheritance [System.Text.Json.Serialization.JsonConverter](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonConverter 'System.Text.Json.Serialization.JsonConverter') &#129106; TenThousandthSecondDurationConverter