// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Runtime.Serialization;

namespace Aydsko.iRacingData.Exceptions;

[Serializable]
public class iRacingForbiddenResponseException : iRacingDataClientException
{
    public static iRacingForbiddenResponseException Create()
    {
        return new("Requested result was forbidden.");
    }

    public iRacingForbiddenResponseException()
    {
    }

    public iRacingForbiddenResponseException(string message)
        : base(message)
    {
    }

    public iRacingForbiddenResponseException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected iRacingForbiddenResponseException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}
