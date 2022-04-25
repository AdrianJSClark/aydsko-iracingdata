// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

public class MemberSummaryYearStatistics
{
    [JsonPropertyName("num_official_sessions")]
    public int NumberOfOfficialSessions { get; set; }

    [JsonPropertyName("num_league_sessions")]
    public int NumberOfLeagueSessions { get; set; }

    [JsonPropertyName("num_official_wins")]
    public int NumberOfOfficialWins { get; set; }

    [JsonPropertyName("num_league_wins")]
    public int NumberOfLeagueWins { get; set; }
}
