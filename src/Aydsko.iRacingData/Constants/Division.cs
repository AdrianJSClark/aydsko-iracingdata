// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Constants;

public class Division
{
    [JsonPropertyName("label")]
    public string Label { get; set; } = null!;

    [JsonPropertyName("value")]
    public int Value { get; set; }
}

[JsonSerializable(typeof(Division[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class DivisionArrayContext : JsonSerializerContext
{ }
