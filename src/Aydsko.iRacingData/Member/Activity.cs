// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

public class Activity
{
    [JsonPropertyName("recent_30days_count")]
    public int Recent30DaysCount { get; set; }

    [JsonPropertyName("prev_30days_count")]
    public int Previous30DaysCount { get; set; }

    [JsonPropertyName("consecutive_weeks")]
    public int ConsecutiveWeeks { get; set; }

    [JsonPropertyName("most_consecutive_weeks")]
    public int MostConsecutiveWeeks { get; set; }
}
