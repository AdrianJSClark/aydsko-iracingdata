// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

public class ResultsWeather : Weather
{
    [JsonPropertyName("simulated_start_utc_time")]
    public DateTimeOffset SimulatedStartUtcTime { get; set; }

    [JsonPropertyName("simulated_start_utc_offset")]
    public int SimulatedStartUtcOffset { get; set; }
}
