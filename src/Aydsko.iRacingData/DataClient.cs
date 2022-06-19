// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Cars;
using Aydsko.iRacingData.Constants;
using Aydsko.iRacingData.Exceptions;
using Aydsko.iRacingData.Leagues;
using Aydsko.iRacingData.Lookups;
using Aydsko.iRacingData.Member;
using Aydsko.iRacingData.Results;
using Aydsko.iRacingData.Searches;
using Aydsko.iRacingData.Series;
using Aydsko.iRacingData.Stats;
using Aydsko.iRacingData.Tracks;
using Microsoft.AspNetCore.WebUtilities;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization.Metadata;

namespace Aydsko.iRacingData;

internal class DataClient : IDataClient
{
    private readonly HttpClient httpClient;
    private readonly ILogger<DataClient> logger;
    private readonly iRacingDataClientOptions options;
    private readonly CookieContainer cookieContainer;

    public bool IsLoggedIn { get; private set; }

    public DataClient(HttpClient httpClient,
                      ILogger<DataClient> logger,
                      iRacingDataClientOptions options,
                      CookieContainer cookieContainer)
    {
        this.httpClient = httpClient;
        this.logger = logger;
        this.options = options;
        this.cookieContainer = cookieContainer;
    }

    /// <inheritdoc />
    public async Task<DataResponse<IReadOnlyDictionary<string, CarAssetDetail>>> GetCarAssetDetailsAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var carAssetDetailsUrl = new Uri("https://members-ng.iracing.com/data/car/assets");
        (var headers, var data) = await CreateResponseViaInfoLinkAsync(carAssetDetailsUrl, CarAssetDetailDictionaryContext.Default.IReadOnlyDictionaryStringCarAssetDetail, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<Cars.CarInfo[]>> GetCarsAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var carInfoUrl = new Uri("https://members-ng.iracing.com/data/car/get");
        (var headers, var data) = await CreateResponseViaInfoLinkAsync(carInfoUrl, CarInfoArrayContext.Default.CarInfoArray, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<CarClass[]>> GetCarClassesAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var carClassUrl = new Uri("https://members-ng.iracing.com/data/carclass/get");
        (var headers, var data) = await CreateResponseViaInfoLinkAsync(carClassUrl, CarClassArrayContext.Default.CarClassArray, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<Division[]>> GetDivisionsAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var constantsDivisionsUrl = new Uri("https://members-ng.iracing.com/data/constants/divisions");
        var constantsDivisionsResponse = await httpClient.GetAsync(constantsDivisionsUrl, cancellationToken).ConfigureAwait(false);

        var data = await constantsDivisionsResponse.Content.ReadFromJsonAsync(DivisionArrayContext.Default.DivisionArray, cancellationToken).ConfigureAwait(false);
        if (data is null)
        {
            throw new iRacingDataClientException("Data not found.");
        }

        return BuildDataResponse(constantsDivisionsResponse.Headers, data, logger)!;
    }

    /// <inheritdoc />
    public async Task<DataResponse<Category[]>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var constantsDivisionsUrl = new Uri("https://members-ng.iracing.com/data/constants/categories");
        var constantsDivisionsResponse = await httpClient.GetAsync(constantsDivisionsUrl, cancellationToken).ConfigureAwait(false);

        var data = await constantsDivisionsResponse.Content.ReadFromJsonAsync(CategoryArrayContext.Default.CategoryArray, cancellationToken).ConfigureAwait(false);
        if (data is null)
        {
            throw new iRacingDataClientException("Data not found.");
        }

        return BuildDataResponse(constantsDivisionsResponse.Headers, data, logger)!;
    }

    /// <inheritdoc />
    public async Task<DataResponse<Constants.EventType[]>> GetEventTypesAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var constantsDivisionsUrl = new Uri("https://members-ng.iracing.com/data/constants/event_types");
        var constantsDivisionsResponse = await httpClient.GetAsync(constantsDivisionsUrl, cancellationToken).ConfigureAwait(false);

        var data = await constantsDivisionsResponse.Content.ReadFromJsonAsync(EventTypeArrayContext.Default.EventTypeArray, cancellationToken).ConfigureAwait(false);
        if (data is null)
        {
            throw new iRacingDataClientException("Data not found.");
        }

        return BuildDataResponse(constantsDivisionsResponse.Headers, data, logger)!;
    }

    /// <inheritdoc />
    public async Task<DataResponse<League>> GetLeagueAsync(int leagueId, bool includeLicenses = false, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var getTrackUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/league/get", new Dictionary<string, string>
        {
            { "league_id", leagueId.ToString(CultureInfo.InvariantCulture) },
            { "include_licenses", includeLicenses.ToString() }
        });
        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(getTrackUrl), LeagueContext.Default.League, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<LookupGroup[]>> GetLookupsAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var lookupsUrl = new Uri("https://members-ng.iracing.com/data/lookup/get?weather=weather_wind_speed_units&weather=weather_wind_speed_max&weather=weather_wind_speed_min&licenselevels=licenselevels");
        (var headers, var data) = await CreateResponseViaInfoLinkAsync(lookupsUrl, LookupGroupArrayContext.Default.LookupGroupArray, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<LicenseLookup[]>> GetLicenseLookupsAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var licenseUrl = new Uri("https://members-ng.iracing.com/data/lookup/licenses");
        (var headers, var data) = await CreateResponseViaInfoLinkAsync(licenseUrl, LicenseLookupArrayContext.Default.LicenseLookupArray, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<DriverInfo[]>> GetDriverInfoAsync(int[] customerIds, bool includeLicenses, CancellationToken cancellationToken = default)
    {
        if (customerIds is not { Length: > 0 })
        {
            throw new ArgumentOutOfRangeException(nameof(customerIds), "Must supply at least one customer identifier value to query.");
        }

        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var driverInfoRequestUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/member/get", new Dictionary<string, string>
        {
            { "cust_ids", string.Join(",", customerIds) },
            { "include_licenses", includeLicenses ? "true" : "false" }
        });
        var infoLinkResponse = await httpClient.GetAsync(new Uri(driverInfoRequestUrl), cancellationToken)
                                               .ConfigureAwait(false);
        if (!infoLinkResponse.IsSuccessStatusCode)
        {
            switch (infoLinkResponse.StatusCode)
            {
                case System.Net.HttpStatusCode.ServiceUnavailable:
                    var systemMaintenanceMessage = await infoLinkResponse.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: cancellationToken)
                                                                                 .ConfigureAwait(false);

                    if (systemMaintenanceMessage is not null && systemMaintenanceMessage.ErrorCode == "Site Maintenance")
                    {
                        throw new iRacingInMaintenancePeriodException("iRacing services are down for maintenance.");
                    }
                    break;
            }
            infoLinkResponse.EnsureSuccessStatusCode();
        }

        var infoLink = await infoLinkResponse.Content.ReadFromJsonAsync(LinkResultContext.Default.LinkResult, cancellationToken).ConfigureAwait(false);
        if (infoLink?.Link is null)
        {
            throw new iRacingDataClientException("Unrecognised result of initial query.");
        }

        var info = await httpClient.GetFromJsonAsync(infoLink.Link, DriverInfoResponseContext.Default.DriverInfoResponse, cancellationToken).ConfigureAwait(false);
        if (!(info?.Success ?? false))
        {
            throw new iRacingDataClientException("Failed to properly retrieve results.");
        }

        return BuildDataResponse(infoLinkResponse.Headers, info.Drivers!, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberInfo>> GetMyInfoAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }
        var memberInfoUrl = new Uri("https://members-ng.iracing.com/data/member/info");
        (var headers, var data) = await CreateResponseViaInfoLinkAsync(memberInfoUrl, MemberInfoContext.Default.MemberInfo, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<SubSessionResult>> GetSubSessionResultAsync(int subSessionId, bool includeLicenses, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var subSessionResultUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/results/get", new Dictionary<string, string>
        {
            { "subsession_id", subSessionId.ToString(CultureInfo.InvariantCulture) },
            { "include_licenses", includeLicenses ? "true" : "false" }
        });

        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(subSessionResultUrl), SubSessionResultContext.Default.SubSessionResult, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SubsessionLapsHeader Header, SubsessionChartLap[] Laps)>> GetSubSessionLapChartAsync(int subSessionId, int simSessionNumber, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var subSessionLapChartUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/results/lap_chart_data", new Dictionary<string, string>
        {
            { "subsession_id", subSessionId.ToString(CultureInfo.InvariantCulture) },
            { "simsession_number", simSessionNumber.ToString(CultureInfo.InvariantCulture) },
        });

        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(subSessionLapChartUrl), SubsessionLapsHeaderContext.Default.SubsessionLapsHeader, cancellationToken).ConfigureAwait(false);

        var baseChunkUrl = new Uri(data.ChunkInfo.BaseDownloadUrl);
        var sessionLapsList = new List<SubsessionChartLap>();
        foreach (var (chunkFileName, index) in data.ChunkInfo.ChunkFileNames.Select<string, (string fn, int i)>((fn, i) => (fn, i)))
        {
            var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

            var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
            if (!chunkResponse.IsSuccessStatusCode)
            {
                logger.FailedToRetrieveChunkError(index, data.ChunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                continue;
            }

            var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SubsessionChartLapArrayContext.Default.SubsessionChartLapArray, cancellationToken).ConfigureAwait(false);
            if (chunkData is null)
            {
                continue;
            }

            sessionLapsList.AddRange(chunkData);
        }

        return BuildDataResponse<(SubsessionLapsHeader Header, SubsessionChartLap[] Laps)>(headers, (data, sessionLapsList.ToArray()), logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SubsessionEventLogHeader Header, SubsessionEventLogItem[] LogItems)>> GetSubsessionEventLogAsync(int subSessionId, int simSessionNumber, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var subSessionLapChartUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/results/event_log", new Dictionary<string, string>
        {
            { "subsession_id", subSessionId.ToString(CultureInfo.InvariantCulture) },
            { "simsession_number", simSessionNumber.ToString(CultureInfo.InvariantCulture) },
        });

        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(subSessionLapChartUrl), SubsessionEventLogHeaderContext.Default.SubsessionEventLogHeader, cancellationToken).ConfigureAwait(false);

        var baseChunkUrl = new Uri(data.ChunkInfo.BaseDownloadUrl);
        var sessionLapsList = new List<SubsessionEventLogItem>();
        foreach (var (chunkFileName, index) in data.ChunkInfo.ChunkFileNames.Select<string, (string fn, int i)>((fn, i) => (fn, i)))
        {
            var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

            var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
            if (!chunkResponse.IsSuccessStatusCode)
            {
                logger.FailedToRetrieveChunkError(index, data.ChunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                continue;
            }

            var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SubsessionEventLogItemArrayContext.Default.SubsessionEventLogItemArray, cancellationToken).ConfigureAwait(false);
            if (chunkData is null)
            {
                continue;
            }

            sessionLapsList.AddRange(chunkData);
        }

        return BuildDataResponse<(SubsessionEventLogHeader Header, SubsessionEventLogItem[] Laps)>(headers, (data, sessionLapsList.ToArray()), logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<SeriesDetail[]>> GetSeriesAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var seriesUrl = new Uri("https://members-ng.iracing.com/data/series/get");
        (var headers, var data) = await CreateResponseViaInfoLinkAsync(seriesUrl, SeriesDetailArrayContext.Default.SeriesDetailArray, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<IReadOnlyDictionary<string, SeriesAsset>>> GetSeriesAssetsAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var seriesUrl = new Uri("https://members-ng.iracing.com/data/series/assets");
        (var headers, var data) = await CreateResponseViaInfoLinkAsync(seriesUrl, SeriesAssetReadOnlyDictionaryContext.Default.IReadOnlyDictionaryStringSeriesAsset, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SubsessionLapsHeader Header, SubsessionLap[] Laps)>> GetSingleDriverSubsessionLapsAsync(int subSessionId, int simSessionNumber, int customerId, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var subSessionLapChartUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/results/lap_data", new Dictionary<string, string>
        {
            { "subsession_id", subSessionId.ToString(CultureInfo.InvariantCulture) },
            { "simsession_number", simSessionNumber.ToString(CultureInfo.InvariantCulture) },
            { "cust_id", customerId.ToString(CultureInfo.InvariantCulture) },
        });

        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(subSessionLapChartUrl), SubsessionLapsHeaderContext.Default.SubsessionLapsHeader, cancellationToken).ConfigureAwait(false);

        var baseChunkUrl = new Uri(data.ChunkInfo.BaseDownloadUrl);
        var sessionLapsList = new List<SubsessionLap>();
        foreach (var (chunkFileName, index) in data.ChunkInfo.ChunkFileNames.Select<string, (string fn, int i)>((fn, i) => (fn, i)))
        {
            var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

            var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
            if (!chunkResponse.IsSuccessStatusCode)
            {
                logger.FailedToRetrieveChunkError(index, data.ChunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                continue;
            }

            var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SubsessionLapArrayContext.Default.SubsessionLapArray, cancellationToken).ConfigureAwait(false);
            if (chunkData is null)
            {
                continue;
            }

            sessionLapsList.AddRange(chunkData);
        }

        return BuildDataResponse<(SubsessionLapsHeader Header, SubsessionLap[] Laps)>(headers, (data, sessionLapsList.ToArray()), logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SubsessionLapsHeader Header, SubsessionLap[] Laps)>> GetTeamSubsessionLapsAsync(int subSessionId, int simSessionNumber, int teamId, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var subSessionLapChartUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/results/lap_data", new Dictionary<string, string>
        {
            { "subsession_id", subSessionId.ToString(CultureInfo.InvariantCulture) },
            { "simsession_number", simSessionNumber.ToString(CultureInfo.InvariantCulture) },
            { "team_id", teamId.ToString(CultureInfo.InvariantCulture) },
        });

        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(subSessionLapChartUrl), SubsessionLapsHeaderContext.Default.SubsessionLapsHeader, cancellationToken).ConfigureAwait(false);

        var baseChunkUrl = new Uri(data.ChunkInfo.BaseDownloadUrl);
        var sessionLapsList = new List<SubsessionLap>();
        foreach (var (chunkFileName, index) in data.ChunkInfo.ChunkFileNames.Select<string, (string fn, int i)>((fn, i) => (fn, i)))
        {
            var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

            var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
            if (!chunkResponse.IsSuccessStatusCode)
            {
                logger.FailedToRetrieveChunkError(index, data.ChunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                continue;
            }

            var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SubsessionLapArrayContext.Default.SubsessionLapArray, cancellationToken).ConfigureAwait(false);
            if (chunkData is null)
            {
                continue;
            }

            sessionLapsList.AddRange(chunkData);
        }

        return BuildDataResponse<(SubsessionLapsHeader Header, SubsessionLap[] Laps)>(headers, (data, sessionLapsList.ToArray()), logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberDivision>> GetMemberDivisionAsync(int seasonId, Common.EventType eventType, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var memberDivisionUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/stats/member_division", new Dictionary<string, string>
        {
            { "season_id", seasonId.ToString(CultureInfo.InvariantCulture) },
            { "event_type", eventType.ToString("D") }
        });
        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(memberDivisionUrl), MemberDivisionContext.Default.MemberDivision, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberYearlyStatistics>> GetMemberYearlyStatisticsAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri("https://members-ng.iracing.com/data/stats/member_yearly"), MemberYearlyStatisticsContext.Default.MemberYearlyStatistics, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SeasonDriverStandingsHeader Header, SeasonDriverStanding[] Standings)>> GetSeasonDriverStandingsAsync(int seasonId, int carClassId, int raceWeekNumber, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var subSessionLapChartUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/stats/season_driver_standings", new Dictionary<string, string>
        {
            { "season_id", seasonId.ToString(CultureInfo.InvariantCulture) },
            { "car_class_id", carClassId.ToString(CultureInfo.InvariantCulture) },
            { "race_week_num", raceWeekNumber.ToString(CultureInfo.InvariantCulture) },
        });

        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(subSessionLapChartUrl), SeasonDriverStandingsHeaderContext.Default.SeasonDriverStandingsHeader, cancellationToken).ConfigureAwait(false);

        var baseChunkUrl = new Uri(data.ChunkInfo.BaseDownloadUrl);
        var sessionLapsList = new List<SeasonDriverStanding>();
        foreach (var (chunkFileName, index) in data.ChunkInfo.ChunkFileNames.Select<string, (string fn, int i)>((fn, i) => (fn, i)))
        {
            var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

            var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
            if (!chunkResponse.IsSuccessStatusCode)
            {
                logger.FailedToRetrieveChunkError(index, data.ChunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                continue;
            }

            var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SeasonDriverStandingArrayContext.Default.SeasonDriverStandingArray, cancellationToken).ConfigureAwait(false);
            if (chunkData is null)
            {
                continue;
            }

            sessionLapsList.AddRange(chunkData);
        }

        return BuildDataResponse<(SeasonDriverStandingsHeader Header, SeasonDriverStanding[] Laps)>(headers, (data, sessionLapsList.ToArray()), logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SeasonQualifyResultsHeader Header, SeasonQualifyResult[] Results)>> GetSeasonQualifyResultsAsync(int seasonId, int carClassId, int raceWeekNumber, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var subSessionLapChartUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/stats/season_driver_standings", new Dictionary<string, string>
        {
            { "season_id", seasonId.ToString(CultureInfo.InvariantCulture) },
            { "car_class_id", carClassId.ToString(CultureInfo.InvariantCulture) },
            { "race_week_num", raceWeekNumber.ToString(CultureInfo.InvariantCulture) },
        });

        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(subSessionLapChartUrl), SeasonQualifyResultsHeaderContext.Default.SeasonQualifyResultsHeader, cancellationToken).ConfigureAwait(false);

        var baseChunkUrl = new Uri(data.ChunkInfo.BaseDownloadUrl);
        var seasonQualifyResults = new List<SeasonQualifyResult>();
        foreach (var (chunkFileName, index) in data.ChunkInfo.ChunkFileNames.Select<string, (string fn, int i)>((fn, i) => (fn, i)))
        {
            var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

            var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
            if (!chunkResponse.IsSuccessStatusCode)
            {
                logger.FailedToRetrieveChunkError(index, data.ChunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                continue;
            }

            var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SeasonQualifyResultArrayContext.Default.SeasonQualifyResultArray, cancellationToken).ConfigureAwait(false);
            if (chunkData is null)
            {
                continue;
            }

            seasonQualifyResults.AddRange(chunkData);
        }

        return BuildDataResponse<(SeasonQualifyResultsHeader Header, SeasonQualifyResult[] Standings)>(headers, (data, seasonQualifyResults.ToArray()), logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SeasonTimeTrialResultsHeader Header, SeasonTimeTrialResult[] Results)>> GetSeasonTimeTrialResultsAsync(int seasonId, int carClassId, int raceWeekNumber, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var subSessionLapChartUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/stats/season_tt_results", new Dictionary<string, string>
        {
            { "season_id", seasonId.ToString(CultureInfo.InvariantCulture) },
            { "car_class_id", carClassId.ToString(CultureInfo.InvariantCulture) },
            { "race_week_num", raceWeekNumber.ToString(CultureInfo.InvariantCulture) },
        });

        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(subSessionLapChartUrl), SeasonTimeTrialResultsHeaderContext.Default.SeasonTimeTrialResultsHeader, cancellationToken).ConfigureAwait(false);

        var baseChunkUrl = new Uri(data.ChunkInfo.BaseDownloadUrl);
        var seasonTimeTrialResults = new List<SeasonTimeTrialResult>();
        foreach (var (chunkFileName, index) in data.ChunkInfo.ChunkFileNames.Select<string, (string fn, int i)>((fn, i) => (fn, i)))
        {
            var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

            var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
            if (!chunkResponse.IsSuccessStatusCode)
            {
                logger.FailedToRetrieveChunkError(index, data.ChunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                continue;
            }

            var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SeasonTimeTrialResultArrayContext.Default.SeasonTimeTrialResultArray, cancellationToken).ConfigureAwait(false);
            if (chunkData is null)
            {
                continue;
            }

            seasonTimeTrialResults.AddRange(chunkData);
        }

        return BuildDataResponse<(SeasonTimeTrialResultsHeader Header, SeasonTimeTrialResult[] Standings)>(headers, (data, seasonTimeTrialResults.ToArray()), logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SeasonTimeTrialStandingsHeader Header, SeasonTimeTrialStanding[] Standings)>> GetSeasonTimeTrialStandingsAsync(int seasonId, int carClassId, int raceWeekNumber, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var subSessionLapChartUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/stats/season_tt_standings", new Dictionary<string, string>
        {
            { "season_id", seasonId.ToString(CultureInfo.InvariantCulture) },
            { "car_class_id", carClassId.ToString(CultureInfo.InvariantCulture) },
            { "race_week_num", raceWeekNumber.ToString(CultureInfo.InvariantCulture) },
        });

        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(subSessionLapChartUrl), SeasonTimeTrialStandingsHeaderContext.Default.SeasonTimeTrialStandingsHeader, cancellationToken).ConfigureAwait(false);

        var baseChunkUrl = new Uri(data.ChunkInfo.BaseDownloadUrl);
        var seasonTimeTrialStandings = new List<SeasonTimeTrialStanding>();
        foreach (var (chunkFileName, index) in data.ChunkInfo.ChunkFileNames.Select((fn, i) => (fn, i)))
        {
            var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

            var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
            if (!chunkResponse.IsSuccessStatusCode)
            {
                logger.FailedToRetrieveChunkError(index, data.ChunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                continue;
            }

            var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SeasonTimeTrialStandingArrayContext.Default.SeasonTimeTrialStandingArray, cancellationToken).ConfigureAwait(false);
            if (chunkData is null)
            {
                continue;
            }

            seasonTimeTrialStandings.AddRange(chunkData);
        }

        return BuildDataResponse<(SeasonTimeTrialStandingsHeader Header, SeasonTimeTrialStanding[] Standings)>(headers, (data, seasonTimeTrialStandings.ToArray()), logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SeasonTeamStandingsHeader Header, SeasonTeamStanding[] Standings)>> GetSeasonTeamStandingsAsync(int seasonId, int carClassId, int raceWeekNumber, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var subSessionLapChartUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/stats/season_team_standings", new Dictionary<string, string>
        {
            { "season_id", seasonId.ToString(CultureInfo.InvariantCulture) },
            { "car_class_id", carClassId.ToString(CultureInfo.InvariantCulture) },
            { "race_week_num", raceWeekNumber.ToString(CultureInfo.InvariantCulture) },
        });

        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(subSessionLapChartUrl), SeasonTeamStandingsHeaderContext.Default.SeasonTeamStandingsHeader, cancellationToken).ConfigureAwait(false);

        var baseChunkUrl = new Uri(data.ChunkInfo.BaseDownloadUrl);
        var seasonTeamStandings = new List<SeasonTeamStanding>();
        foreach (var (chunkFileName, index) in data.ChunkInfo.ChunkFileNames.Select((fn, i) => (fn, i)))
        {
            var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

            var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
            if (!chunkResponse.IsSuccessStatusCode)
            {
                logger.FailedToRetrieveChunkError(index, data.ChunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                continue;
            }

            var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SeasonTeamStandingArrayContext.Default.SeasonTeamStandingArray, cancellationToken).ConfigureAwait(false);
            if (chunkData is null)
            {
                continue;
            }

            seasonTeamStandings.AddRange(chunkData);
        }

        return BuildDataResponse<(SeasonTeamStandingsHeader Header, SeasonTeamStanding[] Standings)>(headers, (data, seasonTeamStandings.ToArray()), logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<SeasonResults>> GetSeasonResultsAsync(int seasonId, Common.EventType eventType, int raceWeekNumber, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var seasonResultsUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/results/season_results", new Dictionary<string, string>
        {
            { "season_id", seasonId.ToString(CultureInfo.InvariantCulture) },
            { "event_type", eventType.ToString("D") },
            { "race_week_num", raceWeekNumber.ToString(CultureInfo.InvariantCulture) }
        });
        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(seasonResultsUrl), SeasonResultsContext.Default.SeasonResults, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<SeasonSeries[]>> GetSeasonsAsync(bool includeSeries, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var seasonSeriesUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/series/seasons", new Dictionary<string, string>
        {
            { "include_series", includeSeries ? "true" : "false" }
        });

        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(seasonSeriesUrl), SeasonSeriesArrayContext.Default.SeasonSeriesArray, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<StatisticsSeries[]>> GetStatisticsSeriesAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri("https://members-ng.iracing.com/data/series/stats_series"), StatisticsSeriesArrayContext.Default.StatisticsSeriesArray, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberCareer>> GetCareerStatisticsAsync(int? customerId = null, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var careerStatisticsUrl = "https://members-ng.iracing.com/data/stats/member_career";
        if (customerId is not null)
        {
            careerStatisticsUrl = QueryHelpers.AddQueryString(careerStatisticsUrl, new Dictionary<string, string>
            {
                { "cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture) }
            });
        }
        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(careerStatisticsUrl), MemberCareerContext.Default.MemberCareer, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberRecentRaces>> GetMemberRecentRacesAsync(int? customerId = null, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var memberRecentRacesUrl = "https://members-ng.iracing.com/data/stats/member_recent_races";
        if (customerId is not null)
        {
            memberRecentRacesUrl = QueryHelpers.AddQueryString(memberRecentRacesUrl, new Dictionary<string, string>
            {
                { "cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture) }
            });
        }

        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(memberRecentRacesUrl), MemberRecentRacesContext.Default.MemberRecentRaces, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberSummary>> GetMemberSummaryAsync(int? customerId = null, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var memberSummaryUrl = "https://members-ng.iracing.com/data/stats/member_summary";
        if (customerId is not null)
        {
            memberSummaryUrl = QueryHelpers.AddQueryString(memberSummaryUrl, new Dictionary<string, string>
            {
                { "cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture) }
            });
        }
        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(memberSummaryUrl), MemberSummaryContext.Default.MemberSummary, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<IReadOnlyDictionary<string, TrackAssets>>> GetTrackAssetsAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var getTrackUrl = "https://members-ng.iracing.com/data/track/assets";
        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(getTrackUrl), TrackAssetsArrayContext.Default.IReadOnlyDictionaryStringTrackAssets, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<Tracks.Track[]>> GetTracksAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var getTrackUrl = "https://members-ng.iracing.com/data/track/get";
        (var headers, var data) = await CreateResponseViaInfoLinkAsync(new Uri(getTrackUrl), TrackArrayContext.Default.TrackArray, cancellationToken).ConfigureAwait(false);
        return BuildDataResponse(headers, data, logger);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(HostedResultsHeader Header, HostedResultItem[] Items)>> SearchHostedResultsAsync(HostedSearchParameters searchParameters, CancellationToken cancellationToken = default)
    {
#if (NET6_0_OR_GREATER)
        ArgumentNullException.ThrowIfNull(searchParameters);
#else
        if (searchParameters is null)
        {
            throw new ArgumentNullException(nameof(searchParameters));
        }
#endif

        if (searchParameters is { StartRangeBegin: null, FinishRangeBegin: null })
        {
            throw new ArgumentException("Must supply one of \"StartRangeBegin\" or \"FinishRangeBegin\"", nameof(searchParameters));
        }

        if (searchParameters is { ParticipantCustomerId: null, HostCustomerId: null, SessionName: null or { Length: 0 } })
        {
            throw new ArgumentException("Must supply one of \"ParticipantCustomerId\", \"HostCustomerId\", or \"SessionName\"", nameof(searchParameters));
        }

        if (ValidateSearchDateRange(searchParameters.StartRangeBegin, searchParameters.StartRangeEnd, nameof(searchParameters), nameof(searchParameters.StartRangeBegin), nameof(searchParameters.StartRangeEnd)) is Exception startRangeEx)
        {
            throw startRangeEx;
        }

        if (ValidateSearchDateRange(searchParameters.FinishRangeBegin, searchParameters.FinishRangeEnd, nameof(searchParameters), nameof(searchParameters.FinishRangeBegin), nameof(searchParameters.FinishRangeEnd)) is Exception finishRangeEx)
        {
            throw finishRangeEx;
        }

        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var queryParams = new Dictionary<string, string>();
        queryParams.AddParameterIfNotNull(() => searchParameters.StartRangeBegin);
        queryParams.AddParameterIfNotNull(() => searchParameters.StartRangeEnd);
        queryParams.AddParameterIfNotNull(() => searchParameters.FinishRangeBegin);
        queryParams.AddParameterIfNotNull(() => searchParameters.FinishRangeEnd);
        queryParams.AddParameterIfNotNull(() => searchParameters.ParticipantCustomerId);
        queryParams.AddParameterIfNotNull(() => searchParameters.HostCustomerId);
        queryParams.AddParameterIfNotNull(() => searchParameters.SessionName);
        queryParams.AddParameterIfNotNull(() => searchParameters.LeagueId);
        queryParams.AddParameterIfNotNull(() => searchParameters.LeagueSeasonId);
        queryParams.AddParameterIfNotNull(() => searchParameters.CarId);
        queryParams.AddParameterIfNotNull(() => searchParameters.TrackId);
        queryParams.AddParameterIfNotNull(() => searchParameters.CategoryIds);

        var searchHostedUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/results/search_hosted", queryParams);

        (var headers, var header) = await GetResponseAsync(new Uri(searchHostedUrl), HostedResultsHeaderContext.Default.HostedResultsHeader, cancellationToken).ConfigureAwait(false);

        var baseChunkUrl = new Uri(header.Data.ChunkInfo.BaseDownloadUrl);
        var searchResults = new List<HostedResultItem>();

        foreach (var (chunkFileName, index) in header.Data.ChunkInfo.ChunkFileNames.Select((fn, i) => (fn, i)))
        {
            var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

            var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
            if (!chunkResponse.IsSuccessStatusCode)
            {
                logger.FailedToRetrieveChunkError(index, header.Data.ChunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                continue;
            }

            var chunkData = await chunkResponse.Content.ReadFromJsonAsync(HostedResultItemContext.Default.HostedResultItemArray, cancellationToken).ConfigureAwait(false);
            if (chunkData is null)
            {
                continue;
            }

            searchResults.AddRange(chunkData);
        }

        return BuildDataResponse<(HostedResultsHeader Header, HostedResultItem[] Results)>(headers, (header, searchResults.ToArray()), logger);
    }

    /// <inheritdoc/>
    public async Task<DataResponse<(OfficialSearchResultHeader Header, OfficialSearchResultItem[] Items)>> SearchOfficialResultsAsync(OfficialSearchParameters searchParameters, CancellationToken cancellationToken = default)
    {
#if (NET6_0_OR_GREATER)
        ArgumentNullException.ThrowIfNull(searchParameters);
#else
        if (searchParameters is null)
        {
            throw new ArgumentNullException(nameof(searchParameters));
        }
#endif

        if ((searchParameters.SeasonYear is null || searchParameters.SeasonQuarter is null)
            && searchParameters.StartRangeBegin is null
            && searchParameters.FinishRangeBegin is null)
        {
            throw new ArgumentException("Must supply one of \"SeasonYear\" and \"SeasonQuarter\", \"StartRangeBegin\", or \"FinishRangeBegin\"", nameof(searchParameters));
        }

        if (searchParameters.SeasonQuarter is not null and (< 1 or > 4))
        {
            throw new ArgumentOutOfRangeException(nameof(searchParameters), searchParameters.SeasonQuarter, "Invalid \"SeasonQuarter\" value. Must be between 1 and 4 (inclusive).");
        }

        if (ValidateSearchDateRange(searchParameters.StartRangeBegin, searchParameters.StartRangeEnd, nameof(searchParameters), nameof(searchParameters.StartRangeBegin), nameof(searchParameters.StartRangeEnd)) is Exception startRangeEx)
        {
            throw startRangeEx;
        }

        if (ValidateSearchDateRange(searchParameters.FinishRangeBegin, searchParameters.FinishRangeEnd, nameof(searchParameters), nameof(searchParameters.FinishRangeBegin), nameof(searchParameters.FinishRangeEnd)) is Exception finishRangeEx)
        {
            throw finishRangeEx;
        }

        if (!IsLoggedIn)
        {
            await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        var queryParams = new Dictionary<string, string>();
        queryParams.AddParameterIfNotNull(() => searchParameters.StartRangeBegin);
        queryParams.AddParameterIfNotNull(() => searchParameters.StartRangeEnd);
        queryParams.AddParameterIfNotNull(() => searchParameters.FinishRangeBegin);
        queryParams.AddParameterIfNotNull(() => searchParameters.FinishRangeEnd);
        queryParams.AddParameterIfNotNull(() => searchParameters.ParticipantCustomerId);
        queryParams.AddParameterIfNotNull(() => searchParameters.SeriesId);
        queryParams.AddParameterIfNotNull(() => searchParameters.RaceWeekIndex);
        queryParams.AddParameterIfNotNull(() => searchParameters.OfficialOnly);
        queryParams.AddParameterIfNotNull(() => searchParameters.EventTypes);
        queryParams.AddParameterIfNotNull(() => searchParameters.CategoryIds);

        var searchHostedUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/results/search_series", queryParams);

        (var headers, var header) = await GetResponseAsync(new Uri(searchHostedUrl), OfficialSearchResultHeaderContext.Default.OfficialSearchResultHeader, cancellationToken).ConfigureAwait(false);

        var baseChunkUrl = new Uri(header.Data.ChunkInfo.BaseDownloadUrl);
        var searchResults = new List<OfficialSearchResultItem>();

        foreach (var (chunkFileName, index) in header.Data.ChunkInfo.ChunkFileNames.Select((fn, i) => (fn, i)))
        {
            var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

            var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
            if (!chunkResponse.IsSuccessStatusCode)
            {
                logger.FailedToRetrieveChunkError(index, header.Data.ChunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                continue;
            }

            var chunkData = await chunkResponse.Content.ReadFromJsonAsync(OfficialSearchResultItemArrayContext.Default.OfficialSearchResultItemArray, cancellationToken).ConfigureAwait(false);
            if (chunkData is null)
            {
                continue;
            }

            searchResults.AddRange(chunkData);
        }

        return BuildDataResponse<(OfficialSearchResultHeader Header, OfficialSearchResultItem[] Results)>(headers, (header, searchResults.ToArray()), logger);
    }

    private static Exception? ValidateSearchDateRange(DateTime? rangeBegin, DateTime? rangeEnd, string paramName, string rangeBeginFieldName, string rangeEndFieldName)
    {
        if (rangeBegin is not null)
        {
            if (rangeBegin.Value > DateTime.UtcNow)
            {
                return new ArgumentOutOfRangeException(paramName, $"Value for \"{rangeBeginFieldName}\" cannot be in the future.");
            }

            if (rangeEnd is null
                && (Math.Abs(DateTime.UtcNow.Subtract(rangeBegin.Value).TotalDays) > 90))
            {
                return new ArgumentOutOfRangeException(paramName, $"Must supply value for \"{rangeEndFieldName}\" if \"{rangeBeginFieldName}\" is more than 90 days in the past.");
            }
        }

        if (rangeEnd is not null)
        {
            if (rangeBegin is not null)
            {
                if (rangeBegin >= rangeEnd)
                {
                    return new ArgumentOutOfRangeException(paramName, $"Value for \"{rangeBeginFieldName}\" cannot be after \"{rangeEndFieldName}\".");
                }

                if (Math.Abs(rangeEnd.Value.Subtract(rangeBegin.Value).TotalDays) > 90)
                {
                    return new ArgumentOutOfRangeException(paramName, $"Value for \"{rangeEndFieldName}\" cannot be more than 90 days after \"{rangeBeginFieldName}\".");
                }
            }
            else
            {
                return new ArgumentException($"Must supply value for \"{rangeBeginFieldName}\" if \"{rangeEndFieldName}\" is specified.", paramName);
            }
        }

        return null;
    }

#pragma warning disable CA1308 // Normalize strings to uppercase - this algorithm requires lower case.
    private async Task LoginInternalAsync(CancellationToken cancellationToken)
    {
        if (options.RestoreCookies is not null
            && options.RestoreCookies() is CookieCollection savedCookies)
        {
            cookieContainer.Add(savedCookies);
        }

        using var sha256 = SHA256.Create();

        var passwordAndEmail = options.Password + (options.Username?.ToLowerInvariant());
        var hashedPasswordAndEmailBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordAndEmail));
        var encodedHash = Convert.ToBase64String(hashedPasswordAndEmailBytes);

        var loginResponse = await httpClient.PostAsJsonAsync("https://members-ng.iracing.com/auth",
                                                             new
                                                             {
                                                                 email = options.Username,
                                                                 password = encodedHash
                                                             },
                                                             cancellationToken)
                                            .ConfigureAwait(false);
        loginResponse.EnsureSuccessStatusCode();
        IsLoggedIn = true;
        logger.LoginSuccessful(options.Username!);

        if (options.SaveCookies is Action<CookieCollection> saveCredentials)
        {
            saveCredentials(cookieContainer.GetAllCookies());
        }
    }
#pragma warning restore CA1308 // Normalize strings to uppercase

    private async Task<(HttpResponseHeaders Headers, TData Data)> CreateResponseViaInfoLinkAsync<TData>(Uri infoLinkUri, JsonTypeInfo<TData> jsonTypeInfo, CancellationToken cancellationToken)
    {
        var infoLinkResponse = await httpClient.GetAsync(infoLinkUri, cancellationToken).ConfigureAwait(false);
        if (!infoLinkResponse.IsSuccessStatusCode)
        {
            await HandleUnsuccessfulResponseAsync(infoLinkResponse, logger).ConfigureAwait(false);
        }

        var infoLink = await infoLinkResponse.Content.ReadFromJsonAsync(LinkResultContext.Default.LinkResult, cancellationToken)
                                                     .ConfigureAwait(false);
        if (infoLink?.Link is null)
        {
            throw new iRacingDataClientException("Unrecognised result.");
        }

        var data = await httpClient.GetFromJsonAsync(infoLink.Link, jsonTypeInfo, cancellationToken: cancellationToken)
                                   .ConfigureAwait(false);
        if (data is null)
        {
            throw new iRacingDataClientException("Data not found.");
        }

        return (infoLinkResponse.Headers, data);
    }

    private async Task<(HttpResponseHeaders Headers, TData Data)> GetResponseAsync<TData>(Uri iRacingUri, JsonTypeInfo<TData> jsonTypeInfo, CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync(iRacingUri, cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            await HandleUnsuccessfulResponseAsync(response, logger).ConfigureAwait(false);
        }

        var data = await response.Content.ReadFromJsonAsync(jsonTypeInfo, cancellationToken: cancellationToken)
                                         .ConfigureAwait(false);
        if (data is null)
        {
            throw new iRacingDataClientException("Data not found.");
        }

        return (response.Headers, data);
    }

    private async static Task HandleUnsuccessfulResponseAsync(HttpResponseMessage httpResponse, ILogger logger)
    {
        var errorResponse = await httpResponse.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: CancellationToken.None)
                                                  .ConfigureAwait(false);

        Exception? exception = errorResponse switch
        {
            { ErrorCode: "Site Maintenance" } => new iRacingInMaintenancePeriodException(errorResponse.ErrorDescription ?? "iRacing services are down for maintenance."),
            { ErrorCode: "Forbidden" } => iRacingForbiddenResponseException.Create(),
            _ => null
        };

        if (exception is null)
        {
            logger.ErrorResponseUnknown();
            httpResponse.EnsureSuccessStatusCode();
        }
        else
        {
            logger.ErrorResponse(errorResponse!.ErrorDescription, exception);
            throw exception;
        }
    }

    private static DataResponse<TData> BuildDataResponse<TData>(HttpResponseHeaders headers, TData data, ILogger logger)
    {
        var response = new DataResponse<TData> { Data = data };

        if (headers.TryGetValues("x-ratelimit-remaining", out var remainingValues)
            && remainingValues.Any()
            && int.TryParse(remainingValues.First(), out var remaining))
        {
            response.RateLimitRemaining = remaining;
        }

        if (headers.TryGetValues("x-ratelimit-limit", out var limitValues)
            && limitValues.Any()
            && int.TryParse(limitValues.First(), out var limit))
        {
            response.TotalRateLimit = limit;
        }

        if (headers.TryGetValues("x-ratelimit-reset", out var resetValues)
            && resetValues.Any()
            && long.TryParse(resetValues.First(), out var resetTimeUnixSeconds))
        {
            response.RateLimitReset = DateTimeOffset.FromUnixTimeSeconds(resetTimeUnixSeconds);
        }

        logger.RateLimitsUpdated(response.RateLimitRemaining, response.TotalRateLimit, response.RateLimitReset);

        return response;
    }
}
