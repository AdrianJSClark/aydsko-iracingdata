// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

/// <summary>Detail about one of the licenses held by the member.</summary>
public class MemberLicense
{
    /// <summary>Display sequence, should match the order shown in iRacing.</summary>
    [JsonPropertyName("seq")]
    public int Sequence { get; set; }

    /// <summary>Unique identifier for the license category.</summary>
    /// <seealso cref="IDataClient.GetCategoriesAsync"/>
    [JsonPropertyName("category_id")]
    public int CategoryId { get; set; }

    /// <summary>License category text identifier.</summary>
    /// <seealso cref="IDataClient.GetCategoriesAsync"/>
    [JsonPropertyName("category")]
    public string Category { get; set; } = default!;

    /// <summary>License group name suitable for display.</summary>
    /// <seealso cref="IDataClient.GetLicenseLookupsAsync"/>
    [JsonPropertyName("category_name")]
    public string CategoryName { get; set; } = default!;

    /// <summary>Level within the license system.</summary>
    /// <seealso cref="Lookups.LicenseLevel"/>
    /// <seealso cref="IDataClient.GetLicenseLookupsAsync"/>"/>
    [JsonPropertyName("license_level")]
    public int LicenseLevel { get; set; }

    /// <summary>Current Safety Rating value.</summary>
    [JsonPropertyName("safety_rating")]
    public float SafetyRating { get; set; }

    /// <summary>Current number of corners per incident point.</summary>
    [JsonPropertyName("cpi")]
    public float CornersPerIncident { get; set; }

    /// <summary>Current iRating value.</summary>
    [JsonPropertyName("irating")]
    public int IRating { get; set; }

    /// <summary>Current Time Trial Rating value.</summary>
    [JsonPropertyName("tt_rating")]
    public int TimeTrialRating { get; set; }

    /// <summary>Number of races completed towards the Minimum Participation Requirement (MPR) for license promotion.</summary>
    [JsonPropertyName("mpr_num_races")]
    public int MinimumParticipationRequirementNumberOfRaces { get; set; }

    /// <summary>Color code for the license category.</summary>
    /// <remarks>This is an HTML color code.</remarks>
    [JsonPropertyName("color")]
    public string Color { get; set; } = default!;

    /// <summary>License group name suitable for display.</summary>
    [JsonPropertyName("group_name")]
    public string GroupName { get; set; } = default!;

    /// <summary>Unique identifier for the license group.</summary>
    [JsonPropertyName("group_id")]
    public int GroupId { get; set; }

    /// <summary>Indicates if this license is suitable for promotion.</summary>
    [JsonPropertyName("pro_promotable")]
    public bool Promotable { get; set; }

    [JsonIgnore, Obsolete("Use \"Promotable\" instead.")]
    public bool ProPromotable { get => Promotable; set => Promotable = value; }

    /// <summary>The number of time trials completed towards the Minimum Participation Requirement (MPR) for license promotion.</summary>
    [JsonPropertyName("mpr_num_tts")]
    public int MinimumParticipationRequirementNumberOfTimeTrials { get; set; }
}
