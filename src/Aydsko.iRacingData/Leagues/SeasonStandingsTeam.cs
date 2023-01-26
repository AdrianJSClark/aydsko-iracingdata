// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class SeasonStandingsTeam
{
    [JsonPropertyName("team_id")]
    public int TeamId { get; set; }

    [JsonPropertyName("owner_id")]
    public int OwnerId { get; set; }

    [JsonPropertyName("team_name")]
    public string TeamName { get; set; } = null!;

    [JsonPropertyName("owner")]
    public SeasonStandingsDriver Owner { get; set; } = null!;
}
