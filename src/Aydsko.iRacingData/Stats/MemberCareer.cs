// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Stats;

public class MemberCareer
{
    [JsonPropertyName("stats")]
    public MemberCareerStatistics[] Statistics { get; set; } = default!;
    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }
}

[JsonSerializable(typeof(MemberCareer)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class MemberCareerContext : JsonSerializerContext
{ }
