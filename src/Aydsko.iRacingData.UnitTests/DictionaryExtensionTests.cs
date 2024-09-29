namespace Aydsko.iRacingData.UnitTests;

internal sealed class DictionaryExtensionTests
{
    [Test, TestCaseSource(nameof(GetTestCases))]
    public KeyValuePair<string, object?> CheckAddParameterIfNotNull<T>(ExampleData<T> example)
    {
        var parameters = new Dictionary<string, object?>();
        parameters.AddParameterIfNotNull(() => example.Value);
        return parameters.ElementAt(0);
    }

#pragma warning disable CA1861 // Avoid constant arrays as arguments - not worth it for these unit test cases.
    public static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new TestCaseData(new ExampleData<string>("foo")).Returns(new KeyValuePair<string, object?>("Value", "foo"));
        yield return new TestCaseData(new ExampleData<DateTime>(new DateTime(2023, 4, 22, 11, 12, 13))).Returns(new KeyValuePair<string, object?>("Value", new DateTime(2023, 4, 22, 11, 12, 13)));
        yield return new TestCaseData(new ExampleData<int[]>([1, 2, 3])).Returns(new KeyValuePair<string, object?>("Value", new[] { 1, 2, 3 }));
        yield return new TestCaseData(new ExampleData<IEnumerable<string>>(new string[] { "a", "b", "c" }.AsEnumerable())).Returns(new KeyValuePair<string, object?>("Value", new string[] { "a", "b", "c" }.AsEnumerable()));
        yield return new TestCaseData(new ExampleData<bool>(true)).Returns(new KeyValuePair<string, object?>("Value", true));
        yield return new TestCaseData(new ExampleData<Common.EventType>(Common.EventType.Practice)).Returns(new KeyValuePair<string, object?>("Value", Common.EventType.Practice));
    }
#pragma warning restore CA1861 // Avoid constant arrays as arguments
}

internal sealed class ExampleData<T>
{
    public T? Value { get; }

    public ExampleData(T value)
    {
        Value = value;
    }
}
