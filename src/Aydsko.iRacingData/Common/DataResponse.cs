// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Common;

public class DataResponse<TData>
{
    /// <summary>The current total rate limit.</summary>
    public int? TotalRateLimit { get; set; }
    /// <summary>Amount of rate limit remaining.</summary>
    public int? RateLimitRemaining { get; set; }
    /// <summary>Instant at which the rate limit will be reset.</summary>
    public DateTimeOffset? RateLimitReset { get; set; }
    /// <summary>Data returned from the API call.</summary>
    public TData Data { get; set; } = default!;
}
