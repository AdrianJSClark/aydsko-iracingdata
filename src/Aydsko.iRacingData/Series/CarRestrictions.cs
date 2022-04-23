// © 2022 Adrian Clark
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
    [JsonPropertyName("qual_setup_id")]
    public int QualSetupId { get; set; }
}
