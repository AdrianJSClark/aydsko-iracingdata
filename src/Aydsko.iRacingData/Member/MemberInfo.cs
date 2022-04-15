// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

/// <summary>Information about the iRacing member.</summary>
public class MemberInfo
{
    /// <summary>Unique identifier for the iRacing member's account.</summary>
    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    /// <summary>Member's email address.</summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = default!;

    [JsonPropertyName("username")]
    public string Username { get; set; } = default!;

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = default!;

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = default!;

    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = default!;

    [JsonPropertyName("on_car_name")]
    public string OnCarName { get; set; } = default!;

    [JsonPropertyName("member_since")]
    public string MemberSince { get; set; } = default!;

    [JsonPropertyName("last_test_track")]
    public int LastTestTrack { get; set; }

    [JsonPropertyName("last_test_car")]
    public int LastTestCar { get; set; }

    [JsonPropertyName("last_season")]
    public int LastSeason { get; set; }

    [JsonPropertyName("flags")]
    public int Flags { get; set; }

    [JsonPropertyName("club_id")]
    public int ClubId { get; set; }

    [JsonPropertyName("club_name")]
    public string ClubName { get; set; } = default!;

    [JsonPropertyName("connection_type")]
    public string ConnectionType { get; set; } = default!;

    [JsonPropertyName("download_server")]
    public string DownloadServer { get; set; } = default!;

    [JsonPropertyName("last_login")]
    public DateTime LastLogin { get; set; }

    [JsonPropertyName("read_comp_rules")]
    public DateTime ReadCompRules { get; set; }

    [JsonPropertyName("account")]
    public Account Account { get; set; } = default!;

    [JsonPropertyName("helmet")]
    public Helmet Helmet { get; set; } = default!;

    [JsonPropertyName("suit")]
    public Suit Suit { get; set; } = default!;

    [JsonPropertyName("licenses")]
    public Licenses Licenses { get; set; } = default!;

    [JsonPropertyName("car_packages")]
    public ContentPackage[] CarPackages { get; set; } = default!;

    [JsonPropertyName("track_packages")]
    public ContentPackage[] TrackPackages { get; set; } = default!;

    [JsonPropertyName("other_owned_packages")]
    public int[] OtherOwnedPackages { get; set; } = default!;

    [JsonPropertyName("dev")]
    public bool Dev { get; set; }

    [JsonPropertyName("alpha_tester")]
    public bool AlphaTester { get; set; }

    [JsonPropertyName("broadcaster")]
    public bool Broadcaster { get; set; }

    [JsonPropertyName("has_read_comp_rules")]
    public bool HasReadCompRules { get; set; }

    [JsonPropertyName("flags_hex")]
    public string FlagsHex { get; set; } = default!;

    /// <summary>Indicates if the user qualifies for the 100% Club.</summary>
    [JsonPropertyName("hundred_pct_club")]
    public bool HundredPercentClub { get; set; }

    /// <summary>Indicates if the user qualifies for the 20% discount.</summary>
    [JsonPropertyName("twenty_pct_discount")]
    public bool TwentyPercentDiscount { get; set; }

    [JsonPropertyName("race_official")]
    public bool RaceOfficial { get; set; }

    /// <summary>Indicates if the member represented by this record an instance of the AI.</summary>
    [JsonPropertyName("ai")]
    public bool AI { get; set; }

    /// <summary>Date and time stamp indicating when the user said they read the Terms &amp; Conditions.</summary>
    [JsonPropertyName("read_tc")]
    public DateTime ReadTc { get; set; }

    /// <summary>Date and time stamp indicating when the user said they read the Privacy Policy.</summary>
    [JsonPropertyName("read_pp")]
    public DateTime ReadPp { get; set; }

    /// <summary>Indicates if the member has read the Terms &amp; Conditions.</summary>
    [JsonPropertyName("has_read_tc")]
    public bool HasReadTC { get; set; }

    /// <summary>Indicates if the member has read the Privacy Policy.</summary>
    [JsonPropertyName("has_read_pp")]
    public bool HasReadPp { get; set; }
}

[JsonSerializable(typeof(MemberInfo)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class MemberInfoContext : JsonSerializerContext
{ }
