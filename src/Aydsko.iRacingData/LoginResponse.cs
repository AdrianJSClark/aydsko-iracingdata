// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;
using Aydsko.iRacingData.Lookups;

namespace Aydsko.iRacingData;

public class LoginResponse
{
    [JsonPropertyName("authcode"), JsonConverter(typeof(StringFromStringOrNumberConverter))]
    public string AuthenticationCode { get; set; } = default!;

    [JsonPropertyName("autoLoginSeries")]
    public object? AutoLoginSeries { get; set; }

    [JsonPropertyName("autoLoginToken")]
    public object? AutoLoginToken { get; set; }

    [JsonPropertyName("custId")]
    public int? CustomerId { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("ssoCookieDomain")]
    public string? SsoCookieDomain { get; set; }

    [JsonPropertyName("ssoCookieName")]
    public string? SsoCookieName { get; set; }

    [JsonPropertyName("ssoCookiePath")]
    public string? SsoCookiePath { get; set; }

    [JsonPropertyName("ssoCookieValue")]
    public string? SsoCookieValue { get; set; }

    [JsonPropertyName("inactive")]
    public bool? IsInactive { get; set; }

    [JsonPropertyName("verificationRequired")]
    public bool? VerificationRequired { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonIgnore]
    public bool Success => (CustomerId is not null) && (AuthenticationCode != "0");
}

[JsonSerializable(typeof(LoginResponse)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class LoginResponseContext : JsonSerializerContext
{ }
