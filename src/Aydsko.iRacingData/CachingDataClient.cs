// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Net;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Caching.Memory;

namespace Aydsko.iRacingData;
/*
internal class CachingDataClient(HttpClient httpClient,
                                 ILogger<CachingDataClient> logger,
                                 iRacingDataClientOptions options,
                                 CookieContainer cookieContainer,
                                 IMemoryCache memoryCache)
    : DataClient(httpClient, logger, options, cookieContainer)
{
    protected override async Task<DataResponse<TData>> CreateResponseViaInfoLinkAsync<TData>(Uri infoLinkUri,
                                                                                             JsonTypeInfo<TData> jsonTypeInfo,
                                                                                             CancellationToken cancellationToken)
    {
        var isHit = true;

        var result = await memoryCache.GetOrCreateAsync(infoLinkUri, async ce =>
        {
            isHit = false;
            var response = await base.CreateResponseViaInfoLinkAsync(infoLinkUri, jsonTypeInfo, cancellationToken)
                                     .ConfigureAwait(false);

            var expiry = response.DataExpires ?? DateTime.UtcNow.AddMinutes(30);
            ce.SetAbsoluteExpiration(expiry);

            return response;
        }).ConfigureAwait(false);

        logger.TraceCacheHitOrMiss(infoLinkUri, isHit);

        return result!;
    }

    protected override async Task<DataResponse<(TData, TChunkData[])>> CreateResponseFromChunkedDataAsync<TData, THeaderData, TChunkData>(Uri uri,
                                                                                                                                          JsonTypeInfo<TData> jsonTypeInfo,
                                                                                                                                          JsonTypeInfo<TChunkData[]> chunkArrayTypeInfo,
                                                                                                                                          CancellationToken cancellationToken)
    {
        var isHit = true;

        var result = await memoryCache.GetOrCreateAsync(uri, async ce =>
        {
            isHit = false;
            var response = await base.CreateResponseFromChunkedDataAsync<TData, THeaderData, TChunkData>(uri, jsonTypeInfo, chunkArrayTypeInfo, cancellationToken)
                                     .ConfigureAwait(false);

            var expiry = response.DataExpires ?? DateTime.UtcNow.AddMinutes(30);
            ce.SetAbsoluteExpiration(expiry);

            return response;
        }).ConfigureAwait(false);

        logger.TraceCacheHitOrMiss(uri, isHit);

        return result!;
    }
}
*/
