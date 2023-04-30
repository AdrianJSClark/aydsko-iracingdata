namespace Aydsko.iRacingData.UnitTests;

public class DictionaryExtensionTests
{
    [Test, TestCaseSource(nameof(GetTestCases))]
    public KeyValuePair<string, string> CheckAddParameterIfNotNull<T>(T value)
    {
        var parameters = new Dictionary<string, string>();
        parameters.AddParameterIfNotNull("value", value);
        return parameters.ElementAt(0);
    }

    public static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new TestCaseData("foo").Returns(new KeyValuePair<string, string>("value", "foo"));
        yield return new TestCaseData(new DateTime(2023, 4, 22, 11, 12, 13)).Returns(new KeyValuePair<string, string>("value", "2023-04-22T11:12Z"));
        yield return new TestCaseData(new[] { 1, 2, 3 }).Returns(new KeyValuePair<string, string>("value", "1,2,3"));
        yield return new TestCaseData(new string[] { "a", "b", "c" }.AsEnumerable()).Returns(new KeyValuePair<string, string>("value", "a,b,c"));
        yield return new TestCaseData(true).Returns(new KeyValuePair<string, string>("value", "true"));
        yield return new TestCaseData(Common.EventType.Practice).Returns(new KeyValuePair<string, string>("value", "2"));
    }
}
