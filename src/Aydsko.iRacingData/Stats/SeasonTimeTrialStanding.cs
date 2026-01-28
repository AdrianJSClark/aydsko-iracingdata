// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Stats;

public class SeasonTimeTrialStanding
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
    public string CountryCode { get; set; } = null!;

    [JsonPropertyName("country")]
    public string Country { get; set; } = null!;

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

    [JsonPropertyName("weeks_counted")]
    public int WeeksCounted { get; set; }

    [JsonPropertyName("starts")]
    public int Starts { get; set; }

    [JsonPropertyName("wins")]
    public int Wins { get; set; }

    [JsonPropertyName("top5")]
    public int Top5 { get; set; }

    [JsonPropertyName("top25_percent")]
    public int Top25Percent { get; set; }

    [JsonPropertyName("poles")]
    public int Poles { get; set; }

    [JsonPropertyName("avg_start_position")]
    public decimal AverageStartPosition { get; set; }

    [JsonPropertyName("avg_finish_position")]
    public decimal AverageFinishPosition { get; set; }

    [JsonPropertyName("avg_field_size")]
    public decimal AverageFieldSize { get; set; }

    [JsonPropertyName("laps")]
    public int Laps { get; set; }

    [JsonPropertyName("laps_led")]
    public int LapsLed { get; set; }

    [JsonPropertyName("incidents")]
    public int Incidents { get; set; }

    [JsonPropertyName("points")]
    public int Points { get; set; }

    [JsonPropertyName("raw_points")]
    public decimal RawPoints { get; set; }

    [JsonPropertyName("week_dropped")]
    public bool WeekDropped { get; set; }
}

[JsonSerializable(typeof(SeasonTimeTrialStanding[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SeasonTimeTrialStandingArrayContext : JsonSerializerContext
{ }
