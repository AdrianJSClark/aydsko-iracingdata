// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Common;

public class Suit
{
    [JsonPropertyName("pattern")]
    public int Pattern { get; set; }
    [JsonPropertyName("color1")]
    public string Color1 { get; set; } = default!;
    [JsonPropertyName("color2")]
    public string Color2 { get; set; } = default!;
    [JsonPropertyName("color3")]
    public string Color3 { get; set; } = default!;
    [JsonPropertyName("body_type")]
    public int? BodyType { get; set; }
}
