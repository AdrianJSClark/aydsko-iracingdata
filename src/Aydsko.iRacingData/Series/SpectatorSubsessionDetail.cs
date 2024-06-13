// © 2024 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

/// <summary>Details of a subsession available to spectate.</summary>
public class SpectatorSubsessionDetail
{
    /// <summary>The subsession identifier.</summary>
    [JsonPropertyName("subsession_id")]
    public int SubsessionId { get; set; }

    /// <summary>The session identifier.</summary>
    /// <remarks>All subsessions which are instances of the same race share a session identifier value.</remarks>
    [JsonPropertyName("session_id")]
    public int SessionId { get; set; }

    /// <summary>The season identifier for which this subsession is a part.</summary>
    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    /// <summary>The start time of the subsession.</summary>
    [JsonPropertyName("start_time")]
    public DateTimeOffset StartTime { get; set; }

    /// <summary>The week number of the season for which this subsession is a part.</summary>
    [JsonPropertyName("race_week_num")]
    public int RaceWeekNum { get; set; }

    /// <summary>The event type of the subsession.</summary>
    [JsonPropertyName("event_type")]
    public EventType EventType { get; set; }
}
