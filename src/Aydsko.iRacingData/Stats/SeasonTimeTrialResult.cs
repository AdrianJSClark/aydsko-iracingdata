// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

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

    [JsonPropertyName("license")]
    public License License { get; set; } = null!;

    [JsonPropertyName("best_nlaps_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? BestNlapsTime { get; set; }

    [JsonPropertyName("starts")]
    public int Starts { get; set; }
}

[JsonSerializable(typeof(SeasonTimeTrialResult[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SeasonTimeTrialResultArrayContext : JsonSerializerContext
{ }
