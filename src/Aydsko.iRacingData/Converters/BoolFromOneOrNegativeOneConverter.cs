using System.Text.Json;

namespace Aydsko.iRacingData.Converters;

public class BoolFromOneOrNegativeOneConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader,
                              Type typeToConvert,
                              JsonSerializerOptions options)
    {
        if (reader.TryGetInt32(out var numberValue) && (numberValue == -1 || numberValue == 1))
        {
            return numberValue == 1;
        }

        throw new Exception("Failed to read value.");
    }

    public override void Write(Utf8JsonWriter writer,
                               bool value,
                               JsonSerializerOptions options)
    {
        if (writer is null)
        {
            throw new ArgumentNullException(nameof(writer));
        }

        writer.WriteNumberValue(value ? 1 : -1);
    }
}
