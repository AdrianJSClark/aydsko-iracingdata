namespace Aydsko.iRacingData.UnitTests;

internal class UrlExtensionsTests
{
    [Test, TestCaseSource(nameof(ValidateToUrlWithQueryMethodTestCases))]
    public Uri ValidateToUrlWithQueryMethod(string url, IEnumerable<KeyValuePair<string, string>> parameters)
    {
        return url.ToUrlWithQuery(parameters);
    }

    private static IEnumerable<TestCaseData> ValidateToUrlWithQueryMethodTestCases()
    {
        yield return new TestCaseData("https://example.com", Array.Empty<KeyValuePair<string, string>>()) { ExpectedResult = new Uri("https://example.com") };
        yield return new TestCaseData("https://example.com", new KeyValuePair<string, string>[] { new("foo", "bar") }) { ExpectedResult = new Uri("https://example.com?foo=bar") };
        yield return new TestCaseData("https://example.com", new KeyValuePair<string, string>[] { new("=&?", "=?&=?&") }) { ExpectedResult = new Uri("https://example.com?%3D%26%3F=%3D%3F%26%3D%3F%26") };
        yield return new TestCaseData("https://example.com", new KeyValuePair<string, string>[] { new("foo", "bar"), new("baz", "bat") }) { ExpectedResult = new Uri("https://example.com?foo=bar&baz=bat") };
        yield return new TestCaseData("https://example.com?a=b", new KeyValuePair<string, string>[] { new("foo", "bar"), new("baz", "bat") }) { ExpectedResult = new Uri("https://example.com?a=b&foo=bar&baz=bat") };
    }
}
