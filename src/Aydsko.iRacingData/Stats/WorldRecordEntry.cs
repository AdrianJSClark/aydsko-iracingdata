// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Stats;

public class WorldRecordEntry
{
    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = default!;

    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("track_id")]
    public int TrackId { get; set; }

    [JsonPropertyName("season_year")]
    public int? SeasonYear { get; set; }

    [JsonPropertyName("season_quarter")]
    public int? SeasonQuarter { get; set; }

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; } = default!;

    [JsonPropertyName("region")]
    public string Region { get; set; } = default!;

    [JsonPropertyName("license")]
    public License License { get; set; } = default!;

    [JsonPropertyName("practice_lap_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? PracticeLapTime { get; set; }

#if NET6_0_OR_GREATER
    [JsonPropertyName("practice_date"), JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly PracticeDate { get; set; } = default!;
#else
    [JsonPropertyName("practice_date"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime PracticeDate { get; set; } = default!;
#endif

    [JsonPropertyName("qualify_lap_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? QualifyLapTime { get; set; }

#if NET6_0_OR_GREATER
    [JsonPropertyName("qualify_date"), JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly QualifyDate { get; set; } = default!;
#else
    [JsonPropertyName("qualify_date"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime QualifyDate { get; set; } = default!;
#endif

    [JsonPropertyName("tt_lap_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? TimeTrialLapTime { get; set; }


#if NET6_0_OR_GREATER
    [JsonPropertyName("tt_date"), JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly TimeTrialDate { get; set; } = default!;
#else
    [JsonPropertyName("tt_date"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime TimeTrialDate { get; set; } = default!;
#endif

    [JsonPropertyName("race_lap_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? RaceLapTime { get; set; }


#if NET6_0_OR_GREATER
    [JsonPropertyName("race_date"), JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly RaceDate { get; set; } = default!;
#else
    [JsonPropertyName("race_date"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime RaceDate { get; set; } = default!;
#endif
}

[JsonSerializable(typeof(WorldRecordEntry[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class WorldRecordEntryArrayContext : JsonSerializerContext
{ }
