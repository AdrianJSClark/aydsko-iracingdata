#### [Aydsko.iRacingData](index.md 'index')
### [Aydsko.iRacingData](index.md#Aydsko.iRacingData 'Aydsko.iRacingData').[IDataClient](IDataClient.md 'Aydsko.iRacingData.IDataClient')

## IDataClient.GetSubSessionLapChartAsync(int, int, CancellationToken) Method

Get the results of a subsession, if the authenticated user is authorized to view them.

```csharp
System.Threading.Tasks.Task<Aydsko.iRacingData.Common.DataResponse<(Aydsko.iRacingData.Results.SubsessionLapsHeader Header,Aydsko.iRacingData.Results.SubsessionChartLap[] Laps)>> GetSubSessionLapChartAsync(int subSessionId, int simSessionNumber, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='Aydsko.iRacingData.IDataClient.GetSubSessionLapChartAsync(int,int,System.Threading.CancellationToken).subSessionId'></a>

`subSessionId` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

The identifier of the subsession for which results should be returned.

<a name='Aydsko.iRacingData.IDataClient.GetSubSessionLapChartAsync(int,int,System.Threading.CancellationToken).simSessionNumber'></a>

`simSessionNumber` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

The number of the session where `0` is the main event, `-1` event before the main, etc

<a name='Aydsko.iRacingData.IDataClient.GetSubSessionLapChartAsync(int,int,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')

A token to allow the operation to be cancelled.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[Aydsko.iRacingData.Common.DataResponse&lt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple 'System.ValueTuple')[Aydsko.iRacingData.Results.SubsessionLapsHeader](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Results.SubsessionLapsHeader 'Aydsko.iRacingData.Results.SubsessionLapsHeader')[,](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple 'System.ValueTuple')[Aydsko.iRacingData.Results.SubsessionChartLap](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Results.SubsessionChartLap 'Aydsko.iRacingData.Results.SubsessionChartLap')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple 'System.ValueTuple')[&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
A [DataResponse&lt;TData&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>') containing the result details in a [Aydsko.iRacingData.Results.SubSessionResult](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Results.SubSessionResult 'Aydsko.iRacingData.Results.SubSessionResult') object.

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
If the client is not currently authenticated.

[Aydsko.iRacingData.iRacingDataClientException](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.iRacingDataClientException 'Aydsko.iRacingData.iRacingDataClientException')  
If there's a problem processing the result.