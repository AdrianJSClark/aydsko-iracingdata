// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Lookups;

/// <summary>Sub-level within a license.</summary>
public class LicenseLevel
{
    /// <summary>Unique identifier for this license level.</summary>
    [JsonPropertyName("license_id")]
    public int LicenseId { get; set; }

    /// <summary>Unique identifier for the license category.</summary>
    [JsonPropertyName("license_group")]
    public int LicenseGroup { get; set; }

    /// <summary>Display name for the license level.</summary>
    [JsonPropertyName("license")]
    public string License { get; set; } = default!;

    /// <summary>Short display name for the license level.</summary>
    [JsonPropertyName("short_name")]
    public string ShortName { get; set; } = default!;

    /// <summary>Letter code for the license level.</summary>
    [JsonPropertyName("license_letter")]
    public string LicenseLetter { get; set; } = default!;

    /// <summary>Color code for the license level.</summary>
    /// <remarks>This is an HTML color code.</remarks>
    [JsonPropertyName("color")]
    public string Color { get; set; } = default!;
}
