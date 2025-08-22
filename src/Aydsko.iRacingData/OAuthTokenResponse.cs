// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

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
