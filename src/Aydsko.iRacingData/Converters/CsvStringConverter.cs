// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json;

namespace Aydsko.iRacingData.Converters;

public class CsvStringConverter : JsonConverter<string[]>
{
    public override string[]? Read(ref Utf8JsonReader reader,
                                   Type typeToConvert,
                                   JsonSerializerOptions options)
    {
        var csvString = reader.GetString();

        if (csvString is null or { Length: 0 })
        {
            return null;
        }

        return csvString.Split(',');
    }

    public override void Write(Utf8JsonWriter writer,
                               string[] value,
                               JsonSerializerOptions options)
    {
        if (writer is null)
        {
            throw new ArgumentNullException(nameof(writer));
        }

        writer.WriteStringValue(string.Join(",", value));
    }
}
