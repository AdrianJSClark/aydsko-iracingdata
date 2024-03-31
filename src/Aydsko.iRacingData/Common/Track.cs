// © 2024 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Common;

public class Track
{
    [JsonPropertyName("track_id")]
    public int TrackId { get; set; }

    [JsonPropertyName("track_name")]
    public string TrackName { get; set; } = default!;

    [JsonPropertyName("config_name"), JsonConverter(typeof(TrackConfigNameNaConverter))]
    public string? ConfigName { get; set; } = default!;

    [JsonPropertyName("category_id")]
    public int? CategoryId { get; set; }

    [JsonPropertyName("category")]
    public string? Category { get; set; }
}
