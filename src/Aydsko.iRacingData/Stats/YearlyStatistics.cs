// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Stats;

public class YearlyStatistics
{
    [JsonPropertyName("category_id")]
    public int CategoryId { get; set; }

    [JsonPropertyName("category")]
    public string? Category { get; set; }

    [JsonPropertyName("starts")]
    public int Starts { get; set; }

    [JsonPropertyName("wins")]
    public int Wins { get; set; }

    [JsonPropertyName("top5")]
    public int Top5 { get; set; }

    [JsonPropertyName("poles")]
    public int Poles { get; set; }

    [JsonPropertyName("avg_start_position")]
    public int AverageStartPosition { get; set; }

    [JsonPropertyName("avg_finish_position")]
    public int AverageFinishPosition { get; set; }

    [JsonPropertyName("laps")]
    public int Laps { get; set; }

    [JsonPropertyName("laps_led")]
    public int LapsLed { get; set; }

    [JsonPropertyName("avg_incidents")]
    public float AverageIncidents { get; set; }

    [JsonPropertyName("avg_points")]
    public int AveragePoints { get; set; }

    [JsonPropertyName("win_percentage")]
    public float WinPercentage { get; set; }

    [JsonPropertyName("top5_percentage")]
    public float Top5Percentage { get; set; }

    [JsonPropertyName("laps_led_percentage")]
    public float LapsLedPercentage { get; set; }

    [JsonPropertyName("total_club_points")]
    public int TotalClubPoints { get; set; }

    [JsonPropertyName("year")]
    public int Year { get; set; }
}
