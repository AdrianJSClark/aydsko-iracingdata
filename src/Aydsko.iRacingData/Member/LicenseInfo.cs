// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

public class LicenseInfo : License
{
    [JsonPropertyName("cpi")]
    public decimal CornersPerIncident { get; set; }

    [JsonPropertyName("irating")]
    public int IRating { get; set; }

    [JsonPropertyName("tt_rating")]
    public int TTRating { get; set; }

    [JsonPropertyName("mpr_num_races")]
    public int MprNumberOfRaces { get; set; }

    [JsonPropertyName("mpr_num_tts")]
    public int MprNumberOfTimeTrials { get; set; }
}
