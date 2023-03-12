namespace Aydsko.iRacingData.Stats;

/// <summary>The member's favorite track in the period.</summary>
public class RecapFavoriteTrack
{
    /// <summary>Unique identifier for the track.</summary>
    [JsonPropertyName("track_id")]
    public int TrackId { get; set; }

    /// <summary>Name of the track.</summary>
    [JsonPropertyName("track_name")]
    public string TrackName { get; set; } = default!;

    /// <summary>Name of the track's configuration.</summary>
    [JsonPropertyName("config_name")]
    public string ConfigName { get; set; } = default!;

    /// <summary>URL pointing to an image of the track.</summary>
    [JsonPropertyName("track_logo")]
    public Uri TrackLogoUrl { get; set; } = default!;
}
