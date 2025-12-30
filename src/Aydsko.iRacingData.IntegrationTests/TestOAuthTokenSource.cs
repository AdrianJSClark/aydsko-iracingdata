// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests;

internal class TestOAuthTokenSource(HttpClient httpClient,
                                    iRacingDataClientOptions options,
                                    TimeProvider timeProvider)
    : PasswordLimitedOAuthAuthenticatingHttpClient(httpClient, options, timeProvider), IOAuthTokenSource
{
    private RequestTokenResult? savedTokenValue;

    public async Task<OAuthTokenValue> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        savedTokenValue ??= await base.RequestTokenAsync(cancellationToken).ConfigureAwait(false);
        return new(savedTokenValue.Token.AccessToken, savedTokenValue.ExpiresAt);
    }

    protected override async Task<RequestTokenResult> RequestTokenAsync(CancellationToken cancellationToken = default)
    {
        savedTokenValue ??= await base.RequestTokenAsync(cancellationToken).ConfigureAwait(false);
        return new(savedTokenValue.Token, savedTokenValue.ExpiresAt, savedTokenValue.RefreshTokenExpiresAt);
    }
}
