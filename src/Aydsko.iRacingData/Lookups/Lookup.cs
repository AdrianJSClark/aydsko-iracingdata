// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Lookups;

public class Lookup
{
    [JsonPropertyName("lookup_type")]
    public string LookupType { get; set; } = default!;

    [JsonPropertyName("lookup_values")]
    public LookupValue[] LookupValues { get; set; } = Array.Empty<LookupValue>();
}
