// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class Session
{
    [JsonPropertyName("cars")]
    public SessionCar[] Cars { get; set; } = [];

    [JsonPropertyName("consec_cautions_single_file")]
    public bool ConsecutiveCautionsSingleFile { get; set; }

    [JsonIgnore, Obsolete("Use \"ConsecutiveCautionsSingleFile\" instead.")]
    public bool ConsecCautionsSingleFile { get => ConsecutiveCautionsSingleFile; set => ConsecutiveCautionsSingleFile = value; }

    [JsonPropertyName("damage_model")]
    public int DamageModel { get; set; }

    [JsonPropertyName("do_not_count_caution_laps")]
    public bool DoNotCountCautionLaps { get; set; }

    [JsonPropertyName("do_not_paint_cars")]
    public bool DoNotPaintCars { get; set; }

    [JsonPropertyName("driver_changes")]
    public bool DriverChanges { get; set; }

    [JsonPropertyName("entry_count")]
    public int EntryCount { get; set; }

    [JsonPropertyName("green_white_checkered_limit")]
    public int GreenWhiteCheckeredLimit { get; set; }

    [JsonPropertyName("has_results")]
    public bool HasResults { get; set; }

    [JsonPropertyName("launch_at")]
    public DateTimeOffset LaunchAt { get; set; }

    [JsonPropertyName("league_id")]
    public int LeagueId { get; set; }

    [JsonPropertyName("league_season_id")]
    public int LeagueSeasonId { get; set; }

    [JsonPropertyName("lone_qualify")]
    public bool LoneQualify { get; set; }

    [JsonPropertyName("max_ai_drivers")]
    public int MaxAiDrivers { get; set; }

    [JsonPropertyName("must_use_diff_tire_types_in_races")]
    public bool MustUseDiffTireTypesInRace { get; set; }

    [JsonPropertyName("no_lapper_wave_arounds")]
    public bool NoLapperWaveArounds { get; set; }

    [JsonPropertyName("num_opt_laps")]
    public int NumOptLaps { get; set; }

    [JsonPropertyName("pace_car_class_id")]
    public int? PaceCarClassId { get; set; }

    [JsonPropertyName("pace_car_id")]
    public int? PaceCarId { get; set; }

    [JsonPropertyName("password_protected")]
    public bool PasswordProtected { get; set; }

    [JsonPropertyName("practice_length")]
    public int PracticeLength { get; set; }

    [JsonPropertyName("private_session_id")]
    public long PrivateSessionId { get; set; }

    [JsonPropertyName("qualify_laps")]
    public int QualifyLaps { get; set; }

    [JsonPropertyName("qualify_length")]
    public int QualifyLength { get; set; }

    [JsonPropertyName("race_laps")]
    public int RaceLaps { get; set; }

    [JsonPropertyName("race_length")]
    public int RaceLength { get; set; }

    [JsonPropertyName("session_id")]
    public long SessionId { get; set; }

    [JsonPropertyName("short_parade_lap")]
    public bool ShortParadeLap { get; set; }

    [JsonPropertyName("start_on_qual_tire")]
    public bool StartOnQualifyingTire { get; set; }

    [JsonIgnore, Obsolete("Use \"StartOnQualifyingTire\" instead.")]
    public bool StartOnQualTire { get => StartOnQualifyingTire; set => StartOnQualifyingTire = value; }

    [JsonPropertyName("start_zone")]
    public bool StartZone { get; set; }

    [JsonPropertyName("status")]
    public int StatusRaw { get; set; }

    [JsonIgnore]
    public SessionStatus Status { get => (SessionStatus)StatusRaw; set => StatusRaw = (int)value; }

    [JsonPropertyName("subsession_id")]
    public int SubSessionId { get; set; }

    [JsonPropertyName("team_entry_count")]
    public int TeamEntryCount { get; set; }

    [JsonPropertyName("telemetry_force_to_disk")]
    public int TelemetryForceToDisk { get; set; }

    [JsonPropertyName("telemetry_restriction")]
    public int TelemetryRestriction { get; set; }

    [JsonPropertyName("time_limit")]
    public int TimeLimit { get; set; }

    [JsonPropertyName("track")]
    public Track Track { get; set; } = default!;

    [JsonPropertyName("track_state")]
    public TrackState TrackState { get; set; } = default!;

    [JsonPropertyName("weather")]
    public LeagueSessionWeather Weather { get; set; } = default!;

    [JsonPropertyName("winner_id")]
    public int WinnerId { get; set; }

    [JsonPropertyName("winner_name")]
    public string WinnerName { get; set; } = default!;

    [JsonPropertyName("heat_ses_info")]
    public HeatSessionInformation? HeatSessionInformation { get; set; }
}
