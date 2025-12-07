using System.ComponentModel.DataAnnotations;

namespace Aydsko.iRacingData;

#if NET6_0_OR_GREATER

public class iRacingOptions
{
    /// <summary>iRacing user name to use for authentication.</summary>
    [Required]
    public required string Username { get; set; }

    /// <summary>Password associated with the iRacing user name used to authenticate.</summary>
    [Required]
    public required string Password { get; set; }

    /// <summary>The <c>client_id</c> value for <c>password_limited</c> OAuth flow.</summary>
    /// <seealso href="https://oauth.iracing.com/oauth2/book/token_endpoint.html#password-limited-grant" />
    [Required]
    public required string ClientId { get; set; }

    /// <summary>The <c>client_secret</c> value for <c>password_limited</c> OAuth flow.</summary>
    /// <seealso href="https://oauth.iracing.com/oauth2/book/token_endpoint.html#password-limited-grant" />
    [Required]
    public required string ClientSecret { get; set; }
}

#else

public class iRacingOptions
{
    /// <summary>iRacing user name to use for authentication.</summary>
    [Required]
    public string Username { get; set; } = default!;

    /// <summary>Password associated with the iRacing user name used to authenticate.</summary>
    [Required]
    public string Password { get; set; } = default!;

    /// <summary>The <c>client_id</c> value for <c>password_limited</c> OAuth flow.</summary>
    /// <seealso href="https://oauth.iracing.com/oauth2/book/token_endpoint.html#password-limited-grant" />
    [Required]
    public string ClientId { get; set; } = default!;

    /// <summary>The <c>client_secret</c> value for <c>password_limited</c> OAuth flow.</summary>
    /// <seealso href="https://oauth.iracing.com/oauth2/book/token_endpoint.html#password-limited-grant" />
    [Required]
    public string ClientSecret { get; set; } = default!;
}

#endif
