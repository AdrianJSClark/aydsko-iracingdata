// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

public class SeasonResultTrack
{
    [JsonPropertyName("track_id")]
    public int TrackId { get; set; }

    [JsonPropertyName("track_name")]
    public string? TrackName { get; set; }

    [JsonPropertyName("config_name")]
    public string? ConfigName { get; set; }
}
