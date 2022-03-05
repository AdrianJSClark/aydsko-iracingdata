// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Tracks;

public class TrackTypes
{
    [JsonPropertyName("track_type")]
    public string TrackType { get; set; } = default!;
}
