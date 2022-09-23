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

    [JsonPropertyName("club_id")]
    public int ClubId { get; set; }

    [JsonPropertyName("club_name")]
    public string ClubName { get; set; } = default!;

    [JsonPropertyName("ai")]
    public bool AI { get; set; }

    [JsonPropertyName("licenses")]
    public MemberLicense[] Licenses { get; set; } = default!;
}
