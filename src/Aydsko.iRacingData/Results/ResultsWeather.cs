// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Results;

public class ResultsWeather : Weather
{
    [JsonPropertyName("simulated_start_utc_time")]
    public DateTimeOffset SimulatedStartUtcTime { get; set; }

    [JsonPropertyName("simulated_start_utc_offset"), JsonConverter(typeof(UtcOffsetToTimeSpanConverter))]
    public TimeSpan SimulatedStartUtcOffset { get; set; }

    [JsonIgnore]
    public DateTimeOffset SimulatedStart => new(SimulatedStartUtcTime.DateTime.Add(SimulatedStartUtcOffset), SimulatedStartUtcOffset);

    [JsonPropertyName("allow_fog")]
    public bool AllowFog { get; set; }

    [JsonPropertyName("track_water")]
    public int TrackWater { get; set; }

    [JsonPropertyName("precip_time_pct")]
    public int PrecipitationTimePercentage { get; set; }

    [JsonPropertyName("precip_mm_final_session")]
    public int PrecipitationMillimetresFinalSession { get; set; }

    [JsonPropertyName("precip_option")]
    public int PrecipitationOption { get; set; }

    [JsonPropertyName("precip_mm2hr_before_final_session")]
    public int PrecipitationMillimetres2HoursBeforeFinalSession { get; set; }
}
