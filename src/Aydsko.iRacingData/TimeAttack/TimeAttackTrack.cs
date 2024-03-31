// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Constants;

namespace Aydsko.iRacingData.TimeAttack;

public class TimeAttackTrack
{
    [JsonPropertyName("trackid")]
    public int Trackid { get; set; }

    [JsonPropertyName("w_time_of_day")]
    public int WTimeOfDay { get; set; }

    [JsonPropertyName("w_type")]
    public int WType { get; set; }

    [JsonPropertyName("w_temp")]
    public int WTemp { get; set; }

    [JsonPropertyName("w_humidity")]
    public int WHumidity { get; set; }

    [JsonPropertyName("w_wind_dir")]
    public int WWindDir { get; set; }

    [JsonIgnore]
    public WindDirection WeatherWindDirection => (WindDirection)WWindDir;

    [JsonPropertyName("w_wind_speed")]
    public int WWindSpeed { get; set; }

    [JsonPropertyName("w_skies")]
    public int WSkies { get; set; }

    [JsonPropertyName("w_fog_level")]
    public int WFogLevel { get; set; }

    [JsonPropertyName("w_wind_speed_units")]
    public int WWindSpeedUnits { get; set; }

    [JsonPropertyName("w_temp_units")]
    public int WTempUnits { get; set; }
}
