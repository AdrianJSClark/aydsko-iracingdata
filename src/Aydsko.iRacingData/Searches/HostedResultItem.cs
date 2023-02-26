// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Results;

namespace Aydsko.iRacingData.Searches;

public class HostedResultItem
{
    [JsonPropertyName("session_id")]
    public int SessionId { get; set; }

    [JsonPropertyName("subsession_id")]
    public int SubsessionId { get; set; }

    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; set; }

    [JsonPropertyName("end_time")]
    public DateTime EndTime { get; set; }

    [JsonPropertyName("license_category_id")]
    public int LicenseCategoryId { get; set; }

    [JsonPropertyName("num_drivers")]
    public int NumberOfDrivers { get; set; }

    [JsonPropertyName("num_cautions")]
    public int NumberOfCautions { get; set; }

    [JsonPropertyName("num_caution_laps")]
    public int NumberOfCautionLaps { get; set; }

    [JsonPropertyName("num_lead_changes")]
    public int NumberOfLeadChanges { get; set; }

    [JsonPropertyName("event_laps_complete")]
    public int EventLapsComplete { get; set; }

    [JsonPropertyName("driver_changes")]
    public bool DriverChanges { get; set; }

    [JsonPropertyName("winner_group_id")]
    public int WinnerGroupId { get; set; }

    [JsonPropertyName("winner_ai")]
    public bool WinnerAi { get; set; }

    [JsonPropertyName("track")]
    public ResultTrackInfo Track { get; set; } = default!;

    [JsonPropertyName("private_session_id")]
    public int PrivateSessionId { get; set; }

    [JsonPropertyName("session_name")]
    public string SessionName { get; set; } = default!;

    [JsonPropertyName("league_id")]
    public int LeagueId { get; set; }

    [JsonPropertyName("league_season_id")]
    public int LeagueSeasonId { get; set; }

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("practice_length")]
    public int PracticeLength { get; set; }

    [JsonPropertyName("qualify_length")]
    public int QualifyLength { get; set; }

    [JsonPropertyName("qualify_laps")]
    public int QualifyLaps { get; set; }

    [JsonPropertyName("race_length")]
    public int RaceLength { get; set; }

    [JsonPropertyName("race_laps")]
    public int RaceLaps { get; set; }

    [JsonPropertyName("heat_race")]
    public bool HeatRace { get; set; }

    [JsonPropertyName("host")]
    public Host Host { get; set; } = default!;

    [JsonPropertyName("cars")]
    public CarInfo[] Cars { get; set; } = default!;
}


public class Host
{
    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = default!;
}


public class CarInfo
{
    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("car_name")]
    public string CarName { get; set; } = default!;

    [JsonPropertyName("car_class_id")]
    public int CarClassId { get; set; }

    [JsonPropertyName("car_class_name")]
    public string CarClassName { get; set; } = default!;

    [JsonPropertyName("car_class_short_name")]
    public string CarClassShortName { get; set; } = default!;

    [JsonPropertyName("car_name_abbreviated")]
    public string CarNameAbbreviated { get; set; } = default!;
}

[JsonSerializable(typeof(HostedResultItem[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class HostedResultItemContext : JsonSerializerContext
{ }
