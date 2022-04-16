// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Results;

public class SeasonResults
{
    [JsonPropertyName("results_list")]
    public SeasonRaceResult[] ResultsList { get; set; } = Array.Empty<SeasonRaceResult>();

    [JsonPropertyName("event_type")]
    public EventType EventType { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("race_week_num")]
    public int RaceWeekNumber { get; set; }
}

[JsonSerializable(typeof(SeasonResults)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SeasonResultsContext : JsonSerializerContext
{ }
