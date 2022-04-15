// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Common;

/// <summary>Details about the driver's helmet.</summary>
public class Helmet
{
    /// <summary>Identifier of the helmet pattern.</summary>
    [JsonPropertyName("pattern")]
    public int Pattern { get; set; }

    /// <summary>First pattern color.</summary>
    [JsonPropertyName("color1")]
    public string Color1 { get; set; } = null!;

    /// <summary>Second pattern color.</summary>
    [JsonPropertyName("color2")]
    public string Color2 { get; set; } = null!;

    /// <summary>Third pattern color.</summary>
    [JsonPropertyName("color3")]
    public string Color3 { get; set; } = null!;

    /// <summary>Identifier for the face type of the driver.</summary>
    [JsonPropertyName("face_type")]
    public int FaceType { get; set; }

    /// <summary>Identifier for the type of helmet of the driver.</summary>
    [JsonPropertyName("helmet_type")]
    public int HelmetType { get; set; }
}
