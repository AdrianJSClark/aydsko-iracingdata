// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Constants;

/// <summary>A division.</summary>
public class Division
{
    /// <summary>Division label.</summary>
    [JsonPropertyName("label")]
    public string Label { get; set; } = null!;

    /// <summary>Number associated with the division.</summary>
    [JsonPropertyName("value")]
    public int Value { get; set; }
}

[JsonSerializable(typeof(Division[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class DivisionArrayContext : JsonSerializerContext
{ }
