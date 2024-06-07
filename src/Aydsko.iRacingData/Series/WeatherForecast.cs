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

    /// <summary>
    /// Can be ignored. Uncorrected air temperature. Used by the sim regarding relative and absolute humidity
    /// </summary>
    [JsonPropertyName("raw_air_temp"), JsonConverter(typeof(TwoDecimalPointsValueConverter))]
    public decimal RawAirTemp { get; set; }

    /// <summary>
    /// Precipitation chance in percentage
    /// </summary>
    [JsonPropertyName("precip_chance"), JsonConverter(typeof(TwoDecimalPointsValueConverter))]
    public decimal PrecipitationChance { get; set; }

    /// <summary>
    /// Index of the key frame
    /// </summary>
    [JsonPropertyName("index")]
    public int Index { get; set; }

    /// <summary>
    /// Is the sun up
    /// </summary>
    [JsonPropertyName("is_sun_up")]
    public bool IsSunUp { get; set; }

    /// <summary>
    /// Atmospheric pressure in hectopascals
    /// </summary>
    [JsonPropertyName("pressure"), JsonConverter(typeof(OneDecimalPointValueConverter))]
    public decimal Pressure { get; set; }

    /// <summary>
    /// Wind direction in degrees (0 - 359)
    /// </summary>
    [JsonPropertyName("wind_dir")]
    public int WindDir { get; set; }
    
    /// <summary>
    /// Rounded wind direction
    /// </summary>
    [JsonIgnore]
    public WindDirection WindDirection => ConvertDegreeToDirection(WindDir);

    /// <summary>
    /// Air temperature in Celcius corrected to be within allowed bounds
    /// </summary>
    [JsonPropertyName("air_temp"), JsonConverter(typeof(TwoDecimalPointsValueConverter))]
    public decimal AirTemp { get; set; }

    /// <summary>
    /// Can be ignore. Are there are rain statistics available for the period
    /// </summary>
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
    /// Is precipitation allowed to occur during the period
    /// </summary>
    [JsonPropertyName("allow_precip")]
    public bool AllowPrecipitation { get; set; }
    
    /// <summary>
    /// Precipitation amount in millimeters per hour
    /// </summary>
    [JsonPropertyName("precip_amount"), JsonConverter(typeof(OneDecimalPointValueConverter))] 
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
