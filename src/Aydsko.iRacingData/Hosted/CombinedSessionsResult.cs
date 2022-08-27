// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Hosted;

public class CombinedSessionsResult
{
    [JsonPropertyName("subscribed")]
    public bool Subscribed { get; set; }

    [JsonPropertyName("sequence")]
    public int Sequence { get; set; }

    [JsonPropertyName("sessions")]
    public Session[] Sessions { get; set; } = Array.Empty<Session>();

    [JsonPropertyName("success")]
    public bool Success { get; set; }
}

[JsonSerializable(typeof(CombinedSessionsResult)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class CombinedSessionsResultContext : JsonSerializerContext
{ }
