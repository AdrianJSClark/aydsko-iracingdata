namespace Aydsko.iRacingData.Member;

public class MemberChart
{
    [JsonPropertyName("blackout")]
    public bool Blackout { get; set; }

    [JsonPropertyName("category_id")]
    public int CategoryId { get; set; }

    [JsonPropertyName("chart_type")]
    public MemberChartType ChartType { get; set; }

    [JsonPropertyName("data")]
    public MemberChartDataPoint[] Points { get; set; } = default!;

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }
}

#pragma warning disable CA1008 // Enums should have zero value - this enum is derived from iRacing API values which don't have a zero member.
public enum MemberChartType
{
    IRating = 1,
    TTRating = 2,
    LicenseSafetyRating = 3
}
#pragma warning restore CA1008 // Enums should have zero value

public class MemberChartDataPoint
{
#if NET6_0_OR_GREATER
    [JsonPropertyName("when"), JsonConverter(typeof(Converters.DateOnlyConverter))]
    public DateOnly Day { get; set; } = default!;
#else
    [JsonPropertyName("when"), JsonConverter(typeof(Converters.DateTimeConverter))]
    public DateTime Day { get; set; } = default!;
#endif

    [JsonPropertyName("value")]
    public int Value { get; set; }
}

[JsonSerializable(typeof(MemberChart)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class MemberChartContext : JsonSerializerContext
{ }
