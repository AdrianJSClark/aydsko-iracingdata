#### [Aydsko.iRacingData](Home 'Home')

## Aydsko.iRacingData.Converters Namespace
### Classes

<a name='Aydsko.iRacingData.Converters.CsvStringConverter'></a>

## CsvStringConverter Class

Convert between a comma-separated string and an array of values.

```csharp
public class CsvStringConverter : System.Text.Json.Serialization.JsonConverter<string[]>
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; [System.Text.Json.Serialization.JsonConverter](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonConverter 'System.Text.Json.Serialization.JsonConverter') &#129106; [System.Text.Json.Serialization.JsonConverter&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonConverter-1 'System.Text.Json.Serialization.JsonConverter`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonConverter-1 'System.Text.Json.Serialization.JsonConverter`1') &#129106; CsvStringConverter
### Methods

<a name='Aydsko.iRacingData.Converters.CsvStringConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions)'></a>

## CsvStringConverter.Read(Utf8JsonReader, Type, JsonSerializerOptions) Method

Read a comma-separated string and convert the values into an array.

```csharp
public override string[]? Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options);
```
#### Parameters

<a name='Aydsko.iRacingData.Converters.CsvStringConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions).reader'></a>

`reader` [System.Text.Json.Utf8JsonReader](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Utf8JsonReader 'System.Text.Json.Utf8JsonReader')

The reader.

<a name='Aydsko.iRacingData.Converters.CsvStringConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions).typeToConvert'></a>

`typeToConvert` [System.Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type 'System.Type')

The type to convert.

<a name='Aydsko.iRacingData.Converters.CsvStringConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions).options'></a>

`options` [System.Text.Json.JsonSerializerOptions](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonSerializerOptions 'System.Text.Json.JsonSerializerOptions')

Serializer options.

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')  
An array of the values or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null').

<a name='Aydsko.iRacingData.Converters.CsvStringConverter.Write(System.Text.Json.Utf8JsonWriter,string[],System.Text.Json.JsonSerializerOptions)'></a>

## CsvStringConverter.Write(Utf8JsonWriter, string[], JsonSerializerOptions) Method

Accept an array of values and write them separated by commas.

```csharp
public override void Write(System.Text.Json.Utf8JsonWriter writer, string[] value, System.Text.Json.JsonSerializerOptions options);
```
#### Parameters

<a name='Aydsko.iRacingData.Converters.CsvStringConverter.Write(System.Text.Json.Utf8JsonWriter,string[],System.Text.Json.JsonSerializerOptions).writer'></a>

`writer` [System.Text.Json.Utf8JsonWriter](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Utf8JsonWriter 'System.Text.Json.Utf8JsonWriter')

The writer.

<a name='Aydsko.iRacingData.Converters.CsvStringConverter.Write(System.Text.Json.Utf8JsonWriter,string[],System.Text.Json.JsonSerializerOptions).value'></a>

`value` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

The values to write.

<a name='Aydsko.iRacingData.Converters.CsvStringConverter.Write(System.Text.Json.Utf8JsonWriter,string[],System.Text.Json.JsonSerializerOptions).options'></a>

`options` [System.Text.Json.JsonSerializerOptions](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonSerializerOptions 'System.Text.Json.JsonSerializerOptions')

Serializer options.

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')  
Thrown if the [writer](Aydsko.iRacingData.Converters#Aydsko.iRacingData.Converters.CsvStringConverter.Write(System.Text.Json.Utf8JsonWriter,string[],System.Text.Json.JsonSerializerOptions).writer 'Aydsko.iRacingData.Converters.CsvStringConverter.Write(System.Text.Json.Utf8JsonWriter, string[], System.Text.Json.JsonSerializerOptions).writer') is [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null').

<a name='Aydsko.iRacingData.Converters.DateOnlyConverter'></a>

## DateOnlyConverter Class

```csharp
public class DateOnlyConverter : System.Text.Json.Serialization.JsonConverter<System.DateOnly>
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; [System.Text.Json.Serialization.JsonConverter](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonConverter 'System.Text.Json.Serialization.JsonConverter') &#129106; [System.Text.Json.Serialization.JsonConverter&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonConverter-1 'System.Text.Json.Serialization.JsonConverter`1')[System.DateOnly](https://docs.microsoft.com/en-us/dotnet/api/System.DateOnly 'System.DateOnly')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonConverter-1 'System.Text.Json.Serialization.JsonConverter`1') &#129106; DateOnlyConverter
### Methods

<a name='Aydsko.iRacingData.Converters.DateOnlyConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions)'></a>

## DateOnlyConverter.Read(Utf8JsonReader, Type, JsonSerializerOptions) Method

```csharp
public override System.DateOnly Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options);
```
#### Parameters

<a name='Aydsko.iRacingData.Converters.DateOnlyConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions).reader'></a>

`reader` [System.Text.Json.Utf8JsonReader](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Utf8JsonReader 'System.Text.Json.Utf8JsonReader')

<a name='Aydsko.iRacingData.Converters.DateOnlyConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions).typeToConvert'></a>

`typeToConvert` [System.Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type 'System.Type')

<a name='Aydsko.iRacingData.Converters.DateOnlyConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions).options'></a>

`options` [System.Text.Json.JsonSerializerOptions](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonSerializerOptions 'System.Text.Json.JsonSerializerOptions')

#### Returns
[System.DateOnly](https://docs.microsoft.com/en-us/dotnet/api/System.DateOnly 'System.DateOnly')

<a name='Aydsko.iRacingData.Converters.DateOnlyConverter.Write(System.Text.Json.Utf8JsonWriter,System.DateOnly,System.Text.Json.JsonSerializerOptions)'></a>

## DateOnlyConverter.Write(Utf8JsonWriter, DateOnly, JsonSerializerOptions) Method

```csharp
public override void Write(System.Text.Json.Utf8JsonWriter writer, System.DateOnly value, System.Text.Json.JsonSerializerOptions options);
```
#### Parameters

<a name='Aydsko.iRacingData.Converters.DateOnlyConverter.Write(System.Text.Json.Utf8JsonWriter,System.DateOnly,System.Text.Json.JsonSerializerOptions).writer'></a>

`writer` [System.Text.Json.Utf8JsonWriter](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Utf8JsonWriter 'System.Text.Json.Utf8JsonWriter')

<a name='Aydsko.iRacingData.Converters.DateOnlyConverter.Write(System.Text.Json.Utf8JsonWriter,System.DateOnly,System.Text.Json.JsonSerializerOptions).value'></a>

`value` [System.DateOnly](https://docs.microsoft.com/en-us/dotnet/api/System.DateOnly 'System.DateOnly')

<a name='Aydsko.iRacingData.Converters.DateOnlyConverter.Write(System.Text.Json.Utf8JsonWriter,System.DateOnly,System.Text.Json.JsonSerializerOptions).options'></a>

`options` [System.Text.Json.JsonSerializerOptions](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonSerializerOptions 'System.Text.Json.JsonSerializerOptions')

<a name='Aydsko.iRacingData.Converters.DateTimeConverter'></a>

## DateTimeConverter Class

```csharp
public class DateTimeConverter : System.Text.Json.Serialization.JsonConverter<System.DateTime>
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; [System.Text.Json.Serialization.JsonConverter](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonConverter 'System.Text.Json.Serialization.JsonConverter') &#129106; [System.Text.Json.Serialization.JsonConverter&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonConverter-1 'System.Text.Json.Serialization.JsonConverter`1')[System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonConverter-1 'System.Text.Json.Serialization.JsonConverter`1') &#129106; DateTimeConverter
### Methods

<a name='Aydsko.iRacingData.Converters.DateTimeConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions)'></a>

## DateTimeConverter.Read(Utf8JsonReader, Type, JsonSerializerOptions) Method

```csharp
public override System.DateTime Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options);
```
#### Parameters

<a name='Aydsko.iRacingData.Converters.DateTimeConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions).reader'></a>

`reader` [System.Text.Json.Utf8JsonReader](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Utf8JsonReader 'System.Text.Json.Utf8JsonReader')

<a name='Aydsko.iRacingData.Converters.DateTimeConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions).typeToConvert'></a>

`typeToConvert` [System.Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type 'System.Type')

<a name='Aydsko.iRacingData.Converters.DateTimeConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions).options'></a>

`options` [System.Text.Json.JsonSerializerOptions](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonSerializerOptions 'System.Text.Json.JsonSerializerOptions')

#### Returns
[System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')

<a name='Aydsko.iRacingData.Converters.DateTimeConverter.Write(System.Text.Json.Utf8JsonWriter,System.DateTime,System.Text.Json.JsonSerializerOptions)'></a>

## DateTimeConverter.Write(Utf8JsonWriter, DateTime, JsonSerializerOptions) Method

```csharp
public override void Write(System.Text.Json.Utf8JsonWriter writer, System.DateTime value, System.Text.Json.JsonSerializerOptions options);
```
#### Parameters

<a name='Aydsko.iRacingData.Converters.DateTimeConverter.Write(System.Text.Json.Utf8JsonWriter,System.DateTime,System.Text.Json.JsonSerializerOptions).writer'></a>

`writer` [System.Text.Json.Utf8JsonWriter](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Utf8JsonWriter 'System.Text.Json.Utf8JsonWriter')

<a name='Aydsko.iRacingData.Converters.DateTimeConverter.Write(System.Text.Json.Utf8JsonWriter,System.DateTime,System.Text.Json.JsonSerializerOptions).value'></a>

`value` [System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')

<a name='Aydsko.iRacingData.Converters.DateTimeConverter.Write(System.Text.Json.Utf8JsonWriter,System.DateTime,System.Text.Json.JsonSerializerOptions).options'></a>

`options` [System.Text.Json.JsonSerializerOptions](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonSerializerOptions 'System.Text.Json.JsonSerializerOptions')

<a name='Aydsko.iRacingData.Converters.TenThousandthSecondDurationConverter'></a>

## TenThousandthSecondDurationConverter Class

The raw iRacing API results use a number type which carries duration values to the ten-thousandth of a second.  
So, for example, a lap which was displayed in the iRacing results page as "1:23.456" would be returned as "834560".

```csharp
public class TenThousandthSecondDurationConverter : System.Text.Json.Serialization.JsonConverter<System.Nullable<System.TimeSpan>>
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; [System.Text.Json.Serialization.JsonConverter](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonConverter 'System.Text.Json.Serialization.JsonConverter') &#129106; [System.Text.Json.Serialization.JsonConverter&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonConverter-1 'System.Text.Json.Serialization.JsonConverter`1')[System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[System.TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/System.TimeSpan 'System.TimeSpan')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonConverter-1 'System.Text.Json.Serialization.JsonConverter`1') &#129106; TenThousandthSecondDurationConverter
### Methods

<a name='Aydsko.iRacingData.Converters.TenThousandthSecondDurationConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions)'></a>

## TenThousandthSecondDurationConverter.Read(Utf8JsonReader, Type, JsonSerializerOptions) Method

```csharp
public override System.Nullable<System.TimeSpan> Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options);
```
#### Parameters

<a name='Aydsko.iRacingData.Converters.TenThousandthSecondDurationConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions).reader'></a>

`reader` [System.Text.Json.Utf8JsonReader](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Utf8JsonReader 'System.Text.Json.Utf8JsonReader')

<a name='Aydsko.iRacingData.Converters.TenThousandthSecondDurationConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions).typeToConvert'></a>

`typeToConvert` [System.Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type 'System.Type')

<a name='Aydsko.iRacingData.Converters.TenThousandthSecondDurationConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions).options'></a>

`options` [System.Text.Json.JsonSerializerOptions](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonSerializerOptions 'System.Text.Json.JsonSerializerOptions')

#### Returns
[System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[System.TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/System.TimeSpan 'System.TimeSpan')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')

<a name='Aydsko.iRacingData.Converters.TenThousandthSecondDurationConverter.Write(System.Text.Json.Utf8JsonWriter,System.Nullable_System.TimeSpan_,System.Text.Json.JsonSerializerOptions)'></a>

## TenThousandthSecondDurationConverter.Write(Utf8JsonWriter, Nullable<TimeSpan>, JsonSerializerOptions) Method

```csharp
public override void Write(System.Text.Json.Utf8JsonWriter writer, System.Nullable<System.TimeSpan> value, System.Text.Json.JsonSerializerOptions options);
```
#### Parameters

<a name='Aydsko.iRacingData.Converters.TenThousandthSecondDurationConverter.Write(System.Text.Json.Utf8JsonWriter,System.Nullable_System.TimeSpan_,System.Text.Json.JsonSerializerOptions).writer'></a>

`writer` [System.Text.Json.Utf8JsonWriter](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Utf8JsonWriter 'System.Text.Json.Utf8JsonWriter')

<a name='Aydsko.iRacingData.Converters.TenThousandthSecondDurationConverter.Write(System.Text.Json.Utf8JsonWriter,System.Nullable_System.TimeSpan_,System.Text.Json.JsonSerializerOptions).value'></a>

`value` [System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[System.TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/System.TimeSpan 'System.TimeSpan')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')

<a name='Aydsko.iRacingData.Converters.TenThousandthSecondDurationConverter.Write(System.Text.Json.Utf8JsonWriter,System.Nullable_System.TimeSpan_,System.Text.Json.JsonSerializerOptions).options'></a>

`options` [System.Text.Json.JsonSerializerOptions](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonSerializerOptions 'System.Text.Json.JsonSerializerOptions')

<a name='Aydsko.iRacingData.Converters.UriConverter'></a>

## UriConverter Class

```csharp
public class UriConverter : System.Text.Json.Serialization.JsonConverter<System.Uri>
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; [System.Text.Json.Serialization.JsonConverter](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonConverter 'System.Text.Json.Serialization.JsonConverter') &#129106; [System.Text.Json.Serialization.JsonConverter&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonConverter-1 'System.Text.Json.Serialization.JsonConverter`1')[System.Uri](https://docs.microsoft.com/en-us/dotnet/api/System.Uri 'System.Uri')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Serialization.JsonConverter-1 'System.Text.Json.Serialization.JsonConverter`1') &#129106; UriConverter
### Methods

<a name='Aydsko.iRacingData.Converters.UriConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions)'></a>

## UriConverter.Read(Utf8JsonReader, Type, JsonSerializerOptions) Method

```csharp
public override System.Uri? Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options);
```
#### Parameters

<a name='Aydsko.iRacingData.Converters.UriConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions).reader'></a>

`reader` [System.Text.Json.Utf8JsonReader](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Utf8JsonReader 'System.Text.Json.Utf8JsonReader')

<a name='Aydsko.iRacingData.Converters.UriConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions).typeToConvert'></a>

`typeToConvert` [System.Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type 'System.Type')

<a name='Aydsko.iRacingData.Converters.UriConverter.Read(System.Text.Json.Utf8JsonReader,System.Type,System.Text.Json.JsonSerializerOptions).options'></a>

`options` [System.Text.Json.JsonSerializerOptions](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonSerializerOptions 'System.Text.Json.JsonSerializerOptions')

#### Returns
[System.Uri](https://docs.microsoft.com/en-us/dotnet/api/System.Uri 'System.Uri')

<a name='Aydsko.iRacingData.Converters.UriConverter.Write(System.Text.Json.Utf8JsonWriter,System.Uri,System.Text.Json.JsonSerializerOptions)'></a>

## UriConverter.Write(Utf8JsonWriter, Uri, JsonSerializerOptions) Method

```csharp
public override void Write(System.Text.Json.Utf8JsonWriter writer, System.Uri value, System.Text.Json.JsonSerializerOptions options);
```
#### Parameters

<a name='Aydsko.iRacingData.Converters.UriConverter.Write(System.Text.Json.Utf8JsonWriter,System.Uri,System.Text.Json.JsonSerializerOptions).writer'></a>

`writer` [System.Text.Json.Utf8JsonWriter](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Utf8JsonWriter 'System.Text.Json.Utf8JsonWriter')

<a name='Aydsko.iRacingData.Converters.UriConverter.Write(System.Text.Json.Utf8JsonWriter,System.Uri,System.Text.Json.JsonSerializerOptions).value'></a>

`value` [System.Uri](https://docs.microsoft.com/en-us/dotnet/api/System.Uri 'System.Uri')

<a name='Aydsko.iRacingData.Converters.UriConverter.Write(System.Text.Json.Utf8JsonWriter,System.Uri,System.Text.Json.JsonSerializerOptions).options'></a>

`options` [System.Text.Json.JsonSerializerOptions](https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonSerializerOptions 'System.Text.Json.JsonSerializerOptions')