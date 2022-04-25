// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Runtime.Serialization;

namespace Aydsko.iRacingData.Exceptions;

[Serializable]
public class iRacingClientOptionsValueMissingException : iRacingDataClientException
{
    public static iRacingClientOptionsValueMissingException Create(string propertyName)
    {
        return new($"Required iRacingDataClientOptions value \"{propertyName}\" is null or whitespace.");
    }

    public iRacingClientOptionsValueMissingException()
    { }

    public iRacingClientOptionsValueMissingException(string message)
        : base(message)
    { }

    public iRacingClientOptionsValueMissingException(string message, Exception innerException)
        : base(message, innerException)
    { }

    protected iRacingClientOptionsValueMissingException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    { }
}
