// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Lookups;

public class LookupGroup
{
    [JsonPropertyName("tag")]
    public string Tag { get; set; } = default!;

    [JsonPropertyName("lookups")]
    public Lookup[] Lookups { get; set; } = Array.Empty<Lookup>();
}

[JsonSerializable(typeof(LookupGroup[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class LookupGroupArrayContext : JsonSerializerContext
{ }
