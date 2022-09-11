// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System;
using System.Runtime.Serialization;

namespace Aydsko.iRacingData.Exceptions;


[Serializable]
public class iRacingLoginFailedException : iRacingDataClientException
{
    public bool? VerificationRequired { get; private set; }

    public static iRacingLoginFailedException Create(string? message, bool? verificationRequired = null)
    {
        var exceptionMessage = message ?? "Login to iRacing failed.";

        return verificationRequired is null
            ? new iRacingLoginFailedException(exceptionMessage)
            : new iRacingLoginFailedException(exceptionMessage, verificationRequired.Value);
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

    public iRacingLoginFailedException(string message, Exception inner)
        : base(message, inner)
    { }

    protected iRacingLoginFailedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        VerificationRequired = info.GetValue(nameof(VerificationRequired), typeof(bool?)) as bool?;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(VerificationRequired), VerificationRequired);
    }
}
