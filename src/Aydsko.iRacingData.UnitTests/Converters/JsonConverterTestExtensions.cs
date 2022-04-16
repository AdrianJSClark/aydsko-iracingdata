// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.UnitTests.Converters;

public static class JsonConverterTestExtensions
{
    private const string InvalidJsonObjectInputFormatMessage = "Expected input to be in the format \"{\"value\":\"(INPUT TO BE TESTED)\"}\" or \"{\"value\":(INPUT TO BE TESTED)}\" where (INPUT TO BE TESTED) was replaced with the value to be tested.";

#pragma warning disable CA1062 // Validate arguments of public methods - check done with "!!" operator

    public static IEnumerable<TestCaseData> ToReadValueTestCases<T>(this IEnumerable<(byte[] JsonBytes, T Value, string Name)> examples!!)
    {
        foreach (var (jsonValueBytes, timeValue, name) in examples)
        {
            yield return new TestCaseData(new object[] { jsonValueBytes }).Returns(timeValue).SetName("Read Value: " + name);
        }
    }

    public static IEnumerable<TestCaseData> ToWriteValueTestCases<T>(this IEnumerable<(byte[] JsonBytes, T Value, string Name)> examples!!)
    {
        foreach (var (jsonValueBytes, timeValue, name) in examples)
        {
            yield return new TestCaseData(timeValue).Returns(jsonValueBytes).SetName("Write Value: " + name);
        }
    }

    public static Utf8JsonReader ToUtf8JsonReaderForTest(this byte[] input!!)
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

        if (!(reader.Read() && (reader.TokenType == JsonTokenType.String || reader.TokenType == JsonTokenType.Number || reader.TokenType == JsonTokenType.Null)))
        {
            throw new ArgumentException(InvalidJsonObjectInputFormatMessage);
        }

        return reader;
    }

    public static byte[] WriteUsingConverter<T>(this T input, JsonConverter<T> converter!!)
    {
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

#pragma warning restore CA1062 // Validate arguments of public methods
}
