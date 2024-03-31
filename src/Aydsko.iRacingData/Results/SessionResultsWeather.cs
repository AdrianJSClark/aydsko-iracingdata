// © 2024 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

public class SessionResultsWeather
{
    /// <summary>Average skies.</summary>
    [JsonPropertyName("avg_skies")]
    public int AverageSkies { get; set; }

    /// <summary>Average cloud cover percentage.</summary>
    [JsonPropertyName("avg_cloud_cover_pct")]
    public double AverageCloudCoverPercentage { get; set; }

    /// <summary>Minimum cloud cover percentage.</summary>
    [JsonPropertyName("min_cloud_cover_pct")]
    public double MinimumCloudCoverPercentage { get; set; }

    /// <summary>Maximum cloud cover percentage.</summary>
    [JsonPropertyName("max_cloud_cover_pct")]
    public double MaximumCloudCoverPercentage { get; set; }

    /// <summary>Temperature units.</summary>
    [JsonPropertyName("temp_units")]
    public int TemperatureUnits { get; set; }

    /// <summary>Average temperature.</summary>
    [JsonPropertyName("avg_temp")]
    public double AverageTemperature { get; set; }

    /// <summary>Minimum temperature.</summary>
    [JsonPropertyName("min_temp")]
    public double MinimumTemperature { get; set; }

    /// <summary>Maximum temperature.</summary>
    [JsonPropertyName("max_temp")]
    public double MaximumTemperature { get; set; }

    /// <summary>Average relative humidity.</summary>
    [JsonPropertyName("avg_rel_humidity")]
    public double AverageRelativeHumidity { get; set; }

    /// <summary>Average wind speed.</summary>
    [JsonPropertyName("avg_wind_speed")]
    public double AverageWindSpeed { get; set; }

    /// <summary>Minimum wind speed.</summary>
    [JsonPropertyName("min_wind_speed")]
    public double MinimumWindSpeed { get; set; }

    /// <summary>Maximum wind speed.</summary>
    [JsonPropertyName("max_wind_speed")]
    public double MaximumWindSpeed { get; set; }

    /// <summary>Average wind direction.</summary>
    [JsonPropertyName("avg_wind_dir")]
    public int AverageWindDirection { get; set; }

    /// <summary>Maximum fog.</summary>
    [JsonPropertyName("max_fog")]
    public int MaximumFog { get; set; }

    /// <summary>Fog time percentage.</summary>
    [JsonPropertyName("fog_time_pct")]
    public int FogTimePercentage { get; set; }

    /// <summary>Precipitation time percentage.</summary>
    [JsonPropertyName("precip_time_pct")]
    public int PrecipitationTimePercentage { get; set; }

    /// <summary>Precipitation in millimetres.</summary>
    [JsonPropertyName("precip_mm")]
    public int PrecipitationMillimetres { get; set; }

    /// <summary>Precipitation in millimetres 2 hours before session.</summary>
    [JsonPropertyName("precip_mm2hr_before_session")]
    public int PrecipitationMillimetres2HoursBeforeSession { get; set; }
}
