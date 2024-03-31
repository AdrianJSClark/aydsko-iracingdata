// © 2024 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json;
using Aydsko.iRacingData.Converters;
using static System.Text.Encoding;

namespace Aydsko.iRacingData.UnitTests.Converters;

public class TrackConfigNameNaConverterTests
{
    private TrackConfigNameNaConverter _sut = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // There is no state in the converter so it only needs to be created once for each set of test runs.
        _sut = new TrackConfigNameNaConverter();
    }

    [Test, TestCaseSource(nameof(ReadValueTestCases))]
    public string? ReadValue(byte[] input)
    {
        var reader = input.ToUtf8JsonReaderForTest();
        return _sut.Read(ref reader, typeof(string), new JsonSerializerOptions());
    }

    [Test, TestCaseSource(nameof(WriteValueTestCases))]
    public string WriteValue(string? input)
    {
        var result = input.WriteUsingConverter(_sut);
        return UTF8.GetString(result);
    }

    public static IEnumerable<TestCaseData> ReadValueTestCases() => ReadExamples().ToReadValueTestCases();
    public static IEnumerable<TestCaseData> WriteValueTestCases() => WriteExamples().ToWriteValueTestCases();

    private static IEnumerable<(byte[] JsonBytes, string? Value, string Name)> ReadExamples()
    {
        yield return (@"{""value"":""International""}"u8.ToArray(), "International", "Value \"International\" returns that");
        yield return (@"{""value"":""Grand Prix""}"u8.ToArray(), "Grand Prix", "Value \"Grand Prix\" returns that");
        yield return (@"{""value"":""N/A""}"u8.ToArray(), null, "Value \"N/A\" returns null");
        yield return (@"{""value"":""N/a""}"u8.ToArray(), null, "Value \"N/a\" returns null");
        yield return (@"{""value"":""n/A""}"u8.ToArray(), null, "Value \"n/A\" returns null");
        yield return (@"{""value"":""n/a""}"u8.ToArray(), null, "Value \"n/a\" returns null");
        yield return (@"{""value"":""""}"u8.ToArray(), null, "Value \"\" returns null");
    }

    private static IEnumerable<(byte[] JsonBytes, string? Value, string Name)> WriteExamples()
    {
        yield return (@"{""value"":""International""}"u8.ToArray(), "International", "Value \"International\" returns that");
        yield return (@"{""value"":""Grand Prix""}"u8.ToArray(), "Grand Prix", "Value \"Grand Prix\" returns that");
        yield return (@"{""value"":""N/A""}"u8.ToArray(), null, "Value \"N/A\" returns null");
    }
}
