// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

public class SeasonBase
{
    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }

    [JsonPropertyName("season_name")]
    public string SeasonName { get; set; } = default!;

    [JsonPropertyName("official")]
    public bool Official { get; set; }

    [JsonPropertyName("season_year")]
    public int SeasonYear { get; set; }

    [JsonPropertyName("season_quarter")]
    public int SeasonQuarter { get; set; }

    [JsonPropertyName("license_group")]
    public int LicenseGroupId { get; set; }

    [JsonPropertyName("driver_changes")]
    public bool DriverChanges { get; set; }
}
