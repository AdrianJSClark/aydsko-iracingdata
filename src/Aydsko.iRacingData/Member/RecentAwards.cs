// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

public class RecentAwards
{
    [JsonPropertyName("member_award_id")]
    public int MemberAwardId { get; set; }

    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("award_id")]
    public int AwardId { get; set; }

    [JsonPropertyName("award_date")]
    public string AwardDate { get; set; } = default!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    [JsonPropertyName("awarded_description")]
    public string AwardedDescription { get; set; } = default!;

    [JsonPropertyName("viewed")]
    public bool Viewed { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("group_name")]
    public string GroupName { get; set; } = default!;

    [JsonPropertyName("icon_url_small")]
    public string IconUrlSmall { get; set; } = default!;

    [JsonPropertyName("icon_url_large")]
    public string IconUrlLarge { get; set; } = default!;

    [JsonPropertyName("icon_url_unawarded")]
    public string IconUrlUnawarded { get; set; } = default!;

    [JsonPropertyName("weight")]
    public int Weight { get; set; }

    [JsonPropertyName("award_count")]
    public int AwardCount { get; set; }

    [JsonPropertyName("award_order")]
    public int AwardOrder { get; set; }

    [JsonPropertyName("achievement")]
    public bool Achievement { get; set; }

    [JsonPropertyName("subsession_id")]
    public int SubsessionId { get; set; }
}
