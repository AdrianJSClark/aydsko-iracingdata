// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json;

namespace Aydsko.iRacingData.Converters;

/// <summary>Convert between a comma-separated string and an array of values.</summary>
public class CsvStringConverter : JsonConverter<string[]>
{

    /// <summary>Read a comma-separated string and convert the values into an array.</summary>
    /// <param name="reader">The reader.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">Serializer options.</param>
    /// <returns>An array of the values or <see langword="null"/>.</returns>
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

    /// <summary>Accept an array of values and write them separated by commas.</summary>
    /// <param name="writer">The writer.</param>
    /// <param name="value">The values to write.</param>
    /// <param name="options">Serializer options.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="writer"/> is <see langword="null"/>.</exception>
    public override void Write(Utf8JsonWriter writer!!,
                               string[] value,
                               JsonSerializerOptions options)
    {
        writer.WriteStringValue(string.Join(",", value));
    }
}
