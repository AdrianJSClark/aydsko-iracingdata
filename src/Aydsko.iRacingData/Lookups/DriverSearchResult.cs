namespace Aydsko.iRacingData.Lookups;

public class DriverSearchResult
{
    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = default!;

    [JsonPropertyName("helmet")]
    public Helmet Helmet { get; set; } = default!;
}

[JsonSerializable(typeof(DriverSearchResult[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class DriverSearchResultContext : JsonSerializerContext
{ }
