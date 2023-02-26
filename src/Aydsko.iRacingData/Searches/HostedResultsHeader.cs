// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Searches;

public class HostedResultsHeader
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("data")]
    public HostedResultsHeaderData Data { get; set; } = default!;
}

public class HostedResultsHeaderData
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("chunk_info")]
    public ChunkInfo ChunkInfo { get; set; } = default!;

    [JsonPropertyName("params")]
    public HostedSearchParameters Params { get; set; } = default!;
}

[JsonSerializable(typeof(HostedResultsHeader)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class HostedResultsHeaderContext : JsonSerializerContext
{ }
