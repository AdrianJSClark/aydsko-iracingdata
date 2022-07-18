// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

public class ListOfSeasons
{
    [JsonPropertyName("season_year")]
    public int SeasonYear { get; set; }

    [JsonPropertyName("season_quarter")]
    public int SeasonQuarter { get; set; }

    [JsonPropertyName("seasons")]
    public ListSeason[] Seasons { get; set; } = default!;
}

[JsonSerializable(typeof(ListOfSeasons)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class ListOfSeasonsContext : JsonSerializerContext
{ }
