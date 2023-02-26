// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

public class Account
{
    [JsonPropertyName("ir_dollars")]
    public decimal IRacingDollars { get; set; }

    [JsonPropertyName("ir_credits")]
    public decimal IRacingCredits { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = default!;
}
