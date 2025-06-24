// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class RosterMember
{
    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = null!;

    [JsonPropertyName("helmet")]
    public Helmet Helmet { get; set; } = null!;

    [JsonPropertyName("owner")]
    public bool Owner { get; set; }

    [JsonPropertyName("admin")]
    public bool Admin { get; set; }

    [JsonPropertyName("league_mail_opt_out")]
    public bool LeagueMailOptOut { get; set; }

    [JsonPropertyName("league_pm_opt_out")]
    public bool LeaguePrivateMessageOptOut { get; set; }

    [JsonPropertyName("league_member_since")]
    public DateTimeOffset LeagueMemberSince { get; set; }

    [JsonPropertyName("car_number")]
    public string? CarNumber { get; set; }

    [JsonPropertyName("nick_name")]
    public string? NickName { get; set; }

    [JsonPropertyName("licenses")]
    public LeagueLicense[] Licenses { get; set; } = [];
}
