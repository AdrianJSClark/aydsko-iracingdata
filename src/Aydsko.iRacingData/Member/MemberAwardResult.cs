namespace Aydsko.iRacingData.Member;

public class MemberAwardResult
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("data")]
    public MemberAwardResultData Data { get; set; } = default!;

    [JsonPropertyName("data_url")]
    public string DataUrl { get; set; } = default!;
}

public class MemberAwardResultData
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }
    [JsonPropertyName("award_count")]
    public int AwardCount { get; set; }
}

[JsonSerializable(typeof(MemberAwardResult)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class MemberAwardResultContext : JsonSerializerContext
{ }
