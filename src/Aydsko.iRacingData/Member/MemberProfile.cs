// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

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

    /// <summary>Get the Club or Region logo image.</summary>
    /// <remarks>If the <c>CLUB_REGION_IMG</c> value is available in the <see cref="ProfileFields"/> collection it will be properly formatted into an absolute URL.</remarks>
    /// <returns>An absolute URL to the image.</returns>
    public Uri? GetClubRegionImageUrl()
    {
        if (ProfileFields.FirstOrDefault(pf => pf.Name == "CLUB_REGION_IMG") is not ProfileField clubRegionImageField || string.IsNullOrWhiteSpace(clubRegionImageField.Value))
        {
            return null;
        }

        return new Uri("https://ir-core-sites.iracing.com/members/" + clubRegionImageField.Value.TrimStart('/'), UriKind.Absolute);
    }
}

[JsonSerializable(typeof(MemberProfile)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class MemberProfileContext : JsonSerializerContext
{ }
