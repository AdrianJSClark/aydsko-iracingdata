namespace Aydsko.iRacingData.Constants;

public class Category
{
    [JsonPropertyName("label")]
    public string Label { get; set; } = null!;

    [JsonPropertyName("value")]
    public int Value { get; set; }
}

[JsonSerializable(typeof(Category[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class CategoryArrayContext : JsonSerializerContext
{ }
