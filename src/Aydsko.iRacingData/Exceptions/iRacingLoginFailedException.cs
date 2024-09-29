// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Runtime.Serialization;

namespace Aydsko.iRacingData.Exceptions;

[Serializable]
public class iRacingLoginFailedException : iRacingDataClientException
{
    /// <summary>Indicates the account requires the user to authenticate via a browser to complete a CAPTCHA or contact iRacing Support.</summary>
    public bool? VerificationRequired { get; private set; }
    /// <summary>If set to <see langword="true"/> the user account must be configured for &quot;Legacy Authentication&quot; to bypass multi-factor authentication.</summary>
    public bool? LegacyAuthenticationRequired { get; private set; }

    public static iRacingLoginFailedException Create(string? message, bool? verificationRequired = null, bool? legacyAuthenticationRequired = null)
    {
        var exceptionMessage = message ?? "Login to iRacing failed.";

        return verificationRequired is null && legacyAuthenticationRequired is null
            ? new iRacingLoginFailedException(exceptionMessage)
            : new iRacingLoginFailedException(exceptionMessage, verificationRequired ?? false, legacyAuthenticationRequired ?? false);
    }

    public static iRacingLoginFailedException Create(Exception ex)
    {
        return new iRacingLoginFailedException("Login to iRacing failed.", ex);
    }

    public iRacingLoginFailedException() { }

    public iRacingLoginFailedException(string message)
        : base(message)
    { }

    public iRacingLoginFailedException(string message, bool verificationRequired)
        : base(message)
    {
        VerificationRequired = verificationRequired;
    }

    public iRacingLoginFailedException(string message, bool verificationRequired, bool legacyAuthenticationRequired)
        : base(message)
    {
        VerificationRequired = verificationRequired;
        LegacyAuthenticationRequired = legacyAuthenticationRequired;
    }

    public iRacingLoginFailedException(string message, Exception inner)
        : base(message, inner)
    { }

#if NET8_0_OR_GREATER
    [Obsolete("Apply cross-targeting work-around for SYSLIB0051 Diagnostic (https://learn.microsoft.com/en-us/dotnet/fundamentals/syslib-diagnostics/syslib0051)", DiagnosticId = "SYSLIB0051")]
#endif
    protected iRacingLoginFailedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        VerificationRequired = info.GetValue(nameof(VerificationRequired), typeof(bool?)) as bool?;
    }

#if NET8_0_OR_GREATER
    [Obsolete("Apply cross-targeting work-around for SYSLIB0051 Diagnostic (https://learn.microsoft.com/en-us/dotnet/fundamentals/syslib-diagnostics/syslib0051)", DiagnosticId = "SYSLIB0051")]
#endif
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(VerificationRequired), VerificationRequired);
    }
}
