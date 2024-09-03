// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Hosted;

public class Car
{
    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("car_class_id")]
    public int CarClassId { get; set; }

    [Obsolete("Use \"MaxPercentFuelFill\" property instead.")]
    public int MaxPctFuelFill { get => (int)MaxPercentFuelFill; set => MaxPercentFuelFill = value; }

    [JsonPropertyName("max_pct_fuel_fill")]
    public decimal MaxPercentFuelFill { get; set; }

    [JsonPropertyName("weight_penalty_kg")]
    public int WeightPenaltyKg { get; set; }

    [JsonPropertyName("power_adjust_pct")]
    public decimal PowerAdjustPercent { get; set; }

    [JsonPropertyName("max_dry_tire_sets")]
    public int MaxDryTireSets { get; set; }

    [JsonPropertyName("package_id")]
    public int PackageId { get; set; }
}
