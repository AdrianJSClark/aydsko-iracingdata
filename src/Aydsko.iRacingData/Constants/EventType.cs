namespace Aydsko.iRacingData.Constants;

public class EventType
{
    [JsonPropertyName("label")]
    public string Label { get; set; } = null!;

    [JsonPropertyName("value")]
    public int Value { get; set; }
}

[JsonSerializable(typeof(EventType[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class EventTypeArrayContext : JsonSerializerContext
{ }
