// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

public class SessionResults
{
    [JsonPropertyName("simsession_number")]
    public int SimSessionNumber { get; set; }

    [JsonPropertyName("simsession_type")]
    public int SimSessionType { get; set; }

    [JsonPropertyName("simsession_type_name")]
    public string SimSessionTypeName { get; set; } = default!;

    [JsonPropertyName("simsession_subtype")]
    public int SimSessionSubType { get; set; }

    [JsonPropertyName("simsession_name")]
    public string SimSessionName { get; set; } = default!;

    [JsonPropertyName("results")]
    public Result[] Results { get; set; } = default!;

    [JsonPropertyName("weather_result")]
    public SessionResultsWeather WeatherResult { get; set; } = default!;
}
