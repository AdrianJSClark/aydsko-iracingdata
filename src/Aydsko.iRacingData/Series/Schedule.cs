// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;
using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Series;

public class Schedule
{
    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }
    [JsonPropertyName("race_week_num")]
    public int RaceWeekNum { get; set; }
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

    [JsonPropertyName("simulated_time_multiplier")]
    public int SimulatedTimeMultiplier { get; set; }
    [JsonPropertyName("race_lap_limit")]
    public int? RaceLapLimit { get; set; }
    [JsonPropertyName("race_time_limit")]
    public int? RaceTimeLimit { get; set; }
    [JsonPropertyName("start_type")]
    public string StartType { get; set; } = default!;
    [JsonPropertyName("restart_type")]
    public string RestartType { get; set; } = default!;
    [JsonPropertyName("qual_attached")]
    public bool QualAttached { get; set; }
    [JsonPropertyName("yellow_flags")]
    public bool YellowFlags { get; set; }
    [JsonPropertyName("special_event_type")]
    public int? SpecialEventType { get; set; }
    [JsonPropertyName("track")]
    public Track Track { get; set; } = default!;
    [JsonPropertyName("weather")]
    public Weather Weather { get; set; } = default!;
    [JsonPropertyName("track_state")]
    public TrackState TrackState { get; set; } = default!;
    [JsonPropertyName("car_restrictions")]
    public CarRestrictions[] CarRestrictions { get; set; } = Array.Empty<CarRestrictions>();
}
