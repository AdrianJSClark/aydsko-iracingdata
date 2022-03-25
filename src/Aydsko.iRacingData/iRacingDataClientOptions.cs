// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Net;

namespace Aydsko.iRacingData;

/// <summary>Configuration options for the iRacing Data Client.</summary>
public class iRacingDataClientOptions
{
    /// <summary>iRacing user name to use for authentication.</summary>
    public string? Username { get; set; }

    /// <summary>Password associated with the iRacing user name used to authenticate.</summary>
    public string? Password { get; set; }

    /// <summary>Called to retrieve cookie values stored from a previous authentication.</summary>
    public Func<CookieCollection>? RestoreCookies { get; set; }

    /// <summary>After a successful authentication called with the cookies to allow them to be saved.</summary>
    public Action<CookieCollection>? SaveCookies { get; set; }
}
