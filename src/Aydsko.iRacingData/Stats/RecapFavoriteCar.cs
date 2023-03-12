namespace Aydsko.iRacingData.Stats;

/// <summary>The member's favorite car in the recap period.</summary>
public class RecapFavoriteCar
{
    /// <summary>Unique identifier for the car.</summary>
    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    /// <summary>Name of the car.</summary>
    [JsonPropertyName("car_name")]
    public string CarName { get; set; } = default!;

    /// <summary>URL pointing to an image of the car.</summary>
    [JsonPropertyName("car_image")]
    public Uri CarImageUrl { get; set; } = default!;
}
