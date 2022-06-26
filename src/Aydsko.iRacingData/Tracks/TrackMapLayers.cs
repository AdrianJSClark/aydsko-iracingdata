// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Tracks;

public class MapLayers
{
    [JsonPropertyName("background")]
    public string Background { get; set; } = default!;

    [JsonPropertyName("inactive")]
    public string Inactive { get; set; } = default!;

    [JsonPropertyName("active")]
    public string Active { get; set; } = default!;

    [JsonPropertyName("pitroad")]
    public string PitRoad { get; set; } = default!;

    [JsonPropertyName("start-finish")]
    public string StartFinish { get; set; } = default!;

    [JsonPropertyName("turns")]
    public string Turns { get; set; } = default!;
}
