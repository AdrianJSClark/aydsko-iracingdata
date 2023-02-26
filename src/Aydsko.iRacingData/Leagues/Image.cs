// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class Image
{
    [JsonPropertyName("small_logo")]
    public string? SmallLogo { get; set; }

    [JsonPropertyName("large_logo")]
    public string? LargeLogo { get; set; }
}
