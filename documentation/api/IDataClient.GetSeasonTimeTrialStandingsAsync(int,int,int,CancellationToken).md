#### [Aydsko.iRacingData](index.md 'index')
### [Aydsko.iRacingData](index.md#Aydsko.iRacingData 'Aydsko.iRacingData').[IDataClient](IDataClient.md 'Aydsko.iRacingData.IDataClient')

## IDataClient.GetSeasonTimeTrialStandingsAsync(int, int, int, CancellationToken) Method

Retrieve the time trial standings for a season.

```csharp
System.Threading.Tasks.Task<Aydsko.iRacingData.Common.DataResponse<(Aydsko.iRacingData.Stats.SeasonTimeTrialStandingsHeader Header,Aydsko.iRacingData.Stats.SeasonTimeTrialStanding[] Standings)>> GetSeasonTimeTrialStandingsAsync(int seasonId, int carClassId, int raceWeekNumber, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='Aydsko.iRacingData.IDataClient.GetSeasonTimeTrialStandingsAsync(int,int,int,System.Threading.CancellationToken).seasonId'></a>

`seasonId` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

Unique identifier for the racing season.

<a name='Aydsko.iRacingData.IDataClient.GetSeasonTimeTrialStandingsAsync(int,int,int,System.Threading.CancellationToken).carClassId'></a>

`carClassId` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

Car class identifier. See [GetCarClassesAsync(CancellationToken)](IDataClient.GetCarClassesAsync(CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetCarClassesAsync(System.Threading.CancellationToken)').

<a name='Aydsko.iRacingData.IDataClient.GetSeasonTimeTrialStandingsAsync(int,int,int,System.Threading.CancellationToken).raceWeekNumber'></a>

`raceWeekNumber` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

Week number within the given season, starting with 0 for the first week.

<a name='Aydsko.iRacingData.IDataClient.GetSeasonTimeTrialStandingsAsync(int,int,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')

A token to allow the operation to be cancelled.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[Aydsko.iRacingData.Common.DataResponse&lt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple 'System.ValueTuple')[Aydsko.iRacingData.Stats.SeasonTimeTrialStandingsHeader](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Stats.SeasonTimeTrialStandingsHeader 'Aydsko.iRacingData.Stats.SeasonTimeTrialStandingsHeader')[,](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple 'System.ValueTuple')[Aydsko.iRacingData.Stats.SeasonTimeTrialStanding](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Stats.SeasonTimeTrialStanding 'Aydsko.iRacingData.Stats.SeasonTimeTrialStanding')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple 'System.ValueTuple')[&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
A header with overall series information and an array of time trial standings.