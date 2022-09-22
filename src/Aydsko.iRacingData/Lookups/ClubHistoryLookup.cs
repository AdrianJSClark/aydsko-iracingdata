namespace Aydsko.iRacingData.Lookups;

public class ClubHistoryLookup
{
    public int ClubId { get; set; }
    public string ClubName { get; set; } = default!;
    public int SeasonYear { get; set; }
    public int SeasonQuarter { get; set; }
    public string Region { get; set; } = default!;
}

[JsonSerializable(typeof(ClubHistoryLookup[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class ClubHistoryLookupArrayContext : JsonSerializerContext
{ }
