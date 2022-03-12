// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Results;

public class SubsessionLapsHeader
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("session_info")]
    public SessionInfo SessionInfo { get; set; } = null!;
    [JsonPropertyName("best_lap_num")]
    public int BestLapNum { get; set; }
    [JsonPropertyName("best_lap_time")]
    public int BestLapTime { get; set; }
    [JsonPropertyName("best_nlaps_num")]
    public int BestNlapsNum { get; set; }
    [JsonPropertyName("best_nlaps_time")]
    public int BestNlapsTime { get; set; }
    [JsonPropertyName("best_qual_lap_num")]
    public int BestQualLapNum { get; set; }
    [JsonPropertyName("best_qual_lap_time")]
    public int BestQualLapTime { get; set; }
    [JsonPropertyName("best_qual_lap_at")]
    public object? BestQualLapAt { get; set; }
    [JsonPropertyName("chunk_info")]
    public ChunkInfo ChunkInfo { get; set; } = null!;
}

[JsonSerializable(typeof(SubsessionLapsHeader)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SubsessionLapsHeaderContext : JsonSerializerContext
{ }
