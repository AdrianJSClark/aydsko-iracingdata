// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Common;

/// <summary>Summary details of a large data set which has been split into &quot;chunks&quot;.</summary>
public class ChunkInfo
{
    /// <summary>Size of each chunk.</summary>
    [JsonPropertyName("chunk_size")]
    public int ChunkSize { get; set; }

    /// <summary>The number of chunks the data was split into.</summary>
    [JsonPropertyName("num_chunks")]
    public int NumChunks { get; set; }

    /// <summary>Total number of rows.</summary>
    [JsonPropertyName("rows")]
    public int Rows { get; set; }

    /// <summary>Common part of the URL for each chunk.</summary>
    [JsonPropertyName("base_download_url")]
    public string BaseDownloadUrl { get; set; } = null!;

    /// <summary>List of the filename for each chunk.</summary>
    [JsonPropertyName("chunk_file_names")]
    public string[] ChunkFileNames { get; set; } = null!;
}
