namespace Aydsko.iRacingData.Member;

public class FieldDefs
{
    [JsonPropertyName("field_id")]
    public int FieldId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("value")]
    public object Value { get; set; } = default!;

    [JsonPropertyName("editable")]
    public bool Editable { get; set; }

    [JsonPropertyName("label")]
    public string Label { get; set; } = default!;

    [JsonPropertyName("section")]
    public string Section { get; set; } = default!;

    [JsonPropertyName("row_order")]
    public int RowOrder { get; set; }

    [JsonPropertyName("column")]
    public int Column { get; set; }

    [JsonPropertyName("number_of_lines")]
    public int NumberOfLines { get; set; }
}
