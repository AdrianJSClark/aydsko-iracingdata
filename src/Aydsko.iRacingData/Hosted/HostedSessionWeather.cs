// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Hosted;

public class HostedSessionWeather : Weather
{
    [JsonPropertyName("simulated_start_time")]
    public DateTime SimulatedStartTime { get; set; }

    [JsonPropertyName("simulated_time_offsets")]
    public int[] SimulatedTimeOffsets { get; set; } = Array.Empty<int>();

    [JsonPropertyName("simulated_time_multiplier")]
    public int SimulatedTimeMultiplier { get; set; }
}
