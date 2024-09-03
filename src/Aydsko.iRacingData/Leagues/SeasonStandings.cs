// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class SeasonStandings
{
    [JsonPropertyName("car_class_id")]
    public int CarClassId { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("league_id")]
    public int LeagueId { get; set; }

    [JsonPropertyName("standings")]
    public SeasonStandingsStandingsDetails Standings { get; set; } = null!;
}

[JsonSerializable(typeof(SeasonStandings)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SeasonStandingsContext : JsonSerializerContext
{ }
