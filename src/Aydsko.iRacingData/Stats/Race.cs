// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Stats;

public class Race
{
    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("series_id")]
    public int SeriesId { get; set; }

    [JsonPropertyName("series_name")]
    public string SeriesName { get; set; } = null!;

    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("car_class_id")]
    public int CarClassId { get; set; }

    [JsonPropertyName("livery")]
    public Livery Livery { get; set; } = null!;

    [JsonPropertyName("license_level")]
    public int LicenseLevel { get; set; }

    [JsonPropertyName("session_start_time")]
    public DateTimeOffset SessionStartTime { get; set; }

    [JsonPropertyName("winner_group_id")]
    public int WinnerGroupId { get; set; }

    [JsonPropertyName("winner_name")]
    public string WinnerName { get; set; } = null!;

    [JsonPropertyName("winner_helmet")]
    public Helmet WinnerHelmet { get; set; } = null!;

    [JsonPropertyName("winner_license_level")]
    public int WinnerLicenseLevel { get; set; }

    [JsonPropertyName("start_position")]
    public int StartPosition { get; set; }

    [JsonPropertyName("finish_position")]
    public int FinishPosition { get; set; }

    [JsonPropertyName("qualifying_time"), JsonConverter(typeof(TenThousandthSecondDurationConverter))]
    public TimeSpan? QualifyingTime { get; set; }

    [JsonPropertyName("laps")]
    public int Laps { get; set; }

    [JsonPropertyName("laps_led")]
    public int LapsLed { get; set; }

    [JsonPropertyName("incidents")]
    public int Incidents { get; set; }

    [JsonPropertyName("club_points")]
    public int ClubPoints { get; set; }

    [JsonPropertyName("points")]
    public int Points { get; set; }

    [JsonPropertyName("strength_of_field")]
    public int StrengthOfField { get; set; }

    [JsonPropertyName("subsession_id")]
    public int SubsessionId { get; set; }

    [JsonPropertyName("old_sub_level")]
    public int OldSubLevel { get; set; }

    [JsonPropertyName("new_sub_level")]
    public int NewSubLevel { get; set; }

    [JsonPropertyName("oldi_rating")]
    public int OldiRating { get; set; }

    [JsonPropertyName("newi_rating")]
    public int NewiRating { get; set; }

    [JsonPropertyName("track")]
    public Track Track { get; set; } = null!;
}
