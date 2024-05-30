// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Cars;

public class CarInfo
{
    [JsonPropertyName("ai_enabled")]
    public bool AiEnabled { get; set; }

    [JsonPropertyName("allow_number_colors")]
    public bool AllowNumberColors { get; set; }

    [JsonPropertyName("allow_number_font")]
    public bool AllowNumberFont { get; set; }

    [JsonPropertyName("allow_sponsor1")]
    public bool AllowSponsor1 { get; set; }

    [JsonPropertyName("allow_sponsor2")]
    public bool AllowSponsor2 { get; set; }

    [JsonPropertyName("allow_wheel_color")]
    public bool AllowWheelColor { get; set; }

    [JsonPropertyName("award_exempt")]
    public bool AwardExempt { get; set; }

    [JsonPropertyName("car_dirpath")]
    public string CarDirectoryPath { get; set; } = default!;

    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("car_name")]
    public string CarName { get; set; } = default!;

    [JsonPropertyName("car_name_abbreviated")]
    public string CarNameAbbreviated { get; set; } = default!;

    [JsonPropertyName("car_types")]
    public CarTypes[] CarTypes { get; set; } = Array.Empty<CarTypes>();

    [JsonPropertyName("car_weight")]
    public int CarWeight { get; set; }

    [JsonPropertyName("categories")]
    public string[] Categories { get; set; } = Array.Empty<string>();

    [JsonPropertyName("created")]
    public DateTimeOffset Created { get; set; }

    [JsonPropertyName("first_sale")]
    public DateTimeOffset FirstSale { get; set; }

    [JsonPropertyName("forum_url")] 
    public string ForumUrl { get; set; } = default!;
    
    [JsonPropertyName("free_with_subscription")]
    public bool FreeWithSubscription { get; set; }

    [JsonPropertyName("has_headlights")]
    public bool HasHeadlights { get; set; }

    [JsonPropertyName("has_multiple_dry_tire_types")]
    public bool HasMultipleDryTireTypes { get; set; }
    
    [JsonPropertyName("has_rain_capable_tire_types")]
    public bool HasRainCapableTireTypes { get; set; }

    [JsonPropertyName("hp")]
    public int Hp { get; set; }
    
    [JsonPropertyName("is_ps_purchasable")]
    public bool IsPsPurchasable { get; set; }

    [JsonPropertyName("max_power_adjust_pct")]
    public int MaxPowerAdjustPct { get; set; }

    [JsonPropertyName("max_weight_penalty_kg")]
    public int MaxWeightPenaltyKg { get; set; }

    [JsonPropertyName("min_power_adjust_pct")]
    public int MinPowerAdjustPct { get; set; }

    [JsonPropertyName("package_id")]
    public int PackageId { get; set; }

    [JsonPropertyName("patterns")]
    public int Patterns { get; set; }

    [JsonPropertyName("price")]
    public float Price { get; set; }
    
    [JsonPropertyName("price_display")] 
    public string PriceDisplay { get; set; } = default!;

    [JsonPropertyName("retired")]
    public bool Retired { get; set; }

    [JsonPropertyName("search_filters")]
    public string SearchFilters { get; set; } = default!;

    [JsonPropertyName("sku")]
    public int Sku { get; set; }

    [JsonPropertyName("paint_rules")]
    public PaintRules PaintRules { get; set; } = default!;

    [JsonPropertyName("car_make")]
    public string CarMake { get; set; } = default!;

    [JsonPropertyName("car_model")]
    public string CarModel { get; set; } = default!;

    [JsonPropertyName("site_url"), JsonConverter(typeof(UriConverter))]
    public Uri? SiteUrl { get; set; } = default!;
    
    [JsonPropertyName("rain_enabled")] 
    public bool RainEnabled { get; set; }
}

[JsonSerializable(typeof(CarInfo[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class CarInfoArrayContext : JsonSerializerContext
{ }
