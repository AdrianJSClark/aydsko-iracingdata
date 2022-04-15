#### [Aydsko.iRacingData](index.md 'index')
### [Aydsko.iRacingData](index.md#Aydsko.iRacingData 'Aydsko.iRacingData').[IDataClient](IDataClient.md 'Aydsko.iRacingData.IDataClient')

## IDataClient.GetCareerStatisticsAsync(Nullable<int>, CancellationToken) Method

Return a summary of statistics for the given customer's career or that or the authenticated user.

```csharp
System.Threading.Tasks.Task<Aydsko.iRacingData.Common.DataResponse<Aydsko.iRacingData.Stats.MemberCareer>> GetCareerStatisticsAsync(System.Nullable<int> customerId=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='Aydsko.iRacingData.IDataClient.GetCareerStatisticsAsync(System.Nullable_int_,System.Threading.CancellationToken).customerId'></a>

`customerId` [System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')

iRacing Customer Id for the member to return statistics for, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null') for the currently authenticated user.

<a name='Aydsko.iRacingData.IDataClient.GetCareerStatisticsAsync(System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')

A token to allow the operation to be cancelled.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[Aydsko.iRacingData.Common.DataResponse&lt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[Aydsko.iRacingData.Stats.MemberCareer](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Stats.MemberCareer 'Aydsko.iRacingData.Stats.MemberCareer')[&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
A [DataResponse&lt;TData&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>') containing the statistics in a [Aydsko.iRacingData.Stats.MemberCareer](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Stats.MemberCareer 'Aydsko.iRacingData.Stats.MemberCareer') object.

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
If the client is not currently authenticated.

[Aydsko.iRacingData.iRacingDataClientException](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.iRacingDataClientException 'Aydsko.iRacingData.iRacingDataClientException')  
If there's a problem processing the result.