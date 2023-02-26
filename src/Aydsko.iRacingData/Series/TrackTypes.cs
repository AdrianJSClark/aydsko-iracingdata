// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

public class TrackTypes
{
    [JsonPropertyName("track_type")]
    public string TrackType { get; set; } = default!;
}
