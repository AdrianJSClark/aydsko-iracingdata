// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

internal class PastSeriesResult
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("series")]
    public PastSeriesDetail Series { get; set; } = default!;

    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }
}

public class PastSeriesDetail
{
    [JsonPropertyName("allowed_licenses")]
    public AllowedLicenses[] AllowedLicenses { get; set; } = Array.Empty<AllowedLicenses>();

    [JsonPropertyName("category")]
    public string Category { get; set; } = default!;

    [JsonPropertyName("category_id")]
    public int CategoryId { get; set; }

    [JsonPropertyName("search_filters")]
    public string SearchFilters { get; set; } = default!;

    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }

    [JsonPropertyName("series_name")]
    public string SeriesName { get; set; } = default!;

    [JsonPropertyName("series_short_name")]
    public string SeriesShortName { get; set; } = default!;

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("official")]
    public bool Official { get; set; }

    [JsonPropertyName("fixed_setup")]
    public bool FixedSetup { get; set; }

    [JsonPropertyName("logo")]
    public string Logo { get; set; } = default!;

    [JsonIgnore]
    public Uri LogoUri => new(new Uri("https://images-static.iracing.com/img/logos/series/"), Logo);

    [JsonPropertyName("license_group")]
    public int LicenseGroup { get; set; }

    [JsonPropertyName("license_group_types")]
    public LicenseGroupType[] LicenseGroupTypes { get; set; } = Array.Empty<LicenseGroupType>();

    [JsonPropertyName("seasons")]
    public PastSeason[] Seasons { get; set; } = Array.Empty<PastSeason>();
}

public class PastSeason
{
    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }

    [JsonPropertyName("season_name")]
    public string SeasonName { get; set; } = default!;

    [JsonPropertyName("season_short_name")]
    public string SeasonShortName { get; set; } = default!;

    [JsonPropertyName("season_year")]
    public int SeasonYear { get; set; }

    [JsonPropertyName("season_quarter")]
    public int SeasonQuarter { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("official")]
    public bool Official { get; set; }

    [JsonPropertyName("driver_changes")]
    public bool DriverChanges { get; set; }

    [JsonPropertyName("fixed_setup")]
    public bool FixedSetup { get; set; }

    [JsonPropertyName("license_group")]
    public int LicenseGroup { get; set; }

    [JsonPropertyName("license_group_types")]
    public LicenseGroupType[] LicenseGroupTypes { get; set; } = Array.Empty<LicenseGroupType>();

    [JsonPropertyName("car_classes")]
    public PastSeasonCarClass[] CarClasses { get; set; } = Array.Empty<PastSeasonCarClass>();

    [JsonPropertyName("race_weeks")]
    public RaceWeek[] RaceWeeks { get; set; } = Array.Empty<RaceWeek>();
}

public class PastSeasonCarClass
{
    [JsonPropertyName("car_class_id")]
    public int CarClassId { get; set; }

    [JsonPropertyName("short_name")]
    public string ShortName { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("relative_speed")]
    public int RelativeSpeed { get; set; }
}

public class RaceWeek
{
    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("race_week_num")]
    public int RaceWeekNum { get; set; }

    [JsonPropertyName("track")]
    public Track Track { get; set; } = default!;
}

[JsonSerializable(typeof(PastSeriesResult)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class PastSeriesResultContext : JsonSerializerContext
{ }
