// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class LeaguePointsSystems
{
    [JsonPropertyName("subscribed")]
    public bool Subscribed { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("points_systems")]
    public PointsSystem[] PointsSystems { get; set; } = default!;

    [JsonPropertyName("league_id")]
    public int LeagueId { get; set; }
}

[JsonSerializable(typeof(LeaguePointsSystems)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class LeaguePointsSystemsContext : JsonSerializerContext
{ }
