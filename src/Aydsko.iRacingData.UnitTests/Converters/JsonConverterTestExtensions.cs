// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.UnitTests.Converters;

internal static class JsonConverterTestExtensions
{
    private const string InvalidJsonObjectInputFormatMessage = "Expected input to be in the format \"{\"value\":\"(INPUT TO BE TESTED)\"}\" or \"{\"value\":(INPUT TO BE TESTED)}\" where (INPUT TO BE TESTED) was replaced with the value to be tested.";

    public static IEnumerable<TestCaseData> ToReadValueTestCases<T>(this IEnumerable<(byte[] JsonBytes, T Value, string Name)> examples)
    {
        foreach (var (jsonValueBytes, timeValue, name) in examples ?? Enumerable.Empty<(byte[] JsonBytes, T Value, string Name)>())
        {
            yield return new TestCaseData(new object[] { jsonValueBytes }).Returns(timeValue).SetName("Read Value: " + name);
        }
    }

    public static IEnumerable<TestCaseData> ToWriteValueTestCases<T>(this IEnumerable<(byte[] JsonBytes, T Value, string Name)> examples)
    {
        foreach (var (jsonValueBytes, timeValue, name) in examples ?? Enumerable.Empty<(byte[] JsonBytes, T Value, string Name)>())
        {
            yield return new TestCaseData(timeValue).Returns(System.Text.Encoding.UTF8.GetString(jsonValueBytes)).SetName("Write Value: " + name);
        }
    }

    public static Utf8JsonReader ToUtf8JsonReaderForTest(this byte[] input)
    {
        var reader = new Utf8JsonReader(input.AsSpan());

        if (!(reader.Read() && reader.TokenType == JsonTokenType.StartObject))
        {
            throw new ArgumentException(InvalidJsonObjectInputFormatMessage);
        }

        if (!(reader.Read() && reader.TokenType == JsonTokenType.PropertyName))
        {
            throw new ArgumentException(InvalidJsonObjectInputFormatMessage);
        }

        if (!(reader.Read() && (reader.TokenType == JsonTokenType.StartArray || reader.TokenType == JsonTokenType.String || reader.TokenType == JsonTokenType.Number || reader.TokenType == JsonTokenType.Null)))
        {
            throw new ArgumentException(InvalidJsonObjectInputFormatMessage);
        }

        return reader;
    }

    public static byte[] WriteUsingConverter<T>(this T input, JsonConverter<T> converter)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(converter);
#else
        if (converter is null)
        {
            throw new ArgumentNullException(nameof(converter));
        }
#endif

        using var outputStream = new MemoryStream();
        using (var writer = new Utf8JsonWriter(outputStream))
        {

            writer.WriteStartObject();
            writer.WritePropertyName("value");
            converter.Write(writer, input, new JsonSerializerOptions());
            writer.WriteEndObject();
        }

        return outputStream.ToArray();
    }
}
