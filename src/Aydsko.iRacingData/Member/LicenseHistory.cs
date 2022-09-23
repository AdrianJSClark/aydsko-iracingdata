﻿namespace Aydsko.iRacingData.Member;

public class LicenseHistory
{
    [JsonPropertyName("category_id")]
    public int CategoryId { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; } = default!;

    [JsonPropertyName("license_level")]
    public int LicenseLevel { get; set; }

    [JsonPropertyName("safety_rating")]
    public float SafetyRating { get; set; }

    [JsonPropertyName("cpi")]
    public float Cpi { get; set; }

    [JsonPropertyName("irating")]
    public int Irating { get; set; }

    [JsonPropertyName("tt_rating")]
    public int TtRating { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; } = default!;

    [JsonPropertyName("group_name")]
    public string GroupName { get; set; } = default!;

    [JsonPropertyName("group_id")]
    public int GroupId { get; set; }
}