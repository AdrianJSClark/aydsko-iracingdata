// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Member;

public class MemberBest
{
    [JsonPropertyName("track")]
    public Track Track { get; set; } = default!;

    [JsonPropertyName("event_type")]
    public string EventType { get; set; } = default!;

    [JsonPropertyName("best_lap_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? BestLapTime { get; set; }
}
