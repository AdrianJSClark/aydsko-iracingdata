// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.CarClasses;
using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Common;

/// <summary>Information on a group of cars considered in the same class.</summary>
public class CarClass
{
    /// <summary>Unique identifier for the car class.</summary>
    [JsonPropertyName("car_class_id")]
    public int CarClassId { get; set; }

    /// <summary>Individual cars which make up this class.</summary>
    [JsonPropertyName("cars_in_class")]
    public CarsInClass[] CarsInClass { get; set; } = Array.Empty<CarsInClass>();

    /// <summary>Unique identifier of the iRacing Member.</summary>
    [JsonPropertyName("cust_id")]
    public int? CustomerId { get; set; }

    /// <summary>Name of the car class.</summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    /// <summary>Value indicating the relative speed of this car class.</summary>
    [JsonPropertyName("relative_speed")]
    public int RelativeSpeed { get; set; }

    /// <summary>A shortened version of the car class' name.</summary>
    [JsonPropertyName("short_name")]
    public string ShortName { get; set; } = default!;
}

[JsonSerializable(typeof(CarClass[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class CarClassArrayContext : JsonSerializerContext
{ }
