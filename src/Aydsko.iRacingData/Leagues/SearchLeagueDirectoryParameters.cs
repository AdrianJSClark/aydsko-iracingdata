namespace Aydsko.iRacingData.Leagues;

public class SearchLeagueDirectoryParameters
{
    /// <summary>Will search against league name, description, owner, and league ID.</summary>
    [JsonPropertyName("search")]
    public string? Search { get; set; }

    [JsonPropertyName("tag")]
    public IList<string> Tag { get; } = [];

    /// <summary>If <see langword="true"/> include only leagues for which customer is a member.</summary>
    [JsonPropertyName("restrict_to_member")]
    public bool? RestrictToMember { get; set; }

    /// <summary>If <see langword="true"/> include only leagues which are recruiting.</summary>
    [JsonPropertyName("restrict_to_recruiting")]
    public bool? RestrictToRecruiting { get; set; }

    /// <summary>If <see langword="true"/> include only leagues owned by a friend.</summary>
    [JsonPropertyName("restrict_to_friends")]
    public bool? RestrictToFriends { get; set; }

    /// <summary>If <see langword="true"/> include only leagues owned by a watched member.</summary>
    [JsonPropertyName("restrict_to_watched")]
    public bool? RestrictToWatched { get; set; }

    /// <summary>If set include leagues with at least this number of members.</summary>
    [JsonPropertyName("minimum_roster_count")]
    public int? MinimumRosterCount { get; set; }

    /// <summary>If set include leagues with no more than this number of members.</summary>
    [JsonPropertyName("maximum_roster_count")]
    public int? MaximumRosterCount { get; set; }

    /// <summary>First row of results to return. Defaults to 1.</summary>
    [JsonPropertyName("lowerbound")]
    public int? Lowerbound { get; set; }

    /// <summary>Last row of results to return. Defaults to <see cref="Lowerbound"/> + 39.</summary>
    [JsonPropertyName("upperbound")]
    public int? Upperbound { get; set; }

    /// <summary>Indicates which field to sort the results by.</summary>
    /// <remarks>Defaults to <see cref="SearchLeagueOrderByField.Relevance"/> if not specified.</remarks>
    [JsonPropertyName("sort")]
    public SearchLeagueOrderByField? OrderByField { get; set; }

    /// <summary>Indicates the direction of the results sort.</summary>
    /// <remarks>Defaults to <see cref="ResultOrderDirection.Ascending"/> if not specified.</remarks>
    [JsonPropertyName("order")]
    public ResultOrderDirection? OrderDirection { get; set; }
}
