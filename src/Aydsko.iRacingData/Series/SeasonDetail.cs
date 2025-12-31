// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

public class SeasonDetail
{
    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("season_name")]
    public string SeasonName { get; set; } = default!;

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("allowed_season_members")]
    public object? AllowedSeasonMembers { get; set; }

    [JsonPropertyName("capture_anon_telem")]
    public bool IsCapturingAnonymousTelemetry { get; set; }

    [JsonPropertyName("car_class_ids")]
    public int[] CarClassIds { get; set; } = [];

    [JsonPropertyName("car_switching")]
    public bool CarSwitching { get; set; }

    [JsonPropertyName("car_types")]
    public CarTypes[] CarTypes { get; set; } = [];

    [JsonPropertyName("caution_laps_do_not_count")]
    public bool CautionLapsDoNotCount { get; set; }

    [JsonPropertyName("complete")]
    public bool Complete { get; set; }

    [JsonPropertyName("connection_black_flag")]
    public bool ConnectionBlackFlagEnabled { get; set; }

    [JsonPropertyName("consec_caution_within_nlaps")]
    public int ConsecutiveCautionWithinNlaps { get; set; }

    [JsonPropertyName("consec_cautions_single_file")]
    public bool ConsecutiveCautionsSingleFile { get; set; }

    [JsonPropertyName("current_week_sched")]
    public WeekScheduleDetail CurrentWeekSchedule { get; set; } = default!;

    [JsonPropertyName("distributed_matchmaking")]
    public bool DistributedMatchmakingEnabled { get; set; }

    [JsonPropertyName("driver_change_rule")]
    public int DriverChangeRule { get; set; }

    [JsonPropertyName("driver_changes")]
    public bool DriverChanges { get; set; }

    [JsonPropertyName("drops")]
    public int Drops { get; set; }

    [JsonPropertyName("elig")]
    public SeasonDetailEligibility Eligibility { get; set; } = default!;

    [JsonPropertyName("enable_pitlane_collisions")]
    public bool EnablePitlaneCollisions { get; set; }

    [JsonPropertyName("fixed_setup")]
    public bool FixedSetup { get; set; }

    [JsonPropertyName("green_white_checkered_limit")]
    public int GreenWhiteCheckeredLimit { get; set; }

    [JsonPropertyName("grid_by_class")]
    public bool GridByClass { get; set; }

    [JsonPropertyName("hardcore_level")]
    public int HardcoreLevel { get; set; }

    [JsonPropertyName("has_mpr")]
    public bool HasMpr { get; set; }

    [JsonPropertyName("has_supersessions")]
    public bool HasSupersessions { get; set; }

    [JsonPropertyName("ignore_license_for_practice")]
    public bool IgnoreLicenseForPractice { get; set; }

    [JsonPropertyName("incident_limit")]
    public int IncidentLimit { get; set; }

    [JsonPropertyName("incident_warn_mode")]
    public int IncidentWarnMode { get; set; }

    [JsonPropertyName("incident_warn_param1")]
    public int IncidentWarnParam1 { get; set; }

    [JsonPropertyName("incident_warn_param2")]
    public int IncidentWarnParam2 { get; set; }

    [JsonPropertyName("is_heat_racing")]
    public bool IsHeatRacing { get; set; }

    [JsonPropertyName("license_group")]
    public int LicenseGroup { get; set; }

    [JsonPropertyName("license_group_types")]
    public LicenseGroupType[] LicenseGroupTypes { get; set; } = [];

    [JsonPropertyName("lucky_dog")]
    public bool LuckyDogRuleEnabled { get; set; }

    [JsonPropertyName("max_team_drivers")]
    public int MaxTeamDrivers { get; set; }

    [JsonPropertyName("max_weeks")]
    public int MaxWeeks { get; set; }

    [JsonPropertyName("min_team_drivers")]
    public int MinTeamDrivers { get; set; }

    [JsonPropertyName("multiclass")]
    public bool IsMulticlass { get; set; }

    [JsonPropertyName("must_use_diff_tire_types_in_race")]
    public bool MustUseDifferentTireTypesInRace { get; set; }

    [JsonPropertyName("num_fast_tows")]
    public int NumberOfFastTows { get; set; }

    [JsonPropertyName("num_opt_laps")]
    public int NumberOfOptLaps { get; set; }

    [JsonPropertyName("official")]
    public bool IsOfficial { get; set; }

    [JsonPropertyName("op_duration")]
    public int OpenPracticeDurationMinutes { get; set; }

    [JsonIgnore]
    public TimeSpan OpenPracticeDuration => TimeSpan.FromMinutes(OpenPracticeDurationMinutes);

    [JsonPropertyName("open_practice_session_type_id")]
    public int OpenPracticeSessionTypeId { get; set; }

    [JsonPropertyName("qualifier_must_start_race")]
    public bool QualifierMustStartRace { get; set; }

    [JsonPropertyName("race_week")]
    public int RaceWeek { get; set; }

    [JsonPropertyName("race_week_to_make_divisions")]
    public int RaceWeekToMakeDivisions { get; set; }

    [JsonPropertyName("reg_open_minutes")]
    public int RegistrationOpenMinutes { get; set; }

    [JsonIgnore]
    public TimeSpan RegistrationOpen => TimeSpan.FromMinutes(RegistrationOpenMinutes);

    [JsonPropertyName("region_competition")]
    public bool RegionCompetition { get; set; }

    [JsonPropertyName("restrict_by_member")]
    public bool RestrictByMember { get; set; }

    [JsonPropertyName("restrict_to_car")]
    public bool RestrictToCar { get; set; }

    [JsonPropertyName("restrict_viewing")]
    public bool RestrictViewing { get; set; }

    [JsonPropertyName("rookie_season")]
    public string RookieSeason { get; set; } = default!;

    [JsonPropertyName("schedule_description")]
    public string ScheduleDescription { get; set; } = default!;

    [JsonPropertyName("season_quarter")]
    public int SeasonQuarter { get; set; }

    [JsonPropertyName("season_short_name")]
    public string SeasonShortName { get; set; } = default!;

    [JsonPropertyName("season_year")]
    public int SeasonYear { get; set; }

    [JsonPropertyName("send_to_open_practice")]
    public bool SendToOpenPractice { get; set; }

    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }

    [JsonPropertyName("short_parade_lap")]
    public bool ShortParadeLap { get; set; }

    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("start_on_qual_tire")]
    public bool StartOnQualifyingTire { get; set; }

    [JsonPropertyName("start_zone")]
    public bool StartZone { get; set; }

    [JsonPropertyName("track_types")]
    public TrackTypes[] TrackTypes { get; set; } = [];

    [JsonPropertyName("unsport_conduct_rule_mode")]
    public int UnsportsmanshipConductRuleMode { get; set; }
}

public class WeekScheduleDetail
{
    [JsonPropertyName("race_week_num")]
    public int RaceWeekNum { get; set; }

    [JsonPropertyName("track")]
    public Track Track { get; set; } = default!;

    [JsonPropertyName("car_restrictions")]
    public CarRestrictions[] CarRestrictions { get; set; } = [];

    [JsonPropertyName("race_lap_limit")]
    public int? RaceLapLimit { get; set; }

    [JsonPropertyName("race_time_limit")]
    public int? RaceTimeLimit { get; set; }

    [JsonPropertyName("precip_chance")]
    public decimal PrecipitationChance { get; set; }

    [JsonPropertyName("start_type")]
    public string StartType { get; set; } = default!;

    [JsonPropertyName("category_id")]
    public int CategoryId { get; set; }
}

public class SeasonDetailEligibility
{
    [JsonPropertyName("own_car")]
    public bool OwnCar { get; set; }

    [JsonPropertyName("own_track")]
    public bool OwnTrack { get; set; }
}

internal sealed class SeasonDetailArrayWrapper
{
    [JsonPropertyName("seasons")]
    public SeasonDetail[] SeasonDetails { get; set; } = [];
}

[JsonSerializable(typeof(SeasonDetailArrayWrapper)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SeasonDetailArrayWrapperContext : JsonSerializerContext
{ }
