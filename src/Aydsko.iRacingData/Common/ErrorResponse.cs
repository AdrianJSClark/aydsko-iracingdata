// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Common;

public class ErrorResponse
{
    [JsonPropertyName("error")]
    public string? ErrorCode { get; set; }

    [JsonPropertyName("note")]
    public string? Note { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; set; }
}
