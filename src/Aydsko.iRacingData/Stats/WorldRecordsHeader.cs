// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Stats;

public class WorldRecordsHeader
{
    [JsonPropertyName("type")]
    public string ResultType { get; set; } = default!;

    [JsonPropertyName("data")]
    public WorldRecordsHeaderInfo Data { get; set; } = default!;
}

[JsonSerializable(typeof(WorldRecordsHeader)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class WorldRecordsHeaderContext : JsonSerializerContext
{ }
