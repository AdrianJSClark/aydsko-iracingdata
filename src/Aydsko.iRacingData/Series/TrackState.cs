// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Series;

public class TrackState
{
    [JsonPropertyName("leave_marbles")]
    public bool LeaveMarbles { get; set; }

    [JsonPropertyName("practice_rubber")]
    public int PracticeRubber { get; set; }
}
