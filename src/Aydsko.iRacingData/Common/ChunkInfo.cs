// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Common;

public interface IChunkInfo
{
    string BaseDownloadUrl { get; set; }
    string[] ChunkFileNames { get; set; }
    int NumberOfChunks { get; set; }
}

public class ChunkInfo : IChunkInfo
{
    [JsonPropertyName("chunk_size")]
    public int ChunkSize { get; set; }

    [JsonPropertyName("num_chunks")]
    public int NumberOfChunks { get; set; }

    [JsonPropertyName("rows")]
    public int Rows { get; set; }

    [JsonPropertyName("base_download_url")]
    public string BaseDownloadUrl { get; set; } = null!;

    [JsonPropertyName("chunk_file_names")]
    public string[] ChunkFileNames { get; set; } = null!;
}
