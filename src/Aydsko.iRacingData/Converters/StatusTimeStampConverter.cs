// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json;

namespace Aydsko.iRacingData.Converters;

public sealed class StatusTimeStampConverter : JsonConverter<DateTimeOffset>
{
    private static readonly DateTimeOffset Epoch = new(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

    public override DateTimeOffset Read(ref Utf8JsonReader reader,
                                         Type typeToConvert,
                                         JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.Null && reader.TryGetDouble(out var rawValue))
        {
            return Epoch.AddMilliseconds(1000 * rawValue);
        }

        return default;
    }

    public override void Write(Utf8JsonWriter writer,
                               DateTimeOffset value,
                               JsonSerializerOptions options)
    {
        if (writer is null)
        {
            throw new ArgumentNullException(nameof(writer));
        }

        if (value is DateTimeOffset instant)
        {
            var rawValue = (decimal)(instant - Epoch).TotalMilliseconds / 1000;
            writer.WriteNumberValue(rawValue);
            return;
        }

        writer.WriteNullValue();
        return;
    }
}
