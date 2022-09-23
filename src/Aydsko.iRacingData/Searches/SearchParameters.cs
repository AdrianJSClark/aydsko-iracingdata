// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Searches;

/// <summary>Basic parameters shared between search calls.</summary>
public class SearchParameters
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

    /// <summary>Track categories to include in the search.</summary>
    /// <remarks>Defaults to all.</remarks>
    /// <seealso cref="Constants.Category"/>
    [JsonPropertyName("category_ids")]
    public int[]? CategoryIds { get; set; }
}
