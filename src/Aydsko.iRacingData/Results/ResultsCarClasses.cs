// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

public class ResultsCarClasses
{
    [JsonPropertyName("car_class_id")]
    public int CarClassId { get; set; }

    [JsonPropertyName("cars_in_class")]
    public CarsInClass[] CarsInClass { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("short_name")]
    public string ShortName { get; set; } = default!;

    [JsonPropertyName("strength_of_field")]
    public int StrengthOfField { get; set; }

    [JsonPropertyName("num_entries")]
    public int NumberOfEntries { get; set; }
}
