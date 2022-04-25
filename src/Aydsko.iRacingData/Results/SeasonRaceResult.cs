// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Results;

public class SeasonRaceResult
{
    [JsonPropertyName("race_week_num")]
    public int RaceWeekNum { get; set; }

    [JsonPropertyName("event_type")]
    public EventType EventType { get; set; }

    [JsonPropertyName("event_type_name")]
    public string? EventTypeName { get; set; }

    [JsonPropertyName("start_time")]
    public DateTimeOffset StartTime { get; set; }

    [JsonPropertyName("session_id")]
    public int SessionId { get; set; }

    [JsonPropertyName("subsession_id")]
    public int SubsessionId { get; set; }

    [JsonPropertyName("official_session")]
    public bool OfficialSession { get; set; }

    [JsonPropertyName("event_strength_of_field")]
    public int EventStrengthOfField { get; set; }

    [JsonPropertyName("event_best_lap_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? EventBestLapTime { get; set; }

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
