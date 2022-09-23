namespace Aydsko.iRacingData.Member;

public class MemberProfile
{
    [JsonPropertyName("recent_awards")]
    public RecentAwards[] RecentAwards { get; set; } = default!;

    [JsonPropertyName("activity")]
    public Activity Activity { get; set; } = default!;

    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; } = default!;

    [JsonPropertyName("profile")]
    public ProfileField[] ProfileFields { get; set; } = default!;

    [JsonPropertyName("member_info")]
    public MemberProfileInfo Info { get; set; } = default!;

    [JsonPropertyName("field_defs")]
    public FieldDefs[] FieldDefs { get; set; } = default!;

    [JsonPropertyName("license_history")]
    public LicenseHistory[] LicenseHistory { get; set; } = default!;

    [JsonPropertyName("is_generic_image")]
    public bool IsGenericImage { get; set; }

    [JsonPropertyName("follow_counts")]
    public FollowCounts FollowCounts { get; set; } = default!;

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("disabled")]
    public bool Disabled { get; set; }

    [JsonPropertyName("recent_events")]
    public RecentEvents[] RecentEvents { get; set; } = default!;

    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }
}

[JsonSerializable(typeof(MemberProfile)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class MemberProfileContext : JsonSerializerContext
{ }
