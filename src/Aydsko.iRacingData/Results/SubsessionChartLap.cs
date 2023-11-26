// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

/// <summary>Contains details about a particular lap for a particular driver suitable for creating a lap chart.</summary>
public class SubsessionChartLap : SubsessionLap
{
    /// <summary>Contains the position of the car at the end of this lap.</summary>
    [JsonPropertyName("lap_position")]
    public int LapPosition { get; set; }

    /// <summary>Contains the value of the interval at the end of this lap to the leader.</summary>
    [JsonPropertyName("interval")]
    public long? IntervalRaw { get; set; }

    /// <summary>Describes the unit for the <see cref="IntervalRaw"/> value. Can be either <c>ms</c> for milliseconds or <c>lap</c> if the interval is one or more laps.</summary>
    [JsonPropertyName("interval_units")]
    public string IntervalUnits { get; set; } = null!;

    /// <summary>If <see cref="IntervalUnits"/> is <c>ms</c> and <see cref="IntervalRaw"/> is available this returns the interval to the leader. Otherwise returns <see langword="null"/>.</summary>
    /// <remarks>There's no reasonable value that can be used to return a value here for lapped cars so if the interval is a lap count we just return <see langword="null"/>.</remarks>
    [JsonIgnore]
    public TimeSpan? Interval => IntervalUnits == "ms" && IntervalRaw is not null ? TimeSpan.FromMilliseconds((double)IntervalRaw) : null;

    /// <summary>
    /// Indicates if this lap time was the fastest lap of the race across all drivers.
    /// </summary>
    [JsonPropertyName("fastest_lap")]
    public bool FastestLap { get; set; }
}

[JsonSerializable(typeof(SubsessionChartLap[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SubsessionChartLapArrayContext : JsonSerializerContext
{ }
