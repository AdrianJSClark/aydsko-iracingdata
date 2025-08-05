// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Tracks;

/// <summary>Content related to a track, including images.</summary>
public class TrackAssets
{
    public const string ImagePathBase = "https://images-static.iracing.com/";

    /// <summary>Unique identifier for the track this relates to.</summary>
    [JsonPropertyName("track_id")]
    public int TrackId { get; set; }

    /// <summary>Latitude and longitude of the circuit.</summary>
    [JsonPropertyName("coordinates")]
    public string? Coordinates { get; set; }

    /// <summary>Latitude and longitude of the circuit.</summary>
    /// <remarks>Parsed version of the <see cref="Coordinates"/> property value.</remarks>
    [JsonIgnore]
    public TrackCoordinate? GeographicalCoordinate => TrackCoordinate.TryParse(Coordinates, out var coordinates) ? coordinates : null;

    /// <summary>Description or details about the track in HTML format.</summary>
    [JsonPropertyName("detail_copy")]
    public string? DetailCopy { get; set; }

    /// <summary>Additional technical detail about the track in HTML format.</summary>
    [JsonPropertyName("detail_techspecs_copy")]
    public string? DetailTechnicalSpecificationsCopy { get; set; }

    [Obsolete("Use DetailTechnicalSpecificationsCopy instead.")]
    public object? DetailTechspecsCopy { get => DetailTechnicalSpecificationsCopy; set => DetailTechnicalSpecificationsCopy = value?.ToString(); }

    [JsonPropertyName("detail_video")]
    public object? DetailVideo { get; set; }

    /// <summary>Folder name within the static images site where the large and small track images are stored.</summary>
    [JsonPropertyName("folder")]
    public string? Folder { get; set; }

    [JsonPropertyName("gallery_images"), Obsolete("Property is a legacy item that does not represent how things work. (See: https://forums.iracing.com/discussion/comment/310714/#Comment_310714)")]
    public string? GalleryImages { get; set; }

    [JsonPropertyName("gallery_prefix"), Obsolete("Property is a legacy item that does not represent how things work. (See: https://forums.iracing.com/discussion/comment/310714/#Comment_310714)")]
    public string? GalleryPrefix { get; set; }

    /// <summary>File name for the blurred image of the track, in a large size.</summary>
    /// <remarks>
    /// <para>Needs to be combined with the <see cref="ImagePathBase"/> and <see cref="Folder"/> to build a full URL.</para>
    /// <para>Pre-built URL available in the <see cref="LargeImageUrl"/> property.</para>
    /// </remarks>
    [JsonPropertyName("large_image")]
    public string? LargeImage { get; set; }

    /// <summary>URL to a blurred image of the track, in a large size.</summary>
    /// <remarks>Pre-built URL of the value in the <see cref="LargeImage"/> property.</remarks>
    [JsonIgnore]
    public Uri LargeImageUrl => new(new(new(ImagePathBase), Folder?.TrimEnd('/') + '/'), LargeImage);

    /// <summary>Logo image for the track.</summary>
    /// <remarks>
    /// <para>Needs to be combined with the <see cref="ImagePathBase"/> to build a full URL.</para>
    /// <para>Pre-built URL available in the <see cref="LogoUrl"/> property.</para>
    /// </remarks>
    [JsonPropertyName("logo"), Obsolete("This value may not be accurate. For working values consult the \"Track.TrackLogoUrlLight\" and \"Track.TrackLogoUrlDark\" properties. See https://forums.iracing.com/discussion/comment/662703#Comment_662703 for details.")]
    public string? Logo { get; set; }

    /// <summary>URL to a logo image for the track.</summary>
    /// <remarks>Pre-built URL of the value in the <see cref="Logo"/> property.</remarks>
    [JsonIgnore, Obsolete("This value may not be accurate. For working values consult the \"Track.TrackLogoUrlLight\" and \"Track.TrackLogoUrlDark\" properties. See https://forums.iracing.com/discussion/comment/662703#Comment_662703 for details.")]
    public Uri LogoUrl => new(new(ImagePathBase), Logo);

    [JsonPropertyName("north")]
    public string? North { get; set; }

    /// <summary>North orientation of the track in degrees.</summary>
    /// <remarks>0° is pointing to the left of the map, 90° straight up.</remarks>
    [JsonIgnore]
    public decimal? NorthDegrees
    {
        get
        {
            var numericValue = North?.Replace("deg", string.Empty);
            if (numericValue is null || !decimal.TryParse(numericValue, out var result))
            {
                return null;
            }
            return result;
        }
    }

    /// <summary>Total number of screenshot images available.</summary>
    [JsonPropertyName("num_svg_images")]
    public int NumberOfSvgImages { get; set; }

    /// <summary>File name for the blurred image of the track, in a small size.</summary>
    /// <remarks>
    /// <para>Needs to be combined with the <see cref="ImagePathBase"/> and <see cref="Folder"/> to build a full URL.</para>
    /// <para>Pre-built URL available in the <see cref="SmallImageUrl"/> property.</para>
    /// </remarks>
    [JsonPropertyName("small_image")]
    public string? SmallImage { get; set; }

    /// <summary>URL to a blurred image of the track, in a small size.</summary>
    /// <remarks>Pre-built URL of the value in the <see cref="SmallImage"/> property.</remarks>
    [JsonIgnore]
    public Uri SmallImageUrl => new(new(new(ImagePathBase), Folder?.TrimEnd('/') + '/'), SmallImage);

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
