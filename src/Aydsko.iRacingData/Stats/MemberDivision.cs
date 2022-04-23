// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Stats;

public class MemberDivision
{
    [JsonPropertyName("division")]
    public int Division { get; set; }

    [JsonPropertyName("projected")]
    public bool Projected { get; set; }

    [JsonPropertyName("event_type")]
    public EventType EventType { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }
}

[JsonSerializable(typeof(MemberDivision)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class MemberDivisionContext : JsonSerializerContext
{ }
