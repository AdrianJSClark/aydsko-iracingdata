#### [Aydsko.iRacingData](index.md 'index')
### [Aydsko.iRacingData.Converters](index.md#Aydsko.iRacingData.Converters 'Aydsko.iRacingData.Converters').[CsvStringConverter](CsvStringConverter.md 'Aydsko.iRacingData.Converters.CsvStringConverter')

## CsvStringConverter.Read(Utf8JsonReader, Type, JsonSerializerOptions) Method

Read a comma-separated string and convert the values into an array.

```csharp
public override string[]? Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options);
```
#### Parameters

<a name='Aydsko.iRacingData.Converters.CsvStringConverter.Read(Utf8JsonReader,System.Type,JsonSerializerOptions).reader'></a>

`reader` [System.Text.Json.Utf8JsonReader](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Utf8JsonReader 'System.Text.Json.Utf8JsonReader')

The reader.

<a name='Aydsko.iRacingData.Converters.CsvStringConverter.Read(Utf8JsonReader,System.Type,JsonSerializerOptions).typeToConvert'></a>

`typeToConvert` [System.Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type 'System.Type')

The type to convert.

<a name='Aydsko.iRacingData.Converters.CsvStringConverter.Read(Utf8JsonReader,System.Type,JsonSerializerOptions).options'></a>

`options` [System.Text.Json.JsonSerializerOptions](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonSerializerOptions 'System.Text.Json.JsonSerializerOptions')

Serializer options.

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
An array of the values or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null').