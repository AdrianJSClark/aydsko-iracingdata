namespace Aydsko.iRacingData.Leagues;

public class LeagueDirectoryResultPage
{
    [JsonPropertyName("results_page")]
    public LeagueDirectoryResultItem[] Items { get; set; } = default!;

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("lowerbound")]
    public int Lowerbound { get; set; }

    [JsonPropertyName("upperbound")]
    public int Upperbound { get; set; }

    [JsonPropertyName("row_count")]
    public int RowCount { get; set; }
}

[JsonSerializable(typeof(LeagueDirectoryResultPage)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class LeagueDirectoryResultPageContext : JsonSerializerContext
{ }
