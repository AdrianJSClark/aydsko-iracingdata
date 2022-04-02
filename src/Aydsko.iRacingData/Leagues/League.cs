﻿// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

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
    public DateTime Created { get; set; }
    [JsonPropertyName("hidden")]
    public bool Hidden { get; set; }
    [JsonPropertyName("message")]
    public string Message { get; set; } = null!;
    [JsonPropertyName("about")]
    public string About { get; set; } = null!;
    [JsonPropertyName("url"), SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "Strings are easier to deal with.")]
    public string Url { get; set; } = null!;
    [JsonPropertyName("recruiting")]
    public bool Recruiting { get; set; }
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
}

[JsonSerializable(typeof(League)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class LeagueContext : JsonSerializerContext
{ }