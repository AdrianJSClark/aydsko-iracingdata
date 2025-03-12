// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Cars;

/// <summary>Images and additional details about a particular vehicle.</summary>
public class CarAssetDetail
{
    public const string ImagePathBase = "https://images-static.iracing.com";

    /// <summary>Unique identifier of the vehicle.</summary>
    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    /// <summary>A collection of additional rules related to this vehicle.</summary>
    [JsonPropertyName("car_rules")]
    public CarRule[] CarRules { get; set; } = default!;

    /// <summary>Text description of the vehicle.</summary>
    /// <remarks>Content may be in HTML format.</remarks>
    [JsonPropertyName("detail_copy")]
    public string DetailCopy { get; set; } = default!;

    [JsonPropertyName("detail_screen_shot_images"), JsonConverter(typeof(CsvStringConverter))]
    public string[] DetailScreenShotImages { get; set; } = default!;

    /// <summary>Technical specifications of the vehicle.</summary>
    /// <remarks>Content may be in HTML format.</remarks>
    [JsonPropertyName("detail_techspecs_copy")]
    public string DetailTechSpecsCopy { get; set; } = default!;

    /// <summary>Relative path of the vehicle folder within <see cref="ImagePathBase"/> for the images.</summary>
    [JsonPropertyName("folder")]
    public string Folder { get; set; } = default!;

    [JsonPropertyName("gallery_images"), JsonConverter(typeof(CsvStringConverter))]
    public string[] GalleryImages { get; set; } = default!;

    [JsonPropertyName("gallery_prefix")]
    public object? GalleryPrefix { get; set; }

    [JsonPropertyName("group_image")]
    public object? GroupImage { get; set; }

    [JsonPropertyName("group_name")]
    public object? GroupName { get; set; }

    /// <summary>File name for the larger image of the vehicle.</summary>
    /// <remarks>Must be combined with <see cref="ImagePathBase"/> and <see cref="Folder"/> to be useful.</remarks>
    [JsonPropertyName("large_image")]
    public string LargeImage { get; set; } = default!;

    /// <summary>The full URL to the vehicle's large image.</summary>
    /// <seealso cref="LargeImage"/>
    [JsonIgnore]
    public Uri LargeImageUri => new(string.Join("/", [ImagePathBase.Trim('/'), Folder.Trim('/'), LargeImage.Trim('/')]));

    /// <summary>File name for the logo image of the vehicle's manufacturer.</summary>
    /// <remarks>Must be combined with <see cref="ImagePathBase"/> to be useful.</remarks>
    [JsonPropertyName("logo")]
    public string Logo { get; set; } = default!;

    /// <summary>The full URL to the image containing the manufacturer logo.</summary>
    /// <seealso cref="Logo"/>
    [JsonIgnore]
    public Uri LogoUri => new(string.Join("/", [ImagePathBase.Trim('/'), Logo.Trim('/')]));

    /// <summary>File name for the smaller image of the vehicle.</summary>
    /// <remarks>Must be combined with <see cref="ImagePathBase"/> and <see cref="Folder"/> to be useful.</remarks>
    [JsonPropertyName("small_image")]
    public string SmallImage { get; set; } = default!;

    /// <summary>The full URL to the vehicle's small image.</summary>
    /// <seealso cref="SmallImage"/>
    [JsonIgnore]
    public Uri SmallImageUri => new(string.Join("/", [ImagePathBase.Trim('/'), Folder.Trim('/'), SmallImage.Trim('/')]));

    /// <summary>File name for the logo image of the vehicle's series sponsor, if available.</summary>
    /// <remarks>Must be combined with <see cref="ImagePathBase"/> to be useful.</remarks>
    [JsonPropertyName("sponsor_logo")]
    public string? SponsorLogo { get; set; }

    /// <summary>The full URL for the logo image of the vehicle's series sponsor, if available.</summary>
    /// <seealso cref="SponsorLogo"/>
    [JsonIgnore]
    public Uri? SponsorLogoUri => SponsorLogo is string { Length: > 0 }
                                  ? new(string.Join("/", [ImagePathBase.Trim('/'), SponsorLogo.Trim('/')]))
                                  : null;

    /// <summary>Relative path of the vehicle paint template, if available.</summary>
    [JsonPropertyName("template_path")]
    public string? TemplatePath { get; set; } = default!;

    /// <summary>The full URL to the vehicle's paint template, if available.</summary>
    [JsonIgnore]
    public Uri? TemplateUri => TemplatePath is string { Length: > 0 }
                                ? new($"https://ir-core-sites.iracing.com/members/member_images/cars/{TemplatePath.Trim('/')}")
                                : null;
}

[JsonSerializable(typeof(IReadOnlyDictionary<string, CarAssetDetail>)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class CarAssetDetailDictionaryContext : JsonSerializerContext
{ }
