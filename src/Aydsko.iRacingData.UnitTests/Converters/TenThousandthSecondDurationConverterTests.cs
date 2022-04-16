// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;
using System.Text;
using System.Text.Json;

namespace Aydsko.iRacingData.UnitTests.Converters;
public class TenThousandthSecondDurationConverterTests
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
    public byte[] WriteValue(TimeSpan? input)
    {
        var result = input.WriteUsingConverter(_sut);
        Console.WriteLine(Encoding.UTF8.GetString(result));
        return result;
    }

    public static IEnumerable<TestCaseData> ReadValueTestCases() => Examples().ToReadValueTestCases();
    public static IEnumerable<TestCaseData> WriteValueTestCases() => Examples().ToWriteValueTestCases();

    private static IEnumerable<(byte[] JsonBytes, TimeSpan? Value, string Name)> Examples()
    {
        yield return (Encoding.UTF8.GetBytes(@"{""value"":834560}"), new TimeSpan(0, 0, 1, 23, 456), "834560 / 1:23.456");
        yield return (Encoding.UTF8.GetBytes(@"{""value"":0}"), TimeSpan.Zero, "0 / 0:00.000");
        yield return (Encoding.UTF8.GetBytes(@"{""value"":null}"), null, "null / null");
        yield return (Encoding.UTF8.GetBytes(@"{""value"":600000}"), new TimeSpan(0, 0, 1, 0, 0), "600000 / 1:00.000");
    }
}
