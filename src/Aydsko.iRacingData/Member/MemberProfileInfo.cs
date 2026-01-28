// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

public class MemberProfileInfo
{
    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = default!;

    [JsonPropertyName("helmet")]
    public Helmet Helmet { get; set; } = default!;

    [JsonPropertyName("last_login")]
    public string LastLogin { get; set; } = default!;

    [JsonPropertyName("member_since")]
    public string MemberSince { get; set; } = default!;

    [JsonPropertyName("ai")]
    public bool AI { get; set; }

    [JsonPropertyName("licenses")]
    public MemberLicense[] Licenses { get; set; } = default!;

    [JsonPropertyName("flair_id")]
    public int FlairId { get; set; }

    [JsonPropertyName("flair_name")]
    public string FlairName { get; set; } = default!;

    [JsonPropertyName("flair_shortname")]
    public string? FlairShortName { get; set; }
}
