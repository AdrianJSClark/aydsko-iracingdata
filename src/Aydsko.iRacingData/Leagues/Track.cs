// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class Track
{
    [JsonPropertyName("track_id")]
    public int TrackId { get; set; }

    [JsonPropertyName("track_name")]
    public string TrackName { get; set; } = default!;

    [JsonPropertyName("config_name")]
    public string? ConfigName { get; set; }
}
