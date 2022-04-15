// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Common;

/// <summary>License information.</summary>
public class License
{
    /// <summary>Identfier for the license category.</summary>
    [JsonPropertyName("category_id")]
    public int CategoryId { get; set; }

    /// <summary>Name of the license category.</summary>
    [JsonPropertyName("category")]
    public string Category { get; set; } = default!;

    /// <summary>An indicator of the level of the license.</summary>
    [JsonPropertyName("license_level")]
    public int LicenseLevel { get; set; }

    /// <summary>Value of the safety rating attached to the license.</summary>
    [JsonPropertyName("safety_rating")]
    public decimal SafetyRating { get; set; }

    /// <summary>Color associated with the license.</summary>
    [JsonPropertyName("color")]
    public string Color { get; set; } = default!;

    /// <summary>Name of the license group.</summary>
    [JsonPropertyName("group_name")]
    public string GroupName { get; set; } = default!;

    /// <summary>Identifier of the license group.</summary>
    [JsonPropertyName("group_id")]
    public int GroupId { get; set; }
}
