// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using System.Globalization;
using Aydsko.iRacingData.Cars;
using Aydsko.iRacingData.Constants;
using Aydsko.iRacingData.Exceptions;
using Aydsko.iRacingData.Hosted;
using Aydsko.iRacingData.Leagues;
using Aydsko.iRacingData.Lookups;
using Aydsko.iRacingData.Member;
using Aydsko.iRacingData.Results;
using Aydsko.iRacingData.Searches;
using Aydsko.iRacingData.Series;
using Aydsko.iRacingData.Stats;
using Aydsko.iRacingData.TimeAttack;
using Aydsko.iRacingData.Tracks;

namespace Aydsko.iRacingData;

/// <summary>Default implementation of the client to access the iRacing "/data" API endpoints.</summary>
/// <remarks>
/// Instead of creating an instance of this class directly, it is recommended that you register the library components in the services
/// collection using <see cref="ServicesExtensions.AddIRacingDataApi(Microsoft.Extensions.DependencyInjection.IServiceCollection)"/>
/// and resolve <see cref="IDataClient"/> service from there.
/// </remarks>
public class DataClient(ApiClientBase apiClient,
                        iRacingDataClientOptions options,
                        ILogger<DataClient> logger)
    : IDataClient
{
    /// <inheritdoc/>
    [Obsolete("Configure via the \"AddIRacingDataApi\" extension method on the IServiceCollection which allows you to configure the \"iRacingDataClientOptions\".")]
    public void UseUsernameAndPassword(string username, string password, bool passwordIsEncoded)
    {
        apiClient.UseUsernameAndPassword(username, password, passwordIsEncoded);
    }

    /// <inheritdoc/>
    [Obsolete("Configure via the \"AddIRacingDataApi\" extension method on the IServiceCollection which allows you to configure the \"iRacingDataClientOptions\".")]
    public void UseUsernameAndPassword(string username, string password)
    {
        UseUsernameAndPassword(username, password, false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<IReadOnlyDictionary<string, CarAssetDetail>>> GetCarAssetDetailsAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Car Asset Details");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Car Asset Details");

        var carAssetDetailsUrl = new Uri("https://members-ng.iracing.com/data/car/assets");

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(carAssetDetailsUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                CarAssetDetailDictionaryContext.Default.IReadOnlyDictionaryStringCarAssetDetail,
                                                                                cancellationToken).ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<Cars.CarInfo[]>> GetCarsAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Cars");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Cars");

        var infoLinkUri = new Uri("https://members-ng.iracing.com/data/car/get");

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(infoLinkUri,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                CarInfoArrayContext.Default.CarInfoArray,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<Common.CarClass[]>> GetCarClassesAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Car Classes");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Car Classes");

        var carClassUrl = new Uri("https://members-ng.iracing.com/data/carclass/get");

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(carClassUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                CarClassArrayContext.Default.CarClassArray,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<Division[]>> GetDivisionsAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Divisions");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Divisions");

        var constantsDivisionsUrl = new Uri("https://members-ng.iracing.com/data/constants/divisions");
        return await apiClient.GetDataResponseAsync(constantsDivisionsUrl,
                                                   DivisionArrayContext.Default.DivisionArray,
                                                   cancellationToken)
                              .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<Category[]>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Categories");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Categories");

        var constantsCategoriesUrl = new Uri("https://members-ng.iracing.com/data/constants/categories");
        return await apiClient.GetDataResponseAsync(constantsCategoriesUrl,
                                                   CategoryArrayContext.Default.CategoryArray,
                                                   cancellationToken)
                              .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<Constants.EventType[]>> GetEventTypesAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Event Types");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Event Types");

        var constantsEventTypesUrl = new Uri("https://members-ng.iracing.com/data/constants/event_types");

        return await apiClient.GetDataResponseAsync(constantsEventTypesUrl,
                                                   EventTypeArrayContext.Default.EventTypeArray,
                                                   cancellationToken)
                              .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<CombinedSessionsResult>> ListHostedSessionsCombinedAsync(int? packageId = null, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("List Hosted Sessions Combined");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("List Hosted Sessions Combined");

        var queryParameters = new Dictionary<string, object?>();

        if (packageId is not null)
        {
            queryParameters.Add("package_id", packageId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var queryUrl = "https://members-ng.iracing.com/data/hosted/combined_sessions".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(queryUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                CombinedSessionsResultContext.Default.CombinedSessionsResult,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<HostedSessionsResult>> ListHostedSessionsAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("List Hosted Sessions");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("List Hosted Sessions");

        var infoLinkUri = new Uri("https://members-ng.iracing.com/data/hosted/sessions");

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(infoLinkUri,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                HostedSessionsResultContext.Default.HostedSessionsResult,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<League>> GetLeagueAsync(int leagueId, bool includeLicenses = false, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get League {LeagueId}", leagueId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get League")?.AddTag("LeagueId", leagueId);

        var queryParameters = new Dictionary<string, object?>
        {
            ["league_id"] = leagueId.ToString(CultureInfo.InvariantCulture),
            ["include_licenses"] = includeLicenses.ToString(),
        };

        var getLeagueUrl = "https://members-ng.iracing.com/data/league/get".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(getLeagueUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                LeagueContext.Default.League,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<LeaguePointsSystems>> GetLeaguePointsSystemsAsync(int leagueId, int? seasonId = null, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get League Points Systems for {LeagueId} and {SeasonId}", leagueId, seasonId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get League Points Systems")?.AddTag("LeagueId", leagueId);

        var queryParameters = new Dictionary<string, object?>
        {
            ["league_id"] = leagueId.ToString(CultureInfo.InvariantCulture),
        };

        if (seasonId is not null)
        {
            queryParameters.Add("season_id", seasonId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var queryUrl = "https://members-ng.iracing.com/data/league/get_points_systems".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(queryUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                LeaguePointsSystemsContext.Default.LeaguePointsSystems,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<CustomerLeagueSessions>> GetCustomerLeagueSessionsAsync(bool mine = false, int? packageId = null, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Customer League Sessions");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Customer League Sessions");

        var queryParameters = new Dictionary<string, object?>
        {
            ["mine"] = mine,
            ["package_id"] = packageId,
        };

        var queryUrl = "https://members-ng.iracing.com/data/league/cust_league_sessions".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(queryUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                CustomerLeagueSessionsContext.Default.CustomerLeagueSessions,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<LookupGroup[]>> GetLookupsAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Lookups");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Lookups");

        var lookupsUrl = new Uri("https://members-ng.iracing.com/data/lookup/get?weather=weather_wind_speed_units&weather=weather_wind_speed_max&weather=weather_wind_speed_min&licenselevels=licenselevels");

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(lookupsUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                LookupGroupArrayContext.Default.LookupGroupArray,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<DriverSearchResult[]>> SearchDriversAsync(string searchTerm, int? leagueId = null, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Search Drivers for {SearchTerm} in League {LeagueId}", searchTerm, leagueId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Search Drivers")
                                ?.AddTag("SearchTerm", searchTerm)
                                ?.AddTag("LeagueId", leagueId);

        var queryParameters = new Dictionary<string, object?>
        {
            ["search_term"] = searchTerm
        };

        if (leagueId is not null)
        {
            queryParameters.Add("league_id", leagueId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var queryUrl = "https://members-ng.iracing.com/data/lookup/drivers".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(queryUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                DriverSearchResultContext.Default.DriverSearchResultArray,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<LicenseLookup[]>> GetLicenseLookupsAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get License Lookups");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get License Lookups");

        var infoLinkUri = new Uri("https://members-ng.iracing.com/data/lookup/licenses");

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(infoLinkUri,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                LicenseLookupArrayContext.Default.LicenseLookupArray,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);
        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<FlairLookupResponse>> GetFlairsAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Flairs");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Flairs");

        var infoLinkUri = new Uri("https://members-ng.iracing.com/data/lookup/flairs");

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(infoLinkUri,
                                                              LinkResultContext.Default.LinkResult,
                                                              infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                              FlairLookupResponseContext.Default.FlairLookupResponse,
                                                              cancellationToken).ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<DriverInfo[]>> GetDriverInfoAsync(int[] customerIds, bool includeLicenses, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Driver Info for Customers {CustomerIds} {IncludeLicenses}", customerIds, includeLicenses);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Driver Info")
                                ?.AddTag("CustomerIds", customerIds)
                                ?.AddTag("IncludeLicenses", includeLicenses);

        if (customerIds is not { Length: > 0 })
        {
            throw new ArgumentOutOfRangeException(nameof(customerIds), "Must supply at least one customer identifier value to query.");
        }

        var queryParameters = new Dictionary<string, object?>
        {
            ["cust_ids"] = customerIds,
            ["include_licenses"] = includeLicenses,
        };

        var driverInfoRequestUrl = "https://members-ng.iracing.com/data/member/get".ToUrlWithQuery(queryParameters);

        var driverInfoResponse = await apiClient.CreateResponseViaIntermediateResultAsync(driverInfoRequestUrl,
                                                                                          LinkResultContext.Default.LinkResult,
                                                                                          infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                          DriverInfoResponseContext.Default.DriverInfoResponse,
                                                                                          cancellationToken)
                                                .ConfigureAwait(false);

        return new DataResponse<DriverInfo[]>
        {
            Data = driverInfoResponse.Data.Drivers,
            DataExpires = driverInfoResponse.DataExpires,
            RateLimitRemaining = driverInfoResponse.RateLimitRemaining,
            RateLimitReset = driverInfoResponse.RateLimitReset,
            TotalRateLimit = driverInfoResponse.TotalRateLimit
        };
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberAward[]>> GetDriverAwardsAsync(int? customerId = null, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Driver Awards for Customer {CustomerId}", customerId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Driver Awards")
                                ?.AddTag("CustomerId", customerId);

        var queryParameters = new Dictionary<string, object?>();

        if (customerId is not null)
        {
            queryParameters.Add("cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var queryUrl = "https://members-ng.iracing.com/data/member/awards".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(queryUrl,
                                                                                MemberAwardResultContext.Default.MemberAwardResult,
                                                                                memberAwardResult => (new Uri(memberAwardResult.DataUrl), null),
                                                                                MemberAwardArrayContext.Default.MemberAwardArray,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);
        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberAwardInstance>> GetDriverAwardInstanceAsync(int awardId, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Driver Award Instance for {AwardId}", awardId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Driver Award Instance")
                                ?.AddTag("AwardId", awardId);

        var queryParameters = new Dictionary<string, object?>()
        {
            ["award_id"] = awardId,
        };

        var queryUrl = "https://members-ng.iracing.com/data/member/award_instances".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(queryUrl,
                                                                                DataUrlResultContext.Default.DataUrlResult,
                                                                                dataUrlResult => (new Uri(dataUrlResult.DataUrl), null),
                                                                                MemberAwardInstanceContext.Default.MemberAwardInstance,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<Member.MemberInfo>> GetMyInfoAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get My Info");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get My Info");

        var memberInfoUrl = new Uri("https://members-ng.iracing.com/data/member/info");

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(memberInfoUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                MemberInfoContext.Default.MemberInfo,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberProfile>> GetMemberProfileAsync(int? customerId = null, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Member Profile for {CustomerId}", customerId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Member Profile")
                                ?.AddTag("CustomerId", customerId);

        var queryParameters = new Dictionary<string, object?>();

        if (customerId is not null)
        {
            queryParameters.Add("cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var memberProfileUrl = "https://members-ng.iracing.com/data/member/profile".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(memberProfileUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                MemberProfileContext.Default.MemberProfile,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<SubSessionResult>> GetSubSessionResultAsync(int subSessionId, bool includeLicenses, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get SubSession Result for {SubSessionId} {IncludeLicenses}", subSessionId, includeLicenses);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get SubSession Result")
                                ?.AddTag("SubSessionId", subSessionId)
                                ?.AddTag("IncludeLicenses", includeLicenses);

        var queryParameters = new Dictionary<string, object?>
        {
            ["subsession_id"] = subSessionId,
            ["include_licenses"] = includeLicenses,
        };

        var subSessionResultUrl = "https://members-ng.iracing.com/data/results/get".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(subSessionResultUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                SubSessionResultContext.Default.SubSessionResult,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SubsessionLapsHeader Header, SubsessionChartLap[] Laps)>> GetSubSessionLapChartAsync(int subSessionId,
                                                                                                                         int simSessionNumber,
                                                                                                                         CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get SubSession Lap Chart for {SubSessionId} session {SimSessionNumber}", subSessionId, simSessionNumber);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get SubSession Lap Chart")
                                ?.AddTag("SubSessionId", subSessionId)
                                ?.AddTag("SimSessionNumber", simSessionNumber);

        var queryParameters = new Dictionary<string, object?>
        {
            ["subsession_id"] = subSessionId.ToString(CultureInfo.InvariantCulture),
            ["simsession_number"] = simSessionNumber.ToString(CultureInfo.InvariantCulture),
        };

        var subSessionLapChartUrl = "https://members-ng.iracing.com/data/results/lap_chart_data".ToUrlWithQuery(queryParameters);

        var results = await apiClient.CreateResponseFromChunksAsync(subSessionLapChartUrl,
                                                                                true,
                                                                                SubsessionLapsHeaderContext.Default.SubsessionLapsHeader,
                                                                                intermediateResult => intermediateResult.ChunkInfo,
                                                                                SubsessionChartLapArrayContext.Default.SubsessionChartLapArray,
                                                                                cancellationToken)
                                     .ConfigureAwait(false);
        return results;
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SubsessionEventLogHeader Header, SubsessionEventLogItem[] LogItems)>> GetSubsessionEventLogAsync(int subSessionId,
                                                                                                                                     int simSessionNumber,
                                                                                                                                     CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Subsession Event Log for {SubSessionId} session {SimSessionNumber}", subSessionId, simSessionNumber);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Subsession Event Log")
                               ?.AddTag("SubSessionId", subSessionId)
                               ?.AddTag("SimSessionNumber", simSessionNumber);

        var queryParameters = new Dictionary<string, object?>
        {
            ["subsession_id"] = subSessionId,
            ["simsession_number"] = simSessionNumber,
        };

        var subSessionLapChartUrl = "https://members-ng.iracing.com/data/results/event_log".ToUrlWithQuery(queryParameters);

        var results = await apiClient.CreateResponseFromChunksAsync(subSessionLapChartUrl,
                                                                                true,
                                                                                SubsessionEventLogHeaderContext.Default.SubsessionEventLogHeader,
                                                                                intermediateResult => intermediateResult.ChunkInfo,
                                                                                SubsessionEventLogItemArrayContext.Default.SubsessionEventLogItemArray,
                                                                                cancellationToken)
                                     .ConfigureAwait(false);
        return results;
    }

    /// <inheritdoc />
    public async Task<DataResponse<SeriesDetail[]>> GetSeriesAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Series");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Series");

        var seriesDetailUrl = new Uri("https://members-ng.iracing.com/data/series/get");

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(seriesDetailUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                SeriesDetailArrayContext.Default.SeriesDetailArray,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<IReadOnlyDictionary<string, SeriesAsset>>> GetSeriesAssetsAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Series Assets");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Series Assets");

        var infoLinkUri = new Uri("https://members-ng.iracing.com/data/series/assets");

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(infoLinkUri,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                SeriesAssetReadOnlyDictionaryContext.Default.IReadOnlyDictionaryStringSeriesAsset,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SubsessionLapsHeader Header, SubsessionLap[] Laps)>> GetSingleDriverSubsessionLapsAsync(int subSessionId, int simSessionNumber, int customerId, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Single Driver Subsession Laps for {CustomerId} driving in {SubSessionId} session {SimSessionNumber}", customerId, subSessionId, simSessionNumber);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Single Driver Subsession Laps")
                                           ?.AddTag("SubSessionId", subSessionId)
                                           ?.AddTag("SimSessionNumber", simSessionNumber)
                                           ?.AddTag("CustomerId", customerId);

        var queryParameters = new Dictionary<string, object?>
        {
            ["subsession_id"] = subSessionId,
            ["simsession_number"] = simSessionNumber,
            ["cust_id"] = customerId,
        };

        var subSessionLapChartUrl = "https://members-ng.iracing.com/data/results/lap_data".ToUrlWithQuery(queryParameters);

        var results = await apiClient.CreateResponseFromChunksAsync(subSessionLapChartUrl,
                                                                                true,
                                                                                SubsessionLapsHeaderContext.Default.SubsessionLapsHeader,
                                                                                intermediateResult => intermediateResult.ChunkInfo,
                                                                                SubsessionLapArrayContext.Default.SubsessionLapArray,
                                                                                cancellationToken)
                                     .ConfigureAwait(false);
        return results;
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SubsessionLapsHeader Header, SubsessionLap[] Laps)>> GetTeamSubsessionLapsAsync(int subSessionId, int simSessionNumber, int teamId, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Team Subsession Laps for {TeamId} driving in {SubSessionId} session {SimSessionNumber}", teamId, subSessionId, simSessionNumber);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Team Subsession Laps")
                                           ?.AddTag("SubSessionId", subSessionId)
                                           ?.AddTag("SimSessionNumber", simSessionNumber)
                                           ?.AddTag("TeamId", teamId);

        var queryParameters = new Dictionary<string, object?>
        {
            ["subsession_id"] = subSessionId,
            ["simsession_number"] = simSessionNumber,
            ["team_id"] = teamId,
        };

        var subSessionLapChartUrl = "https://members-ng.iracing.com/data/results/lap_data".ToUrlWithQuery(queryParameters);

        var results = await apiClient.CreateResponseFromChunksAsync(subSessionLapChartUrl,
                                                                                true,
                                                                                SubsessionLapsHeaderContext.Default.SubsessionLapsHeader,
                                                                                intermediateResult => intermediateResult.ChunkInfo,
                                                                                SubsessionLapArrayContext.Default.SubsessionLapArray,
                                                                                cancellationToken)
                                     .ConfigureAwait(false);
        return results;
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberDivision>> GetMemberDivisionAsync(int seasonId, Common.EventType eventType, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Member Division for {SeasonId} in {EventType}", seasonId, eventType);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Member Division")
                                           ?.AddTag("SeasonId", seasonId)
                                           ?.AddTag("EventType", eventType);

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_id"] = seasonId,
            ["event_type"] = eventType.ToString("D"),
        };

        var memberDivisionUrl = "https://members-ng.iracing.com/data/stats/member_division".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(memberDivisionUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                MemberDivisionContext.Default.MemberDivision,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberYearlyStatistics>> GetMemberYearlyStatisticsAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Member Yearly Statistics");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Member Yearly Statistics");

        var infoLinkUri = new Uri("https://members-ng.iracing.com/data/stats/member_yearly");

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(infoLinkUri,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                MemberYearlyStatisticsContext.Default.MemberYearlyStatistics,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberChart>> GetMemberChartDataAsync(int? customerId, int categoryId, MemberChartType chartType, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Member Chart Data for {CustomerId} in {CategoryId} of type {ChartType}", customerId, categoryId, chartType);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Member Chart Data")
                                ?.AddTag("CustomerId", customerId)
                                ?.AddTag("CategoryId", categoryId)
                                ?.AddTag("ChartType", chartType);

        var parameters = new Dictionary<string, object?>
        {
            ["category_id"] = categoryId,
            ["chart_type"] = chartType,
            ["cust_id"] = customerId
        };

        var memberChartUrl = "https://members-ng.iracing.com/data/member/chart_data".ToUrlWithQuery(parameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(memberChartUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                MemberChartContext.Default.MemberChart,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<(WorldRecordsHeader Header, WorldRecordEntry[] Entries)>> GetWorldRecordsAsync(int carId,
                                                                                                                  int trackId,
                                                                                                                  int? seasonYear = null,
                                                                                                                  int? seasonQuarter = null,
                                                                                                                  CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get World Records for car {CarId} on track {TrackId} season {SeasonYear} {SeasonQuarter}", carId, trackId, seasonYear, seasonQuarter);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get World Records")
                               ?.AddTag("CarId", carId)
                               ?.AddTag("TrackId", trackId)
                               ?.AddTag("SeasonYear", seasonYear)
                               ?.AddTag("SeasonQuarter", seasonQuarter);

        var queryParameters = new Dictionary<string, object?>
        {
            ["car_id"] = carId.ToString(CultureInfo.InvariantCulture),
            ["track_id"] = trackId.ToString(CultureInfo.InvariantCulture),
        };

        if (seasonYear is not null)
        {
            queryParameters.Add("season_year", seasonYear.Value.ToString(CultureInfo.InvariantCulture));
        }

        if (seasonQuarter is not null)
        {
            if (seasonYear is null)
            {
                throw new ArgumentException($"Must supply a value for \"{nameof(seasonYear)}\" to use \"{nameof(seasonQuarter)}\".", nameof(seasonQuarter));
            }
            queryParameters.Add("season_quarter", seasonQuarter.Value.ToString(CultureInfo.InvariantCulture));
        }

        var queryUrl = "https://members-ng.iracing.com/data/stats/world_records".ToUrlWithQuery(queryParameters);

        var result = await apiClient.CreateResponseFromChunksAsync(queryUrl,
                                                                               true,
                                                                               WorldRecordsHeaderContext.Default.WorldRecordsHeader,
                                                                               header => header.Data.ChunkInfo,
                                                                               WorldRecordEntryArrayContext.Default.WorldRecordEntryArray,
                                                                               cancellationToken)
                                    .ConfigureAwait(false);
        return result;
    }

    /// <inheritdoc />
    public async Task<DataResponse<TeamInfo>> GetTeamAsync(int teamId, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Team {TeamId}", teamId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Team")
                                ?.AddTag("TeamId", teamId);

        var queryParameters = new Dictionary<string, object?>
        {
            ["team_id"] = teamId.ToString(CultureInfo.InvariantCulture),
        };

        var queryUrl = "https://members-ng.iracing.com/data/team/get".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(queryUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                TeamInfoContext.Default.TeamInfo,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SeasonDriverStandingsHeader Header, SeasonDriverStanding[] Standings)>> GetSeasonDriverStandingsAsync(int seasonId,
                                                                                                                                          int carClassId,
                                                                                                                                          int? raceWeekIndex = null,
                                                                                                                                          int? division = null,
                                                                                                                                          CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Season Driver Standings for {SeasonId} car class {CarClassId} {RaceWeekIndex} {Division}", seasonId, carClassId, raceWeekIndex, division);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Season Driver Standings")
                               ?.AddTag("SeasonId", seasonId)
                               ?.AddTag("CarClassId", carClassId)
                               ?.AddTag("RaceWeekIndex", raceWeekIndex)
                               ?.AddTag("Division", division);

#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(seasonId);
        ArgumentOutOfRangeException.ThrowIfNegative(carClassId);
#else
        if (seasonId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(seasonId));
        }

        if (carClassId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(carClassId));
        }
#endif

        if (raceWeekIndex is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(raceWeekIndex));
        }

        if (division is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(division));
        }

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_id"] = seasonId.ToString(CultureInfo.InvariantCulture),
            ["car_class_id"] = carClassId.ToString(CultureInfo.InvariantCulture),
            ["race_week_num"] = (raceWeekIndex ?? -1).ToString(CultureInfo.InvariantCulture),
            ["division"] = (division ?? -1).ToString(CultureInfo.InvariantCulture),
        };

        var seasonDriverStandingsUrl = "https://members-ng.iracing.com/data/stats/season_driver_standings".ToUrlWithQuery(queryParameters);

        var results = await apiClient.CreateResponseFromChunksAsync(seasonDriverStandingsUrl,
                                                                                true,
                                                                                SeasonDriverStandingsHeaderContext.Default.SeasonDriverStandingsHeader,
                                                                                header => header.ChunkInfo,
                                                                                SeasonDriverStandingArrayContext.Default.SeasonDriverStandingArray,
                                                                                cancellationToken)
                                     .ConfigureAwait(false);
        return results;
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SeasonQualifyResultsHeader Header, SeasonQualifyResult[] Results)>> GetSeasonQualifyResultsAsync(int seasonId,
                                                                                                                                     int carClassId,
                                                                                                                                     int? raceWeekIndex = null,
                                                                                                                                     int? division = null,
                                                                                                                                     CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Season Qualify Results for {SeasonId} car class {CarClassId} {RaceWeekIndex} {Division}", seasonId, carClassId, raceWeekIndex, division);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Season Qualify Results")
                               ?.AddTag("SeasonId", seasonId)
                               ?.AddTag("CarClassId", carClassId)
                               ?.AddTag("RaceWeekIndex", raceWeekIndex)
                               ?.AddTag("Division", division);

#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(seasonId);
        ArgumentOutOfRangeException.ThrowIfNegative(carClassId);
#else
        if (seasonId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(seasonId));
        }

        if (carClassId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(carClassId));
        }
#endif

        if (raceWeekIndex is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(raceWeekIndex));
        }

        if (division is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(division));
        }

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_id"] = seasonId.ToString(CultureInfo.InvariantCulture),
            ["car_class_id"] = carClassId.ToString(CultureInfo.InvariantCulture),
            ["race_week_num"] = (raceWeekIndex ?? -1).ToString(CultureInfo.InvariantCulture),
            ["division"] = (division ?? -1).ToString(CultureInfo.InvariantCulture),
        };

        var qualifyResultsUrl = "https://members-ng.iracing.com/data/stats/season_qualify_results".ToUrlWithQuery(queryParameters);

        var results = await apiClient.CreateResponseFromChunksAsync(qualifyResultsUrl,
                                                                                true,
                                                                                SeasonQualifyResultsHeaderContext.Default.SeasonQualifyResultsHeader,
                                                                                header => header.ChunkInfo,
                                                                                SeasonQualifyResultArrayContext.Default.SeasonQualifyResultArray,
                                                                                cancellationToken)
                                     .ConfigureAwait(false);
        return results;
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SeasonTimeTrialResultsHeader Header, SeasonTimeTrialResult[] Results)>> GetSeasonTimeTrialResultsAsync(int seasonId,
                                                                                                                                           int carClassId,
                                                                                                                                           int? raceWeekIndex = null,
                                                                                                                                           int? division = null,
                                                                                                                                           CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Season Time Trial Results for {SeasonId} car class {CarClassId} {RaceWeekIndex} {Division}", seasonId, carClassId, raceWeekIndex, division);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Season Time Trial Results")
                               ?.AddTag("SeasonId", seasonId)
                               ?.AddTag("CarClassId", carClassId)
                               ?.AddTag("RaceWeekIndex", raceWeekIndex)
                               ?.AddTag("Division", division);

#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(seasonId);
        ArgumentOutOfRangeException.ThrowIfNegative(carClassId);
#else
        if (seasonId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(seasonId));
        }

        if (carClassId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(carClassId));
        }
#endif

        if (raceWeekIndex is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(raceWeekIndex));
        }

        if (division is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(division));
        }

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_id"] = seasonId.ToString(CultureInfo.InvariantCulture),
            ["car_class_id"] = carClassId.ToString(CultureInfo.InvariantCulture),
            ["race_week_num"] = (raceWeekIndex ?? -1).ToString(CultureInfo.InvariantCulture),
            ["division"] = (division ?? -1).ToString(CultureInfo.InvariantCulture),
        };

        var subSessionLapChartUrl = "https://members-ng.iracing.com/data/stats/season_tt_results".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseFromChunksAsync(subSessionLapChartUrl,
                                                                                 true,
                                                                                 SeasonTimeTrialResultsHeaderContext.Default.SeasonTimeTrialResultsHeader,
                                                                                 header => header.ChunkInfo,
                                                                                 SeasonTimeTrialResultArrayContext.Default.SeasonTimeTrialResultArray,
                                                                                 cancellationToken)
                                      .ConfigureAwait(false);
        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SeasonTimeTrialStandingsHeader Header, SeasonTimeTrialStanding[] Standings)>> GetSeasonTimeTrialStandingsAsync(int seasonId,
                                                                                                                                                   int carClassId,
                                                                                                                                                   int? raceWeekIndex = null,
                                                                                                                                                   int? division = null,
                                                                                                                                                   CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Season Time Trial Standings for {SeasonId} car class {CarClassId} {RaceWeekIndex} {Division}", seasonId, carClassId, raceWeekIndex, division);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Season Time Trial Standings")
                               ?.AddTag("SeasonId", seasonId)
                               ?.AddTag("CarClassId", carClassId)
                               ?.AddTag("RaceWeekIndex", raceWeekIndex)
                               ?.AddTag("Division", division);

#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(seasonId);
        ArgumentOutOfRangeException.ThrowIfNegative(carClassId);
#else
        if (seasonId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(seasonId));
        }

        if (carClassId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(carClassId));
        }
#endif

        if (raceWeekIndex is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(raceWeekIndex));
        }

        if (division is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(division));
        }

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_id"] = seasonId.ToString(CultureInfo.InvariantCulture),
            ["car_class_id"] = carClassId.ToString(CultureInfo.InvariantCulture),
            ["race_week_num"] = (raceWeekIndex ?? -1).ToString(CultureInfo.InvariantCulture),
            ["division"] = (division ?? -1).ToString(CultureInfo.InvariantCulture),
        };

        var subSessionLapChartUrl = "https://members-ng.iracing.com/data/stats/season_tt_standings".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseFromChunksAsync(subSessionLapChartUrl,
                                                                     true,
                                                                     SeasonTimeTrialStandingsHeaderContext.Default.SeasonTimeTrialStandingsHeader,
                                                                     header => header.ChunkInfo,
                                                                     SeasonTimeTrialStandingArrayContext.Default.SeasonTimeTrialStandingArray,
                                                                     cancellationToken)
                                      .ConfigureAwait(false);
        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SeasonTeamStandingsHeader Header, SeasonTeamStanding[] Standings)>> GetSeasonTeamStandingsAsync(int seasonId,
                                                                                                                                    int carClassId,
                                                                                                                                    int? raceWeekIndex = null,
                                                                                                                                    CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Season Team Standings for {SeasonId} car class {CarClassId} {RaceWeekIndex}", seasonId, carClassId, raceWeekIndex);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Season Team Standings")
                               ?.AddTag("SeasonId", seasonId)
                               ?.AddTag("CarClassId", carClassId)
                               ?.AddTag("RaceWeekIndex", raceWeekIndex);

#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(seasonId);
        ArgumentOutOfRangeException.ThrowIfNegative(carClassId);
#else
        if (seasonId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(seasonId));
        }

        if (carClassId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(carClassId));
        }
#endif

        if (raceWeekIndex is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(raceWeekIndex));
        }

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_id"] = seasonId.ToString(CultureInfo.InvariantCulture),
            ["car_class_id"] = carClassId.ToString(CultureInfo.InvariantCulture),
            ["race_week_num"] = (raceWeekIndex ?? -1).ToString(CultureInfo.InvariantCulture),
        };

        var subSessionLapChartUrl = "https://members-ng.iracing.com/data/stats/season_team_standings".ToUrlWithQuery(queryParameters);

        var results = await apiClient.CreateResponseFromChunksAsync(subSessionLapChartUrl,
                                                                                true,
                                                                                SeasonTeamStandingsHeaderContext.Default.SeasonTeamStandingsHeader,
                                                                                header => header.ChunkInfo,
                                                                                SeasonTeamStandingArrayContext.Default.SeasonTeamStandingArray,
                                                                                cancellationToken)
                                     .ConfigureAwait(false);
        return results;
    }

    /// <inheritdoc />
    public async Task<DataResponse<SeasonResults>> GetSeasonResultsAsync(int seasonId, Common.EventType eventType, int raceWeekNumber, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Season Results for {SeasonId} in {EventType} for week {RaceWeekNumber}", seasonId, eventType, raceWeekNumber);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Season Results")
                               ?.AddTag("SeasonId", seasonId)
                               ?.AddTag("EventType", eventType)
                               ?.AddTag("RaceWeekNumber", raceWeekNumber);

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_id"] = seasonId,
            ["event_type"] = eventType,
            ["race_week_num"] = raceWeekNumber,
        };

        var seasonResultsUrl = "https://members-ng.iracing.com/data/results/season_results".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(seasonResultsUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                SeasonResultsContext.Default.SeasonResults,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<SeasonSeries[]>> GetSeasonsAsync(bool includeSeries, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Seasons");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Seasons");

        var queryParameters = new Dictionary<string, object?>
        {
            ["include_series"] = includeSeries ? "true" : "false",
        };

        var seasonSeriesUrl = "https://members-ng.iracing.com/data/series/seasons".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(seasonSeriesUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                SeasonSeriesArrayContext.Default.SeasonSeriesArray,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<StatisticsSeries[]>> GetStatisticsSeriesAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Statistics Series");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Statistics Series");

        var statsSeriesUrl = new Uri("https://members-ng.iracing.com/data/series/stats_series");

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(statsSeriesUrl,
                                                              LinkResultContext.Default.LinkResult,
                                                              infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                              StatisticsSeriesArrayContext.Default.StatisticsSeriesArray,
                                                              cancellationToken).ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberBests>> GetBestLapStatisticsAsync(int? customerId = null,
                                                                           int? carId = null,
                                                                           CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Best Lap Statistics for {CustomerId} in {CarId}", customerId, carId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Best Lap Statistics")
                               ?.AddTag("CustomerId", customerId)
                               ?.AddTag("CarId", carId);

        var queryParameters = new Dictionary<string, object?>();

        if (customerId is not null)
        {
            queryParameters.Add("cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture));
        }

        if (carId is not null)
        {
            queryParameters.Add("car_id", carId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var careerStatisticsUrl = "https://members-ng.iracing.com/data/stats/member_bests".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(careerStatisticsUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                MemberBestsContext.Default.MemberBests,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberCareer>> GetCareerStatisticsAsync(int? customerId = null, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Career Statistics for {CustomerId}", customerId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Career Statistics")
                                ?.AddTag("CustomerId", customerId);

        var queryParameters = new Dictionary<string, object?>();

        if (customerId is not null)
        {
            queryParameters.Add("cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var careerStatisticsUrl = "https://members-ng.iracing.com/data/stats/member_career".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(careerStatisticsUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                MemberCareerContext.Default.MemberCareer,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberRecentRaces>> GetMemberRecentRacesAsync(int? customerId = null, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Member Recent Races for {CustomerId}", customerId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Member Recent Races")
                                ?.AddTag("CustomerId", customerId);

        var queryParameters = new Dictionary<string, object?>();

        if (customerId is not null)
        {
            queryParameters.Add("cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var memberRecentRacesUrl = "https://members-ng.iracing.com/data/stats/member_recent_races".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(memberRecentRacesUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                MemberRecentRacesContext.Default.MemberRecentRaces,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberSummary>> GetMemberSummaryAsync(int? customerId = null, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Member Summary for {CustomerId}", customerId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Member Summary")
                                ?.AddTag("CustomerId", customerId);

        var queryParameters = new Dictionary<string, object?>();

        if (customerId is not null)
        {
            queryParameters.Add("cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var memberSummaryUrl = "https://members-ng.iracing.com/data/stats/member_summary".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(memberSummaryUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                MemberSummaryContext.Default.MemberSummary,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<IReadOnlyDictionary<string, TrackAssets>>> GetTrackAssetsAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Track Assets");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Track Assets");

        var trackAssetsUrl = new Uri("https://members-ng.iracing.com/data/track/assets");

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(trackAssetsUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                TrackAssetsArrayContext.Default.IReadOnlyDictionaryStringTrackAssets,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<Tracks.Track[]>> GetTracksAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Tracks");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Tracks");

        var infoLinkUri = new Uri("https://members-ng.iracing.com/data/track/get");

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(infoLinkUri,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                TrackArrayContext.Default.TrackArray,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<(HostedResultsHeader Header, HostedResultItem[] Items)>> SearchHostedResultsAsync(HostedSearchParameters searchParameters, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Search Hosted Results");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Search Hosted Results");

#if NET6_0_OR_GREATER
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

        if (searchParameters is { ParticipantCustomerId: null, HostCustomerId: null, TeamId: null, SessionName: null or { Length: 0 } })
        {
            throw new ArgumentException("Must supply one of \"ParticipantCustomerId\", \"HostCustomerId\", \"TeamId\", or \"SessionName\"", nameof(searchParameters));
        }

        if (ValidateSearchDateRange(searchParameters.StartRangeBegin,
                                    searchParameters.StartRangeEnd,
                                    nameof(searchParameters),
                                    nameof(searchParameters.StartRangeBegin),
                                    nameof(searchParameters.StartRangeEnd)) is Exception startRangeEx)
        {
            throw startRangeEx;
        }

        if (ValidateSearchDateRange(searchParameters.FinishRangeBegin,
                                    searchParameters.FinishRangeEnd,
                                    nameof(searchParameters),
                                    nameof(searchParameters.FinishRangeBegin),
                                    nameof(searchParameters.FinishRangeEnd)) is Exception finishRangeEx)
        {
            throw finishRangeEx;
        }

        var queryParameters = new Dictionary<string, object?>();
        queryParameters.AddParameterIfNotNull(() => searchParameters.StartRangeBegin);
        queryParameters.AddParameterIfNotNull(() => searchParameters.StartRangeEnd);
        queryParameters.AddParameterIfNotNull(() => searchParameters.FinishRangeBegin);
        queryParameters.AddParameterIfNotNull(() => searchParameters.FinishRangeEnd);
        queryParameters.AddParameterIfNotNull(() => searchParameters.ParticipantCustomerId);
        queryParameters.AddParameterIfNotNull(() => searchParameters.HostCustomerId);
        queryParameters.AddParameterIfNotNull(() => searchParameters.TeamId);
        queryParameters.AddParameterIfNotNull(() => searchParameters.SessionName);
        queryParameters.AddParameterIfNotNull(() => searchParameters.LeagueId);
        queryParameters.AddParameterIfNotNull(() => searchParameters.LeagueSeasonId);
        queryParameters.AddParameterIfNotNull(() => searchParameters.CarId);
        queryParameters.AddParameterIfNotNull(() => searchParameters.TrackId);
        queryParameters.AddParameterIfNotNull(() => searchParameters.CategoryIds);

        var searchHostedUrl = "https://members-ng.iracing.com/data/results/search_hosted".ToUrlWithQuery(queryParameters);

        return await apiClient.CreateResponseFromChunksAsync(searchHostedUrl,
                                                             false,
                                                             HostedResultsHeaderContext.Default.HostedResultsHeader,
                                                             header => header.Data.ChunkInfo,
                                                             HostedResultItemContext.Default.HostedResultItemArray,
                                                             cancellationToken)
                              .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(OfficialSearchResultHeader Header, OfficialSearchResultItem[] Items)>> SearchOfficialResultsAsync(OfficialSearchParameters searchParameters, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Search Official Results");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Search Official Results");

#if NET6_0_OR_GREATER
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

        var queryParameters = new Dictionary<string, object?>();

        // Properties from the SearchParameters basic object
        queryParameters.AddParameterIfNotNull(() => searchParameters.StartRangeBegin);
        queryParameters.AddParameterIfNotNull(() => searchParameters.StartRangeEnd);
        queryParameters.AddParameterIfNotNull(() => searchParameters.FinishRangeBegin);
        queryParameters.AddParameterIfNotNull(() => searchParameters.FinishRangeEnd);
        queryParameters.AddParameterIfNotNull(() => searchParameters.ParticipantCustomerId);
        queryParameters.AddParameterIfNotNull(() => searchParameters.TeamId);
        queryParameters.AddParameterIfNotNull(() => searchParameters.CategoryIds);

        // Properties from the OfficialSearchParameters object
        queryParameters.AddParameterIfNotNull(() => searchParameters.SeasonYear);
        queryParameters.AddParameterIfNotNull(() => searchParameters.SeasonQuarter);
        queryParameters.AddParameterIfNotNull(() => searchParameters.SeriesId);
        queryParameters.AddParameterIfNotNull(() => searchParameters.RaceWeekIndex);
        queryParameters.AddParameterIfNotNull(() => searchParameters.OfficialOnly);
        queryParameters.AddParameterIfNotNull(() => searchParameters.EventTypes);

        var searchHostedUrl = "https://members-ng.iracing.com/data/results/search_series".ToUrlWithQuery(queryParameters);

        return await apiClient.CreateResponseFromChunksAsync(searchHostedUrl,
                                                                  false,
                                                                  OfficialSearchResultHeaderContext.Default.OfficialSearchResultHeader,
                                                                  header => header.Data.ChunkInfo,
                                                                  OfficialSearchResultItemArrayContext.Default.OfficialSearchResultItemArray,
                                                                  cancellationToken)
                              .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<LeagueDirectoryResultPage>> SearchLeagueDirectoryAsync(SearchLeagueDirectoryParameters searchParameters, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Search League Directory");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Search League Directory");

#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(searchParameters);
#else
        if (searchParameters is null)
        {
            throw new ArgumentNullException(nameof(searchParameters));
        }
#endif

        var queryParameters = new Dictionary<string, object?>();
        queryParameters.AddParameterIfNotNull(() => searchParameters.Search);
        queryParameters.AddParameterIfNotNull(() => searchParameters.Tag);
        queryParameters.AddParameterIfNotNull(() => searchParameters.RestrictToMember);
        queryParameters.AddParameterIfNotNull(() => searchParameters.RestrictToRecruiting);
        queryParameters.AddParameterIfNotNull(() => searchParameters.RestrictToFriends);
        queryParameters.AddParameterIfNotNull(() => searchParameters.RestrictToWatched);
        queryParameters.AddParameterIfNotNull(() => searchParameters.MinimumRosterCount);
        queryParameters.AddParameterIfNotNull(() => searchParameters.MaximumRosterCount);
        queryParameters.AddParameterIfNotNull(() => searchParameters.Lowerbound);
        queryParameters.AddParameterIfNotNull(() => searchParameters.Upperbound);

        if (searchParameters.OrderByField is SearchLeagueOrderByField orderBy)
        {
            switch (orderBy)
            {
                case SearchLeagueOrderByField.Relevance:
                    queryParameters["sort"] = "relevance";
                    break;
                case SearchLeagueOrderByField.LeagueName:
                    queryParameters["sort"] = "leaguename";
                    break;
                case SearchLeagueOrderByField.OwnersDisplayName:
                    queryParameters["sort"] = "displayname";
                    break;
                case SearchLeagueOrderByField.RosterCount:
                    queryParameters["sort"] = "rostercount";
                    break;
                default:
                    break;
            }
        }

        if (searchParameters.OrderDirection is ResultOrderDirection orderDirection)
        {
            switch (orderDirection)
            {
                case ResultOrderDirection.Ascending:
                    queryParameters["order"] = "asc";
                    break;
                case ResultOrderDirection.Descending:
                    queryParameters["order"] = "desc";
                    break;
                default:
                    break;
            }
        }

        var searchLeagueDirectoryUrl = "https://members-ng.iracing.com/data/league/directory".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(searchLeagueDirectoryUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                LeagueDirectoryResultPageContext.Default.LeagueDirectoryResultPage,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<ListOfSeasons>> ListSeasonsAsync(int seasonYear, int seasonQuarter, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("List Seasons for {SeasonYear} {SeasonQuarter}", seasonYear, seasonQuarter);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("List Seasons")
                               ?.AddTag("SeasonYear", seasonYear)
                               ?.AddTag("SeasonQuarter", seasonQuarter);

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_year"] = seasonYear.ToString(CultureInfo.InvariantCulture),
            ["season_quarter"] = seasonQuarter.ToString(CultureInfo.InvariantCulture),
        };

        var memberSummaryUrl = "https://members-ng.iracing.com/data/season/list".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(memberSummaryUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                ListOfSeasonsContext.Default.ListOfSeasons,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    private Exception? ValidateSearchDateRange(DateTime? rangeBegin,
                                               DateTime? rangeEnd,
                                               string parameterName,
                                               string rangeBeginFieldName,
                                               string rangeEndFieldName)
    {
        if (rangeBegin is not null)
        {
            if (rangeBegin.Value > GetDateTimeUtcNow())
            {
                return new ArgumentOutOfRangeException(parameterName, $"Value for \"{rangeBeginFieldName}\" cannot be in the future.");
            }

            if (rangeEnd is null
                && (Math.Abs(GetDateTimeUtcNow().Subtract(rangeBegin.Value).TotalDays) > 90))
            {
                return new ArgumentOutOfRangeException(parameterName, $"Must supply value for \"{rangeEndFieldName}\" if \"{rangeBeginFieldName}\" is more than 90 days in the past.");
            }
        }

        if (rangeEnd is not null)
        {
            if (rangeBegin is not null)
            {
                if (rangeBegin >= rangeEnd)
                {
                    return new ArgumentOutOfRangeException(parameterName, $"Value for \"{rangeBeginFieldName}\" cannot be after \"{rangeEndFieldName}\".");
                }

                if (Math.Abs(rangeEnd.Value.Subtract(rangeBegin.Value).TotalDays) > 90)
                {
                    return new ArgumentOutOfRangeException(parameterName, $"Value for \"{rangeEndFieldName}\" cannot be more than 90 days after \"{rangeBeginFieldName}\".");
                }
            }
            else
            {
                return new ArgumentException($"Must supply value for \"{rangeBeginFieldName}\" if \"{rangeEndFieldName}\" is specified.", parameterName);
            }
        }

        return null;
    }

    private DateTime GetDateTimeUtcNow()
    {
        return options.CurrentDateTimeSource is null ? DateTime.UtcNow : options.CurrentDateTimeSource().UtcDateTime;
    }

    /// <inheritdoc />
    public async Task<DataResponse<LeagueMembership[]>> GetLeagueMembershipAsync(bool includeLeague = false, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get League Membership");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get League Membership");

        return await GetLeagueMembershipInternalAsync(null, includeLeague, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<LeagueMembership[]>> GetLeagueMembershipAsync(int customerId, bool includeLeague = false, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get League Membership for {CustomerId}", customerId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get League Membership")
                                           ?.AddTag("CustomerId", customerId);

        return await GetLeagueMembershipInternalAsync(customerId, includeLeague, cancellationToken).ConfigureAwait(false);
    }

    private async Task<DataResponse<LeagueMembership[]>> GetLeagueMembershipInternalAsync(int? customerId, bool includeLeague = false, CancellationToken cancellationToken = default)
    {
        var queryParameters = new Dictionary<string, object?>
        {
            ["include_league"] = includeLeague ? "1" : "0"
        };

        if (customerId is not null)
        {
            queryParameters.Add("cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var getMembershipUrl = "https://members-ng.iracing.com/data/league/membership".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(getMembershipUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                LeagueMembershipArrayContext.Default.LeagueMembershipArray,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<LeagueSeasons>> GetLeagueSeasonsAsync(int leagueId, bool includeRetired = false, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get League Seasons for {LeagueId}", leagueId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get League Seasons")
                                           ?.AddTag("LeagueId", leagueId);

        var queryParameters = new Dictionary<string, object?>
        {
            ["league_id"] = leagueId.ToString(CultureInfo.InvariantCulture),
            ["retired"] = includeRetired ? "1" : "0"
        };

        var getLeagueSeasons = "https://members-ng.iracing.com/data/league/seasons".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(getLeagueSeasons,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                LeagueSeasonsContext.Default.LeagueSeasons,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<RaceGuideResults>> GetRaceGuideAsync(DateTimeOffset? from = null, bool? includeEndAfterFrom = null, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Race Guide");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Race Guide");

        var queryParameters = new Dictionary<string, object?>();

        if (from is not null)
        {
            var fromUtc = from.Value.UtcDateTime.ToString("yyyy-MM-dd\\THH:mm\\Z", CultureInfo.InvariantCulture);
            queryParameters.Add("from", fromUtc);
        }

        if (includeEndAfterFrom is not null)
        {
#pragma warning disable CA1308 // Normalize strings to uppercase - iRacing API requires lowercase
            queryParameters.Add("include_end_after_from", includeEndAfterFrom.Value.ToString().ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase
        }

        var raceGuideUrl = "https://members-ng.iracing.com/data/season/race_guide".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(raceGuideUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                RaceGuideResultsContext.Default.RaceGuideResults,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<Country[]>> GetCountriesAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Countries");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Countries");

        var infoLinkUri = new Uri("https://members-ng.iracing.com/data/lookup/countries");

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(infoLinkUri,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                CountryArrayContext.Default.CountryArray,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<ParticipationCredits[]>> GetMemberParticipationCreditsAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Member Participation Credits");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Member Participation Credits");

        var infoLinkUri = new Uri("https://members-ng.iracing.com/data/member/participation_credits");

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(infoLinkUri,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                ParticipationCreditsArrayContext.Default.ParticipationCreditsArray,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<LeagueSeasonSessions>> GetLeagueSeasonSessionsAsync(int leagueId,
                                                                                       int seasonId,
                                                                                       bool resultsOnly = false,
                                                                                       CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get League Season Sessions for {LeagueId} {SeasonId}", leagueId, seasonId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get League Season Sessions")
                                           ?.AddTag("LeagueId", leagueId)
                                           ?.AddTag("SeasonId", seasonId);

        var queryParameters = new Dictionary<string, object?>
        {
            ["league_id"] = leagueId.ToString(CultureInfo.InvariantCulture),
            ["season_id"] = seasonId.ToString(CultureInfo.InvariantCulture),
            ["results_only"] = resultsOnly ? "1" : "0"
        };

        var getLeagueSeasonSessions = "https://members-ng.iracing.com/data/league/season_sessions".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(getLeagueSeasonSessions,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                LeagueSeasonSessionsContext.Default.LeagueSeasonSessions,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<PastSeriesDetail>> GetPastSeasonsForSeriesAsync(int seriesId, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Past Seasons For Series {SeriesId}", seriesId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Past Seasons For Series")
                                           ?.AddTag("SeriesId", seriesId);

        var queryParameters = new Dictionary<string, object?>
        {
            ["series_id"] = seriesId.ToString(CultureInfo.InvariantCulture),
        };

        var getPastSeasonsForSeriesUrl = "https://members-ng.iracing.com/data/series/past_seasons".ToUrlWithQuery(queryParameters);

        var intermediateResponse = await apiClient.CreateResponseViaIntermediateResultAsync(getPastSeasonsForSeriesUrl,
                                                                                            LinkResultContext.Default.LinkResult,
                                                                                            infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                            PastSeriesResultContext.Default.PastSeriesResult,
                                                                                            cancellationToken)
                                                  .ConfigureAwait(false);

        return new DataResponse<PastSeriesDetail>
        {
            Data = intermediateResponse.Data.Series,
            DataExpires = intermediateResponse.DataExpires,
            RateLimitRemaining = intermediateResponse.RateLimitRemaining,
            RateLimitReset = intermediateResponse.RateLimitReset,
            TotalRateLimit = intermediateResponse.TotalRateLimit
        };
    }

    /// <inheritdoc />
    public async Task<DataResponse<SeasonStandings>> GetSeasonStandingsAsync(int leagueId,
                                                                             int seasonId,
                                                                             int? carClassId = null,
                                                                             int? carId = null,
                                                                             CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get League Season Standings for {LeagueId} {SeasonId}", leagueId, seasonId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get League Season Standings")
                                           ?.AddTag("LeagueId", leagueId)
                                           ?.AddTag("SeasonId", seasonId);

        var queryParameters = new Dictionary<string, object?>
        {
            ["league_id"] = leagueId.ToString(CultureInfo.InvariantCulture),
            ["season_id"] = seasonId.ToString(CultureInfo.InvariantCulture),
        };

        if (carClassId is not null)
        {
            queryParameters.Add("car_class_id", carClassId.Value.ToString(CultureInfo.InvariantCulture));
        }

        if (carId is not null)
        {
            queryParameters.Add("car_id", carId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var queryUrl = "https://members-ng.iracing.com/data/league/season_standings".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(queryUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                SeasonStandingsContext.Default.SeasonStandings,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SeasonSuperSessionResultsHeader Header, SeasonSuperSessionResultItem[] Results)>> GetSeasonSuperSessionStandingsAsync(int seasonId,
                                                                                                                                                          int carClassId,
                                                                                                                                                          int? division = null,
                                                                                                                                                          int? raceWeekIndex = null,
                                                                                                                                                          CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Season Super Session Standings for {SeasonId} {CarClassId}", seasonId, carClassId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Season Super Session Standings")
                               ?.AddTag("SeasonId", seasonId)
                               ?.AddTag("CarClassId", carClassId);

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_id"] = seasonId.ToString(CultureInfo.InvariantCulture),
            ["car_class_id"] = carClassId.ToString(CultureInfo.InvariantCulture),
        };

        if (division is not null)
        {
            queryParameters.Add("division", division.Value.ToString(CultureInfo.InvariantCulture));
        }

        if (raceWeekIndex is not null)
        {
            queryParameters.Add("race_week_num", raceWeekIndex.Value.ToString(CultureInfo.InvariantCulture));
        }

        var queryUrl = "https://members-ng.iracing.com/data/stats/season_supersession_standings".ToUrlWithQuery(queryParameters);

        return await apiClient.CreateResponseFromChunksAsync(queryUrl,
                                                                         true,
                                                                         SeasonSuperSessionResultsHeaderContext.Default.SeasonSuperSessionResultsHeader,
                                                                         header => header.ChunkInfo,
                                                                         SeasonSuperSessionResultItemArrayContext.Default.SeasonSuperSessionResultItemArray,
                                                                         cancellationToken)
                              .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<StatusResult> GetServiceStatusAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Service Status");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Service Status");

        var data = await apiClient.GetUnauthenticatedResponseAsync(new Uri("https://status.iracing.com/status.json"),
                                                                   StatusResultContext.Default.StatusResult,
                                                                   cancellationToken)
                                  .ConfigureAwait(false)
                   ?? throw new iRacingDataClientException("Data not found.");

        return data;
    }

    /// <inheritdoc />
    public async Task<TimeAttackSeason[]> GetTimeAttackSeasonsAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Time Attack Seasons");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Time Attack Seasons");

        // A "magic" sequence of URLs from Nicholas Bailey: https://forums.iracing.com/discussion/comment/302454/#Comment_302454

        var indexData = await apiClient.GetUnauthenticatedResponseAsync(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/time-attack/schedules/time_attack_schedule_index.json"),
                                                                        TimeAttackScheduleIndexContext.Default.TimeAttackScheduleIndex,
                                                                        cancellationToken: cancellationToken)
                                       .ConfigureAwait(false)
                        ?? throw new iRacingDataClientException("Data not found.");

        var data = await apiClient.GetUnauthenticatedResponseAsync(new Uri($"https://dqfp1ltauszrc.cloudfront.net/public/time-attack/schedules/{indexData.ScheduleFilename}.json"),
                                                                   TimeAttackSeasonArrayContext.Default.TimeAttackSeasonArray,
                                                                   cancellationToken: cancellationToken)
                                  .ConfigureAwait(false)
                   ?? throw new iRacingDataClientException("Data not found.");

        return data;
    }

    /// <inheritdoc />
    public async Task<DataResponse<TimeAttackMemberSeasonResult[]>> GetTimeAttackMemberSeasonResultsAsync(int competitionSeasonId, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Time Attack Member Season Results for {CompetitionSeasonId}", competitionSeasonId);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Time Attack Member Season Results")
                                ?.AddTag("CompetitionSeasonId", competitionSeasonId);

        var queryParameters = new Dictionary<string, object?>
        {
            ["ta_comp_season_id"] = competitionSeasonId,
        };

        var queryUrl = "https://members-ng.iracing.com/data/time_attack/member_season_results".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(queryUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                TimeAttackMemberSeasonResultArrayContext.Default.TimeAttackMemberSeasonResultArray,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberRecap>> GetMemberRecapAsync(int? customerId = null,
                                                                     int? seasonYear = null,
                                                                     int? seasonQuarter = null,
                                                                     CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Member Recap for {CustomerId} {SeasonYear} {SeasonQuarter}", customerId, seasonYear, seasonQuarter);
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Member Recap")
                               ?.AddTag("CustomerId", customerId)
                               ?.AddTag("SeasonYear", seasonYear)
                               ?.AddTag("SeasonQuarter", seasonQuarter);

        var queryParameters = new Dictionary<string, object?>
        {
            ["cust_id"] = customerId,
            ["year"] = seasonYear,
            ["season"] = seasonQuarter,
        };

        var queryUrl = "https://members-ng.iracing.com/data/stats/member_recap".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(queryUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                MemberRecapContext.Default.MemberRecap,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<SpectatorSubsessionIds>> GetSpectatorSubsessionIdentifiersAsync(Common.EventType[]? eventTypes = null, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Spectator Subsession Identifiers");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Spectator Subsession Identifiers");

        var queryParameters = new Dictionary<string, object?>
        {
            ["event_types"] = eventTypes,
        };

        var queryUrl = "https://members-ng.iracing.com/data/season/spectator_subsessionids".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(queryUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                SpectatorSubsessionIdsContext.Default.SpectatorSubsessionIds,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DataResponse<SpectatorDetails>> GetSpectatorSubsessionDetailsAsync(Common.EventType[]? eventTypes = null,
                                                                                         int[]? seasonIds = null,
                                                                                         CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Spectator Subsession Details");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Spectator Subsession Details");

        var queryParameters = new Dictionary<string, object?>
        {
            ["event_types"] = eventTypes,
            ["season_ids"] = seasonIds,
        };

        var queryUrl = "https://members-ng.iracing.com/data/season/spectator_subsessionids_detail".ToUrlWithQuery(queryParameters);

        var response = await apiClient.CreateResponseViaIntermediateResultAsync(queryUrl,
                                                                                LinkResultContext.Default.LinkResult,
                                                                                infoLinkResult => (new Uri(infoLinkResult.Link), infoLinkResult.Expires),
                                                                                SpectatorDetailsContext.Default.SpectatorDetails,
                                                                                cancellationToken)
                                      .ConfigureAwait(false);

        return response;
    }

    /// <inheritdoc />
    public async Task<DriverStatisticsCsvFile> GetDriverStatisticsByCategoryCsvAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Driver Statistics By Category CSV");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Driver Statistics By Category CSV")
                                ?.AddTag("CategoryId", categoryId);

        //var attempts = 0;
        var statsUrl = categoryId switch
        {
            1 => new Uri("https://members-ng.iracing.com/data/driver_stats_by_category/oval"),
            2 => new Uri("https://members-ng.iracing.com/data/driver_stats_by_category/road"),
            3 => new Uri("https://members-ng.iracing.com/data/driver_stats_by_category/dirt_oval"),
            4 => new Uri("https://members-ng.iracing.com/data/driver_stats_by_category/dirt_road"),
            5 => new Uri("https://members-ng.iracing.com/data/driver_stats_by_category/sports_car"),
            6 => new Uri("https://members-ng.iracing.com/data/driver_stats_by_category/formula_car"),
            _ => throw new ArgumentOutOfRangeException(nameof(categoryId), categoryId, "Invalid Category Id value. Must be between 1 and 6 (inclusive)."),
        };

        var infoLinkResult = await apiClient.GetDataResponseAsync(statsUrl, LinkResultContext.Default.LinkResult, cancellationToken)
                                            .ConfigureAwait(false)
                             ?? throw new iRacingDataClientException("Invalid or missing link result getting driver statistics.");

        var infoLinkUrl = new Uri(infoLinkResult.Data.Link);
        var csvDataResponse = await apiClient.GetUnauthenticatedRawResponseAsync(infoLinkUrl, cancellationToken)
                                         .ConfigureAwait(false);

        if (!csvDataResponse.IsSuccessStatusCode)
        {
            throw new iRacingDataClientException($"Failed to retrieve CSV data. HTTP response was \"{csvDataResponse.StatusCode} {csvDataResponse.ReasonPhrase}\"");
        }

        var fileName = csvDataResponse.Content.Headers.ContentDisposition?.FileName
                       ?? infoLinkUrl.AbsolutePath.Split('/').LastOrDefault()
                       ?? $"DriverStatistics_CategoryId_{categoryId}.csv";

        var result = new DriverStatisticsCsvFile
        {
            CategoryId = categoryId,
            FileName = fileName,
#if NET6_0_OR_GREATER
            ContentBytes = await csvDataResponse.Content.ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false)
#else
            ContentBytes = await csvDataResponse.Content.ReadAsByteArrayAsync().ConfigureAwait(false)
#endif
        };
        return result;
    }

    /// <inheritdoc />
    public IEnumerable<Uri> GetTrackAssetScreenshotUris(Tracks.Track track, TrackAssets trackAssets)
    {
        logger.LogDebug("Get Track Asset Screenshot URIs");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Track Asset Screenshot URIs");

#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(track);
        ArgumentNullException.ThrowIfNull(trackAssets);
#else
        if (track is null)
        {
            throw new ArgumentNullException(nameof(track));
        }

        if (trackAssets is null)
        {
            throw new ArgumentNullException(nameof(trackAssets));
        }
#endif

        if (track.TrackId != trackAssets.TrackId)
        {
            throw new ArgumentException("TrackAssets must match the Track to build screenshot URLs.", nameof(trackAssets));
        }

        if (string.IsNullOrWhiteSpace(trackAssets.TrackMap) || !Uri.TryCreate(trackAssets.TrackMap, UriKind.Absolute, out var trackMapBaseUrl))
        {
            throw new ArgumentException("TrackMap property of TrackAssets object must be a valid, absolute URI.", nameof(trackAssets));
        }

        if (trackAssets.NumberOfSvgImages <= 0)
        {
            yield break;
        }

        var trackScreenshotBaseUrl = new Uri(trackMapBaseUrl, $"/public/track-maps-screenshots/{track.PackageId}_screenshots/");

        for (var i = 1; i <= trackAssets.NumberOfSvgImages; i++)
        {
            yield return new Uri(trackScreenshotBaseUrl, $"{i:00}.jpg");
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Uri>> GetTrackAssetScreenshotUrisAsync(int trackId, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Track Asset Screenshot URIs for Track ID");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Track Asset Screenshot URIs for Track ID");

        var tracksResponse = await GetTracksAsync(cancellationToken).ConfigureAwait(false);

        if (tracksResponse?.Data.FirstOrDefault(t => t.TrackId == trackId) is not Tracks.Track track)
        {
            throw new ArgumentOutOfRangeException(nameof(trackId), "Track identifier supplied could not be located as a valid track.");
        }

        var trackAssetResponse = await GetTrackAssetsAsync(cancellationToken).ConfigureAwait(false);

        var trackIdString = trackId.ToString(CultureInfo.InvariantCulture);

        return !(trackAssetResponse?.Data.TryGetValue(trackIdString, out var trackAssets) ?? false)
            ? throw new ArgumentOutOfRangeException(nameof(trackId), "Track identifier supplied could not be used to locate track assets.")
            : GetTrackAssetScreenshotUris(track, trackAssets);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastFromUrlAsync(string url, CancellationToken cancellationToken = default)
    {
#if NET8_0_OR_GREATER
        ArgumentException.ThrowIfNullOrEmpty(url, nameof(url));
#else
        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException(nameof(url), "URL must be supplied");
        }
#endif

        return await GetWeatherForecastFromUrlAsync(new Uri(url), cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastFromUrlAsync(Uri url, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Get Weather Forecast From URL");
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Get Weather Forecast From URL");

        var data = await apiClient.GetUnauthenticatedResponseAsync(url,
                                                                   WeatherForecastArrayContext.Default.ListWeatherForecast,
                                                                   cancellationToken)
                                  .ConfigureAwait(false)
                   ?? throw new iRacingDataClientException("Data not found.");

        return data;
    }

    public Task<DataResponse<MemberChart>> GetMemberChartData(int? customerId, int categoryId, MemberChartType chartType, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
