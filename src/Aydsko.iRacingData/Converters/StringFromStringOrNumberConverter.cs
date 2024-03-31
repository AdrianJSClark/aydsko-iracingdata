// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Globalization;
using System.Text.Json;

namespace Aydsko.iRacingData.Converters;

/// <summary>The iRacing login response contains a property that is sometimes a literal zero (0) character and sometimes a string. This converter handles this to/from a <c>string</c> value.</summary>
public sealed class StringFromStringOrNumberConverter : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.String => reader.GetString(),
            JsonTokenType.Number => reader.GetInt32().ToString(CultureInfo.InvariantCulture),
            _ => null
        };
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(writer);
#else
        if (writer is null)
        {
            throw new ArgumentNullException(nameof(writer));
        }
#endif

        switch (value)
        {
            case null:
                writer.WriteNullValue();
                break;

            case "0":
                writer.WriteNumberValue(0);
                break;

            default:
                writer.WriteStringValue(value);
                break;
        }
    }
}
