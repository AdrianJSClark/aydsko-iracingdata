// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Leagues;

public class Tags
{
    [JsonPropertyName("categorized")]
    public object[] Categorized { get; set; } = Array.Empty<object>();
    [JsonPropertyName("not_categorized")]
    public object[] NotCategorized { get; set; } = Array.Empty<object>();
}
