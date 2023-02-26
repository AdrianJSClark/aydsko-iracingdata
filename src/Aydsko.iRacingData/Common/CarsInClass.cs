// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.CarClasses;

public class CarsInClass
{
    [JsonPropertyName("car_dirpath")]
    public string CarDirpath { get; set; } = default!;

    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("retired")]
    public bool Retired { get; set; }
}
