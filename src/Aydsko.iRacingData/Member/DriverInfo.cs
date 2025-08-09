// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

public class DriverInfo
{
    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = default!;

    [JsonPropertyName("helmet")]
    public Helmet Helmet { get; set; } = default!;

    [JsonPropertyName("last_login")]
    public DateTimeOffset LastLogin { get; set; }

    [JsonPropertyName("member_since")]
    public string MemberSince { get; set; } = default!;

    [JsonPropertyName("ai")]
    public bool AI { get; set; }

    [JsonPropertyName("licenses")]
    public LicenseInfo[]? Licenses { get; set; }
}
