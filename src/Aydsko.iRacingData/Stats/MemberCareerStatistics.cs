// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Stats;

public class MemberCareerStatistics
{
    [JsonPropertyName("category_id")]
    public int CategoryId { get; set; }
    [JsonPropertyName("category")]
    public string Category { get; set; } = default!;
    [JsonPropertyName("starts")]
    public int Starts { get; set; }
    [JsonPropertyName("wins")]
    public int Wins { get; set; }
    [JsonPropertyName("top5")]
    public int Top5 { get; set; }
    [JsonPropertyName("poles")]
    public int Poles { get; set; }
    [JsonPropertyName("avg_start_position")]
    public int AvgStartPosition { get; set; }
    [JsonPropertyName("avg_finish_position")]
    public int AvgFinishPosition { get; set; }
    [JsonPropertyName("laps")]
    public int Laps { get; set; }
    [JsonPropertyName("laps_led")]
    public int LapsLed { get; set; }
    [JsonPropertyName("avg_incidents")]
    public float AvgIncidents { get; set; }
    [JsonPropertyName("avg_points")]
    public int AvgPoints { get; set; }
    [JsonPropertyName("win_percentage")]
    public float WinPercentage { get; set; }
    [JsonPropertyName("top5_percentage")]
    public float Top5Percentage { get; set; }
    [JsonPropertyName("laps_led_percentage")]
    public float LapsLedPercentage { get; set; }
    [JsonPropertyName("total_club_points")]
    public int TotalClubPoints { get; set; }
}
