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

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; } = default!;

    [JsonPropertyName("country")]
    public string Country { get; set; } = default!;

    [JsonPropertyName("flair_id")]
    public int FlairId { get; set; }

    [JsonPropertyName("flair_name")]
    public string FlairName { get; set; } = default!;

    [JsonPropertyName("flair_shortname")]
    public string? FlairShortName { get; set; }

    [JsonPropertyName("license")]
    public License License { get; set; } = null!;

    [JsonPropertyName("helmet")]
    public Helmet Helmet { get; set; } = default!;

    [JsonPropertyName("best_nlaps_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? BestNlapsTime { get; set; }

    [JsonPropertyName("starts")]
    public int Starts { get; set; }

    [JsonPropertyName("points")]
    public int Points { get; set; }

    [JsonPropertyName("raw_points")]
    public decimal RawPoints { get; set; }

    [JsonPropertyName("week")]
    public int Week { get; set; }
}

[JsonSerializable(typeof(SeasonTimeTrialResult[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SeasonTimeTrialResultArrayContext : JsonSerializerContext
{ }
