#### [Aydsko.iRacingData](Home 'Home')

## Aydsko.iRacingData.Common Namespace
### Classes

<a name='Aydsko.iRacingData.Common.CarClass'></a>

## CarClass Class

Information on a group of cars considered in the same class.

```csharp
public class CarClass
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; CarClass
### Properties

<a name='Aydsko.iRacingData.Common.CarClass.CarClassId'></a>

## CarClass.CarClassId Property

Unique identifier for the car class.

```csharp
public int CarClassId { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Aydsko.iRacingData.Common.CarClass.CarsInClass'></a>

## CarClass.CarsInClass Property

Individual cars which make up this class.

```csharp
public Aydsko.iRacingData.CarClasses.CarsInClass[] CarsInClass { get; set; }
```

#### Property Value
[CarsInClass](Aydsko.iRacingData.CarClasses#Aydsko.iRacingData.CarClasses.CarsInClass 'Aydsko.iRacingData.CarClasses.CarsInClass')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

<a name='Aydsko.iRacingData.Common.CarClass.CustomerId'></a>

## CarClass.CustomerId Property

Unique identifier of the iRacing Member.

```csharp
public System.Nullable<int> CustomerId { get; set; }
```

#### Property Value
[System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')

<a name='Aydsko.iRacingData.Common.CarClass.Name'></a>

## CarClass.Name Property

Name of the car class.

```csharp
public string Name { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Aydsko.iRacingData.Common.CarClass.RelativeSpeed'></a>

## CarClass.RelativeSpeed Property

Value indicating the relative speed of this car class.

```csharp
public int RelativeSpeed { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Aydsko.iRacingData.Common.CarClass.ShortName'></a>

## CarClass.ShortName Property

A shortened version of the car class' name.

```csharp
public string ShortName { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Aydsko.iRacingData.Common.ChunkInfo'></a>

## ChunkInfo Class

Summary details of a large data set which has been split into "chunks".

```csharp
public class ChunkInfo
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; ChunkInfo
### Properties

<a name='Aydsko.iRacingData.Common.ChunkInfo.BaseDownloadUrl'></a>

## ChunkInfo.BaseDownloadUrl Property

Common part of the URL for each chunk.

```csharp
public string BaseDownloadUrl { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Aydsko.iRacingData.Common.ChunkInfo.ChunkFileNames'></a>

## ChunkInfo.ChunkFileNames Property

List of the filename for each chunk.

```csharp
public string[] ChunkFileNames { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

<a name='Aydsko.iRacingData.Common.ChunkInfo.ChunkSize'></a>

## ChunkInfo.ChunkSize Property

Size of each chunk.

```csharp
public int ChunkSize { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Aydsko.iRacingData.Common.ChunkInfo.NumChunks'></a>

## ChunkInfo.NumChunks Property

The number of chunks the data was split into.

```csharp
public int NumChunks { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Aydsko.iRacingData.Common.ChunkInfo.Rows'></a>

## ChunkInfo.Rows Property

Total number of rows.

```csharp
public int Rows { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Aydsko.iRacingData.Common.DataResponse_TData_'></a>

## DataResponse<TData> Class

Common elements of an API response.

```csharp
public class DataResponse<TData>
```
#### Type parameters

<a name='Aydsko.iRacingData.Common.DataResponse_TData_.TData'></a>

`TData`

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; DataResponse<TData>
### Properties

<a name='Aydsko.iRacingData.Common.DataResponse_TData_.Data'></a>

## DataResponse<TData>.Data Property

Data returned from the API call.

```csharp
public TData Data { get; set; }
```

#### Property Value
[TData](Aydsko.iRacingData.Common#Aydsko.iRacingData.Common.DataResponse_TData_.TData 'Aydsko.iRacingData.Common.DataResponse<TData>.TData')

<a name='Aydsko.iRacingData.Common.DataResponse_TData_.RateLimitRemaining'></a>

## DataResponse<TData>.RateLimitRemaining Property

Amount of rate limit remaining.

```csharp
public System.Nullable<int> RateLimitRemaining { get; set; }
```

#### Property Value
[System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')

<a name='Aydsko.iRacingData.Common.DataResponse_TData_.RateLimitReset'></a>

## DataResponse<TData>.RateLimitReset Property

Instant at which the rate limit will be reset.

```csharp
public System.Nullable<System.DateTimeOffset> RateLimitReset { get; set; }
```

#### Property Value
[System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[System.DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/System.DateTimeOffset 'System.DateTimeOffset')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')

<a name='Aydsko.iRacingData.Common.DataResponse_TData_.TotalRateLimit'></a>

## DataResponse<TData>.TotalRateLimit Property

The current total rate limit.

```csharp
public System.Nullable<int> TotalRateLimit { get; set; }
```

#### Property Value
[System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')

<a name='Aydsko.iRacingData.Common.ErrorResponse'></a>

## ErrorResponse Class

Error details returned by the API.

```csharp
public class ErrorResponse
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; ErrorResponse
### Properties

<a name='Aydsko.iRacingData.Common.ErrorResponse.ErrorCode'></a>

## ErrorResponse.ErrorCode Property

Identifying code of the error.

```csharp
public string? ErrorCode { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Aydsko.iRacingData.Common.ErrorResponse.ErrorDescription'></a>

## ErrorResponse.ErrorDescription Property

Descriptive text of the error.

```csharp
public string? ErrorDescription { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Aydsko.iRacingData.Common.Helmet'></a>

## Helmet Class

Details about the driver's helmet.

```csharp
public class Helmet
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; Helmet
### Properties

<a name='Aydsko.iRacingData.Common.Helmet.Color1'></a>

## Helmet.Color1 Property

First pattern color.

```csharp
public string Color1 { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Aydsko.iRacingData.Common.Helmet.Color2'></a>

## Helmet.Color2 Property

Second pattern color.

```csharp
public string Color2 { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Aydsko.iRacingData.Common.Helmet.Color3'></a>

## Helmet.Color3 Property

Third pattern color.

```csharp
public string Color3 { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Aydsko.iRacingData.Common.Helmet.FaceType'></a>

## Helmet.FaceType Property

Identifier for the face type of the driver.

```csharp
public int FaceType { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Aydsko.iRacingData.Common.Helmet.HelmetType'></a>

## Helmet.HelmetType Property

Identifier for the type of helmet of the driver.

```csharp
public int HelmetType { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Aydsko.iRacingData.Common.Helmet.Pattern'></a>

## Helmet.Pattern Property

Identifier of the helmet pattern.

```csharp
public int Pattern { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Aydsko.iRacingData.Common.License'></a>

## License Class

License information.

```csharp
public class License
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; License

Derived  
&#8627; [LicenseInfo](Aydsko.iRacingData.Member#Aydsko.iRacingData.Member.LicenseInfo 'Aydsko.iRacingData.Member.LicenseInfo')
### Properties

<a name='Aydsko.iRacingData.Common.License.Category'></a>

## License.Category Property

Name of the license category.

```csharp
public string Category { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Aydsko.iRacingData.Common.License.CategoryId'></a>

## License.CategoryId Property

Identfier for the license category.

```csharp
public int CategoryId { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Aydsko.iRacingData.Common.License.Color'></a>

## License.Color Property

Color associated with the license.

```csharp
public string Color { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Aydsko.iRacingData.Common.License.GroupId'></a>

## License.GroupId Property

Identifier of the license group.

```csharp
public int GroupId { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Aydsko.iRacingData.Common.License.GroupName'></a>

## License.GroupName Property

Name of the license group.

```csharp
public string GroupName { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Aydsko.iRacingData.Common.License.LicenseLevel'></a>

## License.LicenseLevel Property

An indicator of the level of the license.

```csharp
public int LicenseLevel { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Aydsko.iRacingData.Common.License.SafetyRating'></a>

## License.SafetyRating Property

Value of the safety rating attached to the license.

```csharp
public decimal SafetyRating { get; set; }
```

#### Property Value
[System.Decimal](https://docs.microsoft.com/en-us/dotnet/api/System.Decimal 'System.Decimal')

<a name='Aydsko.iRacingData.Common.Suit'></a>

## Suit Class

Details about the driver's suit.

```csharp
public class Suit
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; Suit
### Properties

<a name='Aydsko.iRacingData.Common.Suit.BodyType'></a>

## Suit.BodyType Property

Type of body chosen for the driver.

```csharp
public System.Nullable<int> BodyType { get; set; }
```

#### Property Value
[System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')

<a name='Aydsko.iRacingData.Common.Suit.Color1'></a>

## Suit.Color1 Property

First pattern color.

```csharp
public string Color1 { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Aydsko.iRacingData.Common.Suit.Color2'></a>

## Suit.Color2 Property

Second pattern color.

```csharp
public string Color2 { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Aydsko.iRacingData.Common.Suit.Color3'></a>

## Suit.Color3 Property

Third pattern color.

```csharp
public string Color3 { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Aydsko.iRacingData.Common.Suit.Pattern'></a>

## Suit.Pattern Property

Pattern identifier chosen for the suit.

```csharp
public int Pattern { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Aydsko.iRacingData.Common.Track'></a>

## Track Class

Information about a track.

```csharp
public class Track
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; Track
### Properties

<a name='Aydsko.iRacingData.Common.Track.Category'></a>

## Track.Category Property

Track category name.

```csharp
public string? Category { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Aydsko.iRacingData.Common.Track.CategoryId'></a>

## Track.CategoryId Property

Track category identifier

```csharp
public System.Nullable<int> CategoryId { get; set; }
```

#### Property Value
[System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')

<a name='Aydsko.iRacingData.Common.Track.ConfigName'></a>

## Track.ConfigName Property

Track configuration name.

```csharp
public string ConfigName { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Aydsko.iRacingData.Common.Track.TrackId'></a>

## Track.TrackId Property

Identifier for the track.

```csharp
public int TrackId { get; set; }
```

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Aydsko.iRacingData.Common.Track.TrackName'></a>

## Track.TrackName Property

Name of the track.

```csharp
public string TrackName { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')
### Enums

<a name='Aydsko.iRacingData.Common.EventType'></a>

## EventType Enum

The type of the event.

```csharp
public enum EventType
```
### Fields

<a name='Aydsko.iRacingData.Common.EventType.Practice'></a>

`Practice` 2

Event was a practice session.

<a name='Aydsko.iRacingData.Common.EventType.Qualify'></a>

`Qualify` 3

Event was a qualifying session.

<a name='Aydsko.iRacingData.Common.EventType.Race'></a>

`Race` 5

Event was a race session.

<a name='Aydsko.iRacingData.Common.EventType.TimeTrial'></a>

`TimeTrial` 4

Event was a time trial session.

<a name='Aydsko.iRacingData.Common.EventType.Unknown'></a>

`Unknown` 0

Event type was not known.