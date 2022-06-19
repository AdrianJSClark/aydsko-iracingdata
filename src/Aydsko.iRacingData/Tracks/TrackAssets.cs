// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Tracks;

public class TrackAssets
{
    public const string ImagePathBase = "https://images-static.iracing.com/";

    [JsonPropertyName("coordinates")]
    public string? Coordinates { get; set; }

    [JsonPropertyName("detail_copy")]
    public string? DetailCopy { get; set; }

    [JsonPropertyName("detail_techspecs_copy")]
    public object? DetailTechspecsCopy { get; set; }

    [JsonPropertyName("detail_video")]
    public object? DetailVideo { get; set; }

    [JsonPropertyName("folder")]
    public string? Folder { get; set; }

    [JsonPropertyName("gallery_images")]
    public string? GalleryImages { get; set; }

    [JsonPropertyName("gallery_prefix")]
    public string? GalleryPrefix { get; set; }

    [JsonPropertyName("large_image")]
    public string? LargeImage { get; set; }

    [JsonPropertyName("logo")]
    public string? Logo { get; set; }

    [JsonPropertyName("north")]
    public string? North { get; set; }

    [JsonPropertyName("num_svg_images")]
    public int NumberOfSvgImages { get; set; }

    [JsonPropertyName("small_image")]
    public string? SmallImage { get; set; }

    [JsonPropertyName("track_id")]
    public int TrackId { get; set; }

    [JsonPropertyName("track_map")]
    public string? TrackMap { get; set; }
}

[JsonSerializable(typeof(IReadOnlyDictionary<string, TrackAssets>)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class TrackAssetsArrayContext : JsonSerializerContext
{ }
