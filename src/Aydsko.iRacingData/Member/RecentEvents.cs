// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Member;

public class RecentEvents
{
    [JsonPropertyName("event_type")]
    public string EventType { get; set; } = default!;

    [JsonPropertyName("subsession_id")]
    public int SubsessionId { get; set; }

    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; set; }

    [JsonPropertyName("event_id")]
    public int EventId { get; set; }

    [JsonPropertyName("event_name")]
    public string EventName { get; set; } = default!;

    [JsonPropertyName("simsession_type")]
    public int SimsessionType { get; set; }

    [JsonPropertyName("starting_position")]
    public int StartingPosition { get; set; }

    [JsonPropertyName("finish_position")]
    public int FinishPosition { get; set; }

    [JsonPropertyName("best_lap_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? BestLapTime { get; set; }

    [JsonPropertyName("percent_rank")]
    public decimal PercentRank { get; set; }

    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("car_name")]
    public string CarName { get; set; } = default!;

    [JsonPropertyName("logo_url")]
    public string? LogoUrl { get; set; }

    [JsonPropertyName("track")]
    public Track Track { get; set; } = default!;
}
