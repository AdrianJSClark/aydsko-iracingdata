// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using System.Net;

namespace Aydsko.iRacingData;

/// <summary>Configuration options for the iRacing Data Client.</summary>
public class iRacingDataClientOptions
{
    /// <summary>Name of the application or product using the Data Client library to be included in the HTTP <c>User-Agent</c> header.</summary>
    public string? UserAgentProductName { get; set; }

    /// <summary>Version of the application or product using the Data Client library to be included in the HTTP <c>User-Agent</c> header.</summary>
    public Version? UserAgentProductVersion { get; set; }

    /// <summary>iRacing user name to use for authentication.</summary>
    public string? Username { get; set; }

    /// <summary>Password associated with the iRacing user name used to authenticate.</summary>
    public string? Password { get; set; }

    /// <summary>If <see langword="true" /> indicates the <see cref="Password"/> property value is already encoded ready for submission to the iRacing API.</summary>
    /// <seealso href="https://forums.iracing.com/discussion/22109/login-form-changes/p1" />
    public bool PasswordIsEncoded { get; set; }

    /// <summary>Called to retrieve cookie values stored from a previous authentication.</summary>
    public Func<CookieCollection>? RestoreCookies { get; set; }

    /// <summary>After a successful authentication called with the cookies to allow them to be saved.</summary>
    /// <remarks>
    /// <para>One of the cookies returned in this collection <c>irsso_membersv2</c> may be used to authenticate with the <c>/membersite</c> and <c>/memberstats</c> endpoints on the classic site's API.</para>
    /// </remarks>
    public Action<CookieCollection>? SaveCookies { get; set; }

    /// <summary>The source of the current date and time in UTC for the library.</summary>
    /// <remarks>Defaults to <see cref="TimeProvider.GetUtcNow"/>.</remarks>
    [Obsolete("Add your own TimeProvider to the service collection (see https://learn.microsoft.com/en-us/dotnet/api/system.timeprovider).")]
    public Func<DateTimeOffset>? CurrentDateTimeSource { get; set; }

    /// <summary>The <c>client_id</c> value for <c>password_limited</c> OAuth flow.</summary>
    /// <seealso href="https://oauth.iracing.com/oauth2/book/token_endpoint.html#password-limited-grant" />
    public string? ClientId { get; set; }

    /// <summary>The <c>client_secret</c> value for <c>password_limited</c> OAuth flow.</summary>
    /// <seealso href="https://oauth.iracing.com/oauth2/book/token_endpoint.html#password-limited-grant" />
    public string? ClientSecret { get; set; }

    /// <summary>If <see langword="true" /> indicates the <see cref="ClientSecret"/> property value is already encoded ready for submission to the iRacing API.</summary>
    /// <seealso href="https://oauth.iracing.com/oauth2/book/token_endpoint.html#client-secret-and-user-password-masking" />
    public bool ClientSecretIsEncoded { get; set; }

    /// <summary>The base URL for the iRacing "/data" API. All calls will be made relative to this.</summary>
    /// <remarks>
    /// <para>This should not be changed unless you are sure you need an alternate endpoint.</para>
    /// <para>This will default to <c>https://members-ng.iracing.com</c>.</para>
    /// </remarks>
    public string ApiBaseUrl { get; set; } = "https://members-ng.iracing.com";

    /// <summary>The base URL for the iRacing Auth Service. All OAuth calls will be made relative to this.</summary>
    /// <remarks>
    /// <para>This should not be changed unless you are sure you need an alternate endpoint.</para>
    /// <para>This will default to <c>https://oauth.iracing.com</c>.</para>
    /// </remarks>
    public string AuthServiceBaseUrl { get; set; } = "https://oauth.iracing.com";

    /// <summary>Callback to allow the library to request an iRacing OAuth token.</summary>
    /// <remarks>Intended to be used with the &quot;Authorization Code Grant&quot; process.</remarks>
    /// <seealso href="https://oauth.iracing.com/oauth2/book/auth_overview.html"/>
    public GetOAuthTokenResponse? OAuthTokenResponseCallback { get; set; }
}
