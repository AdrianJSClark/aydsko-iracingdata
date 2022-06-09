namespace Aydsko.iRacingData.Series;

public class SeriesDetail
{
    [JsonPropertyName("allowed_licenses")]
    public AllowedLicenses[] AllowedLicenses { get; set; } = Array.Empty<AllowedLicenses>();

    [JsonPropertyName("category")]
    public string Category { get; set; } = default!;

    [JsonPropertyName("category_id")]
    public int CategoryId { get; set; }

    [JsonPropertyName("eligible")]
    public bool Eligible { get; set; }

    [JsonPropertyName("max_starters")]
    public int MaxStarters { get; set; }

    [JsonPropertyName("min_starters")]
    public int MinStarters { get; set; }

    [JsonPropertyName("oval_caution_type")]
    public int OvalCautionType { get; set; }

    [JsonPropertyName("road_caution_type")]
    public int RoadCautionType { get; set; }

    [JsonPropertyName("search_filters")]
    public string SearchFilters { get; set; } = default!;

    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }

    [JsonPropertyName("series_name")]
    public string SeriesName { get; set; } = default!;

    [JsonPropertyName("series_short_name")]
    public string SeriesShortName { get; set; } = default!;
}

[JsonSerializable(typeof(SeriesDetail[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SeriesDetailArrayContext : JsonSerializerContext
{ }
