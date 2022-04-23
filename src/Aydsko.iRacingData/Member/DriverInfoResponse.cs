// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

public class DriverInfoResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("cust_ids")]
    public int[] CustomerIds { get; set; } = Array.Empty<int>();
    [JsonPropertyName("members")]
    public DriverInfo[] Drivers { get; set; } = Array.Empty<DriverInfo>();
}

[JsonSerializable(typeof(DriverInfoResponse)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class DriverInfoResponseContext : JsonSerializerContext
{ }
