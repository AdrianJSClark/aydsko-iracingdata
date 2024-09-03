namespace Aydsko.iRacingData.Series;

public class ForecastOptions
{
    [JsonPropertyName("forecast_type")]
    public int ForecastType { get; set; }

    [JsonPropertyName("precipitation")]
    public int Precipitation { get; set; }

    [JsonPropertyName("skies")]
    public int Skies { get; set; }

    [JsonPropertyName("stop_precip")]
    public int StopPrecip { get; set; }

    [JsonPropertyName("temperature")]
    public int Temperature { get; set; }

    [JsonPropertyName("weather_seed")]
    public long WeatherSeed { get; set; }

    [JsonPropertyName("wind_dir")]
    public int WindDir { get; set; }

    [JsonPropertyName("wind_speed")]
    public int WindSpeed { get; set; }
}
