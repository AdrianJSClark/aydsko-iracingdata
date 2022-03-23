// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Results;

public class SubsessionLap
{
    [JsonPropertyName("group_id")]
    public int GroupId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = null!;
    [JsonPropertyName("lap_number")]
    public int LapNumber { get; set; }
    [JsonPropertyName("flags")]
    public int Flags { get; set; }
    [JsonPropertyName("incident")]
    public bool Incident { get; set; }
    [JsonPropertyName("session_time")]
    public int SessionTime { get; set; }
    [JsonPropertyName("session_start_time")]
    public int? SessionStartTime { get; set; }
    [JsonPropertyName("lap_time")]
    public int LapTime { get; set; }
    [JsonPropertyName("team_fastest_lap")]
    public bool TeamFastestLap { get; set; }
    [JsonPropertyName("personal_best_lap")]
    public bool PersonalBestLap { get; set; }
    [JsonPropertyName("helmet")]
    public Helmet Helmet { get; set; } = null!;
    [JsonPropertyName("license_level")]
    public int LicenseLevel { get; set; }
    [JsonPropertyName("car_number")]
    public string CarNumber { get; set; } = null!;
    [JsonPropertyName("lap_events")]
    public string[] LapEvents { get; set; } = null!;
    [JsonPropertyName("ai")]
    public bool Ai { get; set; }
}

[JsonSerializable(typeof(SubsessionLap[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SubsessionLapArrayContext : JsonSerializerContext
{ }
