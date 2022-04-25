// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

public class MemberSummary
{
    [JsonPropertyName("this_year")]
    public MemberSummaryYearStatistics YearStatistics { get; set; } = null!;

    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }
}


[JsonSerializable(typeof(MemberSummary)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class MemberSummaryContext : JsonSerializerContext
{ }
