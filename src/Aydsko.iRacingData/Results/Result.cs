// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Diagnostics;
using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Results;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class Result
{
    /// <summary>Unique identifier for the Team entry.</summary>
    /// <remarks>This value will be <see langword="null"/> if this is not a Team event.</remarks>
    [JsonPropertyName("team_id")]
    public int? TeamId { get; set; }

    /// <summary>Unique identifier for the Customer.</summary>
    /// <remarks>This value will be <see langword="null"/> if this is a Team event.</remarks>
    [JsonPropertyName("cust_id")]
    public int? CustomerId { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = default!;

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

    [JsonPropertyName("interval"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? Interval { get; set; }

    [JsonPropertyName("class_interval"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? ClassInterval { get; set; }

    [JsonPropertyName("average_lap"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? AverageLap { get; set; }

    [JsonPropertyName("best_lap_num")]
    public int BestLapNumber { get; set; }

    [JsonPropertyName("best_lap_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? BestLapTime { get; set; }

    [JsonPropertyName("best_nlaps_num")]
    public int BestNLapsNumber { get; set; }

    [JsonPropertyName("best_nlaps_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? BestNLapsTime { get; set; }

    [JsonPropertyName("best_qual_lap_at")]
    public DateTimeOffset? BestQualifyingLapAt { get; set; }

    [JsonPropertyName("best_qual_lap_num")]
    public int BestQualifyingLapNumber { get; set; }

    [JsonPropertyName("best_qual_lap_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? BestQualifyingLapTime { get; set; }

    [JsonPropertyName("reason_out_id")]
    public int ReasonOutId { get; set; }

    [JsonPropertyName("reason_out")]
    public string ReasonOut { get; set; } = default!;

    [JsonPropertyName("champ_points")]
    public int ChampPoints { get; set; }

    [JsonPropertyName("drop_race")]
    public bool DropRace { get; set; }

    [JsonPropertyName("club_points")]
    public int ClubPoints { get; set; }

    [JsonPropertyName("position")]
    public int Position { get; set; }

    [JsonPropertyName("qual_lap_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? QualifyingLapTime { get; set; }

    [JsonPropertyName("starting_position")]
    public int StartingPosition { get; set; }

    [JsonPropertyName("starting_position_in_class")]
    public int? StartingPositionInClass { get; set; }

    [JsonPropertyName("car_class_id")]
    public int CarClassId { get; set; }

    [JsonPropertyName("division")]
    public int Division { get; set; }

    [JsonPropertyName("division_name")]
    public string? DivisionName { get; set; } = default!;

    /// <summary>Driver license level at the start of the race. Refers to the <see cref="Lookups.LicenseLevel.LicenseId"/> property.</summary>
    /// <seealso cref="DataClient.GetLicenseLookupsAsync(CancellationToken)"/>
    /// <seealso cref="Lookups.LicenseLevel" />
    [JsonPropertyName("old_license_level")]
    public int OldLicenseLevel { get; set; }

    /// <summary>Detailed driver's license rating before the race.</summary>
    /// <remarks>This value is multiplied by 100 to be expressed as an integer.</remarks>
    [JsonPropertyName("old_sub_level")]
    public int OldSubLevel { get; set; }

    [JsonIgnore]
    public decimal OldSafetyRating => OldSubLevel / 100.0M;

    [JsonPropertyName("old_cpi")]
    public decimal OldCornersPerIncident { get; set; }

    [JsonPropertyName("oldi_rating")]
    public int OldIRating { get; set; }

    [JsonPropertyName("old_ttrating")]
    public int OldTimeTrialRating { get; set; }

    /// <summary>Driver license level at the end of the race. Refers to the <see cref="Lookups.LicenseLevel.LicenseId"/> property.</summary>
    /// <seealso cref="IDataClient.GetLicenseLookupsAsync(CancellationToken)"/>
    /// <seealso cref="Lookups.LicenseLevel" />
    [JsonPropertyName("new_license_level")]
    public int NewLicenseLevel { get; set; }

    /// <summary>Detailed driver's license rating after the race.</summary>
    /// <remarks>This value is multiplied by 100 to be expressed as an integer.</remarks>
    [JsonPropertyName("new_sub_level")]
    public int NewSubLevel { get; set; }

    [JsonIgnore]
    public decimal NewSafetyRating => NewSubLevel / 100.0M;

    [JsonPropertyName("new_cpi")]
    public decimal NewCornersPerIncident { get; set; }

    [JsonPropertyName("newi_rating")]
    public int NewIRating { get; set; }

    [JsonPropertyName("new_ttrating")]
    public int NewTimeTrialRating { get; set; }

    [JsonPropertyName("multiplier")]
    public int Multiplier { get; set; }

    [JsonPropertyName("license_change_oval")]
    public int LicenseChangeOval { get; set; }

    [JsonPropertyName("license_change_road")]
    public int LicenseChangeRoad { get; set; }

    [JsonPropertyName("incidents")]
    public int Incidents { get; set; }

    [JsonPropertyName("max_pct_fuel_fill")]
    public int MaximumPercentageFuelFill { get; set; }

    [JsonPropertyName("weight_penalty_kg")]
    public int WeightPenaltyKg { get; set; }

    [JsonPropertyName("league_points")]
    public int LeaguePoints { get; set; }

    [JsonPropertyName("league_agg_points")]
    public int LeagueAggregatePoints { get; set; }

    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("car_class_name")]
    public string CarClassName { get; set; } = default!;

    [JsonPropertyName("car_class_short_name")]
    public string CarClassShortName { get; set; } = default!;

    [JsonPropertyName("car_name")]
    public string CarName { get; set; } = default!;

    [JsonPropertyName("aggregate_champ_points")]
    public int AggregateChampionshipPoints { get; set; }

    [JsonPropertyName("livery")]
    public Livery Livery { get; set; } = default!;

    [JsonPropertyName("suit")]
    public Suit Suit { get; set; } = default!;

    [JsonPropertyName("helmet")]
    public Helmet Helmet { get; set; } = default!;

    [JsonPropertyName("watched")]
    public bool Watched { get; set; }

    [JsonPropertyName("friend")]
    public bool Friend { get; set; }

    [JsonPropertyName("ai")]
    public bool AI { get; set; }

    [JsonPropertyName("country_code")]
    public string? CountryCode { get; set; } = default!;

    [JsonPropertyName("driver_results")]
    public DriverResult[]? DriverResults { get; set; }

    private string DebuggerDisplay => (TeamId is null)
                                   ? $"{CustomerId} \"{DisplayName}\"{(AI ? " (AI)" : "")} Q{StartingPosition} to P{Position}"
                                   : $"{CustomerId} \"{DisplayName}\"{(AI ? " (AI)" : "")} Team {TeamId} ({DriverResults?.Length} drivers) Q{StartingPosition} to P{Position}";

}
