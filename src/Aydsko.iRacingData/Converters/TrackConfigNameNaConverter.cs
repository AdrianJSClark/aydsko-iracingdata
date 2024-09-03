// © 2024 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json;

namespace Aydsko.iRacingData.Converters;

internal class TrackConfigNameNaConverter : JsonConverter<string?>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
#pragma warning disable IDE0072 // Add missing cases - Correct behaviour is already the default case
        var value = reader.TokenType switch
        {
            JsonTokenType.String => reader.GetString(),
            _ => null
        };
#pragma warning restore IDE0072 // Add missing cases

        return string.IsNullOrWhiteSpace(value) || string.Equals(value, "N/A", StringComparison.OrdinalIgnoreCase) ? null : value;
    }

    public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
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
            case null or { Length: 0 }:
                writer.WriteStringValue("N/A");
                break;
            default:
                writer.WriteStringValue(value);
                break;
        }
    }
}
