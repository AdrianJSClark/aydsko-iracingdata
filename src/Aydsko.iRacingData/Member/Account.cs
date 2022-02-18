// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Member;

public class Account
{
    [JsonPropertyName("ir_dollars")]
    public int IRacingDollars { get; set; }
    [JsonPropertyName("ir_credits")]
    public int IRacingCredits { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; } = default!;
}
