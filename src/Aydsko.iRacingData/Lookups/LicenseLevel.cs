// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Lookups;

public class LicenseLevel
{
    [JsonPropertyName("license_id")]
    public int LicenseId { get; set; }
    [JsonPropertyName("license_group")]
    public int LicenseGroup { get; set; }
    [JsonPropertyName("license")]
    public string License { get; set; } = default!;
    [JsonPropertyName("short_name")]
    public string ShortName { get; set; } = default!;
    [JsonPropertyName("license_letter")]
    public string LicenseLetter { get; set; } = default!;
    [JsonPropertyName("color")]
    public string Color { get; set; } = default!;
}
