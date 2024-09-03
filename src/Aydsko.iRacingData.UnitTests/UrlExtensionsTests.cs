namespace Aydsko.iRacingData.UnitTests;

internal sealed class UrlExtensionsTests
{
    [Test, TestCaseSource(nameof(ValidateToUrlWithQueryMethodTestCases))]
    public Uri ValidateToUrlWithQueryMethod(string url, IEnumerable<KeyValuePair<string, object?>> parameters)
    {
        return url.ToUrlWithQuery(parameters);
    }

    private static IEnumerable<TestCaseData> ValidateToUrlWithQueryMethodTestCases()
    {
        yield return new TestCaseData("https://example.com", Array.Empty<KeyValuePair<string, object?>>()) { ExpectedResult = new Uri("https://example.com") };
        yield return new TestCaseData("https://example.com", new KeyValuePair<string, object?>[] { new("foo", "bar") }) { ExpectedResult = new Uri("https://example.com?foo=bar") };
#pragma warning disable CA1861 // Avoid constant arrays as arguments
        yield return new TestCaseData("https://example.com", new KeyValuePair<string, object?>[] { new("foo", new[] { "bar", "baz" }) }) { ExpectedResult = new Uri("https://example.com?foo=bar,baz") };
#pragma warning restore CA1861 // Avoid constant arrays as arguments
        yield return new TestCaseData("https://example.com", new KeyValuePair<string, object?>[] { new("=&?", "=?&=?&") }) { ExpectedResult = new Uri("https://example.com?%3D%26%3F=%3D%3F%26%3D%3F%26") };
        yield return new TestCaseData("https://example.com", new KeyValuePair<string, object?>[] { new("foo", "bar"), new("baz", "bat") }) { ExpectedResult = new Uri("https://example.com?foo=bar&baz=bat") };
        yield return new TestCaseData("https://example.com?a=b", new KeyValuePair<string, object?>[] { new("foo", "bar"), new("baz", "bat") }) { ExpectedResult = new Uri("https://example.com?a=b&foo=bar&baz=bat") };
    }
}
