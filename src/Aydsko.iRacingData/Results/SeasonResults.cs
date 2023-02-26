// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

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

    /// <summary>An index number identifying the race week.</summary>
    /// <remarks>The iRacing Data API works with zero-based race weeks, most people will use one-based.</remarks>
    /// <seealso cref="RaceWeekNumber" />
    [JsonPropertyName("race_week_num")]
    public int RaceWeekIndex { get; set; }

    /// <summary>The number of the race week within the season.</summary>
    [JsonIgnore]
    public int RaceWeekNumber => RaceWeekIndex + 1;
}

[JsonSerializable(typeof(SeasonResults)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SeasonResultsContext : JsonSerializerContext
{ }
