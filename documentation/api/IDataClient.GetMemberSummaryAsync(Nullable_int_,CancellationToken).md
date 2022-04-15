#### [Aydsko.iRacingData](index.md 'index')
### [Aydsko.iRacingData](index.md#Aydsko.iRacingData 'Aydsko.iRacingData').[IDataClient](IDataClient.md 'Aydsko.iRacingData.IDataClient')

## IDataClient.GetMemberSummaryAsync(Nullable<int>, CancellationToken) Method

Retrieve overall summary figures for the [customerId](IDataClient.GetMemberSummaryAsync(Nullable_int_,CancellationToken).md#Aydsko.iRacingData.IDataClient.GetMemberSummaryAsync(System.Nullable_int_,System.Threading.CancellationToken).customerId 'Aydsko.iRacingData.IDataClient.GetMemberSummaryAsync(System.Nullable<int>, System.Threading.CancellationToken).customerId') given or the current authenticated user.

```csharp
System.Threading.Tasks.Task<Aydsko.iRacingData.Common.DataResponse<Aydsko.iRacingData.Member.MemberSummary>> GetMemberSummaryAsync(System.Nullable<int> customerId=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='Aydsko.iRacingData.IDataClient.GetMemberSummaryAsync(System.Nullable_int_,System.Threading.CancellationToken).customerId'></a>

`customerId` [System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')

iRacing Customer Id for the member to return statistics for, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null') for the currently authenticated user.

<a name='Aydsko.iRacingData.IDataClient.GetMemberSummaryAsync(System.Nullable_int_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')

A token to allow the operation to be cancelled.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[Aydsko.iRacingData.Common.DataResponse&lt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[Aydsko.iRacingData.Member.MemberSummary](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Member.MemberSummary 'Aydsko.iRacingData.Member.MemberSummary')[&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
A [DataResponse&lt;TData&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>') containing the member's recent races in a [Aydsko.iRacingData.Stats.MemberRecentRaces](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Stats.MemberRecentRaces 'Aydsko.iRacingData.Stats.MemberRecentRaces') object.

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
If the client is not currently authenticated.

[Aydsko.iRacingData.iRacingDataClientException](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.iRacingDataClientException 'Aydsko.iRacingData.iRacingDataClientException')  
If there's a problem processing the result.