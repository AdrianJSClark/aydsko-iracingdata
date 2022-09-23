// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

public class MemberLicense
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
    public float CornersPerIncident { get; set; }

    [JsonPropertyName("irating")]
    public int IRating { get; set; }

    [JsonPropertyName("tt_rating")]
    public int TimeTrialRating { get; set; }

    [JsonPropertyName("mpr_num_races")]
    public int MinimumParticipationRequirementNumberOfRaces { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; } = default!;

    [JsonPropertyName("group_name")]
    public string GroupName { get; set; } = default!;

    [JsonPropertyName("group_id")]
    public int GroupId { get; set; }

    [JsonPropertyName("pro_promotable")]
    public bool ProPromotable { get; set; }

    [JsonPropertyName("mpr_num_tts")]
    public int MinimumParticipationRequirementNumberOfTimeTrials { get; set; }
}
