// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Runtime.Serialization;

namespace Aydsko.iRacingData.Exceptions;

[Serializable]
public class iRacingUnauthorizedResponseException : iRacingDataClientException
{
    public static iRacingUnauthorizedResponseException Create(string? message)
    {
        return new($"The iRacing API returned an \"Unauthorized\" response code{(string.IsNullOrEmpty(message)?"":" with message \"" + message + "\"")}.");
    }

    public iRacingUnauthorizedResponseException()
    { }

    public iRacingUnauthorizedResponseException(string message)
        : base(message)
    { }

#if NET8_0_OR_GREATER
    [Obsolete("Apply cross-targeting work-around for SYSLIB0051 Diagnostic (https://learn.microsoft.com/en-us/dotnet/fundamentals/syslib-diagnostics/syslib0051)", DiagnosticId = "SYSLIB0051")]
#endif
    public iRacingUnauthorizedResponseException(string message, Exception innerException)
        : base(message, innerException)
    { }

#if NET8_0_OR_GREATER
    [Obsolete("Apply cross-targeting work-around for SYSLIB0051 Diagnostic (https://learn.microsoft.com/en-us/dotnet/fundamentals/syslib-diagnostics/syslib0051)", DiagnosticId = "SYSLIB0051")]
#endif
    protected iRacingUnauthorizedResponseException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    { }
}
