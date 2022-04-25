// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

public class Livery
{
    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("pattern")]
    public int Pattern { get; set; }

    [JsonPropertyName("color1")]
    public string Color1 { get; set; } = default!;

    [JsonPropertyName("color2")]
    public string Color2 { get; set; } = default!;

    [JsonPropertyName("color3")]
    public string Color3 { get; set; } = default!;

    [JsonPropertyName("number_font")]
    public int NumberFont { get; set; }

    [JsonPropertyName("number_color1")]
    public string NumberColor1 { get; set; } = default!;

    [JsonPropertyName("number_color2")]
    public string NumberColor2 { get; set; } = default!;

    [JsonPropertyName("number_color3")]
    public string NumberColor3 { get; set; } = default!;

    [JsonPropertyName("number_slant")]
    public int NumberSlant { get; set; }

    [JsonPropertyName("sponsor1")]
    public int Sponsor1 { get; set; }

    [JsonPropertyName("sponsor2")]
    public int Sponsor2 { get; set; }

    [JsonPropertyName("car_number")]
    public string CarNumber { get; set; } = default!;

    [JsonPropertyName("wheel_color")]
    public string WheelColor { get; set; } = default!;

    [JsonPropertyName("rim_type")]
    public int RimType { get; set; }
}
