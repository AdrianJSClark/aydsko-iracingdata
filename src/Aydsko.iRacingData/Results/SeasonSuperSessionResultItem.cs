namespace Aydsko.iRacingData.Results;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

public class SeasonSuperSessionResultItem
{
    [JsonPropertyName("rank")]
    public int Rank { get; set; }

    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }

    [JsonPropertyName("division")]
    public int Division { get; set; }

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("license")]
    public License License { get; set; }

    [JsonPropertyName("helmet")]
    public Helmet Helmet { get; set; }

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
    public bool WeekWasDropped { get; set; }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

[JsonSerializable(typeof(SeasonSuperSessionResultItem[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SeasonSuperSessionResultItemArrayContext : JsonSerializerContext
{ }
