#### [Aydsko.iRacingData](index.md 'index')
### [Aydsko.iRacingData.Converters](index.md#Aydsko.iRacingData.Converters 'Aydsko.iRacingData.Converters').[CsvStringConverter](CsvStringConverter.md 'Aydsko.iRacingData.Converters.CsvStringConverter')

## CsvStringConverter.Write(Utf8JsonWriter, string[], JsonSerializerOptions) Method

Accept an array of values and write them separated by commas.

```csharp
public override void Write(Utf8JsonWriter writer, string[] value, JsonSerializerOptions options);
```
#### Parameters

<a name='Aydsko.iRacingData.Converters.CsvStringConverter.Write(Utf8JsonWriter,string[],JsonSerializerOptions).writer'></a>

`writer` [System.Text.Json.Utf8JsonWriter](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Utf8JsonWriter 'System.Text.Json.Utf8JsonWriter')

The writer.

<a name='Aydsko.iRacingData.Converters.CsvStringConverter.Write(Utf8JsonWriter,string[],JsonSerializerOptions).value'></a>

`value` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

The values to write.

<a name='Aydsko.iRacingData.Converters.CsvStringConverter.Write(Utf8JsonWriter,string[],JsonSerializerOptions).options'></a>

`options` [System.Text.Json.JsonSerializerOptions](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonSerializerOptions 'System.Text.Json.JsonSerializerOptions')

Serializer options.

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')  
Thrown if the [writer](CsvStringConverter.Write(Utf8JsonWriter,string[],JsonSerializerOptions).md#Aydsko.iRacingData.Converters.CsvStringConverter.Write(Utf8JsonWriter,string[],JsonSerializerOptions).writer 'Aydsko.iRacingData.Converters.CsvStringConverter.Write(Utf8JsonWriter, string[], JsonSerializerOptions).writer') is [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null').