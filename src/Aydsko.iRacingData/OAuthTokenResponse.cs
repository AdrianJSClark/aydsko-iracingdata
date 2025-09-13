// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData;

/// <summary></summary>
/// <param name="AccessToken">Used to authorize a connection to the server.</param>
/// <param name="TokenType">This will always be the string <c>Bearer</c>.</param>
/// <param name="ExpiresInSeconds">Number of seconds after which this token will no longer be considered valid.</param>
/// <param name="RefreshToken">Value to use as the token for a Refresh Token Grant to obtain new access and refresh tokens. Will be <see langword="null"/> if the server has not issued a refresh token.</param>
/// <param name="RefreshTokenExpiresInSeconds">Number of seconds after which this token will no longer be considered valid. Will be <see langword="null"/> if the server has not issued a refresh token.</param>
/// <param name="Scope">One or more scopes, separated by white space, granted to the access token. If none are granted this field will be <see langword="null"/>.</param>
/// <seealso href="https://oauth.iracing.com/oauth2/book/token_endpoint.html" />
public record OAuthTokenResponse([property: JsonPropertyName("access_token")] string AccessToken,
                                 [property: JsonPropertyName("token_type")] string TokenType,
                                 [property: JsonPropertyName("expires_in")] int ExpiresInSeconds,
                                 [property: JsonPropertyName("refresh_token")] string? RefreshToken,
                                 [property: JsonPropertyName("refresh_token_expires_in")] int? RefreshTokenExpiresInSeconds,
                                 [property: JsonPropertyName("scope")] string? Scope)
{
}
