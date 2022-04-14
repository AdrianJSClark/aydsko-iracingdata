// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Stats;

public class SeasonTimeTrialResult
{
    [JsonPropertyName("rank")]
    public int Rank { get; set; }

    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = null!;

    [JsonPropertyName("division")]
    public int Division { get; set; }

    [JsonPropertyName("club_id")]
    public int ClubId { get; set; }

    [JsonPropertyName("club_name")]
    public string ClubName { get; set; } = null!;

    [JsonPropertyName("license")]
    public License License { get; set; } = null!;

    [JsonPropertyName("best_nlaps_time")]
    public int BestNlapsTime { get; set; }

    [JsonPropertyName("starts")]
    public int Starts { get; set; }
}

[JsonSerializable(typeof(SeasonTimeTrialResult[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SeasonTimeTrialResultArrayContext : JsonSerializerContext
{ }
