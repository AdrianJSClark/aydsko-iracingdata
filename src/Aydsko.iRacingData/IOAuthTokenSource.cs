namespace Aydsko.iRacingData;

/// <summary>An implementation should be available to request tokens from the iRacing authentication service.</summary>
/// <remarks>The implementation should handle securely storing tokens while they are valid to reduce the requirement to call the service or prompt the user.</remarks>
/// <seealso href="https://oauth.iracing.com/oauth2/book/token_endpoint.html" />
public interface IOAuthTokenSource
{
    /// <summary>Retrieve the token from the authentication service.</summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The token and the time it expires in an <see cref="OAuthTokenValue"/> object.</returns>
    Task<OAuthTokenValue> GetTokenAsync(CancellationToken cancellationToken = default);
}

/// <summary>An authentication token value and the instant it is due to expire.</summary>
/// <param name="AccessToken">An iRacing OAuth Access Token suitable for calling the API.</param>
/// <param name="ExpiresAt">The instant the token is due to expire.</param>
public record OAuthTokenValue(string AccessToken, DateTimeOffset ExpiresAt);
