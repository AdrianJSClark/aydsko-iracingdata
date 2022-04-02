// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Results;

public class SubSessionResult
{
    public const string LogoPathBase = "https://images-static.iracing.com/img/logos/series/";

    [JsonPropertyName("subsession_id")]
    public int SubSessionId { get; set; }
    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }
    [JsonPropertyName("season_name")]
    public string SeasonName { get; set; } = default!;
    [JsonPropertyName("season_short_name")]
    public string SeasonShortName { get; set; } = default!;
    [JsonPropertyName("season_year")]
    public int SeasonYear { get; set; }
    [JsonPropertyName("season_quarter")]
    public int SeasonQuarter { get; set; }
    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }
    [JsonPropertyName("series_name")]
    public string SeriesName { get; set; } = default!;
    [JsonPropertyName("series_short_name")]
    public string SeriesShortName { get; set; } = default!;
    [JsonPropertyName("series_logo")]
    public string SeriesLogo { get; set; } = default!;
    [JsonPropertyName("race_week_num")]
    public int RaceWeekNum { get; set; }
    [JsonPropertyName("session_id")]
    public int SessionId { get; set; }
    [JsonPropertyName("license_category_id")]
    public int LicenseCategoryId { get; set; }
    [JsonPropertyName("license_category")]
    public string LicenseCategory { get; set; } = default!;
    [JsonPropertyName("private_session_id")]
    public int PrivateSessionId { get; set; }
    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; set; }
    [JsonPropertyName("end_time")]
    public DateTime EndTime { get; set; }
    [JsonPropertyName("num_laps_for_qual_average")]
    public int NumLapsForQualAverage { get; set; }
    [JsonPropertyName("num_laps_for_solo_average")]
    public int NumLapsForSoloAverage { get; set; }
    [JsonPropertyName("corners_per_lap")]
    public int CornersPerLap { get; set; }
    [JsonPropertyName("caution_type")]
    public int CautionType { get; set; }
    [JsonPropertyName("event_type")]
    public EventType EventType { get; set; }
    [JsonPropertyName("event_type_name")]
    public string EventTypeName { get; set; } = default!;
    [JsonPropertyName("driver_changes")]
    public bool DriverChanges { get; set; }
    [JsonPropertyName("min_team_drivers")]
    public int MinTeamDrivers { get; set; }
    [JsonPropertyName("max_team_drivers")]
    public int MaxTeamDrivers { get; set; }
    [JsonPropertyName("driver_change_rule")]
    public int DriverChangeRule { get; set; }
    [JsonPropertyName("driver_change_param1")]
    public int DriverChangeParam1 { get; set; }
    [JsonPropertyName("driver_change_param2")]
    public int DriverChangeParam2 { get; set; }
    [JsonPropertyName("max_weeks")]
    public int MaxWeeks { get; set; }
    [JsonPropertyName("points_type")]
    public string PointsType { get; set; } = default!;
    [JsonPropertyName("event_strength_of_field")]
    public int EventStrengthOfField { get; set; }
    [JsonPropertyName("event_average_lap")]
    public int EventAverageLap { get; set; }
    [JsonPropertyName("event_laps_complete")]
    public int EventLapsComplete { get; set; }
    [JsonPropertyName("num_cautions")]
    public int NumCautions { get; set; }
    [JsonPropertyName("num_caution_laps")]
    public int NumCautionLaps { get; set; }
    [JsonPropertyName("num_lead_changes")]
    public int NumLeadChanges { get; set; }
    [JsonPropertyName("official_session")]
    public bool OfficialSession { get; set; }
    [JsonPropertyName("heat_info_id")]
    public int HeatInfoId { get; set; }
    [JsonPropertyName("special_event_type")]
    public int SpecialEventType { get; set; }
    [JsonPropertyName("damage_model")]
    public int DamageModel { get; set; }
    [JsonPropertyName("can_protest")]
    public bool CanProtest { get; set; }
    [JsonPropertyName("cooldown_minutes")]
    public int CooldownMinutes { get; set; }
    [JsonPropertyName("limit_minutes")]
    public int LimitMinutes { get; set; }
    [JsonPropertyName("track")]
    public Track Track { get; set; } = default!;
    [JsonPropertyName("weather")]
    public Weather Weather { get; set; } = default!;
    [JsonPropertyName("track_state")]
    public TrackState TrackState { get; set; } = default!;
    [JsonPropertyName("session_results")]
    public SessionResults[] SessionResults { get; set; } = default!;
    [JsonPropertyName("race_summary")]
    public RaceSummary RaceSummary { get; set; } = default!;
    [JsonPropertyName("car_classes")]
    public ResultsCarClasses[] CarClasses { get; set; } = default!;
    [JsonPropertyName("allowed_licenses")]
    public AllowedLicenses[] AllowedLicenses { get; set; } = default!;
    [JsonPropertyName("results_restricted")]
    public bool ResultsRestricted { get; set; }
}

[JsonSerializable(typeof(SubSessionResult)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SubSessionResultContext : JsonSerializerContext
{ }
