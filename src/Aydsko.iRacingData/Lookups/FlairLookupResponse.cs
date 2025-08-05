// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Lookups;

public class FlairLookupResponse
{
    [JsonPropertyName("flairs")]
    public Flair[] Flairs { get; set; } = default!;

    [JsonPropertyName("success")]
    public bool Success { get; set; }
}

public class Flair
{
    [JsonPropertyName("flair_id")]
    public int FlairId { get; set; }

    [JsonPropertyName("flair_name")]
    public string FlairName { get; set; } = default!;

    [JsonPropertyName("seq")]
    public int Sequence { get; set; }

    [JsonPropertyName("flair_shortname")]
    public string? FlairShortName { get; set; }

    [JsonPropertyName("country_code")]
    public string? CountryCode { get; set; }
}

[JsonSerializable(typeof(FlairLookupResponse)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class FlairLookupResponseContext : JsonSerializerContext
{ }
