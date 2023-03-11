namespace Aydsko.iRacingData.Stats;

/// <summary>A summary of statistics about either a member's year or season.</summary>
public class MemberRecap
{
    /// <summary>The year the statistics are for.</summary>
    [JsonPropertyName("year")]
    public int Year { get; set; }

    /// <summary>The statistics themselves.</summary>
    [JsonPropertyName("stats")]
    public RecapStatistics Statistics { get; set; } = default!;

    /// <summary>Indicates if the query was successful.</summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// The season the statistics were for, if the search was season-specific.
    /// It will be <see langword="null"/> if the statistics are for the whole year.
    /// </summary>
    [JsonPropertyName("season")]
    public int? Season { get; set; }

    /// <summary>iRacing Customer Id the statistics relate to.</summary>
    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }
}

[JsonSerializable(typeof(MemberRecap)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class MemberRecapContext : JsonSerializerContext
{ }
