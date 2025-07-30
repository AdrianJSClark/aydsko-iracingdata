// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class League
{
    [JsonPropertyName("league_id")]
    public int LeagueId { get; set; }

    [JsonPropertyName("owner_id")]
    public int OwnerId { get; set; }

    [JsonPropertyName("league_name")]
    public string LeagueName { get; set; } = null!;

    [JsonPropertyName("created")]
    public DateTimeOffset Created { get; set; }

    [JsonPropertyName("hidden")]
    public bool Hidden { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = null!;

    [JsonPropertyName("about")]
    public string About { get; set; } = null!;

    [JsonPropertyName("url")]
    public string Url { get; set; } = null!;

    [JsonPropertyName("recruiting")]
    public bool Recruiting { get; set; }

    [JsonPropertyName("rules")]
    public string Rules { get; set; } = default!;

    [JsonPropertyName("private_wall")]
    public bool PrivateWall { get; set; }

    [JsonPropertyName("private_roster")]
    public bool PrivateRoster { get; set; }

    [JsonPropertyName("private_schedule")]
    public bool PrivateSchedule { get; set; }

    [JsonPropertyName("private_results")]
    public bool PrivateResults { get; set; }

    [JsonPropertyName("roster_count")]
    public int RosterCount { get; set; }

    [JsonPropertyName("owner")]
    public Owner Owner { get; set; } = null!;

    [JsonPropertyName("image")]
    public Image Image { get; set; } = null!;

    [JsonPropertyName("tags")]
    public Tags Tags { get; set; } = null!;

    [JsonPropertyName("roster")]
    public RosterMember[] Roster { get; set; } = null!;

    [JsonPropertyName("is_owner")]
    public bool IsOwner { get; set; }

    [JsonPropertyName("is_admin")]
    public bool IsAdmin { get; set; }

    // TODO: Work out what shape these properties are.
    //[JsonPropertyName("league_applications")]
    //public object[]? LeagueApplications { get; set; }
    //
    //[JsonPropertyName("pending_requests")]
    //public object[]? PendingRequests { get; set; }

    [JsonPropertyName("is_member")]
    public bool IsMember { get; set; }

    [JsonPropertyName("is_applicant")]
    public bool IsApplicant { get; set; }

    [JsonPropertyName("is_invite")]
    public bool IsInvite { get; set; }

    [JsonPropertyName("is_ignored")]
    public bool IsIgnored { get; set; }
}

[JsonSerializable(typeof(League)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class LeagueContext : JsonSerializerContext
{ }
