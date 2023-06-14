// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json;

namespace Aydsko.iRacingData.Converters;

public sealed class ServiceStatusHistoryItemArrayConverter : JsonConverter<ServiceStatusHistoryItem[]>
{
    private readonly StatusTimeStampConverter StatusTimeStampConverter = new();

    public override ServiceStatusHistoryItem[]? Read(ref Utf8JsonReader reader,
                                                   Type typeToConvert,
                                                   JsonSerializerOptions options)
    {
        if (!(reader.Read() && reader.TokenType == JsonTokenType.StartArray))
        {
            return null;
        }

        var items = new List<ServiceStatusHistoryItem>();

        do
        {
            reader.Read();
            var item = ReadItem(ref reader, options);
            if (item is not null)
            {
                items.Add(item);
            }
        }
        while (reader.Read() && reader.TokenType == JsonTokenType.StartArray);

        return items.ToArray();
    }

    private ServiceStatusHistoryItem? ReadItem(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        var instantValue = StatusTimeStampConverter.Read(ref reader, typeof(DateTimeOffset?), options);

        reader.Read();

        if (reader.TokenType == JsonTokenType.Null || !reader.TryGetDecimal(out var statusValue))
        {
            return null;
        }

        reader.Read(); // Read end of the array

        return new ServiceStatusHistoryItem { Instant = instantValue, Value = statusValue };
    }

    public override void Write(Utf8JsonWriter writer,
                               ServiceStatusHistoryItem[] value,
                               JsonSerializerOptions options)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(writer);
        ArgumentNullException.ThrowIfNull(value);
#else
        if (writer is null)
        {
            throw new ArgumentNullException(nameof(writer));
        }

        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
#endif

        writer.WriteStartArray();

        foreach (var item in value)
        {
            writer.WriteStartArray();
            StatusTimeStampConverter.Write(writer, item.Instant, options);
            writer.WriteNumberValue(item.Value);
            writer.WriteEndArray();
        }

        writer.WriteEndArray();
    }
}
