// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class CustomerLeagueSessions
{
    [JsonPropertyName("mine")]
    public bool Mine { get; set; }

    [JsonPropertyName("subscribed")]
    public bool Subscribed { get; set; }

    [JsonPropertyName("sequence")]
    public int Sequence { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("sessions")]
    public CustomerLeagueSession[] Sessions { get; set; } = null!;
}

[JsonSerializable(typeof(CustomerLeagueSessions[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class CustomerLeagueSessionsContext : JsonSerializerContext
{ }
