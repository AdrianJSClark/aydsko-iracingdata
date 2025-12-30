namespace Aydsko.iRacingData;

public record class RequestTokenResult(OAuthTokenResponse Token, DateTimeOffset ExpiresAt, DateTimeOffset? RefreshTokenExpiresAt)
{
    public static implicit operator (OAuthTokenResponse Token, DateTimeOffset ExpiresAt, DateTimeOffset? RefreshTokenExpiresAt)(RequestTokenResult value)
    {
        return value is null ? new() : (value.Token, value.ExpiresAt, value.RefreshTokenExpiresAt);
    }

    public static implicit operator RequestTokenResult((OAuthTokenResponse Token, DateTimeOffset ExpiresAt, DateTimeOffset? RefreshTokenExpiresAt) value)
    {
        return new RequestTokenResult(value.Token, value.ExpiresAt, value.RefreshTokenExpiresAt);
    }

    public RequestTokenResult ToRequestTokenResult()
    {
        return new(Token, ExpiresAt, RefreshTokenExpiresAt);
    }

    public (OAuthTokenResponse Token, DateTimeOffset ExpiresAt, DateTimeOffset? RefreshTokenExpiresAt) ToValueTuple()
    {
        return (Token, ExpiresAt, RefreshTokenExpiresAt);
    }
}
