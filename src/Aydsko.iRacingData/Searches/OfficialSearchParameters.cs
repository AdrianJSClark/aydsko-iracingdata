// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Searches;

/// <summary>Parameters for a hosted or league session search.</summary>
/// <remarks>
/// Valid searches require at least one of:
/// <list type="bullet">
/// <item><see cref="SeasonYear"/> and <see cref="SeasonQuarter"/></item>
/// <item><see cref="SearchParameters.StartRangeBegin"/></item>
/// <item><see cref="SearchParameters.FinishRangeBegin"/></item>
/// </list>
/// </remarks>
public class OfficialSearchParameters : SearchParameters
{
    /// <summary>The year part of the season identifier.</summary>
    /// <remarks>Required when using <see cref="SeasonQuarter"/>.</remarks>
    [JsonPropertyName("season_year")]
    public int? SeasonYear { get; set; }

    /// <summary>The quarter part of the season identifier.</summary>
    /// <remarks>Required when using <see cref="SeasonYear"/>.</remarks>
    [JsonPropertyName("season_quarter")]
    public int? SeasonQuarter { get; set; }

    /// <summary>The Series identifier to search for.</summary>
    [JsonPropertyName("series_id")]
    public int? SeriesId { get; set; }

    /// <summary>The race week index to search for.</summary>
    /// <remarks>The iRacing Data API works with zero-based race weeks (i.e. first week is "0"), most people will use one-based overload (i.e. first week is "1") <see cref="RaceWeekNumber"/>.</remarks>
    [JsonPropertyName("race_week_num")]
    public int? RaceWeekIndex { get; set; }

    /// <summary>The number of the race week within the season to search for.</summary>
    /// <remarks>Converted to & from <see cref="RaceWeekIndex"/>.</remarks>
    [JsonIgnore]
    public int? RaceWeekNumber
    {
        get => RaceWeekIndex is null ? null : RaceWeekIndex + 1;
        set => RaceWeekIndex = value is null ? null : value - 1;
    }

    /// <summary>Search only for sessions earning championship points.</summary>
    /// <remarks>Set to <see langword="true"/> to return sessions which are "official" only, <see langref="false"/> to return all sessions (default).</remarks>
    [JsonPropertyName("official_only")]
    public bool OfficialOnly { get; set; }

    /// <summary>Types of events to search for.</summary>
    /// <remarks>Defaults to all.</remarks>
    /// <seealso cref="Constants.EventType"/>
    [JsonPropertyName("event_types")]
    public int[]? EventTypes { get; set; }
}
