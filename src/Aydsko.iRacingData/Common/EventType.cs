// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Common;

/// <summary>The type of the event.</summary>
public enum EventType
{
    /// <summary>Event type was not known.</summary>
    Unknown = 0,
    /// <summary>Event was a practice session.</summary>
    Practice = 2,
    /// <summary>Event was a qualifying session.</summary>
    Qualify = 3,
    /// <summary>Event was a time trial session.</summary>
    TimeTrial = 4,
    /// <summary>Event was a race session.</summary>
    Race = 5,
}
