#### [Aydsko.iRacingData](index.md 'index')
### [Aydsko.iRacingData](index.md#Aydsko.iRacingData 'Aydsko.iRacingData').[IDataClient](IDataClient.md 'Aydsko.iRacingData.IDataClient')

## IDataClient.GetSeasonResultsAsync(int, EventType, int, CancellationToken) Method

Retrieve information about the races run during a week in the season.

```csharp
System.Threading.Tasks.Task<Aydsko.iRacingData.Common.DataResponse<Aydsko.iRacingData.Results.SeasonResults>> GetSeasonResultsAsync(int seasonId, Aydsko.iRacingData.Common.EventType eventType, int raceWeekNumber, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='Aydsko.iRacingData.IDataClient.GetSeasonResultsAsync(int,Aydsko.iRacingData.Common.EventType,int,System.Threading.CancellationToken).seasonId'></a>

`seasonId` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

Unique identifier for the racing season.

<a name='Aydsko.iRacingData.IDataClient.GetSeasonResultsAsync(int,Aydsko.iRacingData.Common.EventType,int,System.Threading.CancellationToken).eventType'></a>

`eventType` [EventType](EventType.md 'Aydsko.iRacingData.Common.EventType')

The type of events to return.

<a name='Aydsko.iRacingData.IDataClient.GetSeasonResultsAsync(int,Aydsko.iRacingData.Common.EventType,int,System.Threading.CancellationToken).raceWeekNumber'></a>

`raceWeekNumber` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

Week number within the given season, starting with 0 for the first week.

<a name='Aydsko.iRacingData.IDataClient.GetSeasonResultsAsync(int,Aydsko.iRacingData.Common.EventType,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')

A token to allow the operation to be cancelled.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[Aydsko.iRacingData.Common.DataResponse&lt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[Aydsko.iRacingData.Results.SeasonResults](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Results.SeasonResults 'Aydsko.iRacingData.Results.SeasonResults')[&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
A [DataResponse&lt;TData&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>') containing the races in a [Aydsko.iRacingData.Results.SeasonResults](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Results.SeasonResults 'Aydsko.iRacingData.Results.SeasonResults') object.

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
If the client is not currently authenticated.

[Aydsko.iRacingData.iRacingDataClientException](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.iRacingDataClientException 'Aydsko.iRacingData.iRacingDataClientException')  
If there's a problem processing the result.