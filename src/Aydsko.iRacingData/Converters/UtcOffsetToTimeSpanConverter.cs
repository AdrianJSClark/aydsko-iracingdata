using System.Text.Json;

namespace Aydsko.iRacingData.Converters;

public class UtcOffsetToTimeSpanConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
#pragma warning disable IDE0072 // Add missing cases - The default case is valid for these.
        var value = reader.TokenType switch
        {
            JsonTokenType.Number => reader.GetInt32(),
            _ => (int?)null,
        };
#pragma warning restore IDE0072 // Add missing cases

        if (value is null)
        {
            return TimeSpan.Zero;
        }

        return TimeSpan.FromMinutes(value.Value);
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(writer);
#else
        if (writer is null)
        {
            throw new ArgumentNullException(nameof(writer));
        }
#endif

        writer.WriteNumberValue(value.TotalMinutes);
    }
}

public class UtcOffsetToTimeSpanArrayConverter : JsonConverter<TimeSpan[]>
{
    public override TimeSpan[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!(reader.Read() && reader.TokenType == JsonTokenType.Number))
        {
            return [];
        }

        var values = new List<TimeSpan>();
        do
        {
            var value = reader.GetInt32();
            values.Add(TimeSpan.FromMinutes(value));
        }
        while (reader.Read() && reader.TokenType == JsonTokenType.Number);

        return [.. values];
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan[] value, JsonSerializerOptions options)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(writer);
#else
        if (writer is null)
        {
            throw new ArgumentNullException(nameof(writer));
        }
#endif

        if (value is null or { Length: 0 })
        {
            writer.WriteStartArray();
            writer.WriteEndArray();
            return;
        }

        writer.WriteStartArray();
#pragma warning disable CA1062 // Validate arguments of public methods - It is validated above. Not sure why you're complaining, C# compiler.
        foreach (var item in value)
        {
            writer.WriteNumberValue(item.TotalMinutes);
        }
#pragma warning restore CA1062 // Validate arguments of public methods
        writer.WriteEndArray();
    }
}
