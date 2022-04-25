// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class TagValue
{
    [JsonPropertyName("tag_id")]
    public int TagId { get; set; }

    [JsonPropertyName("tag_name")]
    public string TagName { get; set; } = null!;
}
