// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

public class RaceWeeks
{

    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("race_week_num")]
    public int RaceWeekNum { get; set; }

    [JsonPropertyName("track")]
    public Track Track { get; set; } = default!;
}
