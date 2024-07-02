// © 2023-2024 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

public class ResultsWeather : Weather
{
    /// <summary>The date and time within the simulation that this weather is related to.</summary>
    [JsonPropertyName("simulated_start_time")]
    public DateTime SimulatedStart { get; set; }

    /// <summary>If fog was allowed.</summary>
    [JsonPropertyName("allow_fog")]
    public bool AllowFog { get; set; }

    /// <summary>The level of water on the track.</summary>
    [JsonPropertyName("track_water")]
    public int TrackWater { get; set; }

    /// <summary>Percentage of session time it was raining.</summary>
    [JsonPropertyName("precip_time_pct")]
    public decimal PrecipitationTimePercentage { get; set; }

    /// <summary>Amount of rain that fell in millimetres.</summary>
    [JsonPropertyName("precip_mm_final_session")]
    public decimal PrecipitationMillimetresFinalSession { get; set; }

    /// <summary>The rain option value.</summary>
    [JsonPropertyName("precip_option")]
    public decimal PrecipitationOption { get; set; }

    /// <summary>Amount of rain that fell two hours before the final session in millimetres.</summary>
    [JsonPropertyName("precip_mm2hr_before_final_session")]
    public decimal PrecipitationMillimetres2HoursBeforeFinalSession { get; set; }

    /// <summary>How much the time is accelerated by.</summary>
    [JsonPropertyName("simulated_time_multiplier")]
    public int SimulatedTimeMultiplier { get; set; }
}
