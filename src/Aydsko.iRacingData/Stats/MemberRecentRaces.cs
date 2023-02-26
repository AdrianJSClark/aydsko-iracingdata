// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Stats;
public class MemberRecentRaces
{
    [JsonPropertyName("races")]
    public Race[] Races { get; set; } = Array.Empty<Race>();

    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }
}

[JsonSerializable(typeof(MemberRecentRaces)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class MemberRecentRacesContext : JsonSerializerContext
{ }
