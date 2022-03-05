// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Leagues;

public class CategorizedTag
{
    [JsonPropertyName("category_id")]
    public int CategoryId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    [JsonPropertyName("limit")]
    public object? Limit { get; set; }
    [JsonPropertyName("tags")]
    public TagValue[] Tags { get; set; } = Array.Empty<TagValue>();
}
