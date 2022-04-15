// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.CarClasses;

/// <summary>Details about a car.</summary>
public class CarsInClass
{
    /// <summary>The relative directory path for car setups and assets.</summary>
    [JsonPropertyName("car_dirpath")]
    public string CarDirpath { get; set; } = default!;

    /// <summary>Unique identifier for the car.</summary>
    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    /// <summary>Indicates if the car has been retired.</summary>
    [JsonPropertyName("retired")]
    public bool Retired { get; set; }
}
