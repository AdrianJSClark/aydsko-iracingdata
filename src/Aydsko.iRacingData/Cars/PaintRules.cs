// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Cars;

public class PaintRules
{
    [JsonPropertyName("RestrictCustomPaint")]
    public bool RestrictCustomPaint { get; set; }
}
