using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Caching.Memory;

namespace Aydsko.iRacingData;

internal sealed class CachingApiClient(IAuthenticatingHttpClient httpClient,
                                       iRacingDataClientOptions options,
                                       IMemoryCache memoryCache,
                                       ILogger<CachingApiClient> logger,
                                       ILogger<ApiClient> apiClientLogger,
                                       TimeProvider timeProvider)
    : IApiClient, IDisposable
{
    private readonly ApiClient apiClient = new(httpClient, options, apiClientLogger);
    private bool disposedValue;

    public async Task<DataResponse<(THeader, TChunkData[])>> CreateResponseFromChunksAsync<THeader, TChunkData>(Uri uri,
                                                                                                                bool isViaInfoLink,
                                                                                                                JsonTypeInfo<THeader> jsonTypeInfo,
                                                                                                                Func<THeader, IChunkInfo> getChunkDownloadDetail,
                                                                                                                JsonTypeInfo<TChunkData[]> chunkArrayTypeInfo,
                                                                                                                CancellationToken cancellationToken = default)
    {
        var isHit = true;

        var result = await memoryCache.GetOrCreateAsync(new CreateResponseFromChunksKey(uri, isViaInfoLink), async ce =>
        {
            isHit = false;
            var response = await apiClient.CreateResponseFromChunksAsync(uri,
                                                                         isViaInfoLink,
                                                                         jsonTypeInfo,
                                                                         getChunkDownloadDetail,
                                                                         chunkArrayTypeInfo,
                                                                         cancellationToken)
                                          .ConfigureAwait(false);

            var expiry = response.DataExpires ?? timeProvider.GetUtcNow()
                                                             .AddMinutes(30);
            _ = ce.SetAbsoluteExpiration(expiry);

            return response;
        }).ConfigureAwait(false);

        logger.TraceCacheHitOrMiss(uri, isHit);

        return result!;
    }

    private record CreateResponseFromChunksKey(Uri Uri, bool IsViaInfoLink);

    public async Task<DataResponse<TData>> CreateResponseViaIntermediateResultAsync<TIntermediate, TData>(Uri intermediateUri,
                                                                                                    JsonTypeInfo<TIntermediate> intermediateJsonTypeInfo,
                                                                                                    Func<TIntermediate, (Uri DataLink, DateTimeOffset? Expires)> getDataLinkAndExpiry,
                                                                                                    JsonTypeInfo<TData> jsonTypeInfo,
                                                                                                    CancellationToken cancellationToken)
    {
        var isHit = true;

        var result = await memoryCache.GetOrCreateAsync(intermediateUri, async ce =>
        {
            isHit = false;

            var response = await apiClient.CreateResponseViaIntermediateResultAsync(intermediateUri,
                                                                                    intermediateJsonTypeInfo,
                                                                                    getDataLinkAndExpiry,
                                                                                    jsonTypeInfo,
                                                                                    cancellationToken)
                                          .ConfigureAwait(false);

            var expiry = response.DataExpires ?? timeProvider.GetUtcNow()
                                                             .AddMinutes(30);
            _ = ce.SetAbsoluteExpiration(expiry);

            return response;
        }).ConfigureAwait(false);

        logger.TraceCacheHitOrMiss(intermediateUri, isHit);

        return result!;
    }

    public async Task<DataResponse<TData>> GetDataResponseAsync<TData>(Uri uri,
                                                                       JsonTypeInfo<TData> jsonTypeInfo,
                                                                       CancellationToken cancellationToken)
        where TData : class
    {
        var isHit = true;

        var result = await memoryCache.GetOrCreateAsync(uri, async ce =>
        {
            isHit = false;

            var response = await apiClient.GetDataResponseAsync(uri,
                                                                jsonTypeInfo,
                                                                cancellationToken)
                                          .ConfigureAwait(false);

            var expiry = response.DataExpires ?? timeProvider.GetUtcNow()
                                                             .AddMinutes(30);
            _ = ce.SetAbsoluteExpiration(expiry);

            return response;
        }).ConfigureAwait(false);

        logger.TraceCacheHitOrMiss(uri, isHit);

        return result!;
    }

    public async Task<HttpResponseMessage> GetUnauthenticatedRawResponseAsync(Uri uri,
                                                                              CancellationToken cancellationToken = default)
    {
        var isHit = true;

        var result = await memoryCache.GetOrCreateAsync(uri, async ce =>
        {
            isHit = false;

            var response = await apiClient.GetUnauthenticatedRawResponseAsync(uri,
                                                                cancellationToken)
                                          .ConfigureAwait(false);

            var expiry = timeProvider.GetUtcNow()
                                     .AddMinutes(30);
            _ = ce.SetAbsoluteExpiration(expiry);

            return response;
        }).ConfigureAwait(false);

        logger.TraceCacheHitOrMiss(uri, isHit);

        return result!;
    }

    public async Task<TData> GetUnauthenticatedResponseAsync<TData>(Uri uri,
                                                                    JsonTypeInfo<TData> jsonTypeInfo,
                                                                    CancellationToken cancellationToken)
        where TData : class
    {
        var isHit = true;

        var result = await memoryCache.GetOrCreateAsync(uri, async ce =>
        {
            isHit = false;

            var response = await apiClient.GetUnauthenticatedResponseAsync(uri,
                                                                           jsonTypeInfo,
                                                                           cancellationToken)
                                          .ConfigureAwait(false);

            var expiry = timeProvider.GetUtcNow()
                                     .AddMinutes(30);
            _ = ce.SetAbsoluteExpiration(expiry);

            return response;
        }).ConfigureAwait(false);

        logger.TraceCacheHitOrMiss(uri, isHit);

        return result!;
    }


    [Obsolete("Do not use. Configure via the \"AddIRacingDataApi\" extension method on the IServiceCollection which allows you to configure the \"iRacingDataClientOptions\".")]
    public void UseUsernameAndPassword(string username, string password, bool passwordIsEncoded)
    {
        apiClient.UseUsernameAndPassword(username, password, passwordIsEncoded);
    }

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                apiClient?.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~CachingApiClient()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
