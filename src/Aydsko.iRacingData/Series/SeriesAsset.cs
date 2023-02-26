// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

public class SeriesAsset
{
    public const string ImagePathBase = "https://images-static.iracing.com/img/logos/series/";

    [JsonPropertyName("large_image")]
    public object LargeImage { get; set; } = default!;

    [JsonPropertyName("logo")]
    public string Logo { get; set; } = default!;

    [JsonIgnore]
    public Uri LogoUrl => new(new(ImagePathBase), Logo);

    [JsonPropertyName("series_copy")]
    public string SeriesCopy { get; set; } = default!;

    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }

    [JsonPropertyName("small_image")]
    public object SmallImage { get; set; } = default!;
}

[JsonSerializable(typeof(IReadOnlyDictionary<string, SeriesAsset>)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SeriesAssetReadOnlyDictionaryContext : JsonSerializerContext
{ }
