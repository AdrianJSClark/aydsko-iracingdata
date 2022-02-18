// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Member;

public class ContentPackage
{
    [JsonPropertyName("package_id")]
    public int PackageId { get; set; }
    [JsonPropertyName("content_ids")]
    public int[] ContentIds { get; set; } = Array.Empty<int>();
}
