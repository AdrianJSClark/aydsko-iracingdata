// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using System.Net.Http.Json;
using Aydsko.iRacingData.Exceptions;

namespace Aydsko.iRacingData;

public record OAuthTokenResponse([property: JsonPropertyName("access_token")] string AccessToken,
                                 [property: JsonPropertyName("token_type")] string TokenType,
                                 [property: JsonPropertyName("expires_in")] int ExpiresIn,
                                 [property: JsonPropertyName("refresh_token")] string RefreshToken,
                                 [property: JsonPropertyName("refresh_token_expires_in")] int RefreshTokenExpiresIn,
                                 [property: JsonPropertyName("scope")] string Scope)
{
    public DateTimeOffset AccessTokenExpiryInstantUtc { get; } = DateTimeOffset.UtcNow.AddSeconds(ExpiresIn);
    public DateTimeOffset RefreshTokenExpiryInstantUtc { get; } = DateTimeOffset.UtcNow.AddSeconds(ExpiresIn);
}

public class DomainDependentAuthorizationHeaderDelegatingHandler(string domain, HttpMessageHandler innerHandler)
    : DelegatingHandler(innerHandler)
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request?.RequestUri is not null && !request.RequestUri.Host.EndsWith(domain, StringComparison.OrdinalIgnoreCase))
        {
            request.Headers.Authorization = null;
        }
        return await base.SendAsync(request!, cancellationToken)
                         .ConfigureAwait(false);
    }
}

public class PasswordLimitedGrantApiClient(HttpClient httpClient,
                                           iRacingDataClientOptions options,
                                           ILogger<PasswordLimitedGrantApiClient> logger)
    : ApiClientBase(httpClient, options, logger)
{
    private readonly SemaphoreSlim loginSemaphore = new(1, 1);
    private OAuthTokenResponse? tokenResponse;

    protected internal override async Task EnsureLoggedInAsync(CancellationToken cancellationToken)
    {
        if (tokenResponse is null)
        {
            await loginSemaphore.WaitAsync(cancellationToken)
                                .ConfigureAwait(false);
            try
            {
#pragma warning disable CA1508 // Avoid dead conditional code - This is the double-check locking pattern to ensure thread safety.
                if (tokenResponse is null)
                {
                    await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
                }
#pragma warning restore CA1508 // Avoid dead conditional code
            }
            finally
            {
                loginSemaphore.Release();
            }
        }
        else
        {
            Logger.LogDebug("Already logged in with token: {AccessToken}", tokenResponse.AccessToken);
        }
    }
        private async Task LoginInternalAsync(CancellationToken cancellationToken)
    {
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Retrieve \"password_limited\" token");

        // TODO - Check if the token is still valid and refresh it if necessary.
        if (tokenResponse is not null)
        {
            activity?.SetStatus(System.Diagnostics.ActivityStatusCode.Ok, "Token already valid");
            // Already logged in
            return;
        }
        try
        {

            if (string.IsNullOrWhiteSpace(Options.Username)
                || string.IsNullOrWhiteSpace(Options.Password)
                || string.IsNullOrWhiteSpace(Options.ClientId)
                || string.IsNullOrWhiteSpace(Options.ClientSecret))
            {
                throw new InvalidOperationException("All of \"Username\", \"Password\", \"ClientId\", and \"ClientSecret\" must be set in the options.");
            }

            var encodedPassword = Options.PasswordIsEncoded ? Options.Password : EncodePassword(Options.Username!, Options.Password!);
            var encodedClientSecret = Options.ClientSecretIsEncoded ? Options.ClientSecret : EncodePassword(Options.ClientId!, Options.ClientSecret!);

            using var tokenRequestContent = new FormUrlEncodedContent(
            [
                new("grant_type", "password_limited"),
                new("client_id", Options.ClientId),
                new("client_secret", encodedClientSecret),
                new("username", Options.Username),
                new("password", encodedPassword),
                new("scope", "iracing.auth iracing.profile"),
            ]);

            var result = await HttpClient.PostAsync(new Uri("https://oauth.iracing.com/oauth2/token"),
                                                    tokenRequestContent,
                                                    cancellationToken)
                                         .ConfigureAwait(false);

            if (!result.IsSuccessStatusCode)
            {
#if NET6_0_OR_GREATER
            var errorContent = await result.Content.ReadAsStringAsync(cancellationToken)
                                                   .ConfigureAwait(false);
#else
                var errorContent = await result.Content.ReadAsStringAsync()
                                                       .ConfigureAwait(false);
#endif
                throw new iRacingLoginFailedException($"Attempt to retrieve \"password_limited\" OAuth token failed with status code {result.StatusCode} and content: {errorContent}");
            }

            tokenResponse = await result.Content.ReadFromJsonAsync<OAuthTokenResponse>(cancellationToken)
                                                .ConfigureAwait(false)
                            ?? throw new iRacingLoginFailedException("Failed to deserialize OAuth token response from iRacing API.");

            HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(tokenResponse.TokenType, tokenResponse.AccessToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }
    }

    protected internal override void ClearLoggedInState()
    {
        HttpClient.DefaultRequestHeaders.Authorization = null;
        tokenResponse = null;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            loginSemaphore.Dispose();
            tokenResponse = null;
        }
        base.Dispose(disposing);
    }
}
