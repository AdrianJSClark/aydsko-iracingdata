namespace Aydsko.iRacingData.Results;

/// <summary>Parameters for a hosted or league session search.</summary>
/// <remarks>
/// Valid searches must be structured as follows:
/// <list type="bullet">
/// <item>requires one of: <see cref="StartRangeBegin"/>, <see cref="FinishRangeBegin"/></item>
/// <item>requires one of: <see cref="ParticipantCustomerId"/>, <see cref="HostCustomerId"/>, <see cref="SessionName"/></item>
/// </list>
/// </remarks>
public class HostedSearchParameters
{
    /// <summary>Beginning of the session start time search range.</summary>
    [JsonPropertyName("start_range_begin")]
    public DateTime? StartRangeBegin { get; set; }

    /// <summary>End of the session start time search range.</summary>
    /// <remarks>This value is exclusive. The end value may be omitted if <see cref="StartRangeBegin"/> is less than 90 days in the past.</remarks>
    [JsonPropertyName("start_range_end")]
    public DateTime? StartRangeEnd { get; set; }

    /// <summary>Beginning of the session finish time search range.</summary>
    [JsonPropertyName("finish_range_begin")]
    public DateTime? FinishRangeBegin { get; set; }

    /// <summary>End of the session finish time search range.</summary>
    /// <remarks>This value is exclusive. The end value may be omitted if <see cref="FinishRangeBegin"/> is less than 90 days in the past.</remarks>
    [JsonPropertyName("finish_range_end")]
    public DateTime? FinishRangeEnd { get; set; }

    /// <summary>Customer ID of a participant to search for in the sessions.</summary>
    [JsonPropertyName("cust_id")]
    public int? ParticipantCustomerId { get; set; }

    /// <summary>Customer ID of the host to search for in the sessions.</summary>
    [JsonPropertyName("host_cust_id")]
    public int? HostCustomerId { get; set; }

    /// <summary>Part or all of the session's name.</summary>
    [JsonPropertyName("session_name")]
    public string? SessionName { get; set; }

    /// <summary>Include results for this league.</summary>
    [JsonPropertyName("league_id")]
    public int? LeagueId { get; set; }

    /// <summary>Include results for the league season with this ID.</summary>
    [JsonPropertyName("league_season_id")]
    public int? LeagueSeasonId { get; set; }

    /// <summary>One of the cars used by the session.</summary>
    [JsonPropertyName("car_id")]
    public int? CarId { get; set; }

    /// <summary>ID of the track used by the session.</summary>
    [JsonPropertyName("track_id")]
    public int? TrackId { get; set; }

    /// <summary>Track categories to include in the search.</summary>
    /// <remarks>Defaults to all.</remarks>
    [JsonPropertyName("category_ids")]
    public int[]? CategoryIds { get; set; }
}
