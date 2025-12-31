// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Series;

public class SeasonSchedule
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("schedules")]
    public SeasonScheduleItem[] Schedules { get; set; } = [];
}

public class SeasonScheduleItem
{
    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("race_week_num")]
    public int RaceWeekNum { get; set; }

    [JsonPropertyName("car_restrictions")]
    public CarRestrictions[] CarRestrictions { get; set; } = [];

    [JsonPropertyName("category")]
    public string Category { get; set; } = default!;

    [JsonPropertyName("category_id")]
    public int CategoryId { get; set; }

    [JsonPropertyName("enable_pitlane_collisions")]
    public bool EnablePitlaneCollisions { get; set; }

    [JsonPropertyName("full_course_cautions")]
    public bool FullCourseCautions { get; set; }

    [JsonPropertyName("practice_length")]
    public int PracticeLength { get; set; }

    [JsonPropertyName("qual_attached")]
    public bool QualAttached { get; set; }

    [JsonPropertyName("qual_time_descriptors")]
    public RaceTimeDescriptors[] QualifyingTimeDescriptors { get; set; } = [];

    [JsonPropertyName("qualify_laps")]
    public int QualifyLaps { get; set; }

    [JsonPropertyName("qualify_length")]
    public int QualifyLength { get; set; }

    [JsonPropertyName("race_lap_limit")]
    public int? RaceLapLimit { get; set; }

    [JsonPropertyName("race_time_descriptors")]
    public RaceTimeDescriptors[] RaceTimeDescriptors { get; set; } = [];

    [JsonPropertyName("race_time_limit")]
    public int? RaceTimeLimitMinutes { get; set; }

    [JsonIgnore]
    public TimeSpan? RaceTimeLimit => RaceTimeLimitMinutes is int timeLimitMinutes ? TimeSpan.FromMinutes(timeLimitMinutes) : null;

    [JsonPropertyName("race_week_car_class_ids")]
    public int[] RaceWeekCarClassIds { get; set; } = [];

    [JsonPropertyName("race_week_cars")]
    public SeasonScheduleCar[] RaceWeekCars { get; set; } = [];

    [JsonPropertyName("restart_type")]
    public string RestartType { get; set; } = default!;

    [JsonPropertyName("schedule_name")]
    public string ScheduleName { get; set; } = default!;

    [JsonPropertyName("season_name")]
    public string SeasonName { get; set; } = default!;

    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }

    [JsonPropertyName("series_name")]
    public string SeriesName { get; set; } = default!;

    [JsonPropertyName("short_parade_lap")]
    public bool ShortParadeLap { get; set; }

    [JsonPropertyName("special_event_type")]
    public object? SpecialEventType { get; set; }

#if NET6_0_OR_GREATER
    [JsonPropertyName("start_date"), JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly StartDate { get; set; } = default!;
#else
    [JsonPropertyName("start_date"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime StartDate { get; set; } = default!;
#endif

    [JsonPropertyName("start_type")]
    public string StartType { get; set; } = default!;

    [JsonPropertyName("start_zone")]
    public bool StartZone { get; set; }

    [JsonPropertyName("track")]
    public Track Track { get; set; } = default!;

    [JsonPropertyName("track_state")]
    public SeasonScheduleTrackState TrackState { get; set; } = default!;

    [JsonPropertyName("warmup_length")]
    public int WarmupLength { get; set; }

    [JsonPropertyName("weather")]
    public Weather Weather { get; set; } = default!;

    [JsonPropertyName("week_end_time")]
    public DateTimeOffset WeekEndTime { get; set; }
}

public class SeasonScheduleCar
{
    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("car_name")]
    public string CarName { get; set; } = default!;

    [JsonPropertyName("car_name_abbreviated")]
    public string CarNameAbbreviated { get; set; } = default!;
}

public class SeasonScheduleTrackState
{
    [JsonPropertyName("leave_marbles")]
    public bool LeaveMarbles { get; set; }
}


[JsonSerializable(typeof(SeasonSchedule)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SeasonScheduleContext : JsonSerializerContext
{ }
