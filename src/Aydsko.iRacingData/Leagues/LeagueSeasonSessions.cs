// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class LeagueSeasonSessions
{
    [JsonPropertyName("sessions")]
    public Session[] Sessions { get; set; } = Array.Empty<Session>();

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("league_id")]
    public int LeagueId { get; set; }
}

[JsonSerializable(typeof(LeagueSeasonSessions)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class LeagueSeasonSessionsContext : JsonSerializerContext
{ }
