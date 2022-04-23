// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

public class TrackState
{
    [JsonPropertyName("leave_marbles")]
    public bool LeaveMarbles { get; set; }
    [JsonPropertyName("practice_rubber")]
    public int PracticeRubber { get; set; }
    [JsonPropertyName("qualify_rubber")]
    public int QualifyRubber { get; set; }
    [JsonPropertyName("warmup_rubber")]
    public int WarmupRubber { get; set; }
    [JsonPropertyName("race_rubber")]
    public int RaceRubber { get; set; }
    [JsonPropertyName("practice_grip_compound")]
    public int PracticeGripCompound { get; set; }
    [JsonPropertyName("qualify_grip_compound")]
    public int QualifyGripCompound { get; set; }
    [JsonPropertyName("warmup_grip_compound")]
    public int WarmupGripCompound { get; set; }
    [JsonPropertyName("race_grip_compound")]
    public int RaceGripCompound { get; set; }
}
