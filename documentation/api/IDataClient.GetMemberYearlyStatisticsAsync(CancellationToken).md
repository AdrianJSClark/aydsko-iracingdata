#### [Aydsko.iRacingData](index.md 'index')
### [Aydsko.iRacingData](index.md#Aydsko.iRacingData 'Aydsko.iRacingData').[IDataClient](IDataClient.md 'Aydsko.iRacingData.IDataClient')

## IDataClient.GetMemberYearlyStatisticsAsync(CancellationToken) Method

Retrieve the statistics for the currently authenticated member, grouped by year.

```csharp
System.Threading.Tasks.Task<Aydsko.iRacingData.Common.DataResponse<Aydsko.iRacingData.Stats.MemberYearlyStatistics>> GetMemberYearlyStatisticsAsync(System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='Aydsko.iRacingData.IDataClient.GetMemberYearlyStatisticsAsync(System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')

A token to allow the operation to be cancelled.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[Aydsko.iRacingData.Common.DataResponse&lt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[Aydsko.iRacingData.Stats.MemberYearlyStatistics](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Stats.MemberYearlyStatistics 'Aydsko.iRacingData.Stats.MemberYearlyStatistics')[&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
A [DataResponse&lt;TData&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>') containing the member's statistics in a [Aydsko.iRacingData.Stats.MemberYearlyStatistics](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Stats.MemberYearlyStatistics 'Aydsko.iRacingData.Stats.MemberYearlyStatistics') object.

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
If the client is not currently authenticated.

[Aydsko.iRacingData.iRacingDataClientException](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.iRacingDataClientException 'Aydsko.iRacingData.iRacingDataClientException')  
If there's a problem processing the result.