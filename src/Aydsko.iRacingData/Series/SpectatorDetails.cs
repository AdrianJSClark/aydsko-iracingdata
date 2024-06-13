// © 2024 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

/// <summary>Lists the subsession details currently available to spectate.</summary>
public class SpectatorDetails
{
    /// <summary>Indicates if the query was successful.</summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>Season identifiers included in the list of subsession details.</summary>
    [JsonPropertyName("season_ids")]
    public int[] SeasonIds { get; set; } = Array.Empty<int>();

    /// <summary>Types of events included in the list of subsession details.</summary>
    [JsonPropertyName("event_types")]
    public EventType[] EventTypes { get; set; } = Array.Empty<EventType>();

    /// <summary>List of subsession details.</summary>
    [JsonPropertyName("subsessions")]
    public SpectatorSubsessionDetail[] Subsessions { get; set; } = Array.Empty<SpectatorSubsessionDetail>();
}

[JsonSerializable(typeof(SpectatorDetails)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SpectatorDetailsContext : JsonSerializerContext
{ }
