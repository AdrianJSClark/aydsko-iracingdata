// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

public class TeamInfo
{
    [JsonPropertyName("team_id")]
    public int TeamId { get; set; }

    [JsonPropertyName("owner_id")]
    public int OwnerId { get; set; }

    [JsonPropertyName("team_name")]
    public string TeamName { get; set; } = default!;

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("hidden")]
    public bool Hidden { get; set; }

    [JsonPropertyName("about")]
    public string About { get; set; } = default!;

    [JsonPropertyName("url")]
    public string Url { get; set; } = default!;

    [JsonPropertyName("roster_count")]
    public int RosterCount { get; set; }

    [JsonPropertyName("recruiting")]
    public bool Recruiting { get; set; }

    [JsonPropertyName("private_wall")]
    public bool PrivateWall { get; set; }

    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; }

    [JsonPropertyName("is_owner")]
    public bool IsOwner { get; set; }

    [JsonPropertyName("is_admin")]
    public bool IsAdmin { get; set; }

    [JsonPropertyName("suit")]
    public Suit Suit { get; set; } = default!;

    [JsonPropertyName("owner")]
    public TeamMember Owner { get; set; } = default!;

    // TODO - Without an example the structure of these properties remains a mystery.
    //[JsonPropertyName("tags")]
    //public Tags Tags { get; set; }

    // TODO - Without an example the structure of these properties remains a mystery.
    //[JsonPropertyName("team_applications")]
    //public object[] TeamApplications { get; set; }

    // TODO - Without an example the structure of these properties remains a mystery.
    //[JsonPropertyName("pending_requests")]
    //public object[] PendingRequests { get; set; }

    [JsonPropertyName("is_member")]
    public bool IsMember { get; set; }

    [JsonPropertyName("is_applicant")]
    public bool IsApplicant { get; set; }

    [JsonPropertyName("is_invite")]
    public bool IsInvite { get; set; }

    [JsonPropertyName("is_ignored")]
    public bool IsIgnored { get; set; }

    [JsonPropertyName("roster")]
    public TeamMember[] Roster { get; set; } = default!;
}

[JsonSerializable(typeof(TeamInfo)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class TeamInfoContext : JsonSerializerContext
{ }
