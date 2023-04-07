// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json;

namespace Aydsko.iRacingData.Converters;

public sealed class UriConverter : JsonConverter<Uri>
{
    public override Uri? Read(ref Utf8JsonReader reader,
                              Type typeToConvert,
                              JsonSerializerOptions options)
    {
        var uriString = reader.GetString()?.Trim();

        if (uriString is null or { Length: 0 }
            || Uri.TryCreate(uriString, UriKind.Absolute, out var uri))
        {
            return default;
        }

        return uri;
    }

    public override void Write(Utf8JsonWriter writer,
                               Uri value,
                               JsonSerializerOptions options)
    {
        if (writer is null)
        {
            throw new ArgumentNullException(nameof(writer));
        }

        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStringValue(value.AbsoluteUri);
    }
}

