// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

#if NET6_0_OR_GREATER
using System.Globalization;
using System.Text.Json;

namespace Aydsko.iRacingData.Converters;

public sealed class DateOnlyConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader,
                                  Type typeToConvert,
                                  JsonSerializerOptions options)
    {
        var dateString = reader.GetString()?.Trim();

        if (dateString is null or { Length: 0 })
        {
            return default;
        }

        var dateValue = DateOnly.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        return dateValue;
    }

    public override void Write(Utf8JsonWriter writer,
                               DateOnly value,
                               JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(writer);

        writer.WriteStringValue(value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
    }
}
#endif
