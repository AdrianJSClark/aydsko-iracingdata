namespace Aydsko.iRacingData.IntegrationTests.Member;

internal static class Security
{
    internal static string ObfuscateUsernameOrEmail(string input)
    {
        var segments = input.Split('@', 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var localParts = segments[0].Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var domainParts = segments[1].Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        return $"{string.Join(".", localParts.Select(EncodeSegment))}@{string.Join(".", domainParts.Select(EncodeSegment))}";

        static string EncodeSegment(string input)
        {
            return input switch
            {
                { Length: >= 4 } => input[0] + string.Join("", Enumerable.Repeat('*', input.Length - 2)) + input[^1],
                { Length: > 0 } => input[0] + string.Join("", Enumerable.Repeat('*', input.Length - 1)),
                _ => string.Empty,
            };
        }
    }
}
