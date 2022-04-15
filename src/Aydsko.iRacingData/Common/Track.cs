// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Common;

/// <summary>Information about a track.</summary>
public class Track
{
    /// <summary>Identifier for the track.</summary>
    [JsonPropertyName("track_id")]
    public int TrackId { get; set; }

    /// <summary>Name of the track.</summary>
    [JsonPropertyName("track_name")]
    public string TrackName { get; set; } = default!;

    /// <summary>Track configuration name.</summary>
    [JsonPropertyName("config_name")]
    public string ConfigName { get; set; } = default!;

    /// <summary>Track category identifier</summary>
    [JsonPropertyName("category_id")]
    public int? CategoryId { get; set; }

    /// <summary>Track category name.</summary>
    [JsonPropertyName("category")]
    public string? Category { get; set; }
}
