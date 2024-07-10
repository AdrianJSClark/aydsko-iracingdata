// © 2024 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Net;
using System.Runtime.Serialization;

namespace Aydsko.iRacingData.Exceptions;

[Serializable]
public class iRacingUnknownResponseException : iRacingDataClientException
{
    public HttpStatusCode? ResponseHttpStatusCode { get; private set; }
    public string? ResponseContent { get; }

    public static iRacingUnknownResponseException Create(HttpStatusCode responseStatusCode, string httpContent, string? message = null)
    {
        var exceptionMessage = message ?? "Unknown error response.";
        return new iRacingUnknownResponseException(exceptionMessage, responseStatusCode, httpContent);
    }

    public static iRacingUnknownResponseException Create(Exception ex)
    {
        return new iRacingUnknownResponseException("Unknown error response.", ex);
    }

    public iRacingUnknownResponseException() { }

    public iRacingUnknownResponseException(string message)
        : base(message)
    { }

    public iRacingUnknownResponseException(string message, HttpStatusCode responseStatusCode, string httpContent)
        : base(message)
    {
        ResponseHttpStatusCode = responseStatusCode;
        ResponseContent = httpContent;
    }

    public iRacingUnknownResponseException(string message, Exception inner)
        : base(message, inner)
    { }

#if NET8_0_OR_GREATER
    [Obsolete("Apply cross-targeting work-around for SYSLIB0051 Diagnostic (https://learn.microsoft.com/en-us/dotnet/fundamentals/syslib-diagnostics/syslib0051)", DiagnosticId = "SYSLIB0051")]
#endif
    protected iRacingUnknownResponseException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        ResponseHttpStatusCode = info.GetValue(nameof(ResponseHttpStatusCode), typeof(HttpStatusCode?)) as HttpStatusCode?;
        ResponseContent = info.GetString(nameof(ResponseContent));
    }

#if NET8_0_OR_GREATER
    [Obsolete("Apply cross-targeting work-around for SYSLIB0051 Diagnostic (https://learn.microsoft.com/en-us/dotnet/fundamentals/syslib-diagnostics/syslib0051)", DiagnosticId = "SYSLIB0051")]
#endif
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(ResponseHttpStatusCode), ResponseHttpStatusCode);
        info.AddValue(nameof(ResponseContent), ResponseContent);
    }
}
