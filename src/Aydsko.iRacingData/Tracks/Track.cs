// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Tracks;

public class Track
{
    [JsonPropertyName("ai_enabled")]
    public bool AiEnabled { get; set; }

    [JsonPropertyName("award_exempt")]
    public bool AwardExempt { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; } = default!;

    [JsonPropertyName("category_id")]
    public int CategoryId { get; set; }

#if NET6_0_OR_GREATER
    [JsonPropertyName("closes"), JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly Closes { get; set; } = default!;
#else
    [JsonPropertyName("closes"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Closes { get; set; } = default!;
#endif

    [JsonPropertyName("config_name")]
    public string ConfigName { get; set; } = default!;

    [JsonPropertyName("corners_per_lap")]
    public int CornersPerLap { get; set; }

    [JsonPropertyName("created")]
    public DateTimeOffset Created { get; set; }

    [JsonPropertyName("free_with_subscription")]
    public bool FreeWithSubscription { get; set; }

    [JsonPropertyName("fully_lit")]
    public bool FullyLit { get; set; }

    [JsonPropertyName("grid_stalls")]
    public int GridStalls { get; set; }

    [JsonPropertyName("has_opt_path")]
    public bool HasOptPath { get; set; }

    [JsonPropertyName("has_short_parade_lap")]
    public bool HasShortParadeLap { get; set; }

    [JsonPropertyName("has_svg_map")]
    public bool HasSvgMap { get; set; }

    [JsonPropertyName("is_dirt")]
    public bool IsDirt { get; set; }

    [JsonPropertyName("is_oval")]
    public bool IsOval { get; set; }

    [JsonPropertyName("lap_scoring")]
    public int LapScoring { get; set; }

    [JsonPropertyName("latitude")]
    public decimal Latitude { get; set; }

    [JsonPropertyName("location")]
    public string Location { get; set; } = default!;

    [JsonPropertyName("longitude")]
    public decimal Longitude { get; set; }

    [JsonPropertyName("max_cars")]
    public int MaxCars { get; set; }

    [JsonPropertyName("night_lighting")]
    public bool NightLighting { get; set; }

    [JsonPropertyName("nominal_lap_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? NominalLapTime { get; set; }

    [JsonPropertyName("number_pitstalls")]
    public int NumberPitstalls { get; set; }

#if NET6_0_OR_GREATER
    [JsonPropertyName("opens"), JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly Opens { get; set; } = default!;
#else
    [JsonPropertyName("opens"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Opens { get; set; } = default!;
#endif

    [JsonPropertyName("package_id")]
    public int PackageId { get; set; }

    /// <summary>Speed limit for pit road in miles-per-hour (mph)</summary>
    [JsonPropertyName("pit_road_speed_limit")]
    public int PitRoadSpeedLimit { get; set; }

    /// <summary>Speed limit for pit road in kilometres-per-hour (mph)</summary>
    [JsonIgnore]
    public int PitRoadSpeedLimitKph => (int)Math.Round(PitRoadSpeedLimit * 1.609344M);

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("priority")]
    public int Priority { get; set; }

    [JsonPropertyName("purchasable")]
    public bool Purchasable { get; set; }

    [JsonPropertyName("qualify_laps")]
    public int QualifyLaps { get; set; }

    [JsonPropertyName("restart_on_left")]
    public bool RestartOnLeft { get; set; }

    [JsonPropertyName("retired")]
    public bool Retired { get; set; }

    [JsonPropertyName("search_filters")]
    public string SearchFilters { get; set; } = default!;

    [JsonPropertyName("site_url")]
    public string SiteUrl { get; set; } = default!;

    [JsonPropertyName("sku")]
    public int Sku { get; set; }

    [JsonPropertyName("solo_laps")]
    public int SoloLaps { get; set; }

    [JsonPropertyName("start_on_left")]
    public bool StartOnLeft { get; set; }

    [JsonPropertyName("supports_grip_compound")]
    public bool SupportsGripCompound { get; set; }

    [JsonPropertyName("tech_track")]
    public bool TechTrack { get; set; }

    [JsonPropertyName("time_zone")]
    public string TimeZone { get; set; } = default!;

    /// <summary>The length of this track configuration in miles.</summary>
    [JsonPropertyName("track_config_length")]
    public decimal TrackConfigLength { get; set; }

    /// <summary>The length of this track configuration converted to kilometres.</summary>
    [JsonIgnore]
    public decimal TrackConfigLengthKm => Math.Truncate(TrackConfigLength * 160.9344M) / 100;

    [JsonPropertyName("track_dirpath")]
    public string TrackDirpath { get; set; } = default!;

    [JsonPropertyName("track_id")]
    public int TrackId { get; set; }

    [JsonPropertyName("track_name")]
    public string TrackName { get; set; } = default!;

    [JsonPropertyName("track_types")]
    public TrackTypes[] TrackTypes { get; set; } = [];

    [JsonPropertyName("banking")]
    public string Banking { get; set; } = default!;

    [JsonIgnore]
    public string TrackLogoUrlLight => $"{TrackAssets.ImagePathBase}img/logos/tracks/{PackageId}__light.png";

    [JsonIgnore]
    public string TrackLogoUrlDark => $"{TrackAssets.ImagePathBase}img/logos/tracks/{PackageId}__dark.png";
}

[JsonSerializable(typeof(Track[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class TrackArrayContext : JsonSerializerContext
{ }
