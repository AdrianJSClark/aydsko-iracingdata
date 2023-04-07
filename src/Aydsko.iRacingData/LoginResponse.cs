// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData;

/// <summary>Result of a login request.</summary>
public class LoginResponse
{
    [JsonPropertyName("authcode"), JsonConverter(typeof(StringFromStringOrNumberConverter))]
    public string AuthenticationCode { get; set; } = default!;

    [JsonPropertyName("autoLoginSeries")]
    public object? AutoLoginSeries { get; set; }

    [JsonPropertyName("autoLoginToken")]
    public object? AutoLoginToken { get; set; }

    /// <summary>Unique identifier of the customer's account.</summary>
    /// <remarks>Contains <see langword="null"/> if the authentication was not successful.</remarks>
    [JsonPropertyName("custId")]
    public int? CustomerId { get; set; }

    /// <summary>Customer's email address.</summary>
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

    /// <summary>Indicates that CAPTCHA verification is required.</summary>
    /// <remarks>Authentication from this IP Address will continue to fail until the user authenticates with a browser and completes the CAPTCHA.</remarks>
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
