// © 2024 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Constants;

namespace Aydsko.iRacingData.Results;

public class SessionResultsWeather
{
    /// <summary>Average skies.</summary>
    [JsonPropertyName("avg_skies")]
    public int AverageSkies { get; set; }

    /// <summary>Average cloud cover percentage.</summary>
    [JsonPropertyName("avg_cloud_cover_pct")]
    public decimal AverageCloudCoverPercentage { get; set; }

    /// <summary>Minimum cloud cover percentage.</summary>
    [JsonPropertyName("min_cloud_cover_pct")]
    public decimal MinimumCloudCoverPercentage { get; set; }

    /// <summary>Maximum cloud cover percentage.</summary>
    [JsonPropertyName("max_cloud_cover_pct")]
    public decimal MaximumCloudCoverPercentage { get; set; }

    /// <summary>Temperature units used in these values.</summary>
    /// <remarks>
    /// Maps to one of the <c>weather_wind_speed_units</c> lookup values retrieved
    /// from the <see cref="IDataClient.GetLookupsAsync(CancellationToken)"/> call.
    /// </remarks>
    [JsonPropertyName("temp_units")]
    public int TemperatureUnits { get; set; }

    /// <summary>Average temperature.</summary>
    [JsonPropertyName("avg_temp")]
    public decimal AverageTemperature { get; set; }

    /// <summary>Minimum temperature.</summary>
    [JsonPropertyName("min_temp")]
    public decimal MinimumTemperature { get; set; }

    /// <summary>Maximum temperature.</summary>
    [JsonPropertyName("max_temp")]
    public decimal MaximumTemperature { get; set; }

    /// <summary>Average relative humidity.</summary>
    [JsonPropertyName("avg_rel_humidity")]
    public decimal AverageRelativeHumidity { get; set; }

    /// <summary>Average wind speed.</summary>
    [JsonPropertyName("avg_wind_speed")]
    public decimal AverageWindSpeed { get; set; }

    /// <summary>Minimum wind speed.</summary>
    [JsonPropertyName("min_wind_speed")]
    public decimal MinimumWindSpeed { get; set; }

    /// <summary>Maximum wind speed.</summary>
    [JsonPropertyName("max_wind_speed")]
    public decimal MaximumWindSpeed { get; set; }

    /// <summary>Average wind direction.</summary>
    [JsonPropertyName("avg_wind_dir")]
    public WindDirection AverageWindDirection { get; set; }

    /// <summary>Maximum fog.</summary>
    [JsonPropertyName("max_fog")]
    public decimal MaximumFog { get; set; }

    /// <summary>Fog time percentage.</summary>
    [JsonPropertyName("fog_time_pct")]
    public decimal FogTimePercentage { get; set; }

    /// <summary>Precipitation time percentage.</summary>
    [JsonPropertyName("precip_time_pct")]
    public decimal PrecipitationTimePercentage { get; set; }

    /// <summary>Precipitation in millimetres.</summary>
    [JsonPropertyName("precip_mm")]
    public decimal PrecipitationMillimetres { get; set; }

    /// <summary>Precipitation in millimetres 2 hours before session.</summary>
    [JsonPropertyName("precip_mm2hr_before_session")]
    public decimal PrecipitationMillimetres2HoursBeforeSession { get; set; }
}
