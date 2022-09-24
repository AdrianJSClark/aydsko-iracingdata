// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Hosted;

public class HostedSessionsResult
{
    [JsonPropertyName("subscribed")]
    public bool Subscribed { get; set; }

    [JsonPropertyName("sessions")]
    public Session[] Sessions { get; set; } = Array.Empty<Session>();

    [JsonPropertyName("success")]
    public bool Success { get; set; }
}

[JsonSerializable(typeof(HostedSessionsResult)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class HostedSessionsResultContext : JsonSerializerContext
{ }
