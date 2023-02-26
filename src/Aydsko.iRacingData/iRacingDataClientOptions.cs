// © 2023 Adrian Clark
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

    /// <summary>The source of the current date & time in UTC for the library.</summary>
    /// <remarks>Defaults to <see cref="DateTimeOffset.UtcNow"/>.</remarks>
    public Func<DateTimeOffset>? CurrentDateTimeSource { get; set; }
}
