// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Hosted;

public class Session
{
    [JsonPropertyName("num_drivers")]
    public int NumberOfDrivers { get; set; }

    [JsonPropertyName("num_spotters")]
    public int NumberOfSpotters { get; set; }

    [JsonPropertyName("num_spectators")]
    public int NumberOfSpectators { get; set; }

    [JsonPropertyName("num_broadcasters")]
    public int NumberOfBroadcasters { get; set; }

    [JsonPropertyName("available_reserved_broadcaster_slots")]
    public int AvailableReservedBroadcasterSlots { get; set; }

    [JsonPropertyName("num_spectator_slots")]
    public int NumberOfSpectatorSlots { get; set; }

    [JsonPropertyName("available_spectator_slots")]
    public int AvailableSpectatorSlots { get; set; }

    [JsonPropertyName("can_broadcast")]
    public bool CanBroadcast { get; set; }

    [JsonPropertyName("can_watch")]
    public bool CanWatch { get; set; }

    [JsonPropertyName("can_spot")]
    public bool CanSpot { get; set; }

    [JsonPropertyName("elig")]
    public Eligability Eligability { get; set; } = default!;

    [JsonPropertyName("driver_changes")]
    public bool DriverChanges { get; set; }

    [JsonPropertyName("restrict_viewing")]
    public bool RestrictViewing { get; set; }

    [JsonPropertyName("max_users")]
    public int MaxUsers { get; set; }

    [JsonPropertyName("private_session_id")]
    public int PrivateSessionId { get; set; }

    [JsonPropertyName("session_id")]
    public int SessionId { get; set; }

    [JsonPropertyName("subsession_id")]
    public int SubsessionId { get; set; }

    [JsonPropertyName("password_protected")]
    public bool PasswordProtected { get; set; }

    [JsonPropertyName("session_name")]
    public string SessionName { get; set; } = default!;

    [JsonPropertyName("open_reg_expires")]
    public DateTime OpenRegExpires { get; set; }

    [JsonPropertyName("launch_at")]
    public DateTime LaunchAt { get; set; }

    [JsonPropertyName("full_course_cautions")]
    public bool FullCourseCautions { get; set; }

    [JsonPropertyName("num_fast_tows")]
    public int NumberOfFastTows { get; set; }

    [JsonPropertyName("rolling_starts")]
    public bool RollingStarts { get; set; }

    [JsonPropertyName("restarts")]
    public int Restarts { get; set; }

    [JsonPropertyName("multiclass_type")]
    public int MulticlassType { get; set; }

    [JsonPropertyName("pits_in_use")]
    public int PitsInUse { get; set; }

    [JsonPropertyName("cars_left")]
    public int CarsLeft { get; set; }

    [JsonPropertyName("max_drivers")]
    public int MaxDrivers { get; set; }

    [JsonPropertyName("hardcore_level")]
    public int HardcoreLevel { get; set; }

    [JsonPropertyName("practice_length")]
    public int PracticeLength { get; set; }

    [JsonPropertyName("lone_qualify")]
    public bool LoneQualify { get; set; }

    [JsonPropertyName("qualify_laps")]
    public int QualifyLaps { get; set; }

    [JsonPropertyName("qualify_length")]
    public int QualifyLength { get; set; }

    [JsonPropertyName("warmup_length")]
    public int WarmupLength { get; set; }

    [JsonPropertyName("race_laps")]
    public int RaceLaps { get; set; }

    [JsonPropertyName("race_length")]
    public int RaceLength { get; set; }

    [JsonPropertyName("time_limit")]
    public int TimeLimit { get; set; }

    [JsonPropertyName("restrict_results")]
    public bool RestrictResults { get; set; }

    [JsonPropertyName("incident_limit")]
    public int IncidentLimit { get; set; }

    [JsonPropertyName("incident_warn_mode")]
    public int IncidentWarnMode { get; set; }

    [JsonPropertyName("incident_warn_param1")]
    public int IncidentWarnParam1 { get; set; }

    [JsonPropertyName("incident_warn_param2")]
    public int IncidentWarnParam2 { get; set; }

    [JsonPropertyName("unsport_conduct_rule_mode")]
    public int UnsportConductRuleMode { get; set; }

    [JsonPropertyName("lucky_dog")]
    public bool LuckyDog { get; set; }

    [JsonPropertyName("min_team_drivers")]
    public int MinTeamDrivers { get; set; }

    [JsonPropertyName("max_team_drivers")]
    public int MaxTeamDrivers { get; set; }

    [JsonPropertyName("qualifier_must_start_race")]
    public bool QualifierMustStartRace { get; set; }

    [JsonPropertyName("driver_change_rule")]
    public int DriverChangeRule { get; set; }

    [JsonPropertyName("fixed_setup")]
    public bool FixedSetup { get; set; }

    [JsonPropertyName("entry_count")]
    public int EntryCount { get; set; }

    [JsonPropertyName("league_id")]
    public int LeagueId { get; set; }

    [JsonPropertyName("league_season_id")]
    public int LeagueSeasonId { get; set; }

    [JsonPropertyName("session_type")]
    public int SessionType { get; set; }

    [JsonPropertyName("order_id")]
    public int OrderId { get; set; }

    [JsonPropertyName("min_license_level")]
    public int MinLicenseLevel { get; set; }

    [JsonPropertyName("max_license_level")]
    public int MaxLicenseLevel { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("pace_car_id")]
    public int? PaceCarId { get; set; }

    [JsonPropertyName("pace_car_class_id")]
    public int? PaceCarClassId { get; set; }

    [JsonPropertyName("num_opt_laps")]
    public int NumberOfOptLaps { get; set; }

    [JsonPropertyName("damage_model")]
    public int DamageModel { get; set; }

    [JsonPropertyName("do_not_paint_cars")]
    public bool DoNotPaintCars { get; set; }

    [JsonPropertyName("green_white_checkered_limit")]
    public int GreenWhiteCheckeredLimit { get; set; }

    [JsonPropertyName("do_not_count_caution_laps")]
    public bool DoNotCountCautionLaps { get; set; }

    [JsonPropertyName("consec_cautions_single_file")]
    public bool ConsecCautionsSingleFile { get; set; }

    [JsonPropertyName("no_lapper_wave_arounds")]
    public bool NoLapperWaveArounds { get; set; }

    [JsonPropertyName("short_parade_lap")]
    public bool ShortParadeLap { get; set; }

    [JsonPropertyName("start_on_qual_tire")]
    public bool StartOnQualTire { get; set; }

    [JsonPropertyName("telemetry_restriction")]
    public int TelemetryRestriction { get; set; }

    [JsonPropertyName("telemetry_force_to_disk")]
    public int TelemetryForceToDisk { get; set; }

    [JsonPropertyName("max_ai_drivers")]
    public int MaxAiDrivers { get; set; }

    [JsonPropertyName("ai_avoid_players")]
    public bool AiAvoidPlayers { get; set; }

    [JsonPropertyName("must_use_diff_tire_types_in_race")]
    public bool MustUseDiffTireTypesInRace { get; set; }

    [JsonPropertyName("start_zone")]
    public bool StartZone { get; set; }

    [JsonPropertyName("session_full")]
    public bool SessionFull { get; set; }

    [JsonPropertyName("host")]
    public Host Host { get; set; } = default!;

    [JsonPropertyName("track")]
    public Track Track { get; set; } = default!;

    [JsonPropertyName("weather")]
    public HostedSessionWeather Weather { get; set; } = default!;

    [JsonPropertyName("track_state")]
    public TrackState TrackState { get; set; } = default!;

    [JsonPropertyName("farm")]
    public Farm Farm { get; set; } = default!;

    [JsonPropertyName("admins")]
    public Admin[] Admins { get; set; } = Array.Empty<Admin>();

    //[JsonPropertyName("allowed_clubs")]
    //public object[] AllowedClubs { get; set; }

    //[JsonPropertyName("allowed_teams")]
    //public object[] AllowedTeams { get; set; }

    //[JsonPropertyName("allowed_leagues")]
    //public int?[] AllowedLeagues { get; set; }

    [JsonPropertyName("cars")]
    public Car[] Cars { get; set; } = Array.Empty<Car>();

    [JsonPropertyName("count_by_car_id")]
    public IDictionary<string, int> CountByCarId { get; private set; } = default!;

    [JsonPropertyName("count_by_car_class_id")]
    public IDictionary<string, int> CountByCarClassId { get; private set; } = default!;

    [JsonPropertyName("car_types")]
    public CarTypes[] CarTypes { get; set; } = Array.Empty<CarTypes>();

    [JsonPropertyName("track_types")]
    public TrackTypes[] TrackTypes { get; set; } = Array.Empty<TrackTypes>();

    [JsonPropertyName("license_group_types")]
    public LicenseGroupTypes[] LicenseGroupTypes { get; set; } = Array.Empty<LicenseGroupTypes>();

    [JsonPropertyName("event_types")]
    public EventTypes[] EventTypes { get; set; } = Array.Empty<EventTypes>();

    [JsonPropertyName("session_types")]
    public SessionTypes[] SessionTypes { get; set; } = Array.Empty<SessionTypes>();

    [JsonPropertyName("can_join")]
    public bool CanJoin { get; set; }

    [JsonPropertyName("sess_admin")]
    public bool SessAdmin { get; set; }

    [JsonPropertyName("is_heat_racing")]
    public bool IsHeatRacing { get; set; }

    [JsonPropertyName("team_entry_count")]
    public int TeamEntryCount { get; set; }

    [JsonPropertyName("populated")]
    public bool Populated { get; set; }

    [JsonPropertyName("broadcaster")]
    public bool Broadcaster { get; set; }

    [JsonPropertyName("min_ir")]
    public int MinIr { get; set; }

    [JsonPropertyName("max_ir")]
    public int MaxIr { get; set; }

    [JsonPropertyName("session_desc")]
    public string SessionDesc { get; set; } = default!;

    [JsonPropertyName("registered_teams")]
    public int[] RegisteredTeams { get; set; } = Array.Empty<int>();

    [JsonPropertyName("ai_min_skill")]
    public int AiMinSkill { get; set; }

    [JsonPropertyName("ai_max_skill")]
    public int AiMaxSkill { get; set; }

    [JsonPropertyName("ai_roster_name")]
    public string AiRosterName { get; set; } = default!;

    [JsonPropertyName("heat_ses_info")]
    public HeatSessionInfo HeatSesInfo { get; set; } = default!;
}
