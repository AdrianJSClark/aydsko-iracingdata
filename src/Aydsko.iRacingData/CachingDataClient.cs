// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Net;
using System.Net.Http.Headers;
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

    async protected override Task<(HttpResponseHeaders Headers, TData Data, DateTimeOffset? Expires)> CreateResponseViaInfoLinkAsync<TData>(Uri infoLinkUri,
                                                                                                                                      JsonTypeInfo<TData> jsonTypeInfo,
                                                                                                                                      CancellationToken cancellationToken)
    {
        var isHit = true;
        var result = await memoryCache.GetOrCreateAsync(infoLinkUri, async ce =>
        {
            isHit = false;
            var response = await base.CreateResponseViaInfoLinkAsync(infoLinkUri, jsonTypeInfo, cancellationToken)
                                     .ConfigureAwait(false);

            if (response.Expires is not null)
            {
                ce.SetAbsoluteExpiration((DateTimeOffset)response.Expires!);
            }

            return response;
        }).ConfigureAwait(false);

        logger.LogInformation("Cache status for {Url} is {HitStatus}", infoLinkUri, isHit);

        return result;
    }
}
