// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

public class CarsInClass
{
    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("package_id")]
    public int PackageId { get; set; }
}
