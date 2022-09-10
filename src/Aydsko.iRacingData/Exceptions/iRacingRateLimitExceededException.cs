// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Runtime.Serialization;

namespace Aydsko.iRacingData.Exceptions;

[Serializable]
public class iRacingRateLimitExceededException : iRacingDataClientException
{
    public static iRacingRateLimitExceededException Create()
    {
        return new("Rate limit exceeded");
    }

    public iRacingRateLimitExceededException()
    { }

    public iRacingRateLimitExceededException(string message)
        : base(message)
    { }

    public iRacingRateLimitExceededException(string message, Exception innerException)
        : base(message, innerException)
    { }

    protected iRacingRateLimitExceededException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    { }
}
