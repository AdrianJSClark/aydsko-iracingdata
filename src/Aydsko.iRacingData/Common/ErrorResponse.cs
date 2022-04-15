// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Common;

/// <summary>Error details returned by the API.</summary>
public class ErrorResponse
{
    /// <summary>Identifying code of the error.</summary>
    [JsonPropertyName("error")]
    public string? ErrorCode { get; set; }

    /// <summary>Descriptive text of the error.</summary>
    [JsonPropertyName("note")]
    public string? ErrorDescription { get; set; }
}
