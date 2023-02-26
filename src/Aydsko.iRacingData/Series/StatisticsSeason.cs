// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

public class StatisticsSeason : SeasonBase
{
    [JsonPropertyName("season_short_name")]
    public string SeasonShortName { get; set; } = default!;

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("car_classes")]
    public CarClass[] CarClasses { get; set; } = default!;

    [JsonPropertyName("race_weeks")]
    public RaceWeeks[] RaceWeeks { get; set; } = default!;
}
