#### [Aydsko.iRacingData](index.md 'index')
### [Aydsko.iRacingData](index.md#Aydsko.iRacingData 'Aydsko.iRacingData').[IDataClient](IDataClient.md 'Aydsko.iRacingData.IDataClient')

## IDataClient.GetSubSessionResultAsync(int, bool, CancellationToken) Method

Get the results of a subsession, if the authenticated user is authorized to view them.

```csharp
System.Threading.Tasks.Task<Aydsko.iRacingData.Common.DataResponse<Aydsko.iRacingData.Results.SubSessionResult>> GetSubSessionResultAsync(int subSessionId, bool includeLicenses, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='Aydsko.iRacingData.IDataClient.GetSubSessionResultAsync(int,bool,System.Threading.CancellationToken).subSessionId'></a>

`subSessionId` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

The identifier of the subsession for which results should be returned.

<a name='Aydsko.iRacingData.IDataClient.GetSubSessionResultAsync(int,bool,System.Threading.CancellationToken).includeLicenses'></a>

`includeLicenses` [System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

Indicates if license information should be included. Either [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool') or [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool') to exclude for performance purposes.

<a name='Aydsko.iRacingData.IDataClient.GetSubSessionResultAsync(int,bool,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')

A token to allow the operation to be cancelled.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[Aydsko.iRacingData.Common.DataResponse&lt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[Aydsko.iRacingData.Results.SubSessionResult](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Results.SubSessionResult 'Aydsko.iRacingData.Results.SubSessionResult')[&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
A [DataResponse&lt;TData&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>') containing the result details in a [Aydsko.iRacingData.Results.SubSessionResult](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Results.SubSessionResult 'Aydsko.iRacingData.Results.SubSessionResult') object.

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
If the client is not currently authenticated.

[Aydsko.iRacingData.iRacingDataClientException](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.iRacingDataClientException 'Aydsko.iRacingData.iRacingDataClientException')  
If there's a problem processing the result.