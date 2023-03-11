namespace Aydsko.iRacingData.Stats;

/// <summary>Statistics for the member recap.</summary>
public class RecapStatistics
{
    /// <summary>Total number of starts the member made during the period.</summary>
    [JsonPropertyName("starts")]
    public int NumberOfStarts { get; set; }

    /// <summary>Number of wins that the member had.</summary>
    [JsonPropertyName("wins")]
    public int NumberOfWins { get; set; }

    /// <summary>Number of times the member finished in the top 5.</summary>
    [JsonPropertyName("top5")]
    public int NumberOfTop5Finishes { get; set; }

    /// <summary>Average starting position for the member over the period.</summary>
    [JsonPropertyName("avg_start_position")]
    public int AverageStartPosition { get; set; }

    /// <summary>Average finish position for the member over the period.</summary>
    [JsonPropertyName("avg_finish_position")]
    public int AverageFinishPosition { get; set; }

    /// <summary>Total number of laps driven by the member during the period.</summary>
    [JsonPropertyName("laps")]
    public int TotalLaps { get; set; }

    /// <summary>Total number of laps led by the member during the period.</summary>
    [JsonPropertyName("laps_led")]
    public int TotalLapsLed { get; set; }

    /// <summary>The member's favorite car in the period.</summary>
    [JsonPropertyName("favorite_car")]
    public RecapFavoriteCar FavoriteCar { get; set; } = default!;

    /// <summary>The member's favorite track in the period.</summary>
    [JsonPropertyName("favorite_track")]
    public RecapFavoriteTrack FavoriteTrack { get; set; } = default!;
}
