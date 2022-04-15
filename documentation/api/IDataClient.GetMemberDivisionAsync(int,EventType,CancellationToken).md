#### [Aydsko.iRacingData](index.md 'index')
### [Aydsko.iRacingData](index.md#Aydsko.iRacingData 'Aydsko.iRacingData').[IDataClient](IDataClient.md 'Aydsko.iRacingData.IDataClient')

## IDataClient.GetMemberDivisionAsync(int, EventType, CancellationToken) Method

Retrieve information about the authenticated member's division.

```csharp
System.Threading.Tasks.Task<Aydsko.iRacingData.Common.DataResponse<Aydsko.iRacingData.Stats.MemberDivision>> GetMemberDivisionAsync(int seasonId, Aydsko.iRacingData.Common.EventType eventType, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='Aydsko.iRacingData.IDataClient.GetMemberDivisionAsync(int,Aydsko.iRacingData.Common.EventType,System.Threading.CancellationToken).seasonId'></a>

`seasonId` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

Unique identifier for the racing season.

<a name='Aydsko.iRacingData.IDataClient.GetMemberDivisionAsync(int,Aydsko.iRacingData.Common.EventType,System.Threading.CancellationToken).eventType'></a>

`eventType` [EventType](EventType.md 'Aydsko.iRacingData.Common.EventType')

The type of events to return, either [TimeTrial](EventType.md#Aydsko.iRacingData.Common.EventType.TimeTrial 'Aydsko.iRacingData.Common.EventType.TimeTrial') or [Race](EventType.md#Aydsko.iRacingData.Common.EventType.Race 'Aydsko.iRacingData.Common.EventType.Race').

<a name='Aydsko.iRacingData.IDataClient.GetMemberDivisionAsync(int,Aydsko.iRacingData.Common.EventType,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')

A token to allow the operation to be cancelled.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[Aydsko.iRacingData.Common.DataResponse&lt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[Aydsko.iRacingData.Stats.MemberDivision](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Stats.MemberDivision 'Aydsko.iRacingData.Stats.MemberDivision')[&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
A [Aydsko.iRacingData.Stats.MemberDivision](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Stats.MemberDivision 'Aydsko.iRacingData.Stats.MemberDivision') object containing the result.

### Remarks
Divisions are 0-based: 0 is Division 1, 10 is Rookie. See [GetDivisionsAsync(CancellationToken)](IDataClient.GetDivisionsAsync(CancellationToken).md 'Aydsko.iRacingData.IDataClient.GetDivisionsAsync(System.Threading.CancellationToken)') for more information.