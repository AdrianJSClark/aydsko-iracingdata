// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class LeagueMembership
{
    [JsonPropertyName("league_id")]
    public int LeagueId { get; set; }

    [JsonPropertyName("league_name")]
    public string LeagueName { get; set; } = null!;

    [JsonPropertyName("owner")]
    public bool Owner { get; set; }

    [JsonPropertyName("admin")]
    public bool Admin { get; set; }

    [JsonPropertyName("league_mail_opt_out")]
    public bool LeagueMailOptOut { get; set; }

    [JsonPropertyName("league_pm_opt_out")]
    public bool LeaguePmOptOut { get; set; }

    [JsonPropertyName("car_number")]
    public string? CarNumber { get; set; }

    [JsonPropertyName("nick_name")]
    public string? NickName { get; set; }

    [JsonPropertyName("league")]
    public League? League { get; set; }
}

[JsonSerializable(typeof(LeagueMembership[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class LeagueMembershipArrayContext : JsonSerializerContext
{ }
