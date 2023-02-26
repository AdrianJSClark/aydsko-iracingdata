// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

public class MemberBests
{
    [JsonPropertyName("cars_driven")]
    public CarsDriven[] CarsDriven { get; set; } = default!;

    [JsonPropertyName("bests")]
    public MemberBest[] Bests { get; set; } = default!;

    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }
}

[JsonSerializable(typeof(MemberBests)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class MemberBestsContext : JsonSerializerContext
{ }
