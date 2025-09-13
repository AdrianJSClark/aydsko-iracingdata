// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

public class DriverInfoResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("cust_ids")]
    public int[] CustomerIds { get; set; } = [];

    [JsonPropertyName("members")]
    public DriverInfo[] Drivers { get; set; } = [];
}

[JsonSerializable(typeof(DriverInfoResponse)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class DriverInfoResponseContext : JsonSerializerContext
{ }
