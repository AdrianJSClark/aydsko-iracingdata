// © 2025 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Constants;

namespace Aydsko.iRacingData.Leagues;

public class LeagueSessionWeather
{
    [JsonPropertyName("allow_fog")]
    public bool AllowFog { get; set; }

    [JsonPropertyName("fog")]
    public int Fog { get; set; }

    [JsonPropertyName("precip_option")]
    public int PrecipitationOption { get; set; }

    [JsonPropertyName("rel_humidity")]
    public int RelativeHumidity { get; set; }

    [JsonPropertyName("skies")]
    public int Skies { get; set; }

    [JsonPropertyName("temp_units")]
    public int TemperatureUnits { get; set; }

    [JsonPropertyName("temp_value")]
    public int TemperatureValue { get; set; }

    [JsonPropertyName("track_water")]
    public int TrackWater { get; set; }

    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("wind_dir")]
    public int WindDir { get; set; }

    [JsonIgnore]
    public WindDirection WindDirection => (WindDirection)WindDir;

    /// <summary>Wind units.</summary>
    /// <remarks>
    /// Maps to one of the <c>weather_wind_speed_units</c> lookup values retrieved
    /// from the <see cref="IDataClient.GetLookupsAsync(CancellationToken)"/> call.
    /// </remarks>
    [JsonPropertyName("wind_units")]
    public int WindUnits { get; set; }

    [JsonPropertyName("wind_value")]
    public int WindValue { get; set; }

    [JsonPropertyName("weather_var_initial")]
    public int WeatherVariationInitial { get; set; }

    [JsonPropertyName("weather_var_ongoing")]
    public int WeatherVariationOngoing { get; set; }

    [JsonPropertyName("weather_url")]
    public string? WeatherUrl { get; set; }

    [JsonPropertyName("weather_summary")]
    public LeagueWeatherSummary? WeatherSummary { get; set; }
}
