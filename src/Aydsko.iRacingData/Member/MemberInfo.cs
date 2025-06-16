// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

/// <summary>Information about the iRacing member.</summary>
public class MemberInfo
{
    /// <summary>Unique identifier for the member.</summary>
    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    /// <summary>Member's email address.</summary>
    /// <remarks>This value is masked for security reasons.</remarks>
    [JsonPropertyName("email")]
    public string Email { get; set; } = default!;

    /// <summary>Unique user name the member uses to log in.</summary>
    /// <remarks>This value is masked for security reasons.</remarks>
    [JsonPropertyName("username")]
    public string Username { get; set; } = default!;

    /// <summary>The display value used to represent the member on the service.</summary>
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

    [JsonPropertyName("connection_type")]
    public string ConnectionType { get; set; } = default!;

    [JsonPropertyName("download_server")]
    public string DownloadServer { get; set; } = default!;

    [JsonPropertyName("last_login")]
    public DateTimeOffset LastLogin { get; set; }

    [JsonPropertyName("read_comp_rules")]
    public DateTimeOffset ReadCompRules { get; set; }

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

    [JsonPropertyName("hundred_pct_club")]
    public bool HundredPercentClub { get; set; }

    [JsonPropertyName("twenty_pct_discount")]
    public bool TwentyPercentDiscount { get; set; }

    [JsonPropertyName("race_official")]
    public bool RaceOfficial { get; set; }

    [JsonPropertyName("ai")]
    public bool AI { get; set; }

    [JsonPropertyName("read_tc")]
    public DateTimeOffset ReadTc { get; set; }

    [JsonPropertyName("read_pp")]
    public DateTimeOffset ReadPp { get; set; }

    [JsonPropertyName("has_read_tc")]
    public bool HasReadTC { get; set; }

    [JsonPropertyName("has_read_pp")]
    public bool HasReadPp { get; set; }
}

[JsonSerializable(typeof(MemberInfo)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class MemberInfoContext : JsonSerializerContext
{ }
