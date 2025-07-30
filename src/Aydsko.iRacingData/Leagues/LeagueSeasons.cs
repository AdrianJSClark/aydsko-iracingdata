// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class LeagueSeasons
{
    [JsonPropertyName("subscribed")]
    public bool Subscribed { get; set; }

    [JsonPropertyName("seasons")]
    public Season[] Seasons { get; set; } = [];

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("retired")]
    public bool Retired { get; set; }

    [JsonPropertyName("league_id")]
    public int LeagueId { get; set; }
}

[JsonSerializable(typeof(LeagueSeasons)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class LeagueSeasonsContext : JsonSerializerContext
{ }
