// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json;

namespace Aydsko.iRacingData.Converters;

/// <summary>
/// The raw iRacing API results use a number type which carries duration values to the ten-thousandth of a second.
/// So, for example, a lap which was displayed in the iRacing results page as &quot;1:23.456&quot; would be returned as &quot;834560&quot;.
/// </summary>
public sealed class TenThousandthSecondDurationConverter : JsonConverter<TimeSpan?>
{
    public override TimeSpan? Read(ref Utf8JsonReader reader,
                                        Type typeToConvert,
                                        JsonSerializerOptions options)
    {
        long? value = reader switch
        {
            { TokenType: JsonTokenType.String } when long.TryParse(reader.GetString(), out var valueFromString) => valueFromString,
            { TokenType: JsonTokenType.Number } when reader.TryGetInt64(out var valueFromNumber) => valueFromNumber,
            _ => null,
        };

        if (value is null)
        {
            return null;
        }

        return TimeSpan.FromSeconds(value.Value / 10000D); // iRacing reports to the ten-thousandth & this is the easiest way to parse out.
    }

    public override void Write(Utf8JsonWriter writer,
                               TimeSpan? value,
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

        if (value is null)
        {
            writer.WriteNullValue();
        }
        else
        {
            var millisValue = (long)(value.Value.TotalSeconds * 10000);
            writer.WriteNumberValue(millisValue);
        }
    }
}


/// <summary>
/// The raw iRacing API results use a number type which carries duration values to the ten-thousandth of a second.
/// So, for example, a lap which was displayed in the iRacing results page as &quot;1:23.456&quot; would be returned as &quot;834560&quot;.
/// </summary>
public sealed class TenThousandthSecondDurationNotNullConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader,
                                        Type typeToConvert,
                                        JsonSerializerOptions options)
    {
        long? value = reader switch
        {
            { TokenType: JsonTokenType.String } when long.TryParse(reader.GetString(), out var valueFromString) => valueFromString,
            { TokenType: JsonTokenType.Number } when reader.TryGetInt64(out var valueFromNumber) => valueFromNumber,
            _ => null,
        };

        if (value is null)
        {
            return TimeSpan.MinValue;
        }

        return TimeSpan.FromSeconds(value.Value / 10000D); // iRacing reports to the ten-thousandth & this is the easiest way to parse out.
    }

    public override void Write(Utf8JsonWriter writer,
                               TimeSpan value,
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

        if (value == TimeSpan.MinValue)
        {
            writer.WriteNullValue();
        }
        else
        {
            var millisecondsValue = (long)(value.TotalSeconds * 10000);
            writer.WriteNumberValue(millisecondsValue);
        }
    }
}
