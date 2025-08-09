// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Leagues;

public class Season
{
    [JsonPropertyName("league_id")]
    public int LeagueId { get; set; }

    [JsonPropertyName("season_id")]
    public int SeasonId { get; set; }

    [JsonPropertyName("point_system_id")]
    public int PointSystemId { get; set; }

    [JsonPropertyName("season_name")]
    public string SeasonName { get; set; } = default!;

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("hidden")]
    public bool Hidden { get; set; }

    [JsonPropertyName("num_drops")]
    public int NumberOfRacesToDrop { get; set; }

    [JsonPropertyName("no_drops_on_or_after_race_num")]
    public int NoDropsOnOrAfterRaceNum { get; set; }

    [JsonPropertyName("points_cars")]
    public Car[] PointsCars { get; set; } = [];

    [JsonPropertyName("driver_points_car_classes")]
    public CarClass[] DriverPointsCarClasses { get; set; } = [];

    [JsonPropertyName("team_points_car_classes")]
    public CarClass[] TeamPointsCarClasses { get; set; } = [];

    [JsonPropertyName("points_system_name")]
    public string PointsSystemName { get; set; } = default!;

    [JsonPropertyName("points_system_desc")]
    public string PointsSystemDescription { get; set; } = default!;
}
