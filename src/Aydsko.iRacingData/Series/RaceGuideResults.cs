// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

public class RaceGuideResults
{
    [JsonPropertyName("subscribed")]
    public bool Subscribed { get; set; }

    [JsonPropertyName("sessions")]
    public RaceGuideSession[] Sessions { get; set; } = default!;

    [JsonPropertyName("block_begin_time")]
    public DateTime BlockBeginTime { get; set; }

    [JsonPropertyName("block_end_time")]
    public DateTime BlockEndTime { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set; }
}

[JsonSerializable(typeof(RaceGuideResults)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class RaceGuideResultsContext : JsonSerializerContext
{ }
