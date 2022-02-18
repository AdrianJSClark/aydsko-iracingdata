// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Series;

public class LicenseGroupTypes
{
    [JsonPropertyName("license_group_type")]
    public int LicenseGroupType { get; set; }
}
