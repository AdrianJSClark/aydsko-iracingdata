// © 2022 Adrian Clark
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

    protected iRacingDataClientException(System.Runtime.Serialization.SerializationInfo info,
                                         System.Runtime.Serialization.StreamingContext context)
        : base(info, context)
    { }
}
