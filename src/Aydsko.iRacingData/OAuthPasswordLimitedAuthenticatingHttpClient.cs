using System.Net.Http.Headers;
using System.Net.Http.Json;
using Aydsko.iRacingData.Exceptions;

namespace Aydsko.iRacingData;

public class OAuthPasswordLimitedAuthenticatingHttpClient(HttpClient httpClient,
                                                          iRacingDataClientOptions options)
    : IDisposable, IAuthenticatingHttpClient
{
    private readonly SemaphoreSlim loginSemaphore = new(1, 1);
    private OAuthTokenResponse? tokenResponse;
    private bool disposedValue;

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

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                     HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
                                                     CancellationToken cancellationToken = default)
    {
        return await httpClient.SendAsync(request, completionOption, cancellationToken)
                               .ConfigureAwait(false);
    }

    public void ClearLoggedInState()
    {
        tokenResponse = null;
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
                    tokenResponse = await RequestTokenAsync(cancellationToken).ConfigureAwait(false);
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

    private async Task<OAuthTokenResponse> RequestTokenAsync(CancellationToken cancellationToken = default)
    {
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Retrieve \"password_limited\" token", System.Diagnostics.ActivityKind.Client);

        try
        {
            if (string.IsNullOrWhiteSpace(options.Username)
                || string.IsNullOrWhiteSpace(options.Password)
                || string.IsNullOrWhiteSpace(options.ClientId)
                || string.IsNullOrWhiteSpace(options.ClientSecret))
            {
                throw new InvalidOperationException("All of \"Username\", \"Password\", \"ClientId\", and \"ClientSecret\" must be set in the options.");
            }

            if (string.IsNullOrWhiteSpace(options.ApiBaseUrl)
                || !Uri.TryCreate(options.AuthServiceBaseUrl, UriKind.Absolute, out var authServiceBaseUrl))
            {
                throw new InvalidOperationException("The \"AuthServiceBaseUrl\" must be a valid absolute URL in the iRacing Data Client options.");
            }

            var encodedPassword = options.PasswordIsEncoded ? options.Password : ApiClient.EncodePassword(options.Username!, options.Password!);
            var encodedClientSecret = options.ClientSecretIsEncoded ? options.ClientSecret : ApiClient.EncodePassword(options.ClientId!, options.ClientSecret!);

            using var newTokenRequest = new HttpRequestMessage(HttpMethod.Post, new Uri(authServiceBaseUrl, "/oauth2/token"))
            {
                Content = new FormUrlEncodedContent(
                [
                    new("grant_type", "password_limited"),
                    new("client_id", options.ClientId),
                    new("client_secret", encodedClientSecret),
                    new("username", options.Username),
                    new("password", encodedPassword),
                    new("scope", "iracing.auth iracing.profile"),
                ]),
            };

            var newTokenResponse = await httpClient.SendAsync(newTokenRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                                                   .ConfigureAwait(false);

            if (!newTokenResponse.IsSuccessStatusCode)
            {
#if NET6_0_OR_GREATER
                var errorContent = await newTokenResponse.Content.ReadAsStringAsync(cancellationToken)
                                                                 .ConfigureAwait(false);
#else
                var errorContent = await newTokenResponse.Content.ReadAsStringAsync()
                                                                 .ConfigureAwait(false);
#endif
                throw new iRacingLoginFailedException($"Attempt to retrieve \"password_limited\" OAuth token failed with status code {newTokenResponse.StatusCode} and content: {errorContent}");
            }

            var token = await newTokenResponse.Content.ReadFromJsonAsync<OAuthTokenResponse>(cancellationToken)
                                                      .ConfigureAwait(false)
                        ?? throw new iRacingLoginFailedException("Failed to deserialize OAuth token response from iRacing API.");

            activity?.SetStatus(System.Diagnostics.ActivityStatusCode.Ok, "Token retrieved successfully");

            return token;
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            activity?.SetStatus(System.Diagnostics.ActivityStatusCode.Error, "Exception thrown retrieving token");
            throw;
        }
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
    // ~OAuthPasswordLimitedAuthenticatingHttpClient()
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
