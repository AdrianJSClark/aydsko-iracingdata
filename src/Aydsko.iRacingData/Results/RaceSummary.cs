// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

public class RaceSummary
{
    [JsonPropertyName("subsession_id")]
    public int SubsessionId { get; set; }

    [JsonPropertyName("average_lap")]
    public int AverageLap { get; set; }

    [JsonIgnore]
    public TimeSpan AverageLapTime => TimeSpan.FromSeconds(AverageLap / 10000D);

    [JsonPropertyName("laps_complete")]
    public int LapsComplete { get; set; }

    [JsonPropertyName("num_cautions")]
    public int NumberOfCautions { get; set; }

    [JsonPropertyName("num_caution_laps")]
    public int NumberOfCautionLaps { get; set; }

    [JsonPropertyName("num_lead_changes")]
    public int NumberOfLeadChanges { get; set; }

    [JsonPropertyName("field_strength")]
    public int FieldStrength { get; set; }

    [JsonPropertyName("num_opt_laps")]
    public int NumberOfOptLaps { get; set; }

    [JsonPropertyName("has_opt_path")]
    public bool HasOptPath { get; set; }

    [JsonPropertyName("special_event_type")]
    public int SpecialEventType { get; set; }

    [JsonPropertyName("special_event_type_text")]
    public string SpecialEventTypeText { get; set; } = default!;
}
