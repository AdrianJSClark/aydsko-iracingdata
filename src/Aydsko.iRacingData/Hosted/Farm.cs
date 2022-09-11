// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Hosted;

public class Farm
{
    [JsonPropertyName("farm_id")]
    public int FarmId { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = default!;

    [JsonPropertyName("image_path")]
    public string ImagePath { get; set; } = default!;

    [JsonPropertyName("displayed")]
    public bool Displayed { get; set; }
}
