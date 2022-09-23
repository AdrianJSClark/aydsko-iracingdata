// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

public class RaceGuideSession
{
    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; set; }

    [JsonPropertyName("super_session")]
    public bool IsSuperSession { get; set; }

    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }

    [JsonPropertyName("race_week_num")]
    public int RaceWeekNumber { get; set; }

    [JsonPropertyName("end_time")]
    public DateTime EndTime { get; set; }

    [JsonPropertyName("session_id")]
    public int SessionId { get; set; }

    [JsonPropertyName("entry_count")]
    public int EntryCount { get; set; }
}
