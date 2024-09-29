// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json;
using Aydsko.iRacingData.Converters;
using static System.Text.Encoding;

namespace Aydsko.iRacingData.UnitTests.Converters;

internal sealed class ServiceStatusHistoryItemArrayConverterConverterTests
{
    private readonly ServiceStatusHistoryItemArrayConverter _sut;

    public ServiceStatusHistoryItemArrayConverterConverterTests()
    {
        _sut = new();
    }

    [Test, TestCaseSource(nameof(ReadValueTestCases))]
    public ServiceStatusHistoryItem[]? ReadValue(byte[] input)
    {
        var reader = input.ToUtf8JsonReaderForTest();
        return _sut.Read(ref reader, typeof(ServiceStatusHistoryItem[]), new JsonSerializerOptions());
    }

    [Test, TestCaseSource(nameof(WriteValueTestCases))]
    public string WriteValue(ServiceStatusHistoryItem[] input)
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

    private static IEnumerable<(byte[] JsonBytes, ServiceStatusHistoryItem[]? Value, string Name)> Examples()
    {
        yield return (
            UTF8.GetBytes(@"{""value"":[[1674818997.713,0],[1674819012.713,0],[1674819027.713,0],[1674819042.713,0],[1674819057.713,0]]}"),
            new ServiceStatusHistoryItem[]
            {
                new() { Instant = new(2023, 1, 27, 11, 29, 57, 713, TimeSpan.Zero), Value = 0M },
                new() { Instant = new(2023, 1, 27, 11, 30, 12, 713, TimeSpan.Zero), Value = 0M },
                new() { Instant = new(2023, 1, 27, 11, 30, 27, 713, TimeSpan.Zero), Value = 0M },
                new() { Instant = new(2023, 1, 27, 11, 30, 42, 713, TimeSpan.Zero), Value = 0M },
                new() { Instant = new(2023, 1, 27, 11, 30, 57, 713, TimeSpan.Zero), Value = 0M }
            },
            "11:29:57 to 11:30:57 27 Jan 2023"
            );
    }
}
