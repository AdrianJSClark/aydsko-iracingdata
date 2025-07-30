// © 2025 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class HeatSessionInformation
{
    [JsonPropertyName("consolation_delta_max_field_size")]
    public int ConsolationDeltaMaxFieldSize { get; set; }

    [JsonPropertyName("consolation_delta_session_laps")]
    public int ConsolationDeltaSessionLaps { get; set; }

    [JsonPropertyName("consolation_delta_session_length_minutes")]
    public int ConsolationDeltaSessionLengthMinutes { get; set; }

    [JsonPropertyName("consolation_first_max_field_size")]
    public int ConsolationFirstMaxFieldSize { get; set; }

    [JsonPropertyName("consolation_first_session_laps")]
    public int ConsolationFirstSessionLaps { get; set; }

    [JsonPropertyName("consolation_first_session_length_minutes")]
    public int ConsolationFirstSessionLengthMinutes { get; set; }

    [JsonPropertyName("consolation_num_position_to_invert")]
    public int ConsolationNumPositionToInvert { get; set; }

    [JsonPropertyName("consolation_num_to_consolation")]
    public int ConsolationNumToConsolation { get; set; }

    [JsonPropertyName("consolation_num_to_main")]
    public int ConsolationNumToMain { get; set; }

    [JsonPropertyName("consolation_run_always")]
    public bool ConsolationRunAlways { get; set; }

    [JsonPropertyName("consolation_scores_champ_points")]
    public bool ConsolationScoresChampPoints { get; set; }

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    [JsonPropertyName("heat_caution_type")]
    public int HeatCautionType { get; set; }

    [JsonPropertyName("heat_info_id")]
    public int HeatInfoId { get; set; }

    [JsonPropertyName("heat_info_name")]
    public string HeatInfoName { get; set; } = default!;

    [JsonPropertyName("heat_laps")]
    public int HeatLaps { get; set; }

    [JsonPropertyName("heat_length_minutes")]
    public int HeatLengthMinutes { get; set; }

    [JsonPropertyName("heat_max_field_size")]
    public int HeatMaxFieldSize { get; set; }

    [JsonPropertyName("heat_num_from_each_to_main")]
    public int HeatNumFromEachToMain { get; set; }

    [JsonPropertyName("heat_num_position_to_invert")]
    public int HeatNumPositionToInvert { get; set; }

    [JsonPropertyName("heat_scores_champ_points")]
    public bool HeatScoresChampPoints { get; set; }

    [JsonPropertyName("heat_session_minutes_estimate")]
    public int HeatSessionMinutesEstimate { get; set; }

    [JsonPropertyName("hidden")]
    public bool Hidden { get; set; }

    [JsonPropertyName("main_laps")]
    public int MainLaps { get; set; }

    [JsonPropertyName("main_length_minutes")]
    public int MainLengthMinutes { get; set; }

    [JsonPropertyName("main_max_field_size")]
    public int MainMaxFieldSize { get; set; }

    [JsonPropertyName("main_num_position_to_invert")]
    public int MainNumPositionToInvert { get; set; }

    [JsonPropertyName("max_entrants")]
    public int MaxEntrants { get; set; }

    [JsonPropertyName("open_practice")]
    public bool OpenPractice { get; set; }

    [JsonPropertyName("pre_main_practice_length_minutes")]
    public int PreMainPracticeLengthMinutes { get; set; }

    [JsonPropertyName("pre_qual_num_to_main")]
    public int PreQualifyingNumberToMain { get; set; }

    [JsonPropertyName("pre_qual_practice_length_minutes")]
    public int PreQualifyingPracticeLengthMinutes { get; set; }

    [JsonPropertyName("qual_caution_type")]
    public int QualifyingCautionType { get; set; }

    [JsonPropertyName("qual_laps")]
    public int QualifyingLaps { get; set; }

    [JsonPropertyName("qual_length_minutes")]
    public int QualifyingLengthMinutes { get; set; }

    [JsonPropertyName("qual_num_to_main")]
    public int QualifyingNumToMain { get; set; }

    [JsonPropertyName("qual_open_delay_seconds")]
    public int QualifyingOpenDelaySeconds { get; set; }

    [JsonPropertyName("qual_scores_champ_points")]
    public bool QualifyingScoresChampPoints { get; set; }

    [JsonPropertyName("qual_scoring")]
    public int QualifyingScoring { get; set; }

    [JsonPropertyName("qual_style")]
    public int QualifyingStyle { get; set; }

    [JsonPropertyName("race_style")]
    public int RaceStyle { get; set; }
}
