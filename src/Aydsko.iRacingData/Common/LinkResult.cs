// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Common;

internal class LinkResult
{
    [JsonPropertyName("link")]
    public string Link { get; set; } = default!;
}

[JsonSerializable(typeof(LinkResult)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class LinkResultContext : JsonSerializerContext
{ }
