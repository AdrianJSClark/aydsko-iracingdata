// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

public class AllowedLicenses
{
    [JsonPropertyName("license_group")]
    public int LicenseGroup { get; set; }

    [JsonPropertyName("min_license_level")]
    public int MinLicenseLevel { get; set; }

    [JsonPropertyName("max_license_level")]
    public int MaxLicenseLevel { get; set; }

    [JsonPropertyName("group_name")]
    public string GroupName { get; set; } = default!;
}
