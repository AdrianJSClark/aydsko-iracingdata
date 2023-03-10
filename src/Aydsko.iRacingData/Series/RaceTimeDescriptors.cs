using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Series;

public class RaceTimeDescriptors
{
    [JsonPropertyName("repeating")]
    public bool IsRepeating { get; set; }

    [JsonPropertyName("super_session")]
    public bool IsSuperSession { get; set; }

    [JsonPropertyName("session_minutes")]
    public int SessionMinutes { get; set; }

    [JsonPropertyName("session_times")]
    public DateTimeOffset[]? SessionTimes { get; set; }

#if NET6_0_OR_GREATER
    [JsonPropertyName("start_date"), JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly StartDate { get; set; } = default!;
#else
    [JsonPropertyName("start_date"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime StartDate { get; set; } = default!;
#endif

    [JsonPropertyName("day_offset")]
    public int[]? DayOffset { get; set; }

    [JsonPropertyName("first_session_time")]
    public TimeSpan? FirstSessionTime { get; set; }

    [JsonPropertyName("repeat_minutes")]
    public int? RepeatMinutes { get; set; }
}
