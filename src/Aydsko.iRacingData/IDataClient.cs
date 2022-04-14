// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Cars;
using Aydsko.iRacingData.Constants;
using Aydsko.iRacingData.Leagues;
using Aydsko.iRacingData.Lookups;
using Aydsko.iRacingData.Member;
using Aydsko.iRacingData.Results;
using Aydsko.iRacingData.Series;
using Aydsko.iRacingData.Stats;
using Aydsko.iRacingData.Tracks;

namespace Aydsko.iRacingData;

public interface IDataClient
{
    /// <summary>Retrieves details about the car assets, including image paths and descriptions.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing a dictionary which maps the car identifier to a <see cref="CarAssetDetail"/> object for each car.</returns>
    /// <remarks>All image paths are relative to <c>https://images-static.iracing.com</c>.</remarks>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="Exception">If there's a problem processing the result.</exception>
    Task<DataResponse<IReadOnlyDictionary<string, CarAssetDetail>>> GetCarAssetDetailsAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieves details about the car classes.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="CarClass"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="Exception">If there's a problem processing the result.</exception>
    Task<DataResponse<CarClass[]>> GetCarClassesAsync(CancellationToken cancellationToken = default);

    /// <summary>Return a summary of statistics for the given customer's career or that or the authenticated user.</summary>
    /// <param name="customerId">iRacing Customer Id for the member to return statistics for, or <see langword="null"/> for the currently authenticated user.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the statistics in a <see cref="MemberCareer"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    Task<DataResponse<MemberCareer>> GetCareerStatisticsAsync(int? customerId = null, CancellationToken cancellationToken = default);

    /// <summary>Retrieves details about the cars.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="CarInfo"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="Exception">If there's a problem processing the result.</exception>
    Task<DataResponse<CarInfo[]>> GetCarsAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieves a list of the iRacing Divisions.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="Division"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="Exception">If there's a problem processing the result.</exception>
    Task<DataResponse<Division[]>> GetDivisionsAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieve information about one or more other drivers by their customer identifier.</summary>
    /// <param name="customerIds">An array of one or more customer identifiers.</param>
    /// <param name="includeLicenses">Indicates if license information should be included. Either <see langword="true"/> or <see langword="false"/> to exclude for performance purposes.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="DriverInfo"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    Task<DataResponse<DriverInfo[]>> GetDriverInfoAsync(int[] customerIds, bool includeLicenses, CancellationToken cancellationToken = default);

    /// <summary>Get information about a league.</summary>
    /// <param name="leagueId">The unique identifier for the league.</param>
    /// <param name="includeLicenses">Indicates if license information should be included. Either <see langword="true"/> or <see langword="false"/> to exclude for performance purposes.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the season & optionally series detail in a <see cref="Tracks.Track"/> array.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    Task<DataResponse<League>> GetLeagueAsync(int leagueId, bool includeLicenses = false, CancellationToken cancellationToken = default);

    /// <summary>Information about license levels available in the iRacing system.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="License"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="Exception">If there's a problem processing the result.</exception>
    Task<DataResponse<LicenseLookup[]>> GetLicenseLookupsAsync(CancellationToken cancellationToken = default);

    /// <summary>Information about reference data defined by the system.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="LookupGroup"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="Exception">If there's a problem processing the result.</exception>
    Task<DataResponse<LookupGroup[]>> GetLookupsAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieve information about the authenticated member's division.</summary>
    /// <param name="seasonId">Unique identifier for the racing season.</param>
    /// <param name="eventType">The type of events to return, either <see cref="EventType.TimeTrial" /> or <see cref="EventType.Race" />.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="MemberDivision" /> object containing the result.</returns>
    /// <remarks>Divisions are 0-based: 0 is Division 1, 10 is Rookie. See <see cref="GetDivisionsAsync(CancellationToken)"/> for more information.</remarks>
    Task<DataResponse<MemberDivision>> GetMemberDivisionAsync(int seasonId, EventType eventType, CancellationToken cancellationToken = default);

    /// <summary>Retrieve the recent race participation for the currently authenticated member.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the member's recent races in a <see cref="MemberRecentRaces"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    Task<DataResponse<MemberRecentRaces>> GetMemberRecentRacesAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieve overall summary figures for the <paramref name="customerId"/> given or the current authenticated user.</summary>
    /// <param name="customerId">iRacing Customer Id for the member to return statistics for, or <see langword="null"/> for the currently authenticated user.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the member's recent races in a <see cref="MemberRecentRaces"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    Task<DataResponse<MemberSummary>> GetMemberSummaryAsync(int? customerId = null, CancellationToken cancellationToken = default);

    /// <summary>Retrieve the statistics for the currently authenticated member, grouped by year.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the member's statistics in a <see cref="MemberYearlyStatistics"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    Task<DataResponse<MemberYearlyStatistics>> GetMemberYearlyStatisticsAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieve the <see cref="MemberInfo"/> for the currently authenticated user.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the statistics in a <see cref="MemberInfo"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    Task<DataResponse<MemberInfo>> GetMyInfoAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieve the driver standings for a season.</summary>
    /// <param name="seasonId">Unique identifier for the racing season.</param>
    /// <param name="carClassId">Car class identifier. See <see cref="GetCarClassesAsync(CancellationToken)" />.</param>
    /// <param name="raceWeekNumber">Week number within the given season, starting with 0 for the first week.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A header with overall series information and an array of standings.</returns>
    Task<DataResponse<(SeasonDriverStandingsHeader Header, SeasonDriverStanding[] Standings)>> GetSeasonDriverStandingsAsync(int seasonId, int carClassId, int raceWeekNumber, CancellationToken cancellationToken = default);

    /// <summary>Retrieve the qualifying results for a season.</summary>
    /// <param name="seasonId">Unique identifier for the racing season.</param>
    /// <param name="carClassId">Car class identifier. See <see cref="GetCarClassesAsync(CancellationToken)" />.</param>
    /// <param name="raceWeekNumber">Week number within the given season, starting with 0 for the first week.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A header with overall series information and an array of qualifying results.</returns>
    Task<DataResponse<(SeasonQualifyResultsHeader Header, SeasonQualifyResult[] Results)>> GetSeasonQualifyResultsAsync(int seasonId, int carClassId, int raceWeekNumber, CancellationToken cancellationToken = default);

    /// <summary>Retrieve the time trial results for a season.</summary>
    /// <param name="seasonId">Unique identifier for the racing season.</param>
    /// <param name="carClassId">Car class identifier. See <see cref="GetCarClassesAsync(CancellationToken)" />.</param>
    /// <param name="raceWeekNumber">Week number within the given season, starting with 0 for the first week.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A header with overall series information and an array of time trial results.</returns>
    Task<DataResponse<(SeasonTimeTrialResultsHeader Header, SeasonTimeTrialResult[] Results)>> GetSeasonTimeTrialResultsAsync(int seasonId, int carClassId, int raceWeekNumber, CancellationToken cancellationToken = default);

    /// <summary>Retrieve information about the races run during a week in the season.</summary>
    /// <param name="seasonId">Unique identifier for the racing season.</param>
    /// <param name="eventType">The type of events to return.</param>
    /// <param name="raceWeekNumber">Week number within the given season, starting with 0 for the first week.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the races in a <see cref="SeasonResults"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    Task<DataResponse<SeasonResults>> GetSeasonResultsAsync(int seasonId, EventType eventType, int raceWeekNumber, CancellationToken cancellationToken = default);

    /// <summary>Retrieve information about the season & series.</summary>
    /// <param name="includeSeries">Indicate if the series details should be included.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the season & optionally series detail in a <see cref="SeasonSeries"/> array.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    Task<DataResponse<SeasonSeries[]>> GetSeasonsAsync(bool includeSeries, CancellationToken cancellationToken = default);

    /// <summary>Retrieve a list of series.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the season & optionally series detail in a <see cref="SeasonSeries"/> array.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    /// <remarks>To get series and seasons for which standings should be available, filter the list where <see cref="StatisticsSeries.Official" /> is <see langword="true" />.</remarks>
    Task<DataResponse<StatisticsSeries[]>> GetStatisticsSeriesAsync(CancellationToken cancellationToken = default);

    /// <summary>Get the lap details for a particular driver in the given single-driver subsession.</summary>
    /// <param name="subSessionId">The identifier of the subsession for which results should be returned.</param>
    /// <param name="simSessionNumber">The number of the session where <c>0</c> is the main event, <c>-1</c> event before the main, etc</param>
    /// <param name="customerId">A customer identifier value for the driver in the race to return laps for.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the result details in a <see cref="SubSessionResult"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    Task<DataResponse<(SubsessionLapsHeader Header, SubsessionLap[] Laps)>> GetSingleDriverSubsessionLapsAsync(int subSessionId, int simSessionNumber, int customerId, CancellationToken cancellationToken = default);

    /// <summary>Get the results of a subsession, if the authenticated user is authorized to view them.</summary>
    /// <param name="subSessionId">The identifier of the subsession for which results should be returned.</param>
    /// <param name="simSessionNumber">The number of the session where <c>0</c> is the main event, <c>-1</c> event before the main, etc</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the result details in a <see cref="SubSessionResult"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    Task<DataResponse<(SubsessionLapsHeader Header, SubsessionChartLap[] Laps)>> GetSubSessionLapChartAsync(int subSessionId, int simSessionNumber, CancellationToken cancellationToken = default);

    /// <summary>Get the results of a subsession, if the authenticated user is authorized to view them.</summary>
    /// <param name="subSessionId">The identifier of the subsession for which results should be returned.</param>
    /// <param name="includeLicenses">Indicates if license information should be included. Either <see langword="true"/> or <see langword="false"/> to exclude for performance purposes.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the result details in a <see cref="SubSessionResult"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    Task<DataResponse<SubSessionResult>> GetSubSessionResultAsync(int subSessionId, bool includeLicenses, CancellationToken cancellationToken = default);

    /// <summary>Get the lap details for a team in the given team subsession.</summary>
    /// <param name="subSessionId">The identifier of the subsession for which results should be returned.</param>
    /// <param name="simSessionNumber">The number of the session where <c>0</c> is the main event, <c>-1</c> event before the main, etc</param>
    /// <param name="teamId">The unique team identifier value for the team from the race to return laps for.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing overall session details in a <see cref="SubsessionLapsHeader"/> object along with an array of <see cref="SubsessionLap" />.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    Task<DataResponse<(SubsessionLapsHeader Header, SubsessionLap[] Laps)>> GetTeamSubsessionLapsAsync(int subSessionId, int simSessionNumber, int teamId, CancellationToken cancellationToken = default);

    /// <summary>Retrieve information about the track assets.</summary>
    /// <remarks>Image paths are relative to https://images-static.iracing.com/.</remarks>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the season & optionally series detail in a <see cref="Tracks.Track"/> array.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    Task<DataResponse<IReadOnlyDictionary<string, TrackAssets>>> GetTrackAssetsAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieve information about the tracks.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the season & optionally series detail in a <see cref="Tracks.Track"/> array.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    Task<DataResponse<Tracks.Track[]>> GetTracksAsync(CancellationToken cancellationToken = default);

    /// <summary>Retrieve a list of session events.</summary>
    /// <param name="subSessionId">The identifier of the subsession for which results should be returned.</param>
    /// <param name="simSessionNumber">The number of the session where <c>0</c> is the main event, <c>-1</c> event before the main, etc</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns></returns>
    Task<DataResponse<(SubsessionEventLogHeader Header, SubsessionEventLogItem[] LogItems)>> GetSubsessionEventLogAsync(int subSessionId, int simSessionNumber, CancellationToken cancellationToken = default);
}
