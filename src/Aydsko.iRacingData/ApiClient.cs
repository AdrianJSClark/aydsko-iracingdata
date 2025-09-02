// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Aydsko.iRacingData.Exceptions;

namespace Aydsko.iRacingData;

public interface IApiClient
{
    Task<DataResponse<(THeader, TChunkData[])>> CreateResponseFromChunksAsync<THeader, TChunkData>(Uri uri, bool isViaInfoLink, JsonTypeInfo<THeader> jsonTypeInfo, Func<THeader, IChunkInfo> getChunkDownloadDetail, JsonTypeInfo<TChunkData[]> chunkArrayTypeInfo, CancellationToken cancellationToken = default);
    Task<DataResponse<TData>> CreateResponseViaIntermediateResultAsync<TIntermediate, TData>(Uri intermediateUri, JsonTypeInfo<TIntermediate> intermediateJsonTypeInfo, Func<TIntermediate, (Uri DataLink, DateTimeOffset? Expires)> getDataLinkAndExpiry, JsonTypeInfo<TData> jsonTypeInfo, CancellationToken cancellationToken);
    Task<DataResponse<TData>> GetDataResponseAsync<TData>(Uri uri, JsonTypeInfo<TData> jsonTypeInfo, CancellationToken cancellationToken) where TData : class;
    Task<HttpResponseMessage> GetUnauthenticatedRawResponseAsync(Uri uri, CancellationToken cancellationToken = default);
    Task<TData> GetUnauthenticatedResponseAsync<TData>(Uri uri, JsonTypeInfo<TData> jsonTypeInfo, CancellationToken cancellationToken) where TData : class;

    [Obsolete("Do not use. Configure via the \"AddIRacingDataApi\" extension method on the IServiceCollection which allows you to configure the \"iRacingDataClientOptions\".")]
    internal void UseUsernameAndPassword(string username, string password, bool passwordIsEncoded);
}

public class ApiClient(IAuthenticatingHttpClient httpClient,
                       iRacingDataClientOptions options,
                       ILogger<ApiClient> logger)
    : IDisposable, IApiClient
{
    private const string RateLimitExceededContent = "Rate limit exceeded";

    private bool disposedValue;

    protected IAuthenticatingHttpClient HttpClient { get; } = httpClient;
    protected iRacingDataClientOptions Options { get; } = options;
    protected ILogger<ApiClient> Logger { get; } = logger;

    protected DataResponse<TData> BuildDataResponse<TData>(HttpResponseHeaders headers, TData data, DateTimeOffset? expires = null)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(headers);
#else
        if (headers is null)
        {
            throw new ArgumentNullException(nameof(headers));
        }
#endif

        var response = new DataResponse<TData> { Data = data };

        if (headers.TryGetValues("x-ratelimit-remaining", out var remainingValues)
            && remainingValues.Any()
            && int.TryParse(remainingValues.First(), out var remaining))
        {
            response.RateLimitRemaining = remaining;
        }

        if (headers.TryGetValues("x-ratelimit-limit", out var limitValues)
            && limitValues.Any()
            && int.TryParse(limitValues.First(), out var limit))
        {
            response.TotalRateLimit = limit;
        }

        if (headers.TryGetValues("x-ratelimit-reset", out var resetValues)
            && resetValues.Any()
            && long.TryParse(resetValues.First(), out var resetTimeUnixSeconds))
        {
            response.RateLimitReset = DateTimeOffset.FromUnixTimeSeconds(resetTimeUnixSeconds);
        }

        response.DataExpires = expires;

        Logger.RateLimitsUpdated(response.RateLimitRemaining, response.TotalRateLimit, response.RateLimitReset);

        return response;
    }

    public async Task<DataResponse<(THeader, TChunkData[])>> CreateResponseFromChunksAsync<THeader, TChunkData>(Uri uri,
                                                                                                                bool isViaInfoLink,
                                                                                                                JsonTypeInfo<THeader> jsonTypeInfo,
                                                                                                                Func<THeader, IChunkInfo> getChunkDownloadDetail,
                                                                                                                JsonTypeInfo<TChunkData[]> chunkArrayTypeInfo,
                                                                                                                CancellationToken cancellationToken = default)
    {
#pragma warning disable CA1510
        if (getChunkDownloadDetail is null)
        {
            throw new ArgumentNullException(nameof(getChunkDownloadDetail));
        }
#pragma warning restore CA1510

        var attempts = 0;

    RetryResponseViaInfoLinkToChunkInfo:

        HttpResponseMessage? responseMessage = null;

        try
        {
            Uri link;
            HttpResponseHeaders? headers = null;
            DateTimeOffset? expires = null;

            if (isViaInfoLink)
            {
                var (infoLink, infoLinkHeaders) = await BuildIntermediateResultAsync(uri,
                                                                                     LinkResultContext.Default.LinkResult,
                                                                                     cancellationToken)
                                                        .ConfigureAwait(false);

                if (infoLink?.Link is null)
                {
                    throw new iRacingDataClientException("Unrecognized result.");
                }

                link = new Uri(infoLink.Link);
                headers = infoLinkHeaders;
                expires = infoLink.Expires;

                using var linkRequest = new HttpRequestMessage(HttpMethod.Get, link);
                responseMessage = await HttpClient.SendAsync(linkRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                                                  .ConfigureAwait(false);
            }
            else
            {
                using var nonLinkRequest = new HttpRequestMessage(HttpMethod.Get, uri);
                responseMessage = await HttpClient.SendAuthenticatedRequestAsync(nonLinkRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                                                  .ConfigureAwait(false);
                headers = responseMessage.Headers;
            }

            // This isn't the most performant way of going here, but annoyingly if you exceed the rate limit it isn't an error just
            // the string "Rate limit exceeded" so we need the string to check that.
#if NET6_0_OR_GREATER
            var responseContent = await responseMessage.Content.ReadAsStringAsync(cancellationToken)
                                                               .ConfigureAwait(false);
#else
            var responseContent = await responseMessage.Content.ReadAsStringAsync()
                                                               .ConfigureAwait(false);
#endif
            if (!responseMessage.IsSuccessStatusCode || responseContent == RateLimitExceededContent)
            {
                HandleUnsuccessfulResponse(responseMessage, responseContent, Logger);
            }

            var headerData = JsonSerializer.Deserialize(responseContent, jsonTypeInfo)
                             ?? throw new iRacingDataClientException("Data not found.");

            var chunkInfo = getChunkDownloadDetail(headerData);

            var searchResults = new List<TChunkData>();

            _ = Activity.Current?.AddTag("NumberOfResultChunks", chunkInfo.NumberOfChunks);

            if (chunkInfo.NumberOfChunks > 0)
            {
                var baseChunkUrl = new Uri(chunkInfo.BaseDownloadUrl);

                foreach (var (chunkFileName, index) in chunkInfo.ChunkFileNames.Select((fn, i) => (fn, i)))
                {
                    _ = Activity.Current?.AddEvent(new("Start downloading chunk", tags: new([new("ChunkIndex", index)])));

                    using var getChunkRequest = new HttpRequestMessage(HttpMethod.Get, new Uri(baseChunkUrl, chunkFileName));
                    var chunkResponse = await HttpClient.SendAsync(getChunkRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                                                        .ConfigureAwait(false);

                    if (!chunkResponse.IsSuccessStatusCode)
                    {
                        Logger.FailedToRetrieveChunkError(index, chunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                        continue;
                    }

                    var chunkData = await chunkResponse.Content.ReadFromJsonAsync(chunkArrayTypeInfo, cancellationToken)
                                                               .ConfigureAwait(false);
                    if (chunkData is null)
                    {
                        continue;
                    }

                    searchResults.AddRange(chunkData);
                }
            }

            return BuildDataResponse<(THeader Header, TChunkData[] Results)>(headers!, (headerData, searchResults.ToArray()), expires);
        }
        catch (iRacingUnauthorizedResponseException unAuthorizedEx)
        {
            attempts++;
            if (attempts <= 2)
            {
                _ = Activity.Current?.AddEvent(new("Retrying unauthorized response", tags: new([new("AttemptCount", attempts)])));
                Logger.RetryingUnauthorizedResponse(unAuthorizedEx, uri, attempts, 2);
                goto RetryResponseViaInfoLinkToChunkInfo;
            }
            throw;
        }
        finally
        {
            responseMessage?.Dispose();
        }
    }

    public async Task<DataResponse<TData>> CreateResponseViaIntermediateResultAsync<TIntermediate, TData>(Uri intermediateUri,
                                                                                                          JsonTypeInfo<TIntermediate> intermediateJsonTypeInfo,
                                                                                                          Func<TIntermediate, (Uri DataLink, DateTimeOffset? Expires)> getDataLinkAndExpiry,
                                                                                                          JsonTypeInfo<TData> jsonTypeInfo,
                                                                                                          CancellationToken cancellationToken)
    {
#pragma warning disable CA1510 // The alternative here is not available in .NET Standard 2.0
        if (getDataLinkAndExpiry is null)
        {
            throw new ArgumentNullException(nameof(getDataLinkAndExpiry));
        }
#pragma warning restore CA1510

        var attempts = 0;

    RetryResponseViaInfoLink:
        try
        {
            var (finalLinkDto, headers) = await BuildIntermediateResultAsync(intermediateUri,
                                                                             intermediateJsonTypeInfo,
                                                                             cancellationToken)
                                                .ConfigureAwait(false);
            if (finalLinkDto is null)
            {
                throw new iRacingDataClientException("Unrecognized result.");
            }

            var (finalLinkUri, expires) = getDataLinkAndExpiry(finalLinkDto);

            _ = Activity.Current?.AddEvent(new ActivityEvent("Result Link Retrieved"));

            using var finalLinkRequest = new HttpRequestMessage(HttpMethod.Get, finalLinkUri);
            var response = await HttpClient.SendAsync(finalLinkRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                                           .ConfigureAwait(false);

            _ = response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync(jsonTypeInfo, cancellationToken)
                                             .ConfigureAwait(false)
                       ?? throw new iRacingDataClientException("Data not found.");

            _ = Activity.Current?.AddEvent(new ActivityEvent("Data Retrieved"));

            return BuildDataResponse(headers, data, expires);
        }
        catch (iRacingUnauthorizedResponseException unAuthorizedEx)
        {
            attempts++;
            if (attempts <= 2)
            {
                _ = Activity.Current?.AddEvent(new("Retrying unauthorized response", tags: new([new("AttemptCount", attempts)])));
                Logger.RetryingUnauthorizedResponse(unAuthorizedEx, intermediateUri, attempts, 2);
                goto RetryResponseViaInfoLink;
            }
            throw;
        }
    }

    public async Task<DataResponse<TData>> GetDataResponseAsync<TData>(Uri uri,
                                                                       JsonTypeInfo<TData> jsonTypeInfo,
                                                                       CancellationToken cancellationToken)
        where TData : class
    {
        var (data, headers) = await GetResponseWithHeadersFromJsonAsync(uri, jsonTypeInfo, cancellationToken)
                                        .ConfigureAwait(false);
        return BuildDataResponse(headers, data);
    }

    public async Task<TData> GetUnauthenticatedResponseAsync<TData>(Uri uri,
                                                                    JsonTypeInfo<TData> jsonTypeInfo,
                                                                    CancellationToken cancellationToken)
        where TData : class
    {
        var (data, _) = await GetResponseWithHeadersFromJsonAsync(uri, jsonTypeInfo, cancellationToken, true)
                                .ConfigureAwait(false);
        return data;
    }

    public async Task<HttpResponseMessage> GetUnauthenticatedRawResponseAsync(Uri uri, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, uri);
        var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                                       .ConfigureAwait(false);
        return response;
    }

    protected virtual async Task<(TResult?, HttpResponseHeaders)> BuildIntermediateResultAsync<TResult>(Uri intermediateUri,
                                                                                                        JsonTypeInfo<TResult> jsonTypeInfo,
                                                                                                        CancellationToken cancellationToken)
    {
        using var intermediateRequest = new HttpRequestMessage(HttpMethod.Get, intermediateUri);
        var intermediateResponse = await HttpClient.SendAuthenticatedRequestAsync(intermediateRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                                                   .ConfigureAwait(false);

#if NET6_0_OR_GREATER
        var content = await intermediateResponse.Content.ReadAsStringAsync(cancellationToken)
                                                        .ConfigureAwait(false);
#else
        var content = await intermediateResponse.Content.ReadAsStringAsync()
                                                        .ConfigureAwait(false);
#endif

        if (!intermediateResponse.IsSuccessStatusCode || content == RateLimitExceededContent)
        {
            HandleUnsuccessfulResponse(intermediateResponse, content, Logger);
        }

        var result = JsonSerializer.Deserialize(content, jsonTypeInfo);
        return (result, intermediateResponse.Headers);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    protected virtual async Task<(TResult Result, HttpResponseHeaders Headers)> GetResponseWithHeadersFromJsonAsync<TResult>(Uri uri,
                                                                                                                             JsonTypeInfo<TResult> jsonTypeInfo,
                                                                                                                             CancellationToken cancellationToken,
                                                                                                                             bool skipAuthentication = false)
        where TResult : class
    {
        var attempts = 0;

    RetryResponseWithHeadersFromJson:
        try
        {

            using var request = new HttpRequestMessage(HttpMethod.Get, uri);

            HttpResponseMessage? response;
            if (skipAuthentication)
            {
                response = await HttpClient.SendAsync(request,
                                                      HttpCompletionOption.ResponseHeadersRead,
                                                      cancellationToken)
                                           .ConfigureAwait(false);
            }
            else
            {
                response = await HttpClient.SendAuthenticatedRequestAsync(request,
                                                                          HttpCompletionOption.ResponseHeadersRead,
                                                                          cancellationToken)
                                           .ConfigureAwait(false);
            }

#if NET6_0_OR_GREATER
            var content = await response.Content.ReadAsStringAsync(cancellationToken)
                                                .ConfigureAwait(false);
#else
            var content = await response.Content.ReadAsStringAsync()
                                                .ConfigureAwait(false);
#endif

            if (!response.IsSuccessStatusCode
                || content == RateLimitExceededContent)
            {
                HandleUnsuccessfulResponse(response, content, Logger);
            }

            var result = JsonSerializer.Deserialize(content, jsonTypeInfo)
                         ?? throw new iRacingDataClientException("Unrecognized result.");

            _ = Activity.Current?.AddEvent(new ActivityEvent("Data Retrieved"));

            return (result, response.Headers);
        }
        catch (iRacingUnauthorizedResponseException unAuthorizedEx)
        {
            attempts++;
            if (attempts <= 2)
            {
                _ = Activity.Current?.AddEvent(new("Retrying unauthorized response", tags: new([new("AttemptCount", attempts)])));
                Logger.RetryingUnauthorizedResponse(unAuthorizedEx, uri, attempts, 2);
                goto RetryResponseWithHeadersFromJson;
            }
            throw;
        }
    }

    protected virtual void HandleUnsuccessfulResponse(HttpResponseMessage httpResponse, string content, ILogger logger)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(httpResponse);
#else
        if (httpResponse is null)
        {
            throw new ArgumentNullException(nameof(httpResponse));
        }
#endif

        string? errorDescription;
        Exception? exception;
        content = content?.Trim() ?? string.Empty;
        if (content == "Rate limit exceeded")
        {
            errorDescription = content;
            exception = iRacingRateLimitExceededException.Create();
        }
#if NET6_0_OR_GREATER
        else if (content.StartsWith('<'))
#else
        else if (content.StartsWith("<", StringComparison.OrdinalIgnoreCase))
#endif
        {
            exception = iRacingUnknownResponseException.Create(httpResponse.StatusCode, content);
            errorDescription = exception.Message;
        }
        else
        {
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content);
            errorDescription = errorResponse?.Note ?? errorResponse?.Message ?? errorResponse?.ErrorDescription ?? "An error occurred.";

            exception = errorResponse switch
            {
                { ErrorCode: "Site Maintenance" } => new iRacingInMaintenancePeriodException(errorResponse.Note ?? "iRacing services are down for maintenance."),
                { ErrorCode: "Forbidden" } => iRacingForbiddenResponseException.Create(),
                { ErrorCode: "Unauthorized" } or { ErrorCode: "access_denied" } => iRacingUnauthorizedResponseException.Create(errorResponse.Message),
                _ => null
            };
        }

        if (exception is null)
        {
            logger.ErrorResponseUnknown();
            _ = httpResponse.EnsureSuccessStatusCode();
        }
        else
        {
            if (exception is iRacingUnauthorizedResponseException)
            {
                HttpClient.ClearLoggedInState();
            }

            logger.ErrorResponse(errorDescription, exception);
            throw exception;
        }
    }

    protected internal static string EncodePassword(string username, string password)
    {
#pragma warning disable CA1308 // Normalize strings to uppercase - iRacing API requires lowercase
        var passwordAndEmail = password + (username?.ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

#if NET6_0_OR_GREATER
        var hashedPasswordAndEmailBytes = SHA256.HashData(Encoding.UTF8.GetBytes(passwordAndEmail));
#else
        using var sha256 = SHA256.Create();
        var hashedPasswordAndEmailBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordAndEmail));
#endif

        var encodedHash = Convert.ToBase64String(hashedPasswordAndEmailBytes);
        return encodedHash;
    }

    [Obsolete("Do not use. Configure via the \"AddIRacingDataApi\" extension method on the IServiceCollection which allows you to configure the \"iRacingDataClientOptions\".")]
    public void UseUsernameAndPassword(string username, string password, bool passwordIsEncoded)
    {
        if (HttpClient is not LegacyUsernamePasswordApiClient legacyUsernamePasswordApiClient)
        {
            throw new InvalidOperationException($"Must be using the \"{nameof(LegacyUsernamePasswordApiClient)}\" to use this method.");
        }

        legacyUsernamePasswordApiClient.UseUsernameAndPassword(username, password, passwordIsEncoded);
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~LegacyUsernamePasswordApiClient()
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
