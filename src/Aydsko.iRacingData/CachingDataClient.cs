// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Net;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Extensions.Caching.Memory;

namespace Aydsko.iRacingData;

internal class CachingDataClient : DataClient
{
    private readonly IMemoryCache memoryCache;
    private readonly ILogger<CachingDataClient> logger;

    public CachingDataClient(HttpClient httpClient,
                             ILogger<CachingDataClient> logger,
                             iRacingDataClientOptions options,
                             CookieContainer cookieContainer,
                             IMemoryCache memoryCache)
        : base(httpClient, logger, options, cookieContainer)
    {
        this.logger = logger;
        this.memoryCache = memoryCache;
    }

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

            if (response.DataExpires is not null)
            {
                ce.SetAbsoluteExpiration((DateTimeOffset)response.DataExpires!);
            }

            return response;
        }).ConfigureAwait(false);

        logger.TraceCacheHitOrMiss(infoLinkUri, isHit);

        return result!;
    }
}
