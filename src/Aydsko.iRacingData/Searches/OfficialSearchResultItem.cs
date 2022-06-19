using Aydsko.iRacingData.Converters;
using Aydsko.iRacingData.Results;

namespace Aydsko.iRacingData.Searches;

public class OfficialSearchResultItem
{
    [JsonPropertyName("session_id")]
    public int SessionId { get; set; }

    [JsonPropertyName("subsession_id")]
    public int SubsessionId { get; set; }

    [JsonPropertyName("start_time")]
    public DateTimeOffset StartTime { get; set; }

    [JsonPropertyName("end_time")]
    public DateTimeOffset EndTime { get; set; }

    [JsonPropertyName("license_category_id")]
    public int LicenseCategoryId { get; set; }

    [JsonPropertyName("license_category")]
    public string LicenseCategory { get; set; } = default!;

    [JsonPropertyName("num_drivers")]
    public int NumberOfDrivers { get; set; }

    [JsonPropertyName("num_cautions")]
    public int NumberOfCautions { get; set; }

    [JsonPropertyName("num_caution_laps")]
    public int NumberOfCautionLaps { get; set; }

    [JsonPropertyName("num_lead_changes")]
    public int NumberOfLeadChanges { get; set; }

    [JsonPropertyName("event_laps_complete")]
    public int EventLapsComplete { get; set; }

    [JsonPropertyName("driver_changes")]
    public bool DriverChanges { get; set; }

    [JsonPropertyName("winner_group_id")]
    public int WinnerGroupId { get; set; }

    [JsonPropertyName("winner_name")]
    public string WinnerName { get; set; } = default!;

    [JsonPropertyName("winner_ai")]
    public bool WinnerAi { get; set; }

    [JsonPropertyName("track")]
    public ResultTrackInfo Track { get; set; } = default!;

    [JsonPropertyName("official_session")]
    public bool OfficialSession { get; set; }

    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("season_year")]
    public int SeasonYear { get; set; }

    [JsonPropertyName("season_quarter")]
    public int SeasonQuarter { get; set; }

    [JsonPropertyName("event_type")]
    public int EventType { get; set; }

    [JsonPropertyName("event_type_name")]
    public string EventTypeName { get; set; } = default!;

    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }

    [JsonPropertyName("series_name")]
    public string SeriesName { get; set; } = default!;

    [JsonPropertyName("series_short_name")]
    public string SeriesShortName { get; set; } = default!;

    /// <summary>An index number identifying the race week.</summary>
    /// <remarks>The iRacing Data API works with zero-based race weeks, most people will use one-based.</remarks>
    /// <seealso cref="RaceWeekNumber" />
    [JsonPropertyName("race_week_num")]
    public int RaceWeekIndex { get; set; }

    /// <summary>The number of the race week within the season.</summary>
    [JsonIgnore]
    public int RaceWeekNumber => RaceWeekIndex + 1;

    [JsonPropertyName("event_strength_of_field")]
    public int EventStrengthOfField { get; set; }

    [JsonPropertyName("event_average_lap"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? EventAverageLap { get; set; }

    [JsonPropertyName("event_best_lap_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? EventBestLapTime { get; set; }
}

[JsonSerializable(typeof(OfficialSearchResultItem[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class OfficialSearchResultItemArrayContext : JsonSerializerContext
{ }
