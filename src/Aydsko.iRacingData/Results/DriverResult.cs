using System.Diagnostics;

namespace Aydsko.iRacingData.Results;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class DriverResult
{
    [JsonPropertyName("team_id")]
    public int TeamId { get; set; }

    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = null!;

    [JsonPropertyName("finish_position")]
    public int FinishPosition { get; set; }

    [JsonPropertyName("finish_position_in_class")]
    public int FinishPositionInClass { get; set; }

    [JsonPropertyName("laps_lead")]
    public int LapsLead { get; set; }

    [JsonPropertyName("laps_complete")]
    public int LapsComplete { get; set; }

    [JsonPropertyName("opt_laps_complete")]
    public int OptLapsComplete { get; set; }

    [JsonPropertyName("interval")]
    public int Interval { get; set; }

    [JsonPropertyName("class_interval")]
    public int ClassInterval { get; set; }

    [JsonPropertyName("average_lap")]
    public int AverageLap { get; set; }

    [JsonPropertyName("best_lap_num")]
    public int BestLapNum { get; set; }

    [JsonPropertyName("best_lap_time")]
    public int BestLapTime { get; set; }

    [JsonPropertyName("best_nlaps_num")]
    public int BestNlapsNum { get; set; }

    [JsonPropertyName("best_nlaps_time")]
    public int BestNlapsTime { get; set; }

    [JsonPropertyName("best_qual_lap_at")]
    public DateTime BestQualLapAt { get; set; }

    [JsonPropertyName("best_qual_lap_num")]
    public int BestQualLapNum { get; set; }

    [JsonPropertyName("best_qual_lap_time")]
    public int BestQualLapTime { get; set; }

    [JsonPropertyName("reason_out_id")]
    public int ReasonOutId { get; set; }

    [JsonPropertyName("reason_out")]
    public string ReasonOut { get; set; } = null!;

    [JsonPropertyName("champ_points")]
    public int ChampPoints { get; set; }

    [JsonPropertyName("drop_race")]
    public bool DropRace { get; set; }

    [JsonPropertyName("club_points")]
    public int ClubPoints { get; set; }

    [JsonPropertyName("position")]
    public int Position { get; set; }

    [JsonPropertyName("qual_lap_time")]
    public int QualLapTime { get; set; }

    [JsonPropertyName("starting_position")]
    public int StartingPosition { get; set; }

    [JsonPropertyName("starting_position_in_class")]
    public int StartingPositionInClass { get; set; }

    [JsonPropertyName("car_class_id")]
    public int CarClassId { get; set; }

    [JsonPropertyName("club_id")]
    public int ClubId { get; set; }

    [JsonPropertyName("club_name")]
    public string ClubName { get; set; } = null!;

    [JsonPropertyName("club_shortname")]
    public string ClubShortname { get; set; } = null!;

    [JsonPropertyName("division")]
    public int Division { get; set; }

    [JsonPropertyName("division_name")]
    public string DivisionName { get; set; } = null!;

    [JsonPropertyName("old_license_level")]
    public int OldLicenseLevel { get; set; }

    [JsonPropertyName("old_sub_level")]
    public int OldSubLevel { get; set; }

    [JsonPropertyName("old_cpi")]
    public float OldCpi { get; set; }

    [JsonPropertyName("oldi_rating")]
    public int OldiRating { get; set; }

    [JsonPropertyName("old_ttrating")]
    public int OldTtrating { get; set; }

    [JsonPropertyName("new_license_level")]
    public int NewLicenseLevel { get; set; }

    [JsonPropertyName("new_sub_level")]
    public int NewSubLevel { get; set; }

    [JsonPropertyName("new_cpi")]
    public float NewCpi { get; set; }

    [JsonPropertyName("newi_rating")]
    public int NewiRating { get; set; }

    [JsonPropertyName("new_ttrating")]
    public int NewTtrating { get; set; }

    [JsonPropertyName("multiplier")]
    public int Multiplier { get; set; }

    [JsonPropertyName("license_change_oval")]
    public int LicenseChangeOval { get; set; }

    [JsonPropertyName("license_change_road")]
    public int LicenseChangeRoad { get; set; }

    [JsonPropertyName("incidents")]
    public int Incidents { get; set; }

    [JsonPropertyName("max_pct_fuel_fill")]
    public int MaxPctFuelFill { get; set; }

    [JsonPropertyName("weight_penalty_kg")]
    public int WeightPenaltyKg { get; set; }

    [JsonPropertyName("league_points")]
    public int LeaguePoints { get; set; }

    [JsonPropertyName("league_agg_points")]
    public int LeagueAggPoints { get; set; }

    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("aggregate_champ_points")]
    public int AggregateChampPoints { get; set; }

    [JsonPropertyName("livery")]
    public Livery Livery { get; set; } = null!;

    [JsonPropertyName("suit")]
    public Suit Suit { get; set; } = null!;

    [JsonPropertyName("helmet")]
    public Helmet Helmet { get; set; } = null!;

    [JsonPropertyName("watched")]
    public bool Watched { get; set; }

    [JsonPropertyName("friend")]
    public bool Friend { get; set; }

    [JsonPropertyName("ai")]
    public bool AI { get; set; }

    private string DebuggerDisplay => $"{CustomerId} \"{DisplayName}\"{(AI ? " (AI)" : "")} Team {TeamId} Q{StartingPosition} to P{Position}";
}
