// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Common;

/// <summary>Details about the driver's suit.</summary>
public class Suit
{
    /// <summary>Pattern identifier chosen for the suit.</summary>
    [JsonPropertyName("pattern")]
    public int Pattern { get; set; }

    /// <summary>First pattern color.</summary>
    [JsonPropertyName("color1")]
    public string Color1 { get; set; } = default!;

    /// <summary>Second pattern color.</summary>
    [JsonPropertyName("color2")]
    public string Color2 { get; set; } = default!;

    /// <summary>Third pattern color.</summary>
    [JsonPropertyName("color3")]
    public string Color3 { get; set; } = default!;

    /// <summary>Type of body chosen for the driver.</summary>
    [JsonPropertyName("body_type")]
    public int? BodyType { get; set; }
}
