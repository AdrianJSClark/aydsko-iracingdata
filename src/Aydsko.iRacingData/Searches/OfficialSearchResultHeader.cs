﻿// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Searches;

public class OfficialSearchResultHeader : IChunkInfoResultHeader<OfficialSearchResultHeaderData>
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("data")]
    public OfficialSearchResultHeaderData Data { get; set; } = default!;
}

public class OfficialSearchResultHeaderData : IChunkInfoResultHeaderData
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("chunk_info")]
    public ChunkInfo ChunkInfo { get; set; } = default!;

    [JsonPropertyName("params")]
    public OfficialSearchParameters Params { get; set; } = default!;
}

[JsonSerializable(typeof(OfficialSearchResultHeader)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class OfficialSearchResultHeaderContext : JsonSerializerContext
{ }
