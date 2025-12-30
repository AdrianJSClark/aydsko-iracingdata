using Aydsko.iRacingData.Exceptions;

namespace Aydsko.iRacingData;

public delegate Task<OAuthTokenResponse> GetOAuthTokenResponse(CancellationToken cancellationToken = default);

public class OAuthCallbackAuthenticatingApiClient(HttpClient httpClient,
                                                  iRacingDataClientOptions options,
                                                  TimeProvider timeProvider)
    : OAuthAuthenticatingHttpClientBase(httpClient, options, timeProvider), IAuthenticatingHttpClient
{
    protected override async Task<RequestTokenResult> RequestTokenAsync(CancellationToken cancellationToken = default)
    {
        if (Options.OAuthTokenResponseCallback is null)
        {
            throw new InvalidOperationException("The OAuthTokenResponseCallback must be set in the options.");
        }
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Retrieve token via callback", System.Diagnostics.ActivityKind.Client);

        try
        {
            var token = await Options.OAuthTokenResponseCallback(cancellationToken)
                                     .ConfigureAwait(false)
                        ?? throw new iRacingLoginFailedException("The OAuthTokenResponseCallback returned null.");

            var utcNow = TimeProvider.GetUtcNow();

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
