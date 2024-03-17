// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Lookups;

/// <summary>Lookup data about a license.</summary>
public class LicenseLookup
{
    /// <summary>Unique identifier for the license category.</summary>
    [JsonPropertyName("license_group")]
    public int LicenseGroup { get; set; }

    /// <summary>Display name for the license.</summary>
    [JsonPropertyName("group_name")]
    public string GroupName { get; set; } = default!;

    /// <summary>Minimum number of races required to be promoted out of this license.</summary>
    [JsonPropertyName("min_num_races")]
    public int? MinimumNumberOfRaces { get; set; }

    /// <summary>Number of credits available for participation in a series which is at this level.</summary>
    [JsonPropertyName("participation_credits")]
    public int ParticipationCredits { get; set; }

    /// <summary>Minimum Safety Rating required to be &quot;fast-track&quot; promoted out of this license.</summary>
    [JsonPropertyName("min_sr_to_fast_track")]
    public int? MinimumSafetyRatingToFastTrack { get; set; }

    /// <summary>The levels within this license group.</summary>
    [JsonPropertyName("levels")]
    public LicenseLevel[] Levels { get; set; } = default!;

    /// <summary>Minimum number of Time Trials required to be promoted out of this license.</summary>
    [JsonPropertyName("min_num_tt")]
    public int? MinimumNumberOfTimeTrials { get; set; }
}

[JsonSerializable(typeof(LicenseLookup[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class LicenseLookupArrayContext : JsonSerializerContext
{ }
