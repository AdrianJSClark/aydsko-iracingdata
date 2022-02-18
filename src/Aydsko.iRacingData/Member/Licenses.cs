// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Member;

public class Licenses
{
    [JsonPropertyName("oval")]
    public LicenseInfo Oval { get; set; } = default!;
    [JsonPropertyName("road")]
    public LicenseInfo Road { get; set; } = default!;
    [JsonPropertyName("dirt_oval")]
    public LicenseInfo DirtOval { get; set; } = default!;
    [JsonPropertyName("dirt_road")]
    public LicenseInfo DirtRoad { get; set; } = default!;
}
