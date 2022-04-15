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
  - **[GetMyInfoAsync(CancellationToken)](IDataClient.GetMyInfoAsync(CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetMyInfoAsync(System.Threading.CancellationToken)')** `Method` Retrieve the [Aydsko.iRacingData.Member.MemberInfo](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Member.MemberInfo 'Aydsko.iRacingData.Member.MemberInfo') for the currently authenticated user.
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

<a name='Aydsko.iRacingData.Common'></a>

## Aydsko.iRacingData.Common Namespace
- **[DataResponse&lt;TData&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')** `Class`
  - **[Data](DataResponse_TData_.Data.md 'Aydsko.iRacingData.Common.DataResponse<TData>.Data')** `Property` Data returned from the API call.
  - **[RateLimitRemaining](DataResponse_TData_.RateLimitRemaining.md 'Aydsko.iRacingData.Common.DataResponse<TData>.RateLimitRemaining')** `Property` Amount of rate limit remaining.
  - **[RateLimitReset](DataResponse_TData_.RateLimitReset.md 'Aydsko.iRacingData.Common.DataResponse<TData>.RateLimitReset')** `Property` Instant at which the rate limit will be reset.
  - **[TotalRateLimit](DataResponse_TData_.TotalRateLimit.md 'Aydsko.iRacingData.Common.DataResponse<TData>.TotalRateLimit')** `Property` The current total rate limit.

<a name='Aydsko.iRacingData.Results'></a>

## Aydsko.iRacingData.Results Namespace
- **[Result](Result.md 'Aydsko.iRacingData.Results.Result')** `Class`
  - **[NewSubLevel](Result.NewSubLevel.md 'Aydsko.iRacingData.Results.Result.NewSubLevel')** `Property` Detailed driver's license rating after the race.
  - **[OldSubLevel](Result.OldSubLevel.md 'Aydsko.iRacingData.Results.Result.OldSubLevel')** `Property` Detailed driver's license rating before the race.