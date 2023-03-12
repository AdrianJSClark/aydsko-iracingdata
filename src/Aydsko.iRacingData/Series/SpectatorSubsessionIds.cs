// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

/// <summary>Lists the subsession identifiers currently available to spectate.</summary>
public class SpectatorSubsessionIds
{
    /// <summary>Types of events included in the list of subsession identifiers.</summary>
    [JsonPropertyName("event_types")]
    public EventType[] EventTypes { get; set; } = Array.Empty<EventType>();

    /// <summary>Indicates if the query was successful.</summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>List of subsession identifiers.</summary>
    [JsonPropertyName("subsession_ids")]
    public int[] SubsessionIdentifiers { get; set; } = Array.Empty<int>();
}

[JsonSerializable(typeof(SpectatorSubsessionIds)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SpectatorSubsessionIdsContext : JsonSerializerContext
{ }
