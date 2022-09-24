// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Hosted;

public class CombinedSessionsResult : HostedSessionsResult
{
    [JsonPropertyName("sequence")]
    public int Sequence { get; set; }
}

[JsonSerializable(typeof(CombinedSessionsResult)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class CombinedSessionsResultContext : JsonSerializerContext
{ }
