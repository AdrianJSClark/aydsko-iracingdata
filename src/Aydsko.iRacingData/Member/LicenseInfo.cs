// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

public class LicenseInfo : License
{
    [JsonPropertyName("cpi")]
    public decimal CornersPerIncident { get; set; }

    /// <summary>Current iRating for this license.</summary>
    /// <remarks>Will be <see langword="null"/> for &quot;Rookie&quot; licenses.</remarks>
    [JsonPropertyName("irating")]
    public int? IRating { get; set; }

    [JsonPropertyName("tt_rating")]
    public int TimeTrialRating { get; set; }

    [JsonPropertyName("mpr_num_races")]
    public int MinimumParticipationRequirementNumberOfRaces { get; set; }

    [JsonPropertyName("mpr_num_tts")]
    public int MinimumParticipationRequirementNumberOfTimeTrials { get; set; }

    [JsonPropertyName("pro_promotable")]
    public bool IsPromotableToPro { get; set; }

    [JsonPropertyName("seq")]
    public int Sequence { get; set; }

    [JsonIgnore, Obsolete("Use \"TimeTrialRating\" instead.")]
    public int TTRating { get => TimeTrialRating; set => TimeTrialRating = value; }

    [JsonIgnore, Obsolete("Use \"MinimumParticipationRequirementNumberOfRaces\" instead.")]
    public int MprNumberOfRaces { get => MinimumParticipationRequirementNumberOfRaces; set => MinimumParticipationRequirementNumberOfRaces = value; }

    [JsonIgnore, Obsolete("Use \"MinimumParticipationRequirementNumberOfTimeTrials\" instead.")]
    public int MprNumberOfTimeTrials { get => MinimumParticipationRequirementNumberOfTimeTrials; set => MinimumParticipationRequirementNumberOfTimeTrials = value; }
}
