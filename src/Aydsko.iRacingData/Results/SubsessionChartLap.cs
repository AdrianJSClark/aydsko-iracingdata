// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

public class SubsessionChartLap : SubsessionLap
{
    [JsonPropertyName("lap_position")]
    public int LapPosition { get; set; }

    [JsonPropertyName("interval")]
    public long? IntervalRaw { get; set; }

    [JsonPropertyName("interval_units")]
    public string IntervalUnits { get; set; } = null!;

    // If there's one thing iRacing is consistent about, it is inconsistency!
    // Normally an "interval" would be in ten-thousandth's of a second (i.e. 1 minute = "600000")
    // Here they have the "interval" and "interval_units". In all the results I've harvested, the units are "ms" (obviously "milliseconds")
    // So we can't use the normal "JsonConverter(typeof(TenThousandthSecondDurationConverter))" on the "interval" field.
    [JsonIgnore]
    public TimeSpan? Interval => (IntervalRaw, IntervalUnits) switch
    {
        (null, _) => null,
        (_, "ms") => (TimeSpan?)TimeSpan.FromMilliseconds((double)IntervalRaw),
        _ => throw new NotSupportedException($"Unsupported \"IntervalUnits\" value for CustomerId \"{CustomerId}\" on lap number \"{LapNumber}\"")
    };

    [JsonPropertyName("fastest_lap")]
    public bool FastestLap { get; set; }
}

[JsonSerializable(typeof(SubsessionChartLap[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SubsessionChartLapArrayContext : JsonSerializerContext
{ }
