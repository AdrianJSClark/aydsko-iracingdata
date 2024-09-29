// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json;
using Aydsko.iRacingData.Converters;
using static System.Text.Encoding;

namespace Aydsko.iRacingData.UnitTests.Converters;

internal sealed class StatusTimeStampConverterTests
{
    private StatusTimeStampConverter _sut = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // There is no state in the converter so it only needs to be created once for each set of test runs.
        _sut = new StatusTimeStampConverter();
    }

    [Test, TestCaseSource(nameof(ReadValueTestCases))]
    public DateTimeOffset? ReadValue(byte[] input)
    {
        var reader = input.ToUtf8JsonReaderForTest();
        return _sut.Read(ref reader, typeof(DateTimeOffset?), new JsonSerializerOptions());
    }

    [Test, TestCaseSource(nameof(WriteValueTestCases))]
    public string WriteValue(DateTimeOffset input)
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

    private static IEnumerable<(byte[] JsonBytes, DateTimeOffset? Value, string Name)> Examples()
    {
        yield return (UTF8.GetBytes(@"{""value"":1674818352.565}"), new DateTimeOffset(2023, 1, 27, 11, 19, 12, 565, TimeSpan.Zero), "11:19:12 27-Jan-2023");
        yield return (UTF8.GetBytes(@"{""value"":1674822597.713}"), new DateTimeOffset(2023, 1, 27, 12, 29, 57, 713, TimeSpan.Zero), "22:29:57 27 Jan 2023");
    }
}
