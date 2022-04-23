// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

public class Season
{
    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }

    [JsonPropertyName("season_name")]
    public string SeasonName { get; set; } = default!;

    [JsonPropertyName("season_short_name")]
    public string SeasonShortName { get; set; } = default!;

    [JsonPropertyName("season_year")]
    public int SeasonYear { get; set; }

    [JsonPropertyName("season_quarter")]
    public int SeasonQuarter { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("official")]
    public bool Official { get; set; }

    [JsonPropertyName("driver_changes")]
    public bool DriverChanges { get; set; }

    [JsonPropertyName("car_classes")]
    public CarClass[] CarClasses { get; set; } = default!;

    [JsonPropertyName("race_weeks")]
    public RaceWeeks[] RaceWeeks { get; set; } = default!;
}
