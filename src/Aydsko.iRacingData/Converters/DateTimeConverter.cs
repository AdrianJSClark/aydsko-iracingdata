// © 2023-2025 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Globalization;
using System.Text.Json;

namespace Aydsko.iRacingData.Converters;

public sealed class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader,
                                  Type typeToConvert,
                                  JsonSerializerOptions options)
    {
        var dateString = reader.GetString()?.Trim();

        if (dateString is null or { Length: 0 })
        {
            return default;
        }

        var dateValue = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        return dateValue;
    }

    public override void Write(Utf8JsonWriter writer,
                               DateTime value,
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

        writer.WriteStringValue(value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
    }
}
