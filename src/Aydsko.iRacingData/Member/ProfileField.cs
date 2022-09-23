// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

public class ProfileField
{
    [JsonPropertyName("field_id")]
    public int FieldId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("value")]
    public string Value { get; set; } = default!;

    [JsonPropertyName("editable")]
    public bool Editable { get; set; }

    [JsonPropertyName("label")]
    public string Label { get; set; } = default!;
}
