#### [Aydsko.iRacingData](index.md 'index')

## Aydsko.iRacingData Assembly
### Namespaces

<a name='Aydsko.iRacingData'></a>

## Aydsko.iRacingData Namespace
- **[iRacingDataClientOptions](iRacingDataClientOptions.md 'Aydsko.iRacingData.iRacingDataClientOptions')** `Class` Configuration options for the iRacing Data Client.
  - **[Password](iRacingDataClientOptions.Password.md 'Aydsko.iRacingData.iRacingDataClientOptions.Password')** `Property` Password associated with the iRacing user name used to authenticate.
  - **[RestoreCookies](iRacingDataClientOptions.RestoreCookies.md 'Aydsko.iRacingData.iRacingDataClientOptions.RestoreCookies')** `Property` Called to retrieve cookie values stored from a previous authentication.
  - **[SaveCookies](iRacingDataClientOptions.SaveCookies.md 'Aydsko.iRacingData.iRacingDataClientOptions.SaveCookies')** `Property` After a successful authentication called with the cookies to allow them to be saved.
  - **[UserAgentProductName](iRacingDataClientOptions.UserAgentProductName.md 'Aydsko.iRacingData.iRacingDataClientOptions.UserAgentProductName')** `Property` Name of the application or product using the Data Client library to be included in the HTTP `User-Agent` header.
  - **[UserAgentProductVersion](iRacingDataClientOptions.UserAgentProductVersion.md 'Aydsko.iRacingData.iRacingDataClientOptions.UserAgentProductVersion')** `Property` Version of the application or product using the Data Client library to be included in the HTTP `User-Agent` header.
  - **[Username](iRacingDataClientOptions.Username.md 'Aydsko.iRacingData.iRacingDataClientOptions.Username')** `Property` iRacing user name to use for authentication.
- **[IDataClient](IDataClient.md 'Aydsko.iRacingData.IDataClient')** `Interface`
  - **[GetCarAssetDetailsAsync(CancellationToken)](IDataClient.GetCarAssetDetailsAsync(CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetCarAssetDetailsAsync(System.Threading.CancellationToken)')** `Method` Retrieves details about the car assets, including image paths and descriptions.
  - **[GetCarClassesAsync(CancellationToken)](IDataClient.GetCarClassesAsync(CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetCarClassesAsync(System.Threading.CancellationToken)')** `Method` Retrieves details about the car classes.
  - **[GetCareerStatisticsAsync(Nullable&lt;int&gt;, CancellationToken)](IDataClient.GetCareerStatisticsAsync(Nullable_int_,CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetCareerStatisticsAsync(System.Nullable<int>, System.Threading.CancellationToken)')** `Method` Return a summary of statistics for the given customer's career or that or the authenticated user.
  - **[GetCarsAsync(CancellationToken)](IDataClient.GetCarsAsync(CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetCarsAsync(System.Threading.CancellationToken)')** `Method` Retrieves details about the cars.
  - **[GetDivisionsAsync(CancellationToken)](IDataClient.GetDivisionsAsync(CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetDivisionsAsync(System.Threading.CancellationToken)')** `Method` Retrieves a list of the iRacing Divisions.
  - **[GetDriverInfoAsync(int[], bool, CancellationToken)](IDataClient.GetDriverInfoAsync(int[],bool,CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetDriverInfoAsync(int[], bool, System.Threading.CancellationToken)')** `Method` Retrieve information about one or more other drivers by their customer identifier.
  - **[GetLicenseLookupsAsync(CancellationToken)](IDataClient.GetLicenseLookupsAsync(CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetLicenseLookupsAsync(System.Threading.CancellationToken)')** `Method` Information about license levels available in the iRacing system.
  - **[GetLookupsAsync(CancellationToken)](IDataClient.GetLookupsAsync(CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetLookupsAsync(System.Threading.CancellationToken)')** `Method` Information about reference data defined by the system.
  - **[GetMemberDivisionAsync(int, EventType, CancellationToken)](IDataClient.GetMemberDivisionAsync(int,EventType,CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetMemberDivisionAsync(int, Aydsko.iRacingData.Common.EventType, System.Threading.CancellationToken)')** `Method` Retrieve information about the authenticated member's division.
  - **[GetMemberRecentRacesAsync(CancellationToken)](IDataClient.GetMemberRecentRacesAsync(CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetMemberRecentRacesAsync(System.Threading.CancellationToken)')** `Method` Retrieve the recent race participation for the currently authenticated member.
  - **[GetMemberSummaryAsync(Nullable&lt;int&gt;, CancellationToken)](IDataClient.GetMemberSummaryAsync(Nullable_int_,CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetMemberSummaryAsync(System.Nullable<int>, System.Threading.CancellationToken)')** `Method` Retrieve overall summary figures for the customerId given or the current authenticated user.
  - **[GetMemberYearlyStatisticsAsync(CancellationToken)](IDataClient.GetMemberYearlyStatisticsAsync(CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetMemberYearlyStatisticsAsync(System.Threading.CancellationToken)')** `Method` Retrieve the statistics for the currently authenticated member, grouped by year.
  - **[GetMyInfoAsync(CancellationToken)](IDataClient.GetMyInfoAsync(CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetMyInfoAsync(System.Threading.CancellationToken)')** `Method` Retrieve the [MemberInfo](MemberInfo.md 'Aydsko.iRacingData.Member.MemberInfo') for the currently authenticated user.
  - **[GetSeasonDriverStandingsAsync(int, int, int, CancellationToken)](IDataClient.GetSeasonDriverStandingsAsync(int,int,int,CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetSeasonDriverStandingsAsync(int, int, int, System.Threading.CancellationToken)')** `Method` Retrieve the driver standings for a season.
  - **[GetSeasonQualifyResultsAsync(int, int, int, CancellationToken)](IDataClient.GetSeasonQualifyResultsAsync(int,int,int,CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetSeasonQualifyResultsAsync(int, int, int, System.Threading.CancellationToken)')** `Method` Retrieve the qualifying results for a season.
  - **[GetSeasonResultsAsync(int, EventType, int, CancellationToken)](IDataClient.GetSeasonResultsAsync(int,EventType,int,CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetSeasonResultsAsync(int, Aydsko.iRacingData.Common.EventType, int, System.Threading.CancellationToken)')** `Method` Retrieve information about the races run during a week in the season.
  - **[GetSeasonTeamStandingsAsync(int, int, int, CancellationToken)](IDataClient.GetSeasonTeamStandingsAsync(int,int,int,CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetSeasonTeamStandingsAsync(int, int, int, System.Threading.CancellationToken)')** `Method` Retrieve the team standings for a season.
  - **[GetSeasonTimeTrialResultsAsync(int, int, int, CancellationToken)](IDataClient.GetSeasonTimeTrialResultsAsync(int,int,int,CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetSeasonTimeTrialResultsAsync(int, int, int, System.Threading.CancellationToken)')** `Method` Retrieve the time trial results for a season.
  - **[GetSeasonTimeTrialStandingsAsync(int, int, int, CancellationToken)](IDataClient.GetSeasonTimeTrialStandingsAsync(int,int,int,CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetSeasonTimeTrialStandingsAsync(int, int, int, System.Threading.CancellationToken)')** `Method` Retrieve the time trial standings for a season.
  - **[GetSingleDriverSubsessionLapsAsync(int, int, int, CancellationToken)](IDataClient.GetSingleDriverSubsessionLapsAsync(int,int,int,CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetSingleDriverSubsessionLapsAsync(int, int, int, System.Threading.CancellationToken)')** `Method` Get the lap details for a particular driver in the given single-driver subsession.
  - **[GetSubsessionEventLogAsync(int, int, CancellationToken)](IDataClient.GetSubsessionEventLogAsync(int,int,CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetSubsessionEventLogAsync(int, int, System.Threading.CancellationToken)')** `Method` Retrieve a list of session events.
  - **[GetSubSessionLapChartAsync(int, int, CancellationToken)](IDataClient.GetSubSessionLapChartAsync(int,int,CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetSubSessionLapChartAsync(int, int, System.Threading.CancellationToken)')** `Method` Get the results of a subsession, if the authenticated user is authorized to view them.
  - **[GetSubSessionResultAsync(int, bool, CancellationToken)](IDataClient.GetSubSessionResultAsync(int,bool,CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetSubSessionResultAsync(int, bool, System.Threading.CancellationToken)')** `Method` Get the results of a subsession, if the authenticated user is authorized to view them.
  - **[GetTeamSubsessionLapsAsync(int, int, int, CancellationToken)](IDataClient.GetTeamSubsessionLapsAsync(int,int,int,CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetTeamSubsessionLapsAsync(int, int, int, System.Threading.CancellationToken)')** `Method` Get the lap details for a team in the given team subsession.

<a name='Aydsko.iRacingData.CarClasses'></a>

## Aydsko.iRacingData.CarClasses Namespace
- **[CarsInClass](CarsInClass.md 'Aydsko.iRacingData.CarClasses.CarsInClass')** `Class` Details about a car.
  - **[CarDirpath](CarsInClass.CarDirpath.md 'Aydsko.iRacingData.CarClasses.CarsInClass.CarDirpath')** `Property` The relative directory path for car setups and assets.
  - **[CarId](CarsInClass.CarId.md 'Aydsko.iRacingData.CarClasses.CarsInClass.CarId')** `Property` Unique identifier for the car.
  - **[Retired](CarsInClass.Retired.md 'Aydsko.iRacingData.CarClasses.CarsInClass.Retired')** `Property` Indicates if the car has been retired.

<a name='Aydsko.iRacingData.Common'></a>

## Aydsko.iRacingData.Common Namespace
- **[CarClass](CarClass.md 'Aydsko.iRacingData.Common.CarClass')** `Class` Information on a group of cars considered in the same class.
  - **[CarClassId](CarClass.CarClassId.md 'Aydsko.iRacingData.Common.CarClass.CarClassId')** `Property` Unique identifier for the car class.
  - **[CarsInClass](CarClass.CarsInClass.md 'Aydsko.iRacingData.Common.CarClass.CarsInClass')** `Property` Individual cars which make up this class.
  - **[CustomerId](CarClass.CustomerId.md 'Aydsko.iRacingData.Common.CarClass.CustomerId')** `Property` Unique identifier of the iRacing Member.
  - **[Name](CarClass.Name.md 'Aydsko.iRacingData.Common.CarClass.Name')** `Property` Name of the car class.
  - **[RelativeSpeed](CarClass.RelativeSpeed.md 'Aydsko.iRacingData.Common.CarClass.RelativeSpeed')** `Property` Value indicating the relative speed of this car class.
  - **[ShortName](CarClass.ShortName.md 'Aydsko.iRacingData.Common.CarClass.ShortName')** `Property` A shortened version of the car class' name.
- **[ChunkInfo](ChunkInfo.md 'Aydsko.iRacingData.Common.ChunkInfo')** `Class` Summary details of a large data set which has been split into "chunks".
  - **[BaseDownloadUrl](ChunkInfo.BaseDownloadUrl.md 'Aydsko.iRacingData.Common.ChunkInfo.BaseDownloadUrl')** `Property` Common part of the URL for each chunk.
  - **[ChunkFileNames](ChunkInfo.ChunkFileNames.md 'Aydsko.iRacingData.Common.ChunkInfo.ChunkFileNames')** `Property` List of the filename for each chunk.
  - **[ChunkSize](ChunkInfo.ChunkSize.md 'Aydsko.iRacingData.Common.ChunkInfo.ChunkSize')** `Property` Size of each chunk.
  - **[NumChunks](ChunkInfo.NumChunks.md 'Aydsko.iRacingData.Common.ChunkInfo.NumChunks')** `Property` The number of chunks the data was split into.
  - **[Rows](ChunkInfo.Rows.md 'Aydsko.iRacingData.Common.ChunkInfo.Rows')** `Property` Total number of rows.
- **[DataResponse&lt;TData&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')** `Class` Common elements of an API response.
  - **[Data](DataResponse_TData_.Data.md 'Aydsko.iRacingData.Common.DataResponse<TData>.Data')** `Property` Data returned from the API call.
  - **[RateLimitRemaining](DataResponse_TData_.RateLimitRemaining.md 'Aydsko.iRacingData.Common.DataResponse<TData>.RateLimitRemaining')** `Property` Amount of rate limit remaining.
  - **[RateLimitReset](DataResponse_TData_.RateLimitReset.md 'Aydsko.iRacingData.Common.DataResponse<TData>.RateLimitReset')** `Property` Instant at which the rate limit will be reset.
  - **[TotalRateLimit](DataResponse_TData_.TotalRateLimit.md 'Aydsko.iRacingData.Common.DataResponse<TData>.TotalRateLimit')** `Property` The current total rate limit.
- **[ErrorResponse](ErrorResponse.md 'Aydsko.iRacingData.Common.ErrorResponse')** `Class` Error details returned by the API.
  - **[ErrorCode](ErrorResponse.ErrorCode.md 'Aydsko.iRacingData.Common.ErrorResponse.ErrorCode')** `Property` Identifying code of the error.
  - **[ErrorDescription](ErrorResponse.ErrorDescription.md 'Aydsko.iRacingData.Common.ErrorResponse.ErrorDescription')** `Property` Descriptive text of the error.
- **[Helmet](Helmet.md 'Aydsko.iRacingData.Common.Helmet')** `Class` Details about the driver's helmet.
  - **[Color1](Helmet.Color1.md 'Aydsko.iRacingData.Common.Helmet.Color1')** `Property` First pattern color.
  - **[Color2](Helmet.Color2.md 'Aydsko.iRacingData.Common.Helmet.Color2')** `Property` Second pattern color.
  - **[Color3](Helmet.Color3.md 'Aydsko.iRacingData.Common.Helmet.Color3')** `Property` Third pattern color.
  - **[FaceType](Helmet.FaceType.md 'Aydsko.iRacingData.Common.Helmet.FaceType')** `Property` Identifier for the face type of the driver.
  - **[HelmetType](Helmet.HelmetType.md 'Aydsko.iRacingData.Common.Helmet.HelmetType')** `Property` Identifier for the type of helmet of the driver.
  - **[Pattern](Helmet.Pattern.md 'Aydsko.iRacingData.Common.Helmet.Pattern')** `Property` Identifier of the helmet pattern.
- **[License](License.md 'Aydsko.iRacingData.Common.License')** `Class` License information.
  - **[Category](License.Category.md 'Aydsko.iRacingData.Common.License.Category')** `Property` Name of the license category.
  - **[CategoryId](License.CategoryId.md 'Aydsko.iRacingData.Common.License.CategoryId')** `Property` Identfier for the license category.
  - **[Color](License.Color.md 'Aydsko.iRacingData.Common.License.Color')** `Property` Color associated with the license.
  - **[GroupId](License.GroupId.md 'Aydsko.iRacingData.Common.License.GroupId')** `Property` Identifier of the license group.
  - **[GroupName](License.GroupName.md 'Aydsko.iRacingData.Common.License.GroupName')** `Property` Name of the license group.
  - **[LicenseLevel](License.LicenseLevel.md 'Aydsko.iRacingData.Common.License.LicenseLevel')** `Property` An indicator of the level of the license.
  - **[SafetyRating](License.SafetyRating.md 'Aydsko.iRacingData.Common.License.SafetyRating')** `Property` Value of the safety rating attached to the license.
- **[Suit](Suit.md 'Aydsko.iRacingData.Common.Suit')** `Class` Details about the driver's suit.
  - **[BodyType](Suit.BodyType.md 'Aydsko.iRacingData.Common.Suit.BodyType')** `Property` Type of body chosen for the driver.
  - **[Color1](Suit.Color1.md 'Aydsko.iRacingData.Common.Suit.Color1')** `Property` First pattern color.
  - **[Color2](Suit.Color2.md 'Aydsko.iRacingData.Common.Suit.Color2')** `Property` Second pattern color.
  - **[Color3](Suit.Color3.md 'Aydsko.iRacingData.Common.Suit.Color3')** `Property` Third pattern color.
  - **[Pattern](Suit.Pattern.md 'Aydsko.iRacingData.Common.Suit.Pattern')** `Property` Pattern identifier chosen for the suit.
- **[Track](Track.md 'Aydsko.iRacingData.Common.Track')** `Class` Information about a track.
  - **[Category](Track.Category.md 'Aydsko.iRacingData.Common.Track.Category')** `Property` Track category name.
  - **[CategoryId](Track.CategoryId.md 'Aydsko.iRacingData.Common.Track.CategoryId')** `Property` Track category identifier
  - **[ConfigName](Track.ConfigName.md 'Aydsko.iRacingData.Common.Track.ConfigName')** `Property` Track configuration name.
  - **[TrackId](Track.TrackId.md 'Aydsko.iRacingData.Common.Track.TrackId')** `Property` Identifier for the track.
  - **[TrackName](Track.TrackName.md 'Aydsko.iRacingData.Common.Track.TrackName')** `Property` Name of the track.
- **[EventType](EventType.md 'Aydsko.iRacingData.Common.EventType')** `Enum` The type of the event.
  - **[Practice](EventType.md#Aydsko.iRacingData.Common.EventType.Practice 'Aydsko.iRacingData.Common.EventType.Practice')** `Field` Event was a practice session.
  - **[Qualify](EventType.md#Aydsko.iRacingData.Common.EventType.Qualify 'Aydsko.iRacingData.Common.EventType.Qualify')** `Field` Event was a qualifying session.
  - **[Race](EventType.md#Aydsko.iRacingData.Common.EventType.Race 'Aydsko.iRacingData.Common.EventType.Race')** `Field` Event was a race session.
  - **[TimeTrial](EventType.md#Aydsko.iRacingData.Common.EventType.TimeTrial 'Aydsko.iRacingData.Common.EventType.TimeTrial')** `Field` Event was a time trial session.
  - **[Unknown](EventType.md#Aydsko.iRacingData.Common.EventType.Unknown 'Aydsko.iRacingData.Common.EventType.Unknown')** `Field` Event type was not known.

<a name='Aydsko.iRacingData.Constants'></a>

## Aydsko.iRacingData.Constants Namespace
- **[Division](Division.md 'Aydsko.iRacingData.Constants.Division')** `Class` A division.
  - **[Label](Division.Label.md 'Aydsko.iRacingData.Constants.Division.Label')** `Property` Division label.
  - **[Value](Division.Value.md 'Aydsko.iRacingData.Constants.Division.Value')** `Property` Number associated with the division.

<a name='Aydsko.iRacingData.Converters'></a>

## Aydsko.iRacingData.Converters Namespace
- **[CsvStringConverter](CsvStringConverter.md 'Aydsko.iRacingData.Converters.CsvStringConverter')** `Class` Convert between a comma-separated string and an array of values.
  - **[Read(Utf8JsonReader, Type, JsonSerializerOptions)](CsvStringConverter.Read(Utf8JsonReader,Type,JsonSerializerOptions).md 'Aydsko.iRacingData.Converters.CsvStringConverter.Read(Utf8JsonReader, System.Type, JsonSerializerOptions)')** `Method` Read a comma-separated string and convert the values into an array.
  - **[Write(Utf8JsonWriter, string[], JsonSerializerOptions)](CsvStringConverter.Write(Utf8JsonWriter,string[],JsonSerializerOptions).md 'Aydsko.iRacingData.Converters.CsvStringConverter.Write(Utf8JsonWriter, string[], JsonSerializerOptions)')** `Method` Accept an array of values and write them separated by commas.
- **[TenThousandthSecondDurationConverter](TenThousandthSecondDurationConverter.md 'Aydsko.iRacingData.Converters.TenThousandthSecondDurationConverter')** `Class` The raw iRacing API results use a number type which carries duration values to the ten-thousandth of a second.  
  So, for example, a lap which was displayed in the iRacing results page as "1:23.456" would be returned as "834560".

<a name='Aydsko.iRacingData.Member'></a>

## Aydsko.iRacingData.Member Namespace
- **[MemberInfo](MemberInfo.md 'Aydsko.iRacingData.Member.MemberInfo')** `Class` Information about the iRacing member.
  - **[AI](MemberInfo.AI.md 'Aydsko.iRacingData.Member.MemberInfo.AI')** `Property` Indicates if the member represented by this record an instance of the AI.
  - **[CustomerId](MemberInfo.CustomerId.md 'Aydsko.iRacingData.Member.MemberInfo.CustomerId')** `Property` Unique identifier for the iRacing member's account.
  - **[Email](MemberInfo.Email.md 'Aydsko.iRacingData.Member.MemberInfo.Email')** `Property` Member's email address.
  - **[HasReadPp](MemberInfo.HasReadPp.md 'Aydsko.iRacingData.Member.MemberInfo.HasReadPp')** `Property` Indicates if the member has read the Privacy Policy.
  - **[HasReadTC](MemberInfo.HasReadTC.md 'Aydsko.iRacingData.Member.MemberInfo.HasReadTC')** `Property` Indicates if the member has read the Terms & Conditions.
  - **[HundredPercentClub](MemberInfo.HundredPercentClub.md 'Aydsko.iRacingData.Member.MemberInfo.HundredPercentClub')** `Property` Indicates if the user qualifies for the 100% Club.
  - **[ReadPp](MemberInfo.ReadPp.md 'Aydsko.iRacingData.Member.MemberInfo.ReadPp')** `Property` Date and time stamp indicating when the user said they read the Privacy Policy.
  - **[ReadTc](MemberInfo.ReadTc.md 'Aydsko.iRacingData.Member.MemberInfo.ReadTc')** `Property` Date and time stamp indicating when the user said they read the Terms & Conditions.
  - **[TwentyPercentDiscount](MemberInfo.TwentyPercentDiscount.md 'Aydsko.iRacingData.Member.MemberInfo.TwentyPercentDiscount')** `Property` Indicates if the user qualifies for the 20% discount.

<a name='Aydsko.iRacingData.Results'></a>

## Aydsko.iRacingData.Results Namespace
- **[Result](Result.md 'Aydsko.iRacingData.Results.Result')** `Class`
  - **[NewSubLevel](Result.NewSubLevel.md 'Aydsko.iRacingData.Results.Result.NewSubLevel')** `Property` Detailed driver's license rating after the race.
  - **[OldSubLevel](Result.OldSubLevel.md 'Aydsko.iRacingData.Results.Result.OldSubLevel')** `Property` Detailed driver's license rating before the race.