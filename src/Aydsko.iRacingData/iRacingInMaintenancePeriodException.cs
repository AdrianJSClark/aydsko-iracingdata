// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData;

[Serializable]
public class iRacingInMaintenancePeriodException : iRacingDataClientException
{
    public iRacingInMaintenancePeriodException()
    {
        HelpLink = "https://status.iracing.com/";
    }

    public iRacingInMaintenancePeriodException(string message) : base(message)
    {
        HelpLink = "https://status.iracing.com/";
    }

    public iRacingInMaintenancePeriodException(string message, Exception inner) : base(message, inner)
    {
        HelpLink = "https://status.iracing.com/";
    }

    protected iRacingInMaintenancePeriodException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
