// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
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
public class DataClient(HttpClient httpClient,
                        ILogger<DataClient> logger,
                        iRacingDataClientOptions options,
                        CookieContainer cookieContainer)
    : IDataClient, IDisposable
{
    private readonly SemaphoreSlim loginSemaphore = new(1, 1);
    private bool disposedValue;

    public bool IsLoggedIn { get; private set; }

    /// <inheritdoc/>
    public void UseUsernameAndPassword(string username, string password, bool passwordIsEncoded)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw iRacingClientOptionsValueMissingException.Create(nameof(username));
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw iRacingClientOptionsValueMissingException.Create(nameof(password));
        }

        options.Username = username;
        options.Password = password;
        options.PasswordIsEncoded = passwordIsEncoded;

        // If the username & password has been updated likely the authentication needs to run again.
        IsLoggedIn = false;
    }

    /// <inheritdoc/>
    public void UseUsernameAndPassword(string username, string password)
    {
        UseUsernameAndPassword(username, password, false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<IReadOnlyDictionary<string, CarAssetDetail>>> GetCarAssetDetailsAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        return await CreateResponseViaInfoLinkAsync(new Uri("https://members-ng.iracing.com/data/car/assets"),
                                                    CarAssetDetailDictionaryContext.Default.IReadOnlyDictionaryStringCarAssetDetail,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<Cars.CarInfo[]>> GetCarsAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        return await CreateResponseViaInfoLinkAsync(new Uri("https://members-ng.iracing.com/data/car/get"),
                                                    CarInfoArrayContext.Default.CarInfoArray,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<Common.CarClass[]>> GetCarClassesAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var carClassUrl = new Uri("https://members-ng.iracing.com/data/carclass/get");
        return await CreateResponseViaInfoLinkAsync(carClassUrl, CarClassArrayContext.Default.CarClassArray, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<Division[]>> GetDivisionsAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var constantsDivisionsUrl = new Uri("https://members-ng.iracing.com/data/constants/divisions");
        var constantsDivisionsResponse = await httpClient.GetAsync(constantsDivisionsUrl, cancellationToken).ConfigureAwait(false);

        var data = await constantsDivisionsResponse.Content.ReadFromJsonAsync(DivisionArrayContext.Default.DivisionArray, cancellationToken).ConfigureAwait(false)
                   ?? throw new iRacingDataClientException("Data not found.");

        return BuildDataResponse(constantsDivisionsResponse.Headers, data, logger)!;
    }

    /// <inheritdoc />
    public async Task<DataResponse<Category[]>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var constantsDivisionsUrl = new Uri("https://members-ng.iracing.com/data/constants/categories");
        var constantsDivisionsResponse = await httpClient.GetAsync(constantsDivisionsUrl, cancellationToken)
                                                         .ConfigureAwait(false);

        var data = await constantsDivisionsResponse.Content.ReadFromJsonAsync(CategoryArrayContext.Default.CategoryArray, cancellationToken)
                                                           .ConfigureAwait(false)
                                                           ?? throw new iRacingDataClientException("Data not found.");

        return BuildDataResponse(constantsDivisionsResponse.Headers, data, logger)!;
    }

    /// <inheritdoc />
    public async Task<DataResponse<Constants.EventType[]>> GetEventTypesAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var constantsDivisionsUrl = new Uri("https://members-ng.iracing.com/data/constants/event_types");
        var constantsDivisionsResponse = await httpClient.GetAsync(constantsDivisionsUrl, cancellationToken).ConfigureAwait(false);

        var data = await constantsDivisionsResponse.Content.ReadFromJsonAsync(EventTypeArrayContext.Default.EventTypeArray, cancellationToken)
                                                           .ConfigureAwait(false)
                                                           ?? throw new iRacingDataClientException("Data not found.");

        return BuildDataResponse(constantsDivisionsResponse.Headers, data, logger)!;
    }

    /// <inheritdoc />
    public async Task<DataResponse<CombinedSessionsResult>> ListHostedSessionsCombinedAsync(int? packageId = null, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>();

        if (packageId is not null)
        {
            queryParameters.Add("package_id", packageId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var queryUrl = "https://members-ng.iracing.com/data/hosted/combined_sessions".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(queryUrl,
                                                    CombinedSessionsResultContext.Default.CombinedSessionsResult,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<HostedSessionsResult>> ListHostedSessionsAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        return await CreateResponseViaInfoLinkAsync(new Uri("https://members-ng.iracing.com/data/hosted/sessions"),
                                                    HostedSessionsResultContext.Default.HostedSessionsResult,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<League>> GetLeagueAsync(int leagueId, bool includeLicenses = false, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["league_id"] = leagueId.ToString(CultureInfo.InvariantCulture),
            ["include_licenses"] = includeLicenses.ToString(),
        };

        var getLeagueUrl = "https://members-ng.iracing.com/data/league/get".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(getLeagueUrl,
                                                    LeagueContext.Default.League,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<LeagePointsSystems>> GetLeaguePointsSystemsAsync(int leagueId, int? seasonId = null, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["league_id"] = leagueId.ToString(CultureInfo.InvariantCulture),
        };

        if (seasonId is not null)
        {
            queryParameters.Add("season_id", seasonId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var queryUrl = "https://members-ng.iracing.com/data/league/get_points_systems".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(queryUrl, LeagePointsSystemsContext.Default.LeagePointsSystems, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<CustomerLeagueSessions>> GetCustomerLeagueSessionsAsync(bool mine = false, int? packageId = null, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["mine"] = mine,
            ["package_id"] = packageId,
        };

        var queryUrl = "https://members-ng.iracing.com/data/league/cust_league_sessions".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(queryUrl,
                                                    CustomerLeagueSessionsContext.Default.CustomerLeagueSessions,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<LookupGroup[]>> GetLookupsAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        return await CreateResponseViaInfoLinkAsync(new Uri("https://members-ng.iracing.com/data/lookup/get?weather=weather_wind_speed_units&weather=weather_wind_speed_max&weather=weather_wind_speed_min&licenselevels=licenselevels"),
                                                    LookupGroupArrayContext.Default.LookupGroupArray,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<ClubHistoryLookup[]>> GetClubHistoryLookupsAsync(int seasonYear, int seasonQuarter, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_year"] = seasonYear,
            ["season_quarter"] = seasonQuarter,
        };

        var queryUrl = "https://members-ng.iracing.com/data/lookup/club_history".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(queryUrl,
                                                    ClubHistoryLookupArrayContext.Default.ClubHistoryLookupArray,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<DriverSearchResult[]>> SearchDriversAsync(string searchTerm, int? leagueId = null, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["search_term"] = searchTerm
        };

        if (leagueId is not null)
        {
            queryParameters.Add("league_id", leagueId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var queryUrl = "https://members-ng.iracing.com/data/lookup/drivers".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(queryUrl,
                                                    DriverSearchResultContext.Default.DriverSearchResultArray,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<LicenseLookup[]>> GetLicenseLookupsAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        return await CreateResponseViaInfoLinkAsync(new Uri("https://members-ng.iracing.com/data/lookup/licenses"),
                                                    LicenseLookupArrayContext.Default.LicenseLookupArray,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<DriverInfo[]>> GetDriverInfoAsync(int[] customerIds, bool includeLicenses, CancellationToken cancellationToken = default)
    {
        if (customerIds is not { Length: > 0 })
        {
            throw new ArgumentOutOfRangeException(nameof(customerIds), "Must supply at least one customer identifier value to query.");
        }

        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["cust_ids"] = customerIds,
            ["include_licenses"] = includeLicenses,
        };

        var driverInfoRequestUrl = "https://members-ng.iracing.com/data/member/get".ToUrlWithQuery(queryParameters);

        var driverInfoResponse = await CreateResponseViaInfoLinkAsync(driverInfoRequestUrl,
                                                                      DriverInfoResponseContext.Default.DriverInfoResponse,
                                                                      cancellationToken).ConfigureAwait(false);

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
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>();

        if (customerId is not null)
        {
            queryParameters.Add("cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture));
        };

        var queryUrl = "https://members-ng.iracing.com/data/member/awards".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(queryUrl,
                                                    MemberAwardArrayContext.Default.MemberAwardArray,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberInfo>> GetMyInfoAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        return await CreateResponseViaInfoLinkAsync(new Uri("https://members-ng.iracing.com/data/member/info"),
                                                    MemberInfoContext.Default.MemberInfo,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberProfile>> GetMemberProfileAsync(int? customerId = null, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);
        var queryParameters = new Dictionary<string, object?>();

        if (customerId is not null)
        {
            queryParameters.Add("cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var memberProfileUrl = "https://members-ng.iracing.com/data/member/profile".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(memberProfileUrl,
                                                    MemberProfileContext.Default.MemberProfile,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<SubSessionResult>> GetSubSessionResultAsync(int subSessionId, bool includeLicenses, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["subsession_id"] = subSessionId,
            ["include_licenses"] = includeLicenses,
        };

        var subSessionResultUrl = "https://members-ng.iracing.com/data/results/get".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(subSessionResultUrl,
                                                    SubSessionResultContext.Default.SubSessionResult,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SubsessionLapsHeader Header, SubsessionChartLap[] Laps)>> GetSubSessionLapChartAsync(int subSessionId, int simSessionNumber, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["subsession_id"] = subSessionId.ToString(CultureInfo.InvariantCulture),
            ["simsession_number"] = simSessionNumber.ToString(CultureInfo.InvariantCulture),
        };

        var subSessionLapChartUrl = "https://members-ng.iracing.com/data/results/lap_chart_data".ToUrlWithQuery(queryParameters);

        var intermediateResponse = await CreateResponseViaInfoLinkAsync(subSessionLapChartUrl,
                                                                        SubsessionLapsHeaderContext.Default.SubsessionLapsHeader,
                                                                        cancellationToken).ConfigureAwait(false);

        var sessionLapsList = new List<SubsessionChartLap>();

        if (intermediateResponse.Data.ChunkInfo.NumberOfChunks > 0)
        {
            var baseChunkUrl = new Uri(intermediateResponse.Data.ChunkInfo.BaseDownloadUrl);

            foreach (var (chunkFileName, index) in intermediateResponse.Data.ChunkInfo.ChunkFileNames.Select<string, (string fn, int i)>((fn, i) => (fn, i)))
            {
                var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

                var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
                if (!chunkResponse.IsSuccessStatusCode)
                {
                    logger.FailedToRetrieveChunkError(index, intermediateResponse.Data.ChunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                    continue;
                }

                var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SubsessionChartLapArrayContext.Default.SubsessionChartLapArray, cancellationToken).ConfigureAwait(false);
                if (chunkData is null)
                {
                    continue;
                }

                sessionLapsList.AddRange(chunkData);
            }
        }

        return new DataResponse<(SubsessionLapsHeader Header, SubsessionChartLap[] Laps)>
        {
            Data = (intermediateResponse.Data, sessionLapsList.ToArray()),
            DataExpires = intermediateResponse.DataExpires,
            RateLimitRemaining = intermediateResponse.RateLimitRemaining,
            RateLimitReset = intermediateResponse.RateLimitReset,
            TotalRateLimit = intermediateResponse.TotalRateLimit
        };
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SubsessionEventLogHeader Header, SubsessionEventLogItem[] LogItems)>> GetSubsessionEventLogAsync(int subSessionId, int simSessionNumber, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["subsession_id"] = subSessionId,
            ["simsession_number"] = simSessionNumber,
        };

        var subSessionLapChartUrl = "https://members-ng.iracing.com/data/results/event_log".ToUrlWithQuery(queryParameters);

        var intermediateResponse = await CreateResponseViaInfoLinkAsync(subSessionLapChartUrl,
                                                                        SubsessionEventLogHeaderContext.Default.SubsessionEventLogHeader,
                                                                        cancellationToken).ConfigureAwait(false);

        var sessionLapsList = new List<SubsessionEventLogItem>();

        if (intermediateResponse.Data.ChunkInfo.NumberOfChunks > 0)
        {
            var baseChunkUrl = new Uri(intermediateResponse.Data.ChunkInfo.BaseDownloadUrl);

            foreach (var (chunkFileName, index) in intermediateResponse.Data.ChunkInfo.ChunkFileNames.Select<string, (string fn, int i)>((fn, i) => (fn, i)))
            {
                var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

                var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
                if (!chunkResponse.IsSuccessStatusCode)
                {
                    logger.FailedToRetrieveChunkError(index, intermediateResponse.Data.ChunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                    continue;
                }

                var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SubsessionEventLogItemArrayContext.Default.SubsessionEventLogItemArray, cancellationToken).ConfigureAwait(false);
                if (chunkData is null)
                {
                    continue;
                }

                sessionLapsList.AddRange(chunkData);
            }
        }

        return new DataResponse<(SubsessionEventLogHeader Header, SubsessionEventLogItem[] Laps)>
        {
            Data = (intermediateResponse.Data, sessionLapsList.ToArray()),
            DataExpires = intermediateResponse.DataExpires,
            RateLimitRemaining = intermediateResponse.RateLimitRemaining,
            RateLimitReset = intermediateResponse.RateLimitReset,
            TotalRateLimit = intermediateResponse.TotalRateLimit
        };
    }

    /// <inheritdoc />
    public async Task<DataResponse<SeriesDetail[]>> GetSeriesAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        return await CreateResponseViaInfoLinkAsync(new Uri("https://members-ng.iracing.com/data/series/get"),
                                                    SeriesDetailArrayContext.Default.SeriesDetailArray,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<IReadOnlyDictionary<string, SeriesAsset>>> GetSeriesAssetsAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        return await CreateResponseViaInfoLinkAsync(new Uri("https://members-ng.iracing.com/data/series/assets"),
                                                    SeriesAssetReadOnlyDictionaryContext.Default.IReadOnlyDictionaryStringSeriesAsset,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SubsessionLapsHeader Header, SubsessionLap[] Laps)>> GetSingleDriverSubsessionLapsAsync(int subSessionId, int simSessionNumber, int customerId, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["subsession_id"] = subSessionId,
            ["simsession_number"] = simSessionNumber,
            ["cust_id"] = customerId,
        };

        var subSessionLapChartUrl = "https://members-ng.iracing.com/data/results/lap_data".ToUrlWithQuery(queryParameters);

        var intermediateResponse = await CreateResponseViaInfoLinkAsync(subSessionLapChartUrl,
                                                                        SubsessionLapsHeaderContext.Default.SubsessionLapsHeader,
                                                                        cancellationToken).ConfigureAwait(false);

        var sessionLapsList = new List<SubsessionLap>();

        if (intermediateResponse.Data.ChunkInfo.NumberOfChunks > 0)
        {
            var baseChunkUrl = new Uri(intermediateResponse.Data.ChunkInfo.BaseDownloadUrl);

            foreach (var (chunkFileName, index) in intermediateResponse.Data.ChunkInfo.ChunkFileNames.Select<string, (string fn, int i)>((fn, i) => (fn, i)))
            {
                var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

                var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
                if (!chunkResponse.IsSuccessStatusCode)
                {
                    logger.FailedToRetrieveChunkError(index, intermediateResponse.Data.ChunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                    continue;
                }

                var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SubsessionLapArrayContext.Default.SubsessionLapArray, cancellationToken).ConfigureAwait(false);
                if (chunkData is null)
                {
                    continue;
                }

                sessionLapsList.AddRange(chunkData);
            }
        }

        return new DataResponse<(SubsessionLapsHeader Header, SubsessionLap[] Laps)>
        {
            Data = (intermediateResponse.Data, sessionLapsList.ToArray()),
            DataExpires = intermediateResponse.DataExpires,
            RateLimitRemaining = intermediateResponse.RateLimitRemaining,
            RateLimitReset = intermediateResponse.RateLimitReset,
            TotalRateLimit = intermediateResponse.TotalRateLimit
        };
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SubsessionLapsHeader Header, SubsessionLap[] Laps)>> GetTeamSubsessionLapsAsync(int subSessionId, int simSessionNumber, int teamId, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["subsession_id"] = subSessionId,
            ["simsession_number"] = simSessionNumber,
            ["team_id"] = teamId,
        };

        var subSessionLapChartUrl = "https://members-ng.iracing.com/data/results/lap_data".ToUrlWithQuery(queryParameters);

        var intermediateResponse = await CreateResponseViaInfoLinkAsync(subSessionLapChartUrl,
                                                                        SubsessionLapsHeaderContext.Default.SubsessionLapsHeader,
                                                                        cancellationToken).ConfigureAwait(false);

        var sessionLapsList = new List<SubsessionLap>();

        if (intermediateResponse.Data.ChunkInfo.NumberOfChunks > 0)
        {
            var baseChunkUrl = new Uri(intermediateResponse.Data.ChunkInfo.BaseDownloadUrl);

            foreach (var (chunkFileName, index) in intermediateResponse.Data.ChunkInfo.ChunkFileNames.Select<string, (string fn, int i)>((fn, i) => (fn, i)))
            {
                var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

                var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
                if (!chunkResponse.IsSuccessStatusCode)
                {
                    logger.FailedToRetrieveChunkError(index, intermediateResponse.Data.ChunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                    continue;
                }

                var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SubsessionLapArrayContext.Default.SubsessionLapArray, cancellationToken).ConfigureAwait(false);
                if (chunkData is null)
                {
                    continue;
                }

                sessionLapsList.AddRange(chunkData);
            }
        }

        return new DataResponse<(SubsessionLapsHeader Header, SubsessionLap[] Laps)>
        {
            Data = (intermediateResponse.Data, sessionLapsList.ToArray()),
            DataExpires = intermediateResponse.DataExpires,
            RateLimitRemaining = intermediateResponse.RateLimitRemaining,
            RateLimitReset = intermediateResponse.RateLimitReset,
            TotalRateLimit = intermediateResponse.TotalRateLimit
        };
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberDivision>> GetMemberDivisionAsync(int seasonId, Common.EventType eventType, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_id"] = seasonId,
            ["event_type"] = eventType.ToString("D"),
        };

        var memberDivisionUrl = "https://members-ng.iracing.com/data/stats/member_division".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(memberDivisionUrl,
                                                    MemberDivisionContext.Default.MemberDivision,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberYearlyStatistics>> GetMemberYearlyStatisticsAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        return await CreateResponseViaInfoLinkAsync(new Uri("https://members-ng.iracing.com/data/stats/member_yearly"),
                                                    MemberYearlyStatisticsContext.Default.MemberYearlyStatistics,
                                                    cancellationToken).ConfigureAwait(false);
    }

    [Obsolete("Use \"GetMemberChartDataAsync\" instead.")]
    public async Task<DataResponse<MemberChart>> GetMemberChartData(int? customerId, int categoryId, MemberChartType chartType, CancellationToken cancellationToken = default)
    {
        return await GetMemberChartDataAsync(customerId, categoryId, chartType, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberChart>> GetMemberChartDataAsync(int? customerId, int categoryId, MemberChartType chartType, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var parameters = new Dictionary<string, object?>
        {
            ["category_id"] = categoryId,
            ["chart_type"] = chartType,
            ["cust_id"] = customerId
        };

        var memberChartUrl = "https://members-ng.iracing.com/data/member/chart_data".ToUrlWithQuery(parameters);

        return await CreateResponseViaInfoLinkAsync(memberChartUrl, MemberChartContext.Default.MemberChart, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(WorldRecordsHeader Header, WorldRecordEntry[] Entries)>> GetWorldRecordsAsync(int carId, int trackId, int? seasonYear = null, int? seasonQuarter = null, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

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

        var intermediateResponse = await CreateResponseViaInfoLinkAsync(queryUrl,
                                                                        WorldRecordsHeaderContext.Default.WorldRecordsHeader,
                                                                        cancellationToken).ConfigureAwait(false);

        var entries = new List<WorldRecordEntry>();

        if (intermediateResponse.Data.Data.ChunkInfo is ChunkInfo { NumberOfChunks: > 0 } chunkInfo)
        {
            var baseChunkUrl = new Uri(chunkInfo.BaseDownloadUrl);

            foreach (var (chunkFileName, index) in chunkInfo.ChunkFileNames.Select<string, (string fn, int i)>((fn, i) => (fn, i)))
            {
                var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

                var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
                if (!chunkResponse.IsSuccessStatusCode)
                {
                    logger.FailedToRetrieveChunkError(index, chunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                    continue;
                }

                var chunkData = await chunkResponse.Content.ReadFromJsonAsync(WorldRecordEntryArrayContext.Default.WorldRecordEntryArray, cancellationToken).ConfigureAwait(false);
                if (chunkData is null)
                {
                    continue;
                }

                entries.AddRange(chunkData);
            }
        }

        return new DataResponse<(WorldRecordsHeader Header, WorldRecordEntry[] Entries)>
        {
            Data = (intermediateResponse.Data, entries.ToArray()),
            DataExpires = intermediateResponse.DataExpires,
            RateLimitRemaining = intermediateResponse.RateLimitRemaining,
            RateLimitReset = intermediateResponse.RateLimitReset,
            TotalRateLimit = intermediateResponse.TotalRateLimit
        };
    }

    /// <inheritdoc />
    public async Task<DataResponse<TeamInfo>> GetTeamAsync(int teamId, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["team_id"] = teamId.ToString(CultureInfo.InvariantCulture),
        };

        var queryUrl = "https://members-ng.iracing.com/data/team/get".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(queryUrl,
                                                    TeamInfoContext.Default.TeamInfo,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SeasonDriverStandingsHeader Header, SeasonDriverStanding[] Standings)>> GetSeasonDriverStandingsAsync(int seasonId, int carClassId, int? raceWeekIndex = null, int? clubId = null, int? division = null, CancellationToken cancellationToken = default)
    {
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

        if (clubId is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(clubId));
        }

        if (division is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(division));
        }

        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_id"] = seasonId.ToString(CultureInfo.InvariantCulture),
            ["car_class_id"] = carClassId.ToString(CultureInfo.InvariantCulture),
            ["race_week_num"] = (raceWeekIndex ?? -1).ToString(CultureInfo.InvariantCulture),
            ["club_id"] = (clubId ?? -1).ToString(CultureInfo.InvariantCulture),
            ["division"] = (division ?? -1).ToString(CultureInfo.InvariantCulture),
        };

        var seasonDriverStandingsUrl = "https://members-ng.iracing.com/data/stats/season_driver_standings".ToUrlWithQuery(queryParameters);

        var intermediateResponse = await CreateResponseViaInfoLinkAsync(seasonDriverStandingsUrl,
                                                                        SeasonDriverStandingsHeaderContext.Default.SeasonDriverStandingsHeader,
                                                                        cancellationToken).ConfigureAwait(false);

        var sessionLapsList = new List<SeasonDriverStanding>();

        if (intermediateResponse.Data.ChunkInfo is ChunkInfo { NumberOfChunks: > 0 } chunkInfo)
        {
            var baseChunkUrl = new Uri(chunkInfo.BaseDownloadUrl);

            foreach (var (chunkFileName, index) in chunkInfo.ChunkFileNames.Select<string, (string fn, int i)>((fn, i) => (fn, i)))
            {
                var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

                var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
                if (!chunkResponse.IsSuccessStatusCode)
                {
                    logger.FailedToRetrieveChunkError(index, chunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                    continue;
                }

                var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SeasonDriverStandingArrayContext.Default.SeasonDriverStandingArray, cancellationToken).ConfigureAwait(false);
                if (chunkData is null)
                {
                    continue;
                }

                sessionLapsList.AddRange(chunkData);
            }
        }

        return new DataResponse<(SeasonDriverStandingsHeader Header, SeasonDriverStanding[] Laps)>
        {
            Data = (intermediateResponse.Data, sessionLapsList.ToArray()),
            DataExpires = intermediateResponse.DataExpires,
            RateLimitRemaining = intermediateResponse.RateLimitRemaining,
            RateLimitReset = intermediateResponse.RateLimitReset,
            TotalRateLimit = intermediateResponse.TotalRateLimit
        };
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SeasonQualifyResultsHeader Header, SeasonQualifyResult[] Results)>> GetSeasonQualifyResultsAsync(int seasonId, int carClassId, int? raceWeekIndex = null, int? clubId = null, int? division = null, CancellationToken cancellationToken = default)
    {
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

        if (clubId is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(clubId));
        }

        if (division is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(division));
        }

        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_id"] = seasonId.ToString(CultureInfo.InvariantCulture),
            ["car_class_id"] = carClassId.ToString(CultureInfo.InvariantCulture),
            ["race_week_num"] = (raceWeekIndex ?? -1).ToString(CultureInfo.InvariantCulture),
            ["club_id"] = (clubId ?? -1).ToString(CultureInfo.InvariantCulture),
            ["division"] = (division ?? -1).ToString(CultureInfo.InvariantCulture),
        };

        var qualifyResultsUrl = "https://members-ng.iracing.com/data/stats/season_qualify_results".ToUrlWithQuery(queryParameters);

        var intermediateResponse = await CreateResponseViaInfoLinkAsync(qualifyResultsUrl,
                                                                        SeasonQualifyResultsHeaderContext.Default.SeasonQualifyResultsHeader,
                                                                        cancellationToken).ConfigureAwait(false);

        var seasonQualifyResults = new List<SeasonQualifyResult>();

        if (intermediateResponse.Data.ChunkInfo is ChunkInfo { NumberOfChunks: > 0 } chunkInfo)
        {
            var baseChunkUrl = new Uri(chunkInfo.BaseDownloadUrl);

            foreach (var (chunkFileName, index) in chunkInfo.ChunkFileNames.Select<string, (string fn, int i)>((fn, i) => (fn, i)))
            {
                var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

                var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
                if (!chunkResponse.IsSuccessStatusCode)
                {
                    logger.FailedToRetrieveChunkError(index, chunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                    continue;
                }

                var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SeasonQualifyResultArrayContext.Default.SeasonQualifyResultArray, cancellationToken).ConfigureAwait(false);
                if (chunkData is null)
                {
                    continue;
                }

                seasonQualifyResults.AddRange(chunkData);
            }
        }

        return new DataResponse<(SeasonQualifyResultsHeader Header, SeasonQualifyResult[] Standings)>
        {
            Data = (intermediateResponse.Data, seasonQualifyResults.ToArray()),
            DataExpires = intermediateResponse.DataExpires,
            RateLimitRemaining = intermediateResponse.RateLimitRemaining,
            RateLimitReset = intermediateResponse.RateLimitReset,
            TotalRateLimit = intermediateResponse.TotalRateLimit
        };
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SeasonTimeTrialResultsHeader Header, SeasonTimeTrialResult[] Results)>> GetSeasonTimeTrialResultsAsync(int seasonId, int carClassId, int? raceWeekIndex = null, int? clubId = null, int? division = null, CancellationToken cancellationToken = default)
    {
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

        if (clubId is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(clubId));
        }

        if (division is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(division));
        }

        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_id"] = seasonId.ToString(CultureInfo.InvariantCulture),
            ["car_class_id"] = carClassId.ToString(CultureInfo.InvariantCulture),
            ["race_week_num"] = (raceWeekIndex ?? -1).ToString(CultureInfo.InvariantCulture),
            ["club_id"] = (clubId ?? -1).ToString(CultureInfo.InvariantCulture),
            ["division"] = (division ?? -1).ToString(CultureInfo.InvariantCulture),
        };

        var subSessionLapChartUrl = "https://members-ng.iracing.com/data/stats/season_tt_results".ToUrlWithQuery(queryParameters);

        var intermediateResponse = await CreateResponseViaInfoLinkAsync(subSessionLapChartUrl,
                                                                        SeasonTimeTrialResultsHeaderContext.Default.SeasonTimeTrialResultsHeader,
                                                                        cancellationToken).ConfigureAwait(false);

        var seasonTimeTrialResults = new List<SeasonTimeTrialResult>();

        if (intermediateResponse.Data.ChunkInfo is ChunkInfo { NumberOfChunks: > 0 } chunkInfo)
        {
            var baseChunkUrl = new Uri(chunkInfo.BaseDownloadUrl);

            foreach (var (chunkFileName, index) in chunkInfo.ChunkFileNames.Select<string, (string fn, int i)>((fn, i) => (fn, i)))
            {
                var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

                var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
                if (!chunkResponse.IsSuccessStatusCode)
                {
                    logger.FailedToRetrieveChunkError(index, chunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                    continue;
                }

                var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SeasonTimeTrialResultArrayContext.Default.SeasonTimeTrialResultArray, cancellationToken).ConfigureAwait(false);
                if (chunkData is null)
                {
                    continue;
                }

                seasonTimeTrialResults.AddRange(chunkData);
            }
        }

        return new DataResponse<(SeasonTimeTrialResultsHeader Header, SeasonTimeTrialResult[] Standings)>
        {
            Data = (intermediateResponse.Data, seasonTimeTrialResults.ToArray()),
            DataExpires = intermediateResponse.DataExpires,
            RateLimitRemaining = intermediateResponse.RateLimitRemaining,
            RateLimitReset = intermediateResponse.RateLimitReset,
            TotalRateLimit = intermediateResponse.TotalRateLimit
        };
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SeasonTimeTrialStandingsHeader Header, SeasonTimeTrialStanding[] Standings)>> GetSeasonTimeTrialStandingsAsync(int seasonId, int carClassId, int? raceWeekIndex = null, int? clubId = null, int? division = null, CancellationToken cancellationToken = default)
    {
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

        if (clubId is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(clubId));
        }

        if (division is not null and < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(division));
        }

        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_id"] = seasonId.ToString(CultureInfo.InvariantCulture),
            ["car_class_id"] = carClassId.ToString(CultureInfo.InvariantCulture),
            ["race_week_num"] = (raceWeekIndex ?? -1).ToString(CultureInfo.InvariantCulture),
            ["club_id"] = (clubId ?? -1).ToString(CultureInfo.InvariantCulture),
            ["division"] = (division ?? -1).ToString(CultureInfo.InvariantCulture),
        };

        var subSessionLapChartUrl = "https://members-ng.iracing.com/data/stats/season_tt_standings".ToUrlWithQuery(queryParameters);

        var intermediateResponse = await CreateResponseViaInfoLinkAsync(subSessionLapChartUrl,
                                                                        SeasonTimeTrialStandingsHeaderContext.Default.SeasonTimeTrialStandingsHeader,
                                                                        cancellationToken).ConfigureAwait(false);

        var seasonTimeTrialStandings = new List<SeasonTimeTrialStanding>();

        if (intermediateResponse.Data.ChunkInfo is ChunkInfo { NumberOfChunks: > 0 } chunkInfo)
        {
            var baseChunkUrl = new Uri(chunkInfo.BaseDownloadUrl);
            foreach (var (chunkFileName, index) in chunkInfo.ChunkFileNames.Select((fn, i) => (fn, i)))
            {
                var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

                var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
                if (!chunkResponse.IsSuccessStatusCode)
                {
                    logger.FailedToRetrieveChunkError(index, chunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                    continue;
                }

                var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SeasonTimeTrialStandingArrayContext.Default.SeasonTimeTrialStandingArray, cancellationToken).ConfigureAwait(false);
                if (chunkData is null)
                {
                    continue;
                }

                seasonTimeTrialStandings.AddRange(chunkData);
            }
        }

        return new DataResponse<(SeasonTimeTrialStandingsHeader Header, SeasonTimeTrialStanding[] Standings)>
        {
            Data = (intermediateResponse.Data, seasonTimeTrialStandings.ToArray()),
            DataExpires = intermediateResponse.DataExpires,
            RateLimitRemaining = intermediateResponse.RateLimitRemaining,
            RateLimitReset = intermediateResponse.RateLimitReset,
            TotalRateLimit = intermediateResponse.TotalRateLimit
        };
    }

    /// <inheritdoc />
    public async Task<DataResponse<(SeasonTeamStandingsHeader Header, SeasonTeamStanding[] Standings)>> GetSeasonTeamStandingsAsync(int seasonId, int carClassId, int? raceWeekIndex = null, CancellationToken cancellationToken = default)
    {
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

        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_id"] = seasonId.ToString(CultureInfo.InvariantCulture),
            ["car_class_id"] = carClassId.ToString(CultureInfo.InvariantCulture),
            ["race_week_num"] = (raceWeekIndex ?? -1).ToString(CultureInfo.InvariantCulture),
        };

        var subSessionLapChartUrl = "https://members-ng.iracing.com/data/stats/season_team_standings".ToUrlWithQuery(queryParameters);

        var intermediateResponse = await CreateResponseViaInfoLinkAsync(subSessionLapChartUrl,
                                                                        SeasonTeamStandingsHeaderContext.Default.SeasonTeamStandingsHeader,
                                                                        cancellationToken).ConfigureAwait(false);

        var seasonTeamStandings = new List<SeasonTeamStanding>();

        if (intermediateResponse.Data.ChunkInfo is ChunkInfo { NumberOfChunks: > 0 } chunkInfo)
        {
            var baseChunkUrl = new Uri(chunkInfo.BaseDownloadUrl);

            foreach (var (chunkFileName, index) in chunkInfo.ChunkFileNames.Select((fn, i) => (fn, i)))
            {
                var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

                var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
                if (!chunkResponse.IsSuccessStatusCode)
                {
                    logger.FailedToRetrieveChunkError(index, chunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                    continue;
                }

                var chunkData = await chunkResponse.Content.ReadFromJsonAsync(SeasonTeamStandingArrayContext.Default.SeasonTeamStandingArray, cancellationToken).ConfigureAwait(false);
                if (chunkData is null)
                {
                    continue;
                }

                seasonTeamStandings.AddRange(chunkData);
            }
        }

        return new DataResponse<(SeasonTeamStandingsHeader Header, SeasonTeamStanding[] Standings)>
        {
            Data = (intermediateResponse.Data, seasonTeamStandings.ToArray()),
            DataExpires = intermediateResponse.DataExpires,
            RateLimitRemaining = intermediateResponse.RateLimitRemaining,
            RateLimitReset = intermediateResponse.RateLimitReset,
            TotalRateLimit = intermediateResponse.TotalRateLimit
        };
    }

    /// <inheritdoc />
    public async Task<DataResponse<SeasonResults>> GetSeasonResultsAsync(int seasonId, Common.EventType eventType, int raceWeekNumber, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_id"] = seasonId,
            ["event_type"] = eventType,
            ["race_week_num"] = raceWeekNumber,
        };

        var seasonResultsUrl = "https://members-ng.iracing.com/data/results/season_results".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(seasonResultsUrl,
                                                    SeasonResultsContext.Default.SeasonResults,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<SeasonSeries[]>> GetSeasonsAsync(bool includeSeries, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["include_series"] = includeSeries ? "true" : "false",
        };

        var seasonSeriesUrl = "https://members-ng.iracing.com/data/series/seasons".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(seasonSeriesUrl,
                                                    SeasonSeriesArrayContext.Default.SeasonSeriesArray,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<StatisticsSeries[]>> GetStatisticsSeriesAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        return await CreateResponseViaInfoLinkAsync(new Uri("https://members-ng.iracing.com/data/series/stats_series"),
                                                    StatisticsSeriesArrayContext.Default.StatisticsSeriesArray,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberBests>> GetBestLapStatisticsAsync(int? customerId = null, int? carId = null, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

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

        return await CreateResponseViaInfoLinkAsync(careerStatisticsUrl,
                                                    MemberBestsContext.Default.MemberBests,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberCareer>> GetCareerStatisticsAsync(int? customerId = null, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>();

        if (customerId is not null)
        {
            queryParameters.Add("cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var careerStatisticsUrl = "https://members-ng.iracing.com/data/stats/member_career".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(careerStatisticsUrl,
                                                    MemberCareerContext.Default.MemberCareer,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberRecentRaces>> GetMemberRecentRacesAsync(int? customerId = null, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>();

        if (customerId is not null)
        {
            queryParameters.Add("cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var memberRecentRacesUrl = "https://members-ng.iracing.com/data/stats/member_recent_races".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(memberRecentRacesUrl,
                                                    MemberRecentRacesContext.Default.MemberRecentRaces,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberSummary>> GetMemberSummaryAsync(int? customerId = null, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>();

        if (customerId is not null)
        {
            queryParameters.Add("cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var memberSummaryUrl = "https://members-ng.iracing.com/data/stats/member_summary".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(memberSummaryUrl,
                                                    MemberSummaryContext.Default.MemberSummary,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<IReadOnlyDictionary<string, TrackAssets>>> GetTrackAssetsAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        return await CreateResponseViaInfoLinkAsync(new Uri("https://members-ng.iracing.com/data/track/assets"),
                                                    TrackAssetsArrayContext.Default.IReadOnlyDictionaryStringTrackAssets,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<Tracks.Track[]>> GetTracksAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        return await CreateResponseViaInfoLinkAsync(new Uri("https://members-ng.iracing.com/data/track/get"),
                                                    TrackArrayContext.Default.TrackArray,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(HostedResultsHeader Header, HostedResultItem[] Items)>> SearchHostedResultsAsync(HostedSearchParameters searchParameters, CancellationToken cancellationToken = default)
    {
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

        if (ValidateSearchDateRange(searchParameters.StartRangeBegin, searchParameters.StartRangeEnd, nameof(searchParameters), nameof(searchParameters.StartRangeBegin), nameof(searchParameters.StartRangeEnd)) is Exception startRangeEx)
        {
            throw startRangeEx;
        }

        if (ValidateSearchDateRange(searchParameters.FinishRangeBegin, searchParameters.FinishRangeEnd, nameof(searchParameters), nameof(searchParameters.FinishRangeBegin), nameof(searchParameters.FinishRangeEnd)) is Exception finishRangeEx)
        {
            throw finishRangeEx;
        }

        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

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

        return await CreateResponseFromChunkedDataAsync<HostedResultsHeader, HostedResultsHeaderData, HostedResultItem>(searchHostedUrl, HostedResultsHeaderContext.Default.HostedResultsHeader, HostedResultItemContext.Default.HostedResultItemArray, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<(OfficialSearchResultHeader Header, OfficialSearchResultItem[] Items)>> SearchOfficialResultsAsync(OfficialSearchParameters searchParameters, CancellationToken cancellationToken = default)
    {
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

        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

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

        return await CreateResponseFromChunkedDataAsync<OfficialSearchResultHeader, OfficialSearchResultHeaderData, OfficialSearchResultItem>(searchHostedUrl,
                                                                                                                                              OfficialSearchResultHeaderContext.Default.OfficialSearchResultHeader,
                                                                                                                                              OfficialSearchResultItemArrayContext.Default.OfficialSearchResultItemArray,
                                                                                                                                              cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<LeagueDirectoryResultPage>> SearchLeagueDirectoryAsync(SearchLeagueDirectoryParameters searchParameters, CancellationToken cancellationToken = default)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(searchParameters);
#else
        if (searchParameters is null)
        {
            throw new ArgumentNullException(nameof(searchParameters));
        }
#endif

        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

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
            }
        }

        var searchLeagueDirectoryUrl = "https://members-ng.iracing.com/data/league/directory".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(searchLeagueDirectoryUrl,
                                                    LeagueDirectoryResultPageContext.Default.LeagueDirectoryResultPage,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<ListOfSeasons>> ListSeasonsAsync(int seasonYear, int seasonQuarter, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["season_year"] = seasonYear.ToString(CultureInfo.InvariantCulture),
            ["season_quarter"] = seasonQuarter.ToString(CultureInfo.InvariantCulture),
        };

        var memberSummaryUrl = "https://members-ng.iracing.com/data/season/list".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(memberSummaryUrl,
                                                    ListOfSeasonsContext.Default.ListOfSeasons,
                                                    cancellationToken).ConfigureAwait(false);
    }

    private Exception? ValidateSearchDateRange(DateTime? rangeBegin, DateTime? rangeEnd, string parameterName, string rangeBeginFieldName, string rangeEndFieldName)
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
        return await GetLeagueMembershipInternalAsync(null, includeLeague, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<LeagueMembership[]>> GetLeagueMembershipAsync(int customerId, bool includeLeague = false, CancellationToken cancellationToken = default)
    {
        return await GetLeagueMembershipInternalAsync(customerId, includeLeague, cancellationToken).ConfigureAwait(false);
    }

    private async Task<DataResponse<LeagueMembership[]>> GetLeagueMembershipInternalAsync(int? customerId, bool includeLeague = false, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["include_league"] = includeLeague ? "1" : "0"
        };

        if (customerId is not null)
        {
            queryParameters.Add("cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture));
        }

        var getMembershipUrl = "https://members-ng.iracing.com/data/league/membership".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(getMembershipUrl,
                                                    LeagueMembershipArrayContext.Default.LeagueMembershipArray,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<LeagueSeasons>> GetLeagueSeasonsAsync(int leagueId, bool includeRetired = false, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["league_id"] = leagueId.ToString(CultureInfo.InvariantCulture),
            ["retired"] = includeRetired ? "1" : "0"
        };

        var getLeagueSeasons = "https://members-ng.iracing.com/data/league/seasons".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(getLeagueSeasons,
                                                    LeagueSeasonsContext.Default.LeagueSeasons,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<RaceGuideResults>> GetRaceGuideAsync(DateTimeOffset? from = null, bool? includeEndAfterFrom = null, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>();

        if (from is not null)
        {
            var fromUtc = from.Value.UtcDateTime.ToString("yyyy-MM-dd\\THH:mm\\Z", CultureInfo.InvariantCulture);
            queryParameters.Add("from", fromUtc);
        }

        if (includeEndAfterFrom is not null)
        {
#pragma warning disable CA1308 // Normalize strings to uppercase - This value needs to be lowercase for API compatibility.
            queryParameters.Add("include_end_after_from", includeEndAfterFrom.Value.ToString().ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase
        }

        var raceGuideUrl = "https://members-ng.iracing.com/data/season/race_guide".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(raceGuideUrl,
                                                    RaceGuideResultsContext.Default.RaceGuideResults,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<Country[]>> GetCountriesAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        return await CreateResponseViaInfoLinkAsync(new Uri("https://members-ng.iracing.com/data/lookup/countries"),
                                                    CountryArrayContext.Default.CountryArray,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<ParticipationCredits[]>> GetMemberParticipationCreditsAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        return await CreateResponseViaInfoLinkAsync(new Uri("https://members-ng.iracing.com/data/member/participation_credits"),
                                                    ParticipationCreditsArrayContext.Default.ParticipationCreditsArray,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<LeagueSeasonSessions>> GetLeagueSeasonSessionsAsync(int leagueId, int seasonId, bool resultsOnly = false, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["league_id"] = leagueId.ToString(CultureInfo.InvariantCulture),
            ["season_id"] = seasonId.ToString(CultureInfo.InvariantCulture),
            ["results_only"] = resultsOnly ? "1" : "0"
        };

        var getLeagueSeasonSessions = "https://members-ng.iracing.com/data/league/season_sessions".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(getLeagueSeasonSessions,
                                                    LeagueSeasonSessionsContext.Default.LeagueSeasonSessions,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<PastSeriesDetail>> GetPastSeasonsForSeriesAsync(int seriesId, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["series_id"] = seriesId.ToString(CultureInfo.InvariantCulture),
        };

        var getPastSeasonsForSeriesUrl = "https://members-ng.iracing.com/data/series/past_seasons".ToUrlWithQuery(queryParameters);

        var intermediateResponse = await CreateResponseViaInfoLinkAsync(getPastSeasonsForSeriesUrl,
                                                                        PastSeriesResultContext.Default.PastSeriesResult,
                                                                        cancellationToken).ConfigureAwait(false);

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
    public async Task<DataResponse<SeasonStandings>> GetSeasonStandingsAsync(int leagueId, int seasonId, int? carClassId = null, int? carId = null, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

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

        return await CreateResponseViaInfoLinkAsync(queryUrl,
                                                    SeasonStandingsContext.Default.SeasonStandings,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<StatusResult> GetServiceStatusAsync(CancellationToken cancellationToken = default)
    {
        var data = (await httpClient.GetFromJsonAsync("https://status.iracing.com/status.json",
                                                     StatusResultContext.Default.StatusResult,
                                                     cancellationToken: cancellationToken)
                                   .ConfigureAwait(false))
                    ?? throw new iRacingDataClientException("Data not found.");

        return data!;
    }

    /// <inheritdoc />
    public async Task<TimeAttackSeason[]> GetTimeAttackSeasonsAsync(CancellationToken cancellationToken = default)
    {
        // A "magic" sequence of URLs from Nicholas Bailey: https://forums.iracing.com/discussion/comment/302454/#Comment_302454

        var indexData = (await httpClient.GetFromJsonAsync("https://dqfp1ltauszrc.cloudfront.net/public/time-attack/schedules/time_attack_schedule_index.json",
                                                           TimeAttackScheduleIndexContext.Default.TimeAttackScheduleIndex,
                                                           cancellationToken: cancellationToken)
                                   .ConfigureAwait(false))
                         ?? throw new iRacingDataClientException("Data not found.");

        var data = (await httpClient.GetFromJsonAsync($"https://dqfp1ltauszrc.cloudfront.net/public/time-attack/schedules/{indexData.ScheduleFilename}.json",
                                                      TimeAttackSeasonArrayContext.Default.TimeAttackSeasonArray,
                                                      cancellationToken: cancellationToken)
                                   .ConfigureAwait(false))
                    ?? throw new iRacingDataClientException("Data not found.");

        return data!;
    }

    /// <inheritdoc />
    public async Task<DataResponse<TimeAttackMemberSeasonResult[]>> GetTimeAttackMemberSeasonResultsAsync(int competitionSeasonId, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["ta_comp_season_id"] = competitionSeasonId,
        };

        var queryUrl = "https://members-ng.iracing.com/data/time_attack/member_season_results".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(queryUrl,
                                                    TimeAttackMemberSeasonResultArrayContext.Default.TimeAttackMemberSeasonResultArray,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<MemberRecap>> GetMemberRecapAsync(int? customerId = null, int? seasonYear = null, int? seasonQuarter = null, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["cust_id"] = customerId,
            ["year"] = seasonYear,
            ["season"] = seasonQuarter,
        };

        var queryUrl = "https://members-ng.iracing.com/data/stats/member_recap".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(queryUrl,
                                                    MemberRecapContext.Default.MemberRecap,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DataResponse<SpectatorSubsessionIds>> GetSpectatorSubsessionIdentifiersAsync(Common.EventType[]? eventTypes = null, CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);

        var queryParameters = new Dictionary<string, object?>
        {
            ["event_types"] = eventTypes,
        };

        var queryUrl = "https://members-ng.iracing.com/data/season/spectator_subsessionids".ToUrlWithQuery(queryParameters);

        return await CreateResponseViaInfoLinkAsync(queryUrl,
                                                    SpectatorSubsessionIdsContext.Default.SpectatorSubsessionIds,
                                                    cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Will ensure the client is authenticated by checking the <see cref="IsLoggedIn"/> property and executing the login process if required.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="Task"/> that resolves when the process is complete.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code", Justification = "Double-check of the precondition is a common pattern when using a lock and initialisation method.")]
    protected async Task EnsureLoggedInAsync(CancellationToken cancellationToken)
    {
        if (IsLoggedIn is false)
        {
            await loginSemaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                if (IsLoggedIn is false)
                {
                    await LoginInternalAsync(cancellationToken).ConfigureAwait(false); 
                }
            }
            finally
            {
                loginSemaphore.Release();
            }
        }
    }

    private async Task LoginInternalAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(options.Username))
        {
            throw iRacingClientOptionsValueMissingException.Create(nameof(options.Username));
        }

        if (string.IsNullOrWhiteSpace(options.Password))
        {
            throw iRacingClientOptionsValueMissingException.Create(nameof(options.Password));
        }

        try
        {
            if (options.RestoreCookies is not null
                && options.RestoreCookies() is CookieCollection savedCookies)
            {
                cookieContainer.Add(savedCookies);

                // Assume we're logged in if we have cookies for our target domain
                if (cookieContainer.GetCookies(new Uri("https://members-ng.iracing.com")).Count > 0)
                {
                    IsLoggedIn = true;
                    logger.LoginCookiesRestored(options.Username!);
                    return;
                }
            }

            string? encodedHash = null;

            if (options.PasswordIsEncoded)
            {
                encodedHash = options.Password;
            }
            else
            {

#pragma warning disable CA1308 // Normalize strings to uppercase - this algorithm requires lower case.
                var passwordAndEmail = options.Password + (options.Username?.ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

#if NET6_0_OR_GREATER
                var hashedPasswordAndEmailBytes = SHA256.HashData(Encoding.UTF8.GetBytes(passwordAndEmail));
#else
                using var sha256 = SHA256.Create();
                var hashedPasswordAndEmailBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordAndEmail));
#endif

                encodedHash = Convert.ToBase64String(hashedPasswordAndEmailBytes);
            }

            var loginResponse = await httpClient.PostAsJsonAsync("https://members-ng.iracing.com/auth",
                                                                 new
                                                                 {
                                                                     email = options.Username,
                                                                     password = encodedHash
                                                                 },
                                                                 cancellationToken)
                                                .ConfigureAwait(false);

            if (loginResponse.IsSuccessStatusCode is false)
            {
                if (loginResponse.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new iRacingInMaintenancePeriodException("Maintenance assumed because login returned HTTP Error 503 \"Service Unavailable\".");
                }
                throw new iRacingLoginFailedException($"Login failed with HTTP response \"{loginResponse.StatusCode} {loginResponse.ReasonPhrase}\"");
            }

            var loginResult = await loginResponse.Content.ReadFromJsonAsync(LoginResponseContext.Default.LoginResponse, cancellationToken).ConfigureAwait(false);

            if (loginResult is null || loginResult.Success is false)
            {
                var message = loginResult?.Message ?? $"Login failed with HTTP response \"{loginResponse.StatusCode} {loginResponse.ReasonPhrase}\"";
                throw iRacingLoginFailedException.Create(message, loginResult?.VerificationRequired);
            }

            IsLoggedIn = true;
            logger.LoginSuccessful(options.Username!);

            if (options.SaveCookies is Action<CookieCollection> saveCredentials)
            {
                saveCredentials(cookieContainer.GetAllCookies());
            }
        }
        catch (Exception ex) when (ex is not iRacingDataClientException)
        {
            throw iRacingLoginFailedException.Create(ex);
        }
    }

    private const string RateLimitExceededContent = "Rate limit exceeded";

    protected virtual async Task<DataResponse<TData>> CreateResponseViaInfoLinkAsync<TData>(Uri infoLinkUri, JsonTypeInfo<TData> jsonTypeInfo, CancellationToken cancellationToken)
    {
        var infoLinkResponse = await httpClient.GetAsync(infoLinkUri, cancellationToken).ConfigureAwait(false);

#if NET6_0_OR_GREATER
        var content = await infoLinkResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#else
        var content = await infoLinkResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif

        if (!infoLinkResponse.IsSuccessStatusCode || content == RateLimitExceededContent)
        {
            HandleUnsuccessfulResponse(infoLinkResponse, content, logger);
        }

        var infoLink = JsonSerializer.Deserialize(content, LinkResultContext.Default.LinkResult);
        if (infoLink?.Link is null)
        {
            throw new iRacingDataClientException("Unrecognized result.");
        }

        var data = await httpClient.GetFromJsonAsync(infoLink.Link, jsonTypeInfo, cancellationToken: cancellationToken)
                                   .ConfigureAwait(false)
                                   ?? throw new iRacingDataClientException("Data not found.");

        return BuildDataResponse(infoLinkResponse.Headers, data, logger, infoLink.Expires);
    }

    protected virtual async Task<DataResponse<(TData, TChunkData[])>> CreateResponseFromChunkedDataAsync<TData, THeaderData, TChunkData>(Uri uri, JsonTypeInfo<TData> jsonTypeInfo, JsonTypeInfo<TChunkData[]> chunkArrayTypeInfo, CancellationToken cancellationToken)
        where TData : IChunkInfoResultHeader<THeaderData>
        where THeaderData : IChunkInfoResultHeaderData
    {
        var response = await httpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);

        // This isn't the most performant way of going here, but annoyingly if you exceed the rate limit it isn't an issue just
        // the string "Rate limit exceeded" so we need the string to check that.
#if NET6_0_OR_GREATER
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#else
        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif
        if (!response.IsSuccessStatusCode || responseContent == RateLimitExceededContent)
        {
            HandleUnsuccessfulResponse(response, responseContent, logger);
        }

        var headerData = await response.Content.ReadFromJsonAsync(jsonTypeInfo, cancellationToken: cancellationToken)
                                         .ConfigureAwait(false)
                                         ?? throw new iRacingDataClientException("Data not found.");

        var searchResults = new List<TChunkData>();

        if (headerData.Data.ChunkInfo is ChunkInfo { NumberOfChunks: > 0 } chunkInfo)
        {
            var baseChunkUrl = new Uri(chunkInfo.BaseDownloadUrl);

            foreach (var (chunkFileName, index) in chunkInfo.ChunkFileNames.Select((fn, i) => (fn, i)))
            {
                var chunkUrl = new Uri(baseChunkUrl, chunkFileName);

                var chunkResponse = await httpClient.GetAsync(chunkUrl, cancellationToken).ConfigureAwait(false);
                if (!chunkResponse.IsSuccessStatusCode)
                {
                    logger.FailedToRetrieveChunkError(index, chunkInfo.NumberOfChunks, chunkResponse.StatusCode, chunkResponse.ReasonPhrase);
                    continue;
                }

                var chunkData = await chunkResponse.Content.ReadFromJsonAsync(chunkArrayTypeInfo, cancellationToken).ConfigureAwait(false);
                if (chunkData is null)
                {
                    continue;
                }

                searchResults.AddRange(chunkData);
            }
        }

        return BuildDataResponse<(TData Header, TChunkData[] Results)>(response.Headers, (headerData, searchResults.ToArray()), logger);
    }

    protected virtual void HandleUnsuccessfulResponse(HttpResponseMessage httpResponse, string content, ILogger logger)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(httpResponse);
#else
        if (httpResponse is null)
        {
            throw new ArgumentNullException(nameof(httpResponse));
        }
#endif

        string? errorDescription;
        Exception? exception;

        if (content == "Rate limit exceeded")
        {
            errorDescription = content;
            exception = iRacingRateLimitExceededException.Create();
        }
        else
        {
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content);
            errorDescription = errorResponse?.Note ?? errorResponse?.Message ?? "An error occurred.";

            exception = errorResponse switch
            {
                { ErrorCode: "Site Maintenance" } => new iRacingInMaintenancePeriodException(errorResponse.Note ?? "iRacing services are down for maintenance."),
                { ErrorCode: "Forbidden" } => iRacingForbiddenResponseException.Create(),
                { ErrorCode: "Unauthorized" } => iRacingUnauthorizedResponseException.Create(errorResponse.Message),
                _ => null
            };
        }

        if (exception is null)
        {
            logger.ErrorResponseUnknown();
            _ = httpResponse.EnsureSuccessStatusCode();
        }
        else
        {
            if (exception is iRacingUnauthorizedResponseException)
            {
                // Unauthorized might just be our session expired
                IsLoggedIn = false;

                // Clear any saved cookies
                options.SaveCookies?.Invoke([]);
            }

            logger.ErrorResponse(errorDescription, exception);
            throw exception;
        }
    }

    protected static DataResponse<TData> BuildDataResponse<TData>(HttpResponseHeaders headers, TData data, ILogger logger, DateTimeOffset? expires = null)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(headers);
#else
        if (headers is null)
        {
            throw new ArgumentNullException(nameof(headers));
        }
#endif

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

        response.DataExpires = expires;

        logger.RateLimitsUpdated(response.RateLimitRemaining, response.TotalRateLimit, response.RateLimitReset);

        return response;
    }

    /// <inheritdoc />
    public IEnumerable<Uri> GetTrackAssetScreenshotUris(Tracks.Track track, TrackAssets trackAssets)
    {
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
        var tracksResponse = await GetTracksAsync(cancellationToken).ConfigureAwait(false);

        if (tracksResponse?.Data.FirstOrDefault(t => t.TrackId == trackId) is not Tracks.Track track)
        {
            throw new ArgumentOutOfRangeException(nameof(trackId), "Track identifier supplied could not be located as a valid track.");
        }

        var trackAssetResponse = await GetTrackAssetsAsync(cancellationToken).ConfigureAwait(false);

        var trackIdString = trackId.ToString(CultureInfo.InvariantCulture);

        if (!(trackAssetResponse?.Data.TryGetValue(trackIdString, out var trackAssets) ?? false))
        {
            throw new ArgumentOutOfRangeException(nameof(trackId), "Track identifier supplied could not be used to locate track assets.");
        }

        return GetTrackAssetScreenshotUris(track, trackAssets);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                loginSemaphore.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~DataClient()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
