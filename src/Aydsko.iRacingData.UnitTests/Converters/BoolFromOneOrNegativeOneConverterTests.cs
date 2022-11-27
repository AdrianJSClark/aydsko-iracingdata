using System.Text.Json;
using Aydsko.iRacingData.Converters;
using static System.Text.Encoding;

namespace Aydsko.iRacingData.UnitTests.Converters;

public class BoolFromOneOrNegativeOneConverterTests
{
    private BoolFromOneOrNegativeOneConverter _sut = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _sut = new BoolFromOneOrNegativeOneConverter();
    }

    [Test, TestCaseSource(nameof(ReadValueTestCases))]
    public bool? ReadValue(byte[] input)
    {
        var reader = input.ToUtf8JsonReaderForTest();
        return _sut.Read(ref reader, typeof(bool?), new JsonSerializerOptions());
    }

    [Test, TestCaseSource(nameof(WriteValueTestCases))]
    public byte[] WriteValue(bool input)
    {
        var result = input.WriteUsingConverter(_sut);
        Console.WriteLine(UTF8.GetString(result));
        return result;
    }

    public static IEnumerable<TestCaseData> ReadValueTestCases() => Examples().ToReadValueTestCases();
    public static IEnumerable<TestCaseData> WriteValueTestCases() => Examples().ToWriteValueTestCases();

    private static IEnumerable<(byte[] JsonBytes, bool? Value, string Name)> Examples()
    {
        yield return (UTF8.GetBytes(@"{""value"":1}"), true, "\"1\" gives \"true\"");
        yield return (UTF8.GetBytes(@"{""value"":-1}"), false, "\"-1\" gives \"false\"");
    }
}
