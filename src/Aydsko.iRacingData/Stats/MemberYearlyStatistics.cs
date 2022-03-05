using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Stats;

public class MemberYearlyStatistics
{
    [JsonPropertyName("stats")]
    public YearlyStatistics[] Statistics { get; set; } = Array.Empty<YearlyStatistics>();
    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }
}

[JsonSerializable(typeof(MemberYearlyStatistics)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class MemberYearlyStatisticsContext : JsonSerializerContext
{ }
