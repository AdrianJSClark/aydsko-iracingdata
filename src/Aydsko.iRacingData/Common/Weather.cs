// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Constants;

namespace Aydsko.iRacingData.Common;

public class Weather
{
    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonIgnore, Obsolete("Use \"TemperatureUnits\" property instead.")]
    public int TempUnits { get => TemperatureUnits; set => TemperatureUnits = value; }

    [JsonPropertyName("temp_units")]
    public int TemperatureUnits { get; set; }

    [JsonIgnore, Obsolete("Use \"TemperatureValue\" property instead.")]
    public int TempValue { get => TemperatureValue; set => TemperatureValue = value; }

    [JsonPropertyName("temp_value")]
    public int TemperatureValue { get; set; }

    [JsonIgnore, Obsolete("Use \"RelativeHumidity\" property instead.")]
    public int RelHumidity { get => RelativeHumidity; set => RelativeHumidity = value; }

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
    public int WeatherVarInitial { get; set; }

    [JsonPropertyName("weather_var_ongoing")]
    public int WeatherVarOngoing { get; set; }

    [JsonPropertyName("time_of_day")]
    public int TimeOfDay { get; set; }
}
