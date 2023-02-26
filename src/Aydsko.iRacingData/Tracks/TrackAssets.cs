// © 2023 Adrian Clark
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

    [JsonIgnore]
    public Uri LargeImageUrl => new(new(new(ImagePathBase), Folder?.TrimEnd('/') + '/'), LargeImage);

    [JsonPropertyName("logo")]
    public string? Logo { get; set; }

    [JsonIgnore]
    public Uri LogoUrl => new(new(ImagePathBase), Logo);

    [JsonPropertyName("north")]
    public string? North { get; set; }

    [JsonPropertyName("num_svg_images")]
    public int NumberOfSvgImages { get; set; }

    [JsonPropertyName("small_image")]
    public string? SmallImage { get; set; }

    [JsonIgnore]
    public Uri SmallImageUrl => new(new(new(ImagePathBase), Folder?.TrimEnd('/') + '/'), SmallImage);

    [JsonPropertyName("track_id")]
    public int TrackId { get; set; }

    /// <summary>The base URL for the track map images detailed in <see cref="TrackMapLayers"/>.</summary>
    /// <seealso cref="MapLayers" />
    [JsonPropertyName("track_map")]
    public string TrackMap { get; set; } = default!;

    /// <summary>Track map image files to be combined with the base URL in <see cref="TrackMap"/>.</summary>
    [JsonPropertyName("track_map_layers")]
    public MapLayers TrackMapLayers { get; set; } = default!;
}

[JsonSerializable(typeof(IReadOnlyDictionary<string, TrackAssets>)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class TrackAssetsArrayContext : JsonSerializerContext
{ }
