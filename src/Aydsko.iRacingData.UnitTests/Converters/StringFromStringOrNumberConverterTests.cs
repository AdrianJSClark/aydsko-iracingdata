// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text;
using System.Text.Json;
using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.UnitTests.Converters;

internal sealed class StringFromStringOrNumberConverterTests
{
    private StringFromStringOrNumberConverter _sut = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // There is no state in the converter so it only needs to be created once for each set of test runs.
        _sut = new StringFromStringOrNumberConverter();
    }

    [Test, TestCaseSource(nameof(ReadValueTestCases))]
    public string? ReadValue(byte[] input)
    {
        var reader = input.ToUtf8JsonReaderForTest();
        return _sut.Read(ref reader, typeof(string), new JsonSerializerOptions());
    }

    [Test, TestCaseSource(nameof(WriteValueTestCases))]
    public string WriteValue(string input)
    {
        var result = input.WriteUsingConverter(_sut);
        return Encoding.UTF8.GetString(result);
    }

    private static IEnumerable<TestCaseData> ReadValueTestCases()
    {
        return Examples().ToReadValueTestCases();
    }

    private static IEnumerable<TestCaseData> WriteValueTestCases()
    {
        return Examples().ToWriteValueTestCases();
    }

    private static IEnumerable<(byte[] JsonBytes, string? Value, string Name)> Examples()
    {
        yield return (Encoding.UTF8.GetBytes(@"{""value"":0}"), "0", "JSON Number value of zero");
        yield return (Encoding.UTF8.GetBytes(@"{""value"":""1""}"), "1", "JSON Number value of 1");
        yield return (Encoding.UTF8.GetBytes(@"{""value"":""ABC123""}"), "ABC123", "JSON String value of 'ABC123'");
        yield return (Encoding.UTF8.GetBytes(@"{""value"":null}"), null, "JSON Null value");
    }
}
