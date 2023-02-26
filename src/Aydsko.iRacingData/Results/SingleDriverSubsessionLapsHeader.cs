// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Results;

public class SingleDriverSubsessionLapsHeader : SubsessionLapsHeader
{
    [JsonPropertyName("group_id")]
    public int GroupId { get; set; }

    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("license_level")]
    public int LicenseLevel { get; set; }

    [JsonPropertyName("livery")]
    public Livery Livery { get; set; } = null!;
}

[JsonSerializable(typeof(SingleDriverSubsessionLapsHeader)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class SingleDriverSubsessionLapsHeaderContext : JsonSerializerContext
{ }
