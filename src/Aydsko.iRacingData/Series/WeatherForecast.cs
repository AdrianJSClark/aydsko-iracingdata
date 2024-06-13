using Aydsko.iRacingData.Constants;
using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Series;

/// <summary>Contains a point-in-time weather forecast.</summary>
/// <remarks>
/// These results are a condensed forecast, compared to the hourly
/// forecast available within the session inside the simulator.
/// </remarks>
/// <seealso href="https://forums.iracing.com/discussion/comment/541516#Comment_541516"/>
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
    public decimal RawAirTemperature { get; set; }

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
    public int WindDirectionDegrees { get; set; }

    /// <summary>Approximate wind direction.</summary>
    /// <seealso cref="Constants.WindDirection" />
    [JsonIgnore]
    public WindDirection WindDirection => ConvertDegreeToDirection(WindDirectionDegrees);

    /// <summary>Air temperature in degrees Celsius corrected to be within allowed bounds.</summary>
    [JsonPropertyName("air_temp"), JsonConverter(typeof(TwoDecimalPointsValueConverter))]
    public decimal AirTemperature { get; set; }

    /// <summary>Can be ignored. Indicates if there are rain statistics available for the period.</summary>
    [JsonPropertyName("valid_stats")]
    public bool ValidStatistics { get; set; }

    /// <summary>Indicates if a session is expected to be running at some point during the period this forecast covers.</summary>
    [JsonPropertyName("affects_session")]
    public bool AffectsSession { get; set; }

    /// <summary>The percentage of the sky with cloud cover.</summary>
    [JsonPropertyName("cloud_cover"), JsonConverter(typeof(OneDecimalPointValueConverter))]
    public decimal CloudCoverPercentage { get; set; }

    /// <summary>Relative humidity percentage.</summary>
    [JsonPropertyName("rel_humidity"), JsonConverter(typeof(TwoDecimalPointsValueConverter))]
    public decimal RelativeHumidity { get; set; }

    /// <summary>Wind speed (in meters per second)</summary>
    [JsonPropertyName("wind_speed"), JsonConverter(typeof(TwoDecimalPointsValueConverter))]
    public decimal WindSpeed { get; set; }

    /// <summary>Indicates if precipitation is allowed to occur during the period.</summary>
    [JsonPropertyName("allow_precip")]
    public bool AllowPrecipitation { get; set; }

    /// <summary>Precipitation rate in millimeters per hour.</summary>
    [JsonPropertyName("precip_amount"), JsonConverter(typeof(OneDecimalPointValueConverter))]
    public decimal PrecipitationAmount { get; set; }

    /// <summary>The instant this forecast block begins in track local time.</summary>
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
