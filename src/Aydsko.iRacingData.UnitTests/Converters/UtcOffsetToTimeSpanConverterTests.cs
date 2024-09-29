using System.Text.Json;
using Aydsko.iRacingData.Converters;
using static System.Text.Encoding;

namespace Aydsko.iRacingData.UnitTests.Converters;

internal sealed class UtcOffsetToTimeSpanConverterTests
{
    private UtcOffsetToTimeSpanConverter _sut = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // There is no state in the converter so it only needs to be created once for each set of test runs.
        _sut = new UtcOffsetToTimeSpanConverter();
    }

    [Test, TestCaseSource(nameof(ReadValueTestCases))]
    public TimeSpan ReadValue(byte[] input)
    {
        var reader = input.ToUtf8JsonReaderForTest();
        return _sut.Read(ref reader, typeof(TimeSpan), new JsonSerializerOptions());
    }

    [Test, TestCaseSource(nameof(WriteValueTestCases))]
    public string WriteValue(TimeSpan input)
    {
        var result = input.WriteUsingConverter(_sut);
        return UTF8.GetString(result);
    }

    private static IEnumerable<TestCaseData> ReadValueTestCases()
    {
        return Examples().ToReadValueTestCases();
    }

    private static IEnumerable<TestCaseData> WriteValueTestCases()
    {
        return Examples().ToWriteValueTestCases();
    }

    private static IEnumerable<(byte[] JsonBytes, TimeSpan Value, string Name)> Examples()
    {
        yield return (@"{""value"":0}"u8.ToArray(), TimeSpan.Zero, "Value 0 returns TimeSpan.Zero");
        yield return (@"{""value"":-240}"u8.ToArray(), TimeSpan.FromHours(-4), "Value -14400 returns TimeSpan.FromHours(-4)");
        yield return (@"{""value"":240}"u8.ToArray(), TimeSpan.FromHours(4), "Value 14400 returns TimeSpan.FromHours(4)");
        yield return (@"{""value"":630}"u8.ToArray(), TimeSpan.FromHours(10.5), "Value 14400 returns TimeSpan.FromHours(10.5)");
    }
}
