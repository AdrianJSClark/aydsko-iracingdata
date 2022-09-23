// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Member;

public class FollowCounts
{
    [JsonPropertyName("followers")]
    public int Followers { get; set; }

    [JsonPropertyName("follows")]
    public int Follows { get; set; }
}
