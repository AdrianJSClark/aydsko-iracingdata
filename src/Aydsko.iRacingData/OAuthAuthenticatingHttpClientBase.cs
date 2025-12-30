using System.Net.Http.Headers;
using System.Net.Http.Json;
using Aydsko.iRacingData.Exceptions;

namespace Aydsko.iRacingData;

public abstract class OAuthAuthenticatingHttpClientBase(HttpClient httpClient,
                                                        iRacingDataClientOptions options,
                                                        TimeProvider timeProvider)
    : IDisposable
{
    protected HttpClient HttpClient { get; } = httpClient;
    protected iRacingDataClientOptions Options { get; } = options;
    protected TimeProvider TimeProvider { get; } = timeProvider;

    private readonly SemaphoreSlim loginSemaphore = new(1, 1);

    private DateTimeOffset? accessTokenExpiryInstantUtc;
    private bool disposedValue;
    private DateTimeOffset? refreshTokenExpiryInstantUtc;
    private OAuthTokenResponse? tokenResponse;

    public void ClearLoggedInState()
    {
        tokenResponse = null;
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~OAuthAuthenticatingHttpClientBase()
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

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                     HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
                                                     CancellationToken cancellationToken = default)
    {
        return await HttpClient.SendAsync(request, completionOption, cancellationToken)
                               .ConfigureAwait(false);
    }

    public async Task<HttpResponseMessage> SendAuthenticatedRequestAsync(HttpRequestMessage request,
                                                                         HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
                                                                         CancellationToken cancellationToken = default)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(request);
#else
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }
#endif

        var accessToken = await GetAccessTokenAsync(cancellationToken).ConfigureAwait(false);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        return await SendAsync(request, completionOption, cancellationToken).ConfigureAwait(false);
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

    private async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        if (tokenResponse is null)
        {
            await loginSemaphore.WaitAsync(cancellationToken)
                                .ConfigureAwait(false);
            try
            {
#pragma warning disable CA1508 // Avoid dead conditional code - This is the double-check locking pattern to ensure thread safety.
#pragma warning disable IDE0074 // Use compound assignment
                if (tokenResponse is null)
                {
                    (tokenResponse, accessTokenExpiryInstantUtc, refreshTokenExpiryInstantUtc) = await RequestTokenAsync(cancellationToken).ConfigureAwait(false);
                    return tokenResponse.AccessToken;
                }
#pragma warning restore IDE0074 // Use compound assignment
#pragma warning restore CA1508 // Avoid dead conditional code
            }
            finally
            {
                _ = loginSemaphore.Release();
            }
        }
        else if (accessTokenExpiryInstantUtc <= TimeProvider.GetUtcNow())
        {
            await loginSemaphore.WaitAsync(cancellationToken)
                                .ConfigureAwait(false);
            try
            {
#pragma warning disable CA1508 // Avoid dead conditional code - This is the double-check locking pattern to ensure thread safety.
#pragma warning disable IDE0074 // Use compound assignment

                // If the refresh token doesn't exist or is expired it is no good so we'll need to request a whole new token.
                if ((refreshTokenExpiryInstantUtc ?? DateTimeOffset.MinValue) <= TimeProvider.GetUtcNow())
                {
                    (tokenResponse, accessTokenExpiryInstantUtc, refreshTokenExpiryInstantUtc) = await RequestTokenAsync(cancellationToken).ConfigureAwait(false);
                    return tokenResponse.AccessToken;
                }
                else if (accessTokenExpiryInstantUtc <= TimeProvider.GetUtcNow())
                {
                    (tokenResponse, accessTokenExpiryInstantUtc, refreshTokenExpiryInstantUtc) = await RefreshTokenAsync(cancellationToken).ConfigureAwait(false);
                    return tokenResponse.AccessToken;
                }

#pragma warning restore IDE0074 // Use compound assignment
#pragma warning restore CA1508 // Avoid dead conditional code
            }
            finally
            {
                _ = loginSemaphore.Release();
            }
        }

        return tokenResponse.AccessToken;
    }

    private async Task<RequestTokenResult> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Refresh \"password_limited\" token", System.Diagnostics.ActivityKind.Client);

        try
        {
            if (string.IsNullOrWhiteSpace(Options.ClientId)
                || string.IsNullOrWhiteSpace(Options.ClientSecret))
            {
                throw new InvalidOperationException("All of \"ClientId\" and \"ClientSecret\" must be set in the options.");
            }

            if (string.IsNullOrWhiteSpace(Options.AuthServiceBaseUrl)
                || !Uri.TryCreate(Options.AuthServiceBaseUrl, UriKind.Absolute, out var authServiceBaseUrl))
            {
                throw new InvalidOperationException("The \"AuthServiceBaseUrl\" must be a valid absolute URL in the iRacing Data Client options.");
            }

            if (tokenResponse?.RefreshToken is not string refreshToken
                || string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new InvalidOperationException("Previous response must have contained a refresh token value.");
            }

            var encodedClientSecret = Options.ClientSecretIsEncoded ? Options.ClientSecret : ApiClient.EncodePassword(Options.ClientId!, Options.ClientSecret!);

            using var newTokenRequest = new HttpRequestMessage(HttpMethod.Post,
                                                               new Uri(authServiceBaseUrl, "/oauth2/token"))
            {
                Content = new FormUrlEncodedContent(
                [
                    new("grant_type", "refresh_token"),
                    new("client_id", Options.ClientId),
                    new("client_secret", encodedClientSecret),
                    new("refresh_token", refreshToken),
                ]),
            };

            var newTokenResponse = await HttpClient.SendAsync(newTokenRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                                                   .ConfigureAwait(false);
            var utcNow = TimeProvider.GetUtcNow();

            if (!newTokenResponse.IsSuccessStatusCode)
            {
#if NET6_0_OR_GREATER
                var errorContent = await newTokenResponse.Content.ReadAsStringAsync(cancellationToken)
                                                                 .ConfigureAwait(false);
#else
                var errorContent = await newTokenResponse.Content.ReadAsStringAsync()
                                                                 .ConfigureAwait(false);
#endif
                throw new iRacingLoginFailedException($"Attempt to refresh OAuth token failed with status code {newTokenResponse.StatusCode} and content: {errorContent}");
            }

            var token = await newTokenResponse.Content.ReadFromJsonAsync<OAuthTokenResponse>(cancellationToken)
                                                      .ConfigureAwait(false)
                        ?? throw new iRacingLoginFailedException("Failed to deserialize OAuth token response from iRacing API.");

            var expiresAt = utcNow.AddSeconds(token.ExpiresInSeconds);
            var refreshExpiresAt = token.RefreshTokenExpiresInSeconds != null ? utcNow.AddSeconds(token.RefreshTokenExpiresInSeconds.Value) : (DateTimeOffset?)null;

            activity?.SetStatus(System.Diagnostics.ActivityStatusCode.Ok, "Token refreshed successfully");

            return (token, expiresAt, refreshExpiresAt);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            activity?.SetStatus(System.Diagnostics.ActivityStatusCode.Error, "Exception thrown refreshing token");
            throw;
        }
    }

    protected abstract Task<RequestTokenResult> RequestTokenAsync(CancellationToken cancellationToken = default);
}
