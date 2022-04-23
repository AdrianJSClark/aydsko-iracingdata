// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

public class StatisticsSeries
{
    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }

    [JsonPropertyName("series_name")]
    public string SeriesName { get; set; } = default!;

    [JsonPropertyName("series_short_name")]
    public string SeriesShortName { get; set; } = default!;

    [JsonPropertyName("category_id")]
    public int CategoryId { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; } = default!;

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("official")]
    public bool Official { get; set; }

    [JsonPropertyName("seasons")]
    public Season[] Seasons { get; set; } = default!;
}

[JsonSerializable(typeof(StatisticsSeries[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class StatisticsSeriesArrayContext : JsonSerializerContext
{ }
