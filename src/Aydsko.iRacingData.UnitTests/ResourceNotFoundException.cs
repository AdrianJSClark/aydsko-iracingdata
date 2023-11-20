// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Runtime.Serialization;

namespace Aydsko.iRacingData.UnitTests;

[Serializable]
public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException()
    {
    }

    public ResourceNotFoundException(string? message)
        : base(message)
    {
    }

    public ResourceNotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

#if NET8_0_OR_GREATER
    [Obsolete("Apply cross-targeting work-around for SYSLIB0051 Diagnostic (https://learn.microsoft.com/en-us/dotnet/fundamentals/syslib-diagnostics/syslib0051)", DiagnosticId = "SYSLIB0051")]
#endif
    protected ResourceNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public static Exception ForManifestResourceName(string manifestResourceName)
    {
        return new ResourceNotFoundException($"Failed to locate resource with name \"{manifestResourceName}\".");
    }
}
