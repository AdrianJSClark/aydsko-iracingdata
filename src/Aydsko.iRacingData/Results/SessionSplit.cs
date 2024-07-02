// © 2024 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

public class SessionSplit : IEquatable<SessionSplit>
{
    [JsonPropertyName("subsession_id")]
    public int SubSessionId { get; set; }

    [JsonPropertyName("event_strength_of_field")]
    public int EventStrengthOfField { get; set; }

    public bool Equals(SessionSplit? other)
    {
        if (other is null)
        {
            return false;
        }

        return SubSessionId == other.SubSessionId;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as SessionSplit);
    }

    public override int GetHashCode()
    {
#if NET6_0_OR_GREATER
        return HashCode.Combine(SubSessionId);
#else
        return SubSessionId;
#endif
    }
}
