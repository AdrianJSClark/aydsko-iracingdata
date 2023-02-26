// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Results;

public class SeasonRaceResult
{
    /// <summary>An index number identifying the race week.</summary>
    /// <remarks>The iRacing Data API works with zero-based race weeks, most people will use one-based.</remarks>
    /// <seealso cref="RaceWeekNumber" />
    [JsonPropertyName("race_week_num")]
    public int RaceWeekIndex { get; set; }

    /// <summary>The number of the race week within the season.</summary>
    [JsonIgnore]
    public int RaceWeekNumber => RaceWeekIndex + 1;

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
    public int NumberOfCautions { get; set; }

    [JsonPropertyName("num_caution_laps")]
    public int NumberOfCautionLaps { get; set; }

    [JsonPropertyName("num_lead_changes")]
    public int NumberOfLeadChanges { get; set; }

    [JsonPropertyName("num_drivers")]
    public int NumberOfDrivers { get; set; }

    [JsonPropertyName("track")]
    public ResultTrackInfo Track { get; set; } = null!;
}
