// © 2025 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class LeagueWeatherSummary
{
    [JsonPropertyName("max_precip_rate")]
    public decimal MaxPrecipitationRate { get; set; }

    [JsonPropertyName("max_precip_rate_desc")]
    public string? MaxPrecipitationRateDesc { get; set; }

    [JsonPropertyName("precip_chance")]
    public decimal PrecipitationChance { get; set; }

    [JsonPropertyName("skies_high")]
    public int SkiesHigh { get; set; }

    [JsonPropertyName("skies_low")]
    public int SkiesLow { get; set; }

    [JsonPropertyName("temp_high")]
    public decimal TemperatureHigh { get; set; }

    [JsonPropertyName("temp_low")]
    public decimal TemperatureLow { get; set; }

    [JsonPropertyName("temp_units")]
    public int TemperatureUnits { get; set; }

    [JsonPropertyName("wind_high")]
    public decimal WindHigh { get; set; }

    [JsonPropertyName("wind_low")]
    public decimal WindLow { get; set; }

    /// <summary>Wind units.</summary>
    /// <remarks>
    /// Maps to one of the <c>weather_wind_speed_units</c> lookup values retrieved
    /// from the <see cref="IDataClient.GetLookupsAsync(CancellationToken)"/> call.
    /// </remarks>
    [JsonPropertyName("wind_units")]
    public int WindUnits { get; set; }
}
