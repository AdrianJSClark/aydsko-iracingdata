// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Lookups;

public class Country
{
    [JsonPropertyName("country_name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("country_code")]
    public string Code { get; set; } = default!;
}

[JsonSerializable(typeof(Country[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class CountryArrayContext : JsonSerializerContext
{ }
