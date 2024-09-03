// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

public class CarRestrictions
{
    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("race_setup_id")]
    public int RaceSetupId { get; set; }

    [JsonPropertyName("max_pct_fuel_fill")]
    public decimal MaxPercentFuelFill { get; set; }

    [JsonPropertyName("weight_penalty_kg")]
    public decimal WeightPenaltyKg { get; set; }

    [JsonPropertyName("power_adjust_pct")]
    public decimal PowerAdjustPercent { get; set; }

    [JsonPropertyName("max_dry_tire_sets")]
    public int MaxDryTireSets { get; set; }

    [Obsolete("Use \"QualifyingSetupId\" property instead.")]
    public int QualSetupId { get => QualifyingSetupId; set => QualifyingSetupId = value; }

    [JsonPropertyName("qual_setup_id")]
    public int QualifyingSetupId { get; set; }
}
