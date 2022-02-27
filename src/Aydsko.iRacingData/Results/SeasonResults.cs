using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Results;

public class SeasonResults
{
    [JsonPropertyName("results_list")]
    public SeasonRaceResult[] ResultsList { get; set; } = Array.Empty<SeasonRaceResult>();
    [JsonPropertyName("event_type")]
    public int EventType { get; set; }
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }
    [JsonPropertyName("race_week_num")]
    public int RaceWeekNumber { get; set; }
}

public class SeasonRaceResult
{
    [JsonPropertyName("race_week_num")]
    public int RaceWeekNum { get; set; }
    [JsonPropertyName("event_type")]
    public int EventType { get; set; }
    [JsonPropertyName("event_type_name")]
    public string? EventTypeName { get; set; }
    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; set; }
    [JsonPropertyName("session_id")]
    public int SessionId { get; set; }
    [JsonPropertyName("subsession_id")]
    public int SubsessionId { get; set; }
    [JsonPropertyName("official_session")]
    public bool OfficialSession { get; set; }
    [JsonPropertyName("event_strength_of_field")]
    public int EventStrengthOfField { get; set; }
    [JsonPropertyName("event_best_lap_time")]
    public int EventBestLapTime { get; set; }
    [JsonPropertyName("num_cautions")]
    public int NumCautions { get; set; }
    [JsonPropertyName("num_caution_laps")]
    public int NumCautionLaps { get; set; }
    [JsonPropertyName("num_lead_changes")]
    public int NumLeadChanges { get; set; }
    [JsonPropertyName("num_drivers")]
    public int NumDrivers { get; set; }
    [JsonPropertyName("track")]
    public SeasonResultTrack Track { get; set; } = null!;
}

public class SeasonResultTrack
{
    [JsonPropertyName("track_id")]
    public int TrackId { get; set; }
    [JsonPropertyName("track_name")]
    public string? TrackName { get; set; }
    [JsonPropertyName("config_name")]
    public string? ConfigName { get; set; }
}

[JsonSerializable(typeof(SeasonResults)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SeasonResultsContext : JsonSerializerContext
{ }
