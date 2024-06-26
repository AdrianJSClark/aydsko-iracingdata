﻿// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Constants;

namespace Aydsko.iRacingData.Leagues;

public class CustomerLeagueSessionWeather
{
    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("temp_units")]
    public int TemperatureUnits { get; set; }

    [JsonPropertyName("temp_value")]
    public int TemperatureValue { get; set; }

    [JsonPropertyName("rel_humidity")]
    public int RelativeHumidity { get; set; }

    [JsonPropertyName("fog")]
    public int Fog { get; set; }

    [JsonPropertyName("wind_dir")]
    public int WindDir { get; set; }

    [JsonIgnore]
    public WindDirection WindDirection => (WindDirection)WindDir;

    [JsonPropertyName("wind_units")]
    public int WindUnits { get; set; }

    [JsonPropertyName("wind_value")]
    public int WindValue { get; set; }

    [JsonPropertyName("skies")]
    public int Skies { get; set; }

    [JsonPropertyName("weather_var_initial")]
    public int WeatherVariationInitial { get; set; }

    [JsonPropertyName("weather_var_ongoing")]
    public int WeatherVariationOngoing { get; set; }

    [JsonPropertyName("time_of_day")]
    public int TimeOfDay { get; set; }

    [JsonPropertyName("simulated_start_time")]
    public DateTimeOffset SimulatedStartTime { get; set; } = default!;

    [JsonPropertyName("simulated_time_offset")]
    public int[] SimulatedTimeOffsets { get; set; } = Array.Empty<int>();

    [JsonPropertyName("simulated_time_multiplier")]
    public int SimulatedTimeMultiplier { get; set; }
}
