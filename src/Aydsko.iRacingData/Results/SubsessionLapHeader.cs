// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Results;

public class SubsessionLapsHeader
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("session_info")]
    public SessionInfo SessionInfo { get; set; } = null!;

    [JsonPropertyName("best_lap_num")]
    public int BestLapNum { get; set; }

    [JsonPropertyName("best_lap_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? BestLapTime { get; set; }

    [JsonPropertyName("best_nlaps_num")]
    public int BestNlapsNum { get; set; }

    [JsonPropertyName("best_nlaps_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? BestNlapsTime { get; set; }

    [JsonPropertyName("best_qual_lap_num")]
    public int BestQualifyingLapNum { get; set; }

    [JsonPropertyName("best_qual_lap_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? BestQualifyingLapTime { get; set; }

    [JsonPropertyName("best_qual_lap_at")]
    public object? BestQualifyingLapAt { get; set; }

    [JsonPropertyName("chunk_info")]
    public ChunkInfo ChunkInfo { get; set; } = null!;
}

[JsonSerializable(typeof(SubsessionLapsHeader)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SubsessionLapsHeaderContext : JsonSerializerContext
{ }
