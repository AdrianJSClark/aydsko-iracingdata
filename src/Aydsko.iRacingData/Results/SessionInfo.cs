// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Results;

public class SessionInfo
{
    [JsonPropertyName("subsession_id")]
    public int SubsessionId { get; set; }
    [JsonPropertyName("session_id")]
    public int SessionId { get; set; }
    [JsonPropertyName("simsession_number")]
    public int SimsessionNumber { get; set; }
    [JsonPropertyName("simsession_type")]
    public int SimsessionType { get; set; }
    [JsonPropertyName("num_laps_for_qual_average")]
    public int NumLapsForQualAverage { get; set; }
    [JsonPropertyName("num_laps_for_solo_average")]
    public int NumLapsForSoloAverage { get; set; }
    [JsonPropertyName("event_type")]
    public EventType EventType { get; set; }
    [JsonPropertyName("event_type_name")]
    public string EventTypeName { get; set; } = null!;
    [JsonPropertyName("private_session_id")]
    public int PrivateSessionId { get; set; }
    [JsonPropertyName("season_name")]
    public string SeasonName { get; set; } = null!;
    [JsonPropertyName("season_short_name")]
    public string SeasonShortName { get; set; } = null!;
    [JsonPropertyName("series_name")]
    public string SeriesName { get; set; } = null!;
    [JsonPropertyName("series_short_name")]
    public string SeriesShortName { get; set; } = null!;
    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; set; }
    [JsonPropertyName("track")]
    public Track Track { get; set; } = null!;
}
