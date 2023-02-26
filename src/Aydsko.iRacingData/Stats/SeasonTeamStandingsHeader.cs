// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Stats;

public class SeasonTeamStandingsHeader
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("season_name")]
    public string SeasonName { get; set; } = null!;

    [JsonPropertyName("season_short_name")]
    public string SeasonShortName { get; set; } = null!;

    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }

    [JsonPropertyName("series_name")]
    public string SeriesName { get; set; } = null!;

    [JsonPropertyName("car_class_id")]
    public int CarClassId { get; set; }

    /// <summary>An index number identifying the race week.</summary>
    /// <remarks>The iRacing Data API works with zero-based race weeks, most people will use one-based.</remarks>
    /// <seealso cref="RaceWeekNumber" />
    [JsonPropertyName("race_week_num")]
    public int RaceWeekIndex { get; set; }

    /// <summary>The number of the race week within the season.</summary>
    [JsonIgnore]
    public int RaceWeekNumber => RaceWeekIndex + 1;

    [JsonPropertyName("chunk_info")]
    public ChunkInfo ChunkInfo { get; set; } = null!;

    [JsonPropertyName("last_updated")]
    public DateTimeOffset LastUpdated { get; set; }
}

[JsonSerializable(typeof(SeasonTeamStandingsHeader)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SeasonTeamStandingsHeaderContext : JsonSerializerContext
{ }
