namespace Aydsko.iRacingData.Leagues;

public class PointsSystem
{
    [JsonPropertyName("points_system_id")]
    public int PointsSystemId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    [JsonPropertyName("league_id")]
    public int LeagueId { get; set; }

    [JsonPropertyName("retired")]
    public bool Retired { get; set; }

    [JsonPropertyName("iracing_system")]
    public bool IRacingSystem { get; set; }
}
