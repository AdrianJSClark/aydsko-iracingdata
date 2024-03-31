// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Constants;

namespace Aydsko.iRacingData.Leagues;

public class Weather
{
    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("temp_units")]
    public int TempUnits { get; set; }

    [JsonPropertyName("temp_value")]
    public int TempValue { get; set; }

    [JsonPropertyName("rel_humidity")]
    public int RelHumidity { get; set; }

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

    [JsonPropertyName("allow_fog")]
    public bool AllowFog { get; set; }

    [JsonPropertyName("track_water")]
    public int TrackWater { get; set; }

    [JsonPropertyName("precip_option")]
    public int PrecipOption { get; set; }
}
