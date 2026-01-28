// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Common;

public class License
{
    [JsonPropertyName("category_id")]
    public int CategoryId { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; } = default!;

    [JsonPropertyName("category_name")]
    public string CategoryName { get; set; } = default!;

    [JsonPropertyName("license_level")]
    public int LicenseLevel { get; set; }

    [JsonPropertyName("safety_rating")]
    public decimal SafetyRating { get; set; }

    [JsonPropertyName("irating")]
    public int iRating { get; set; }

    [JsonPropertyName("tt_rating")]
    public int TimeTrialRating { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; } = default!;

    [JsonPropertyName("group_name")]
    public string GroupName { get; set; } = default!;

    [JsonPropertyName("group_id")]
    public int GroupId { get; set; }
}
