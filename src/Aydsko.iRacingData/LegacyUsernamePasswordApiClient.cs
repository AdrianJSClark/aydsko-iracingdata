// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Aydsko.iRacingData.Exceptions;

namespace Aydsko.iRacingData;

public class LegacyUsernamePasswordApiClient(HttpClient httpClient,
                                             iRacingDataClientOptions options,
                                             CookieContainer cookieContainer,
                                             ILogger<LegacyUsernamePasswordApiClient> logger)
    : IDisposable
{
    private readonly SemaphoreSlim loginSemaphore = new(1, 1);
    private bool disposedValue;

    public bool IsLoggedIn { get; private set; }

    [Obsolete("Configure via the \"AddIRacingDataApi\" extension method on the IServiceCollection which allows you to configure the \"iRacingDataClientOptions\".")]
    public void UseUsernameAndPassword(string username, string password, bool passwordIsEncoded)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw iRacingClientOptionsValueMissingException.Create(nameof(username));
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw iRacingClientOptionsValueMissingException.Create(nameof(password));
        }

        options.Username = username;
        options.Password = password;
        options.PasswordIsEncoded = passwordIsEncoded;

        // If the username & password has been updated likely the authentication needs to run again.
        IsLoggedIn = false;
    }

    public async Task<DataResponse<TData>> CreateResponseAsync<TData>(Uri uri,
                                                                      JsonTypeInfo<TData> jsonTypeInfo,
                                                                      CancellationToken cancellationToken)
        where TData : class
    {
        var (data, headers) = await GetResponseWithHeadersFromJsonAsync(uri, jsonTypeInfo, cancellationToken)
                                        .ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
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

    public async Task<DataResponse<TData>> CreateResponseViaInfoLinkAsync<TData>(Uri infoLinkUri,
                                                                                 JsonTypeInfo<TData> jsonTypeInfo,
                                                                                 CancellationToken cancellationToken)
    {
        var response = await CreateResponseViaIntermediateResultAsync(infoLinkUri,
                                                                      LinkResultContext.Default.LinkResult,
                                                                      infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                      jsonTypeInfo,
                                                                      cancellationToken).ConfigureAwait(false);
        return response;
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
            await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

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

            var data = await httpClient.GetFromJsonAsync(finalLinkUri, jsonTypeInfo, cancellationToken)
                                       .ConfigureAwait(false)
                                       ?? throw new iRacingDataClientException("Data not found.");
            _ = Activity.Current?.AddEvent(new ActivityEvent("Data Retrieved"));

            return BuildDataResponse(headers, data, logger, expires);
        }
        catch (iRacingUnauthorizedResponseException unAuthorizedEx)
        {
            attempts++;
            if (attempts <= 2)
            {
                _ = Activity.Current?.AddEvent(new("Retrying unauthorized response", tags: new([new("AttemptCount", attempts)])));
                logger.RetryingUnauthorizedResponse(unAuthorizedEx, intermediateUri, attempts, 2);
                goto RetryResponseViaInfoLink;
            }
            throw;
        }
    }

    public async Task<DataResponse<TData>> CreateResponseViaDataUrlAsync<TData>(Uri dataUrlUri,
                                                                                JsonTypeInfo<TData> jsonTypeInfo,
                                                                                CancellationToken cancellationToken)
    {
        var attempts = 0;

    RetryResponseViaInfoLink:
        try
        {
            await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

            var (dataUrlResult, headers) = await BuildIntermediateResultAsync(dataUrlUri,
                                                                              DataUrlResultContext.Default.DataUrlResult,
                                                                              cancellationToken)
                                                 .ConfigureAwait(false);

            if (dataUrlResult is null || string.IsNullOrWhiteSpace(dataUrlResult.DataUrl))
            {
                throw new iRacingDataClientException("Unrecognized result.");
            }

            _ = Activity.Current?.AddEvent(new ActivityEvent("Data URL Link Retrieved"));

            var data = await httpClient.GetFromJsonAsync(dataUrlResult.DataUrl, jsonTypeInfo, cancellationToken)
                                       .ConfigureAwait(false)
                                       ?? throw new iRacingDataClientException("Data not found.");
            _ = Activity.Current?.AddEvent(new ActivityEvent("Data Retrieved"));

            return BuildDataResponse(headers, data, logger);
        }
        catch (iRacingUnauthorizedResponseException unAuthorizedEx)
        {
            attempts++;
            if (attempts <= 2)
            {
                _ = Activity.Current?.AddEvent(new("Retrying unauthorized response", tags: new([new("AttemptCount", attempts)])));
                logger.RetryingUnauthorizedResponse(unAuthorizedEx, dataUrlUri, attempts, 2);
                goto RetryResponseViaInfoLink;
            }
            throw;
        }
    }

    public async Task<DataResponse<(THeader, TChunkData[])>> CreateResponseFromChunkedDataAsync<THeader, TChunkData>(Uri uri,
                                                                                                                     JsonTypeInfo<THeader> jsonTypeInfo,
                                                                                                                     Func<THeader, IChunkInfo> getChunkDownloadDetail,
                                                                                                                     JsonTypeInfo<TChunkData[]> chunkArrayTypeInfo,
                                                                                                                     CancellationToken cancellationToken)
    {
#pragma warning disable CA1510
        if (getChunkDownloadDetail is null)
        {
            throw new ArgumentNullException(nameof(getChunkDownloadDetail));
        }
#pragma warning restore CA1510

        var attempts = 0;

    RetryResponseFromChunkedData:
        try
        {
            await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

            var response = await httpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);

            // This isn't the most performant way of going here, but annoyingly if you exceed the rate limit it isn't an issue just
            // the string "Rate limit exceeded" so we need the string to check that.
#if NET6_0_OR_GREATER
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken)
                                                        .ConfigureAwait(false);
#else
            var responseContent = await response.Content.ReadAsStringAsync()
                                                        .ConfigureAwait(false);
#endif
            if (!response.IsSuccessStatusCode || responseContent == RateLimitExceededContent)
            {
                HandleUnsuccessfulResponse(response, responseContent, logger);
            }

            var headerData = await response.Content.ReadFromJsonAsync(jsonTypeInfo, cancellationToken: cancellationToken)
                                                   .ConfigureAwait(false)
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

                    var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

                    var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
                    if (!chunkResponse.IsSuccessStatusCode)
                    {
                        logger.FailedToRetrieveChunkError(index, chunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                        continue;
                    }

                    var chunkData = await chunkResponse.Content.ReadFromJsonAsync(chunkArrayTypeInfo, cancellationToken).ConfigureAwait(false);
                    if (chunkData is null)
                    {
                        continue;
                    }

                    searchResults.AddRange(chunkData);
                }
            }

            return BuildDataResponse<(THeader Header, TChunkData[] Results)>(response.Headers, (headerData, searchResults.ToArray()), logger);
        }
        catch (iRacingUnauthorizedResponseException unAuthorizedEx)
        {
            attempts++;
            if (attempts <= 2)
            {
                _ = Activity.Current?.AddEvent(new("Retrying unauthorized response", tags: new([new("AttemptCount", attempts)])));
                logger.RetryingUnauthorizedResponse(unAuthorizedEx, uri, attempts, 2);
                goto RetryResponseFromChunkedData;
            }
            throw;
        }
    }

    public async Task<DataResponse<(THeader, TChunkData[])>> CreateResponseViaInfoLinkToChunkInfoAsync<THeader, TChunkData>(Uri infoLinkUri,
                                                                                                                            JsonTypeInfo<THeader> jsonTypeInfo,
                                                                                                                            Func<THeader, IChunkInfo> getChunkDownloadDetail,
                                                                                                                            JsonTypeInfo<TChunkData[]> chunkArrayTypeInfo,
                                                                                                                            CancellationToken cancellationToken)
    {
#pragma warning disable CA1510
        if (getChunkDownloadDetail is null)
        {
            throw new ArgumentNullException(nameof(getChunkDownloadDetail));
        }
#pragma warning restore CA1510

        var attempts = 0;

    RetryResponseViaInfoLinkToChunkInfo:
        try
        {
            await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

            var (infoLink, headers) = await BuildIntermediateResultAsync(infoLinkUri,
                                                                         LinkResultContext.Default.LinkResult,
                                                                         cancellationToken)
                                            .ConfigureAwait(false);

            if (infoLink?.Link is null)
            {
                throw new iRacingDataClientException("Unrecognized result.");
            }

            var headerData = await httpClient.GetFromJsonAsync(infoLink?.Link, jsonTypeInfo, cancellationToken)
                                             .ConfigureAwait(false)
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

                    var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

                    var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
                    if (!chunkResponse.IsSuccessStatusCode)
                    {
                        logger.FailedToRetrieveChunkError(index, chunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                        continue;
                    }

                    var chunkData = await chunkResponse.Content.ReadFromJsonAsync(chunkArrayTypeInfo, cancellationToken).ConfigureAwait(false);
                    if (chunkData is null)
                    {
                        continue;
                    }

                    searchResults.AddRange(chunkData);
                }
            }

            return BuildDataResponse<(THeader Header, TChunkData[] Results)>(headers, (headerData, searchResults.ToArray()), logger, infoLink?.Expires);
        }
        catch (iRacingUnauthorizedResponseException unAuthorizedEx)
        {
            attempts++;
            if (attempts <= 2)
            {
                _ = Activity.Current?.AddEvent(new("Retrying unauthorized response", tags: new([new("AttemptCount", attempts)])));
                logger.RetryingUnauthorizedResponse(unAuthorizedEx, infoLinkUri, attempts, 2);
                goto RetryResponseViaInfoLinkToChunkInfo;
            }
            throw;
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
            if (!skipAuthentication)
            {
                await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);
            }

            var response = await httpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);

#if NET6_0_OR_GREATER
            var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#else
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif

            if (!response.IsSuccessStatusCode || content == RateLimitExceededContent)
            {
                HandleUnsuccessfulResponse(response, content, logger);
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
                logger.RetryingUnauthorizedResponse(unAuthorizedEx, uri, attempts, 2);
                goto RetryResponseWithHeadersFromJson;
            }
            throw;
        }
    }

    protected virtual async Task<TResult> GetResponseFromJsonAsync<TResult>(Uri uri, JsonTypeInfo<TResult> jsonTypeInfo, CancellationToken cancellationToken)
        where TResult : class
    {
        var attempts = 0;

    RetryResponseFromJson:
        try
        {
            await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

            var response = await httpClient.GetFromJsonAsync(uri, jsonTypeInfo, cancellationToken).ConfigureAwait(false)
                           ?? throw new iRacingDataClientException("Data not found.");

            _ = Activity.Current?.AddEvent(new ActivityEvent("Data Retrieved"));

            return response;
        }
        catch (iRacingUnauthorizedResponseException unAuthorizedEx)
        {
            attempts++;
            if (attempts <= 2)
            {
                _ = Activity.Current?.AddEvent(new("Retrying unauthorized response", tags: new([new("AttemptCount", attempts)])));
                logger.RetryingUnauthorizedResponse(unAuthorizedEx, uri, attempts, 2);
                goto RetryResponseFromJson;
            }
            throw;
        }
    }

    /// <summary>Will ensure the client is authenticated by checking the <see cref="IsLoggedIn"/> property and executing the login process if required.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="Task"/> that resolves when the process is complete.</returns>
    protected internal async Task EnsureLoggedInAsync(CancellationToken cancellationToken)
    {
        if (!IsLoggedIn)
        {
            await loginSemaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                if (!IsLoggedIn)
                {
                    await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
                }
            }
            finally
            {
                _ = loginSemaphore.Release();
            }
        }
    }

    private async Task LoginInternalAsync(CancellationToken cancellationToken)
    {
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Login");

        if (string.IsNullOrWhiteSpace(options.Username))
        {
            throw iRacingClientOptionsValueMissingException.Create(nameof(options.Username));
        }

        if (string.IsNullOrWhiteSpace(options.Password))
        {
            throw iRacingClientOptionsValueMissingException.Create(nameof(options.Password));
        }

        try
        {
            if (options.RestoreCookies is not null
                && options.RestoreCookies() is CookieCollection savedCookies)
            {
                cookieContainer.Add(savedCookies);
            }

            var cookies = cookieContainer.GetCookies(new Uri("https://members-ng.iracing.com"));
            if (cookies["authtoken_members"] is { Expired: false })
            {
                IsLoggedIn = true;
                logger.LoginCookiesRestored(options.Username!);
                return;
            }

            string? encodedHash = null;

            if (options.PasswordIsEncoded)
            {
                encodedHash = options.Password;
            }
            else
            {
#pragma warning disable CA1308 // Normalize strings to uppercase - iRacing API requires lowercase
                var passwordAndEmail = options.Password + (options.Username?.ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

#if NET6_0_OR_GREATER
                var hashedPasswordAndEmailBytes = SHA256.HashData(Encoding.UTF8.GetBytes(passwordAndEmail));
#else
                using var sha256 = SHA256.Create();
                var hashedPasswordAndEmailBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordAndEmail));
#endif

                encodedHash = Convert.ToBase64String(hashedPasswordAndEmailBytes);
            }

            var loginResponse = await httpClient.PostAsJsonAsync("https://members-ng.iracing.com/auth",
                                                                 new
                                                                 {
                                                                     email = options.Username,
                                                                     password = encodedHash
                                                                 },
                                                                 cancellationToken)
                                                .ConfigureAwait(false);

            if (!loginResponse.IsSuccessStatusCode)
            {
                if (loginResponse.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new iRacingInMaintenancePeriodException("Maintenance assumed because login returned HTTP Error 503 \"Service Unavailable\".");
                }
                else if (loginResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
#if NET6_0_OR_GREATER
                    var content = await loginResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#else
                    var content = await loginResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif
                    var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content);

                    if (errorResponse is not null && errorResponse.ErrorCode == "access_denied")
                    {
                        var errorDescription = errorResponse.ErrorDescription ?? errorResponse.Note ?? errorResponse.Message ?? string.Empty;
                        throw iRacingLoginFailedException.Create($"Access was denied with message \"{errorDescription}\"",
                                                                 false,
                                                                 errorDescription.Equals("legacy authorization refused", StringComparison.OrdinalIgnoreCase));
                    }
                }
                throw new iRacingLoginFailedException($"Login failed with HTTP response \"{loginResponse.StatusCode} {loginResponse.ReasonPhrase}\"");
            }

            var loginResult = await loginResponse.Content.ReadFromJsonAsync(LoginResponseContext.Default.LoginResponse, cancellationToken).ConfigureAwait(false);

            if (loginResult is null || !loginResult.Success)
            {
                var message = loginResult?.Message ?? $"Login failed with HTTP response \"{loginResponse.StatusCode} {loginResponse.ReasonPhrase}\"";
                throw iRacingLoginFailedException.Create(message, loginResult?.VerificationRequired, string.Equals(loginResult?.Message, "Legacy authorization refused.", StringComparison.OrdinalIgnoreCase));
            }

            IsLoggedIn = true;
            logger.LoginSuccessful(options.Username!);

            if (options.SaveCookies is Action<CookieCollection> saveCredentials)
            {
                saveCredentials(cookieContainer.GetAllCookies());
            }
        }
        catch (Exception ex) when (ex is not iRacingDataClientException)
        {
            throw iRacingLoginFailedException.Create(ex);
        }
    }

    protected static DataResponse<TData> BuildDataResponse<TData>(HttpResponseHeaders headers, TData data, ILogger logger, DateTimeOffset? expires = null)
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

        logger.RateLimitsUpdated(response.RateLimitRemaining, response.TotalRateLimit, response.RateLimitReset);

        return response;
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
                // Unauthorized might just be our session expired
                IsLoggedIn = false;

                // Clear any externally saved cookies
                options.SaveCookies?.Invoke([]);

                // Reset the cookie container so we can re-login
                cookieContainer = new CookieContainer();
            }

            logger.ErrorResponse(errorDescription, exception);
            throw exception;
        }
    }

    private const string RateLimitExceededContent = "Rate limit exceeded";

    protected virtual async Task<(TResult?, HttpResponseHeaders)> BuildIntermediateResultAsync<TResult>(Uri intermediateUri,
                                                                                                        JsonTypeInfo<TResult> jsonTypeInfo,
                                                                                                        CancellationToken cancellationToken)
    {
        var intermediateResponse = await httpClient.GetAsync(intermediateUri, cancellationToken)
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
            HandleUnsuccessfulResponse(intermediateResponse, content, logger);
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
                loginSemaphore.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
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
