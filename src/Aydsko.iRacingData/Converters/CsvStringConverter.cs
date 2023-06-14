// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json;

namespace Aydsko.iRacingData.Converters;

public sealed class CsvStringConverter : JsonConverter<string[]>
{
    public override string[]? Read(ref Utf8JsonReader reader,
                                   Type typeToConvert,
                                   JsonSerializerOptions options)
    {
        var csvString = reader.GetString();

        return csvString is null or { Length: 0 }
            ? null
            : csvString.Split(',');
    }

    public override void Write(Utf8JsonWriter writer,
                               string[] value,
                               JsonSerializerOptions options)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(writer);
#else
        if (writer is null)
        {
            throw new ArgumentNullException(nameof(writer));
        }
#endif

        writer.WriteStringValue(string.Join(",", value));
    }
}
