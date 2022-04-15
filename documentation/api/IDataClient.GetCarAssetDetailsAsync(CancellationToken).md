#### [Aydsko.iRacingData](index.md 'index')
### [Aydsko.iRacingData](index.md#Aydsko.iRacingData 'Aydsko.iRacingData').[IDataClient](IDataClient.md 'Aydsko.iRacingData.IDataClient')

## IDataClient.GetCarAssetDetailsAsync(CancellationToken) Method

Retrieves details about the car assets, including image paths and descriptions.

```csharp
System.Threading.Tasks.Task<Aydsko.iRacingData.Common.DataResponse<System.Collections.Generic.IReadOnlyDictionary<string,Aydsko.iRacingData.Cars.CarAssetDetail>>> GetCarAssetDetailsAsync(System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='Aydsko.iRacingData.IDataClient.GetCarAssetDetailsAsync(System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')

A token to allow the operation to be cancelled.

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[Aydsko.iRacingData.Common.DataResponse&lt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[System.Collections.Generic.IReadOnlyDictionary&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2 'System.Collections.Generic.IReadOnlyDictionary`2')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2 'System.Collections.Generic.IReadOnlyDictionary`2')[Aydsko.iRacingData.Cars.CarAssetDetail](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Cars.CarAssetDetail 'Aydsko.iRacingData.Cars.CarAssetDetail')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2 'System.Collections.Generic.IReadOnlyDictionary`2')[&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
A [DataResponse&lt;TData&gt;](DataResponse_TData_.md 'Aydsko.iRacingData.Common.DataResponse<TData>') containing a dictionary which maps the car identifier to a [Aydsko.iRacingData.Cars.CarAssetDetail](https://docs.microsoft.com/en-us/dotnet/api/Aydsko.iRacingData.Cars.CarAssetDetail 'Aydsko.iRacingData.Cars.CarAssetDetail') object for each car.

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
If the client is not currently authenticated.

[System.Exception](https://docs.microsoft.com/en-us/dotnet/api/System.Exception 'System.Exception')  
If there's a problem processing the result.

### Remarks
All image paths are relative to `https://images-static.iracing.com`.