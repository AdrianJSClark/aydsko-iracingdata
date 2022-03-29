using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Series;

public class RaceWeeks
{

    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("race_week_num")]
    public int RaceWeekNum { get; set; }

    [JsonPropertyName("track")]
    public Track Track { get; set; } = default!;
}
