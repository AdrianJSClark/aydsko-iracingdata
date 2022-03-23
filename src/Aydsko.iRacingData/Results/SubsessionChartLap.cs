// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Results;

public class SubsessionChartLap : SubsessionLap
{
    [JsonPropertyName("lap_position")]
    public int LapPosition { get; set; }
    [JsonPropertyName("interval")]
    public int? Interval { get; set; }
    [JsonPropertyName("interval_units")]
    public string IntervalUnits { get; set; } = null!;
    [JsonPropertyName("fastest_lap")]
    public bool FastestLap { get; set; }
}

[JsonSerializable(typeof(SubsessionChartLap[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SubsessionChartLapArrayContext : JsonSerializerContext
{ }
