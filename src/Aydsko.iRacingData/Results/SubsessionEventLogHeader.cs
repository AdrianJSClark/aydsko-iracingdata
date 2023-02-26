// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

public class SubsessionEventLogHeader
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("session_info")]
    public SessionInfo SessionInfo { get; set; } = null!;

    [JsonPropertyName("chunk_info")]
    public ChunkInfo ChunkInfo { get; set; } = null!;
}

[JsonSerializable(typeof(SubsessionEventLogHeader)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SubsessionEventLogHeaderContext : JsonSerializerContext
{ }
