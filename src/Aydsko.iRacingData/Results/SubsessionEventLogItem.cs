// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

public class SubsessionEventLogItem
{
    [JsonPropertyName("subsession_id")]
    public int SubsessionId { get; set; }

    [JsonPropertyName("simsession_number")]
    public int SimsessionNumber { get; set; }

    [JsonPropertyName("session_time")]
    public int SessionTime { get; set; }

    [JsonPropertyName("event_seq")]
    public int EventSeq { get; set; }

    [JsonPropertyName("event_code")]
    public int EventCode { get; set; }

    [JsonPropertyName("group_id")]
    public int GroupId { get; set; }

    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("lap_number")]
    public int LapNumber { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    [JsonPropertyName("message")]
    public string Message { get; set; } = default!;

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = default!;
}

[JsonSerializable(typeof(SubsessionEventLogItem[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SubsessionEventLogItemArrayContext : JsonSerializerContext
{ }
