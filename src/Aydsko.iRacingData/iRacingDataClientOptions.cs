// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Net;

namespace Aydsko.iRacingData;

public class iRacingDataClientOptions
{
    public Func<CookieCollection>? RestoreCookies { get; set; }

    public Action<CookieCollection>? SaveCookies { get; set; }
}
