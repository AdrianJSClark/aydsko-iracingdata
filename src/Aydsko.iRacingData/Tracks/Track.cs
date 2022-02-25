using System.Text.Json.Serialization;

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
    [JsonPropertyName("closes")]
    public string Closes { get; set; } = default!;
    [JsonPropertyName("config_name")]
    public string ConfigName { get; set; } = default!;
    [JsonPropertyName("corners_per_lap")]
    public int CornersPerLap { get; set; }
    [JsonPropertyName("created")]
    public DateTime Created { get; set; }
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
    public float Latitude { get; set; }
    [JsonPropertyName("location")]
    public string Location { get; set; } = default!;
    [JsonPropertyName("longitude")]
    public float Longitude { get; set; }
    [JsonPropertyName("max_cars")]
    public int MaxCars { get; set; }
    [JsonPropertyName("night_lighting")]
    public bool NightLighting { get; set; }
    [JsonPropertyName("nominal_lap_time")]
    public float NominalLapTime { get; set; }
    [JsonPropertyName("number_pitstalls")]
    public int NumberPitstalls { get; set; }
    [JsonPropertyName("opens")]
    public string Opens { get; set; } = default!;
    [JsonPropertyName("package_id")]
    public int PackageId { get; set; }
    [JsonPropertyName("pit_road_speed_limit")]
    public int PitRoadSpeedLimit { get; set; }
    [JsonPropertyName("price")]
    public float Price { get; set; }
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
    [JsonPropertyName("track_config_length")]
    public float TrackConfigLength { get; set; }
    [JsonPropertyName("track_dirpath")]
    public string TrackDirpath { get; set; } = default!;
    [JsonPropertyName("track_id")]
    public int TrackId { get; set; }
    [JsonPropertyName("track_name")]
    public string TrackName { get; set; } = default!;
    [JsonPropertyName("track_types")]
    public TrackTypes[] TrackTypes { get; set; } = Array.Empty<TrackTypes>();
    [JsonPropertyName("banking")]
    public string Banking { get; set; } = default!;
}

[JsonSerializable(typeof(Track[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class TrackArrayContext : JsonSerializerContext
{ }
