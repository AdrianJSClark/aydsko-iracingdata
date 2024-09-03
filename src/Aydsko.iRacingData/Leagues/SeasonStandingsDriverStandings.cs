// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class SeasonStandingsDriverStandings
{
    [JsonPropertyName("rownum")]
    public int RowNumber { get; set; }

    [JsonPropertyName("position")]
    public int Position { get; set; }

    [JsonPropertyName("driver")]
    public SeasonStandingsDriver Driver { get; set; } = null!;

    [JsonPropertyName("car_number")]
    public string CarNumber { get; set; } = default!;

    [JsonPropertyName("driver_nickname")]
    public string DriverNickname { get; set; } = default!;

    [JsonPropertyName("wins")]
    public int Wins { get; set; }

    [JsonPropertyName("average_start")]
    public int AverageStart { get; set; }

    [JsonPropertyName("average_finish")]
    public int AverageFinish { get; set; }

    [JsonPropertyName("base_points")]
    public int BasePoints { get; set; }

    [JsonPropertyName("negative_adjustments")]
    public int NegativeAdjustments { get; set; }

    [JsonPropertyName("positive_adjustments")]
    public int PositiveAdjustments { get; set; }

    [JsonPropertyName("total_adjustments")]
    public int TotalAdjustments { get; set; }

    [JsonPropertyName("total_points")]
    public int TotalPoints { get; set; }
}
