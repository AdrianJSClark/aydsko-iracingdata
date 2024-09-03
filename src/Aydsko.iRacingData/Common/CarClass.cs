// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Common;

public class CarClass
{
    [JsonPropertyName("car_class_id")]
    public int CarClassId { get; set; }

    [JsonPropertyName("cars_in_class")]
    public CarsInClass[] CarsInClass { get; set; } = Array.Empty<CarsInClass>();

    [JsonPropertyName("cust_id")]
    public int? CustomerId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("rain_enabled")]
    public bool RainEnabled { get; set; }

    [JsonPropertyName("relative_speed")]
    public int RelativeSpeed { get; set; }

    [JsonPropertyName("short_name")]
    public string ShortName { get; set; } = default!;
}

[JsonSerializable(typeof(CarClass[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class CarClassArrayContext : JsonSerializerContext
{ }
