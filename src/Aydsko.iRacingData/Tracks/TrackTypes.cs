using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Tracks;

public class TrackTypes
{
    [JsonPropertyName("track_type")]
    public string TrackType { get; set; } = default!;
}
