// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Common;

public sealed class DataUrlResult
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("data")]
    public DataUrlResultData Data { get; set; } = default!;

    [JsonPropertyName("data_url")]
    public string DataUrl { get; set; } = default!;
}

public sealed class DataUrlResultData
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }
}

[JsonSerializable(typeof(DataUrlResult)), JsonSourceGenerationOptions(WriteIndented = true)]
public partial class DataUrlResultContext : JsonSerializerContext
{ }
