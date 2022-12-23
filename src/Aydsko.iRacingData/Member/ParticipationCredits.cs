namespace Aydsko.iRacingData.Member;

public class ParticipationCredits
{
    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }

    [JsonPropertyName("series_name")]
    public string SeriesName { get; set; } = default!;

    [JsonPropertyName("license_group")]
    public int LicenseGroup { get; set; }

    [JsonPropertyName("license_group_name")]
    public string LicenseGroupName { get; set; } = default!;

    [JsonPropertyName("participation_credits")]
    public int Credits { get; set; }

    [JsonPropertyName("min_weeks")]
    public int MinimumWeeks { get; set; }

    [JsonPropertyName("weeks")]
    public int Weeks { get; set; }

    [JsonPropertyName("earned_credits")]
    public int EarnedCredits { get; set; }

    [JsonPropertyName("total_credits")]
    public int TotalCredits { get; set; }
}

[JsonSerializable(typeof(ParticipationCredits[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class ParticipationCreditsArrayContext : JsonSerializerContext
{ }
