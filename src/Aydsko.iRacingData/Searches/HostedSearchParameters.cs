// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Searches;

/// <summary>Parameters for a hosted or league session search.</summary>
/// <remarks>
/// Valid searches must be structured as follows:
/// <list type="bullet">
/// <item>requires one of: <see cref="SearchParameters.StartRangeBegin"/>, <see cref="SearchParameters.FinishRangeBegin"/></item>
/// <item>requires one of: <see cref="SearchParameters.ParticipantCustomerId"/>, <see cref="HostCustomerId"/>, <see cref="SessionName"/></item>
/// </list>
/// </remarks>
public class HostedSearchParameters : SearchParameters
{
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
}
