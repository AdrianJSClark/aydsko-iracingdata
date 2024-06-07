// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Cars;

public class CarAssetDetail
{
    public const string ImagePathBase = "https://images-static.iracing.com/";

    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("car_rules")] 
    public CarRule[] CarRules { get; set; } = default!;

    [JsonPropertyName("detail_copy")]
    public string DetailCopy { get; set; } = default!;

    [JsonPropertyName("detail_screen_shot_images"), JsonConverter(typeof(CsvStringConverter))]
    public string[] DetailScreenShotImages { get; set; } = default!;

    [JsonPropertyName("detail_techspecs_copy")]
    public string DetailTechSpecsCopy { get; set; } = default!;

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

    [JsonPropertyName("large_image")]
    public string LargeImage { get; set; } = default!;

    [JsonPropertyName("logo")]
    public string Logo { get; set; } = default!;

    [JsonPropertyName("small_image")]
    public string SmallImage { get; set; } = default!;

    [JsonPropertyName("sponsor_logo")]
    public object? SponsorLogo { get; set; }

    [JsonPropertyName("template_path")]
    public string TemplatePath { get; set; } = default!;
}

[JsonSerializable(typeof(IReadOnlyDictionary<string, CarAssetDetail>)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class CarAssetDetailDictionaryContext : JsonSerializerContext
{ }
