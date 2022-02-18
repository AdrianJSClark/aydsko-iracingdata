// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData;

public class ErrorResponse
{
    [JsonPropertyName("error")]
    public string? ErrorCode { get; set; }
    [JsonPropertyName("note")]
    public string? ErrorDescription { get; set; }
}
