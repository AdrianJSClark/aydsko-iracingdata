// © 2025 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class Tags
{
    [JsonPropertyName("categorized")]
    public Categorized[] Categorized { get; set; } = [];

    [JsonPropertyName("not_categorized")]
    public Tag[] NotCategorized { get; set; } = [];
}

public class Categorized
{
    [JsonPropertyName("category_id")]
    public int CategoryId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    [JsonPropertyName("tags")]
    public Tag[] Tags { get; set; } = [];
}

public class Tag
{
    [JsonPropertyName("tag_id")]
    public int TagId { get; set; }

    [JsonPropertyName("tag_name")]
    public string TagName { get; set; } = default!;
}
