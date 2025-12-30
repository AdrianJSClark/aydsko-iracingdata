using Aydsko.iRacingData.Exceptions;

namespace Aydsko.iRacingData;

public class OAuthTokenSourceApiClient(HttpClient httpClient,
                                          iRacingDataClientOptions options,
                                          TimeProvider timeProvider,
                                          IOAuthTokenSource tokenSource)
    : OAuthAuthenticatingHttpClientBase(httpClient, options, timeProvider), IAuthenticatingHttpClient
{
    protected override async Task<RequestTokenResult> RequestTokenAsync(CancellationToken cancellationToken = default)
    {
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Retrieve token via IOAuthTokenSource", System.Diagnostics.ActivityKind.Client);
        try
        {
            var (token, expiry) = (await tokenSource.GetTokenAsync(cancellationToken).ConfigureAwait(false))
                                  ?? throw new iRacingLoginFailedException("The IOAuthTokenSource returned null.");
            return (new OAuthTokenResponse(token, "Bearer", 0, null, null, null), expiry, null);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            activity?.SetStatus(System.Diagnostics.ActivityStatusCode.Error, "Exception thrown retrieving token");
            throw;
        }
    }
}
