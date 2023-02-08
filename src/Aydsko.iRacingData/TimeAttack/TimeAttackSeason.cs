// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.TimeAttack;

public class TimeAttackSeason
{
    [JsonPropertyName("comp_id")]
    public int CompetitionId { get; set; }

    [JsonPropertyName("comp_name")]
    public string CompetitionName { get; set; } = default!;

    [JsonPropertyName("comp_short_name")]
    public string CompetitionShortName { get; set; } = default!;

    [JsonPropertyName("comp_category")]
    public int CompetitionCategory { get; set; }

    [JsonPropertyName("sponsored")]
    public bool Sponsored { get; set; }

    [JsonPropertyName("sponsor_name")]
    public string SponsorName { get; set; } = default!;

    [JsonPropertyName("sponsor_logo_url")]
    public string SponsorLogoUrl { get; set; } = default!;

    [JsonPropertyName("sponsor_banner_url")]
    public string SponsorBannerUrl { get; set; } = default!;

    [JsonPropertyName("sponsor_cover_url")]
    public string SponsorCoverUrl { get; set; } = default!;

    [JsonPropertyName("sponsor_website_url")]
    public string SponsorWebsiteUrl { get; set; } = default!;

    [JsonPropertyName("terms_doc_url")]
    public string TermsDocumentUrl { get; set; } = default!;

    [JsonPropertyName("acceptance_msg")]
    public string AcceptanceMessage { get; set; } = default!;

    [JsonPropertyName("comp_season_id")]
    public int CompetitionSeasonId { get; set; }

    [JsonPropertyName("car_class_id")]
    public int CarClassId { get; set; }

#if NET6_0_OR_GREATER
    [JsonPropertyName("start_date"), JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly StartDate { get; set; }

    [JsonPropertyName("end_date"), JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly EndDate { get; set; }
#else
    [JsonPropertyName("start_date"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("end_date"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime EndDate { get; set; }
#endif

    [JsonPropertyName("start_date_time")]
    public DateTimeOffset StartDateTime { get; set; }

    [JsonPropertyName("end_date_time")]
    public DateTimeOffset EndDateTime { get; set; }

    [JsonPropertyName("season_year")]
    public string SeasonYear { get; set; } = default!;

    [JsonPropertyName("season_quarter")]
    public string SeasonQuarter { get; set; } = default!;

    [JsonPropertyName("fixed_setup")]
    public bool FixedSetup { get; set; }

    [JsonPropertyName("unsport_conduct_rule_mode")]
    public int UnsportConductRuleMode { get; set; }

    [JsonPropertyName("tracks")]
    public TimeAttackTrack[] Tracks { get; set; } = default!;

    [JsonPropertyName("allowed_clubs")]
    public int[] AllowedClubs { get; set; } = default!;

    [JsonPropertyName("allowed_members")]
    public object AllowedMembers { get; set; } = default!;
}

[JsonSerializable(typeof(TimeAttackSeason[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class TimeAttackSeasonArrayContext : JsonSerializerContext
{ }
