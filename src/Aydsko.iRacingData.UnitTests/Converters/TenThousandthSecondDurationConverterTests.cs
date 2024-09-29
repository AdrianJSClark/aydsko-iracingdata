// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json;
using Aydsko.iRacingData.Converters;
using static System.Text.Encoding;

namespace Aydsko.iRacingData.UnitTests.Converters;

internal sealed class TenThousandthSecondDurationConverterTests
{
    private TenThousandthSecondDurationConverter _sut = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // There is no state in the converter so it only needs to be created once for each set of test runs.
        _sut = new TenThousandthSecondDurationConverter();
    }

    [Test, TestCaseSource(nameof(ReadValueTestCases))]
    public TimeSpan? ReadValue(byte[] input)
    {
        var reader = input.ToUtf8JsonReaderForTest();
        return _sut.Read(ref reader, typeof(TimeSpan?), new JsonSerializerOptions());
    }

    [Test, TestCaseSource(nameof(WriteValueTestCases))]
    public string WriteValue(TimeSpan? input)
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

    private static IEnumerable<(byte[] JsonBytes, TimeSpan? Value, string Name)> Examples()
    {
        yield return (UTF8.GetBytes(@"{""value"":834560}"), new TimeSpan(0, 0, 1, 23, 456), "834560 / 1:23.456");
        yield return (UTF8.GetBytes(@"{""value"":0}"), TimeSpan.Zero, "0 / 0:00.000");
        yield return (UTF8.GetBytes(@"{""value"":null}"), null, "null / null");
        yield return (UTF8.GetBytes(@"{""value"":600000}"), new TimeSpan(0, 0, 1, 0, 0), "600000 / 1:00.000");
    }
}
