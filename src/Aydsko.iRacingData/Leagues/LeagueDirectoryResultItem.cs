namespace Aydsko.iRacingData.Leagues;

public class LeagueDirectoryResultItem
{
    [JsonPropertyName("league_id")]
    public int LeagueId { get; set; }

    [JsonPropertyName("owner_id")]
    public int OwnerId { get; set; }

    [JsonPropertyName("league_name")]
    public string LeagueName { get; set; } = default!;

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("about")]
    public string About { get; set; } = default!;

    [JsonPropertyName("url")]
    public string Url { get; set; } = default!;

    [JsonPropertyName("roster_count")]
    public int RosterCount { get; set; }

    [JsonPropertyName("recruiting")]
    public bool Recruiting { get; set; }

    [JsonPropertyName("is_admin")]
    public bool IsAdmin { get; set; }

    [JsonPropertyName("is_member")]
    public bool IsMember { get; set; }

    [JsonPropertyName("pending_application")]
    public bool PendingApplication { get; set; }

    [JsonPropertyName("pending_invitation")]
    public bool PendingInvitation { get; set; }

    [JsonPropertyName("owner")]
    public Owner Owner { get; set; } = default!;
}
