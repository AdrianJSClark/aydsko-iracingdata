// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

// Source: https://forums.iracing.com/discussion/comment/330994/#Comment_330994

/// <summary>Events which may occur during a lap or characteristics of the lap itself.</summary>
/// <seealso href="https://forums.iracing.com/discussion/comment/330994/#Comment_330994" />
[Flags]
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix - This matches the terminology in the API.
public enum LapFlags
{
    /// <summary>No events.</summary>
    None = 0,
    /// <summary>Indicates the lap was not valid.</summary>
    Invalid = 0x0001,
    /// <summary>The driver entered or exited the pits.</summary>
    Pitted = 0x0002,
    /// <summary>The driver left the track surface.</summary>
    OffTrack = 0x0004,
    /// <summary>The driver was shown a black flag.</summary>
    BlackFlag = 0x0008,
    /// <summary>Driver reset the car.</summary>
    CarReset = 0x0010,
    /// <summary>The car made contact.</summary>
    Contact = 0x0020,
    /// <summary>The car made contact with another car.</summary>
    CarContact = 0x0040,
    /// <summary>The driver lost control of their vehicle.</summary>
    LostControl = 0x0080,
    /// <summary>There was an interruption in the lap data.</summary>
    Discontinuity = 0x0100,
    /// <summary></summary>
    InterpolatedCrossing = 0x0200,
    /// <summary></summary>
    ClockSmash = 0x0400,
    /// <summary>The driver towed the car.</summary>
    Tow = 0x0800,
    /// <summary>The driver was changed for another one.</summary>
    DriverChange = 0x1000,
    /// <summary>It is the initial lap of the race.</summary>
    FirstLap = 0x2000,
    /// <summary>The checkered flag was shown on this lap.</summary>
    Checkered = 0x4000,
    /// <summary>The driver took an alternate path on the lap, usually indicates a joker lap.</summary>
    OptionalPath = 0x8000 // i.e. took joker lap
}
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
