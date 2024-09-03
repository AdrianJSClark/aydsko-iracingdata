using System.Text.Json;

namespace Aydsko.iRacingData.Converters;

/// <summary>
/// Convert the cloud cover value from the API to a decimal.
/// </summary>
public class OneDecimalPointValueConverter : JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TryGetInt32(out var intValue) ? intValue / 10m : default;
    }

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(writer);
#else
        if (writer is null)
        {
            throw new ArgumentNullException(nameof(writer));
        }
#endif

        writer.WriteNumberValue(value * 10);
    }
}
