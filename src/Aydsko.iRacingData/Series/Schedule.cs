// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Series;

public class Schedule
{
    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    /// <summary>An index number identifying the race week.</summary>
    /// <remarks>The iRacing Data API works with zero-based race weeks, most people will use one-based.</remarks>
    /// <seealso cref="RaceWeekNumber" />
    [JsonPropertyName("race_week_num")]
    public int RaceWeekIndex { get; set; }

    /// <summary>The number of the race week within the season.</summary>
    [JsonIgnore]
    public int RaceWeekNumber => RaceWeekIndex + 1;

    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }

    [JsonPropertyName("series_name")]
    public string SeriesName { get; set; } = default!;

    [JsonPropertyName("season_name")]
    public string SeasonName { get; set; } = default!;

    [JsonPropertyName("schedule_name")]
    public string ScheduleName { get; set; } = default!;

#if NET6_0_OR_GREATER
    [JsonPropertyName("start_date"), JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly StartDate { get; set; } = default!;
#else
    [JsonPropertyName("start_date"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime StartDate { get; set; } = default!;
#endif

    [JsonPropertyName("race_lap_limit")]
    public int? RaceLapLimit { get; set; }

    [JsonPropertyName("race_time_limit")]
    public int? RaceTimeLimit { get; set; }

    [JsonPropertyName("start_type")]
    public string StartType { get; set; } = default!;

    [JsonPropertyName("restart_type")]
    public string RestartType { get; set; } = default!;

    [JsonPropertyName("qual_attached")]
    public bool QualifyingIsAttached { get; set; }

    [Obsolete("Use property \"QualifyingIsAttached\"")]
    public bool QualAttached { get => QualifyingIsAttached; set => QualifyingIsAttached = value; }

    [Obsolete("No longer in use. Renamed to \"HasFullCourseCautions\".")]
    public bool YellowFlags { get => HasFullCourseCautions ?? false; set => HasFullCourseCautions = value; }

    [JsonPropertyName("full_course_cautions")]
    public bool? HasFullCourseCautions { get; set; }

    [JsonPropertyName("special_event_type")]
    public int? SpecialEventType { get; set; }

    [JsonPropertyName("start_zone")]
    public bool? HasStartZone { get; set; }

    [JsonPropertyName("enable_pitlane_collisions")]
    public bool? PitlaneCollisionsEnabled { get; set; }

    [JsonPropertyName("short_parade_lap")]
    public bool? HasShortParadeLap { get; set; }

    [JsonPropertyName("track")]
    public Track Track { get; set; } = default!;

    [JsonPropertyName("weather")]
    public Weather Weather { get; set; } = default!;

    [JsonPropertyName("track_state")]
    public TrackState TrackState { get; set; } = default!;

    [JsonPropertyName("race_time_descriptors")]
    public RaceTimeDescriptors[]? RaceTimeDescriptors { get; set; }

    [JsonPropertyName("car_restrictions")]
    public CarRestrictions[] CarRestrictions { get; set; } = Array.Empty<CarRestrictions>();
}
