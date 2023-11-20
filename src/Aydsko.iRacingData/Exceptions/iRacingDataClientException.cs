// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Exceptions;

[Serializable]
public class iRacingDataClientException : Exception
{
    public iRacingDataClientException()
    { }

    public iRacingDataClientException(string message)
        : base(message)
    { }

    public iRacingDataClientException(string message, Exception inner)
        : base(message, inner)
    { }

#if NET8_0_OR_GREATER
    [Obsolete("Apply cross-targeting work-around for SYSLIB0051 Diagnostic (https://learn.microsoft.com/en-us/dotnet/fundamentals/syslib-diagnostics/syslib0051)", DiagnosticId = "SYSLIB0051")]
#endif
    protected iRacingDataClientException(System.Runtime.Serialization.SerializationInfo info,
                                         System.Runtime.Serialization.StreamingContext context)
        : base(info, context)
    { }
}
