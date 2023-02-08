// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.TimeAttack;

public class TimeAttackMemberSeasonResult
{
    [JsonPropertyName("comp_season_id")]
    public int CompetitionSeasonId { get; set; }

    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("track_id")]
    public int TrackId { get; set; }

    [JsonPropertyName("best_lap_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? BestLapTime { get; set; }

    [JsonPropertyName("best_lap_at")]
    public DateTimeOffset BestLapAt { get; set; }

    [JsonPropertyName("record_date")]
    public DateTimeOffset RecordDate { get; set; }

    [JsonPropertyName("comparative_rank")]
    public int ComparativeRank { get; set; }

    [JsonPropertyName("decile")]
    public int Decile { get; set; }

    [JsonPropertyName("percent_rank")]
    public decimal PercentRank { get; set; }

    [JsonPropertyName("time_behind_first_of_next_decile"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? TimeBehindFirstOfNextDecile { get; set; }

    [JsonPropertyName("time_behind_last_of_next_decile"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? TimeBehindLastOfNextDecile { get; set; }
}

[JsonSerializable(typeof(TimeAttackMemberSeasonResult[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class TimeAttackMemberSeasonResultArrayContext : JsonSerializerContext
{ }
