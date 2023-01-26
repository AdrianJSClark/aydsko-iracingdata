// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class SeasonStandingsStandingsDetails
{
    [JsonPropertyName("driver_standings")]
    public SeasonStandingsDriverStandings[] DriverStandings { get; set; } = null!;
    [JsonPropertyName("team_standings")]
    public SeasonStandingsTeamStandings[] TeamStandings { get; set; } = null!;
}
