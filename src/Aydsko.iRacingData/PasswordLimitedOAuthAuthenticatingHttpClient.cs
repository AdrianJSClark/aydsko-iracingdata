using System.Net.Http.Json;
using Aydsko.iRacingData.Exceptions;

namespace Aydsko.iRacingData;

public class PasswordLimitedOAuthAuthenticatingHttpClient(HttpClient httpClient,
                                                          iRacingDataClientOptions options,
                                                          TimeProvider timeProvider)
    : OAuthAuthenticatingHttpClientBase(httpClient, options, timeProvider), IAuthenticatingHttpClient
{
    protected override async Task<RequestTokenResult> RequestTokenAsync(CancellationToken cancellationToken = default)
    {
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Retrieve \"password_limited\" token", System.Diagnostics.ActivityKind.Client);

        try
        {
            if (string.IsNullOrWhiteSpace(Options.Username)
                || string.IsNullOrWhiteSpace(Options.Password)
                || string.IsNullOrWhiteSpace(Options.ClientId)
                || string.IsNullOrWhiteSpace(Options.ClientSecret))
            {
                throw new InvalidOperationException("All of \"Username\", \"Password\", \"ClientId\", and \"ClientSecret\" must be set in the options.");
            }

            if (string.IsNullOrWhiteSpace(Options.AuthServiceBaseUrl)
                || !Uri.TryCreate(Options.AuthServiceBaseUrl, UriKind.Absolute, out var authServiceBaseUrl))
            {
                throw new InvalidOperationException("The \"AuthServiceBaseUrl\" must be a valid absolute URL in the iRacing Data Client options.");
            }

            var encodedPassword = Options.PasswordIsEncoded ? Options.Password : ApiClient.EncodePassword(Options.Username!, Options.Password!);
            var encodedClientSecret = Options.ClientSecretIsEncoded ? Options.ClientSecret : ApiClient.EncodePassword(Options.ClientId!, Options.ClientSecret!);

            using var newTokenRequest = new HttpRequestMessage(HttpMethod.Post,
                                                               new Uri(authServiceBaseUrl, "/oauth2/token"))
            {
                Content = new FormUrlEncodedContent(
                [
                    new("grant_type", "password_limited"),
                    new("client_id", Options.ClientId),
                    new("client_secret", encodedClientSecret),
                    new("username", Options.Username),
                    new("password", encodedPassword),
                    new("scope", "iracing.auth iracing.profile"),
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
                throw new iRacingLoginFailedException($"Attempt to retrieve \"password_limited\" OAuth token failed with status code {newTokenResponse.StatusCode} and content: {errorContent}");
            }

            var token = await newTokenResponse.Content.ReadFromJsonAsync<OAuthTokenResponse>(cancellationToken)
                                                      .ConfigureAwait(false)
                        ?? throw new iRacingLoginFailedException("Failed to deserialize OAuth token response from iRacing API.");

            var expiresAt = utcNow.AddSeconds(token.ExpiresInSeconds);
            var refreshExpiresAt = token.RefreshTokenExpiresInSeconds != null ? utcNow.AddSeconds(token.RefreshTokenExpiresInSeconds.Value) : (DateTimeOffset?)null;

            activity?.SetStatus(System.Diagnostics.ActivityStatusCode.Ok, "Token retrieved successfully");

            return (token, expiresAt, refreshExpiresAt);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            activity?.SetStatus(System.Diagnostics.ActivityStatusCode.Error, "Exception thrown retrieving token");
            throw;
        }
    }
}
