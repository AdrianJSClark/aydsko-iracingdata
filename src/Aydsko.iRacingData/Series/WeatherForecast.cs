using Aydsko.iRacingData.Constants;
using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Series;

public class WeatherForecast
{
    /// <summary>
    /// Offset from race start time
    /// </summary>
    [JsonPropertyName("time_offset"), JsonConverter(typeof(UtcOffsetToTimeSpanConverter))]
    public TimeSpan TimeOffset { get; set; }

    [JsonPropertyName("raw_air_temp"), JsonConverter(typeof(TwoDecimalPointsValueConverter))]
    public decimal RawAirTemp { get; set; }

    /// <summary>
    /// Precipitation chance in percentage
    /// </summary>
    [JsonPropertyName("precip_chance"), JsonConverter(typeof(TwoDecimalPointsValueConverter))]
    public decimal PrecipitationChance { get; set; }

    /// <summary>
    /// Index of the forecast
    /// </summary>
    [JsonPropertyName("index")]
    public int Index { get; set; }

    /// <summary>
    /// Is the sun up
    /// </summary>
    [JsonPropertyName("is_sun_up")]
    public bool IsSunUp { get; set; }

    /// <summary>
    /// Pressure in hPa
    /// </summary>
    [JsonPropertyName("pressure"), JsonConverter(typeof(OneDecimalPointValueConverter))]
    public decimal Pressure { get; set; }

    /// <summary>
    /// Wind direction in degrees
    /// </summary>
    [JsonPropertyName("wind_dir")]
    public int WindDir { get; set; }
    
    /// <summary>
    /// Rounded wind direction
    /// </summary>
    [JsonIgnore]
    public WindDirection WindDirection => ConvertDegreeToDirection(WindDir);

    /// <summary>
    /// Air temperature
    /// </summary>
    [JsonPropertyName("air_temp"), JsonConverter(typeof(TwoDecimalPointsValueConverter))]
    public decimal AirTemp { get; set; }

    [JsonPropertyName("valid_stats")]
    public bool ValidStats { get; set; }

    /// <summary>
    /// Is the session affected by the weather
    /// </summary>
    [JsonPropertyName("affects_session")]
    public bool AffectsSession { get; set; }

    /// <summary>
    /// Cloud cover in percentage
    /// </summary>
    [JsonPropertyName("cloud_cover"), JsonConverter(typeof(OneDecimalPointValueConverter))]
    public decimal CloudCover { get; set; }

    /// <summary>
    /// Relative humidity in percentage
    /// </summary>
    [JsonPropertyName("rel_humidity"), JsonConverter(typeof(TwoDecimalPointsValueConverter))]
    public decimal RelativeHumidity { get; set; }

    /// <summary>
    /// Wind speed (in meters per second)
    /// </summary>
    [JsonPropertyName("wind_speed"), JsonConverter(typeof(TwoDecimalPointsValueConverter))]
    public decimal WindSpeed { get; set; }

    /// <summary>
    /// Is precipitation allowed
    /// </summary>
    [JsonPropertyName("allow_precip")]
    public bool AllowPrecipitation { get; set; }
    
    /// <summary>
    /// Precipitation amount in millimeters
    /// </summary>
    [JsonPropertyName("precip_amount")] 
    public decimal PrecipitationAmount { get; set; }

    /// <summary>
    /// Date and time of the forecast
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
    
    private static WindDirection ConvertDegreeToDirection(int degree)
    {
        return (WindDirection)(int)Math.Round(((double)degree % 360) / 45);
    }
}

[JsonSerializable(typeof(List<WeatherForecast>)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class WeatherForecastArrayContext : JsonSerializerContext
{
   
}
