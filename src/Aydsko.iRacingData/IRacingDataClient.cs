// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.CarClasses;
using Aydsko.iRacingData.Cars;
using Aydsko.iRacingData.Lookups;
using Aydsko.iRacingData.Member;
using Aydsko.iRacingData.Results;
using Aydsko.iRacingData.Series;
using Aydsko.iRacingData.Stats;
using Aydsko.iRacingData.Tracks;
using Microsoft.AspNetCore.WebUtilities;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;

namespace Aydsko.iRacingData;

public class iRacingDataClient
{
    private readonly HttpClient httpClient;
    private readonly ILogger<iRacingDataClient> logger;

    public bool IsLoggedIn { get; private set; }

    public iRacingDataClient(HttpClient httpClient, ILogger<iRacingDataClient> logger)
    {
        this.httpClient = httpClient;
        this.logger = logger;
    }

    /// <summary>Authenticate with the iRacing "/data" API.</summary>
    /// <param name="userEmail">The email for a valid iRacing account.</param>
    /// <param name="userPassword">The password associated with the valid iRacing account.</param>
    /// <exception cref="ArgumentException">If either <paramref name="userEmail"/> or <paramref name="userPassword"/> is <see langword="null"/> or whitespace.</exception>
    public async Task LoginAsync(string userEmail, string userPassword, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userEmail))
        {
            throw new ArgumentException($"'{nameof(userEmail)}' cannot be null or whitespace.", nameof(userEmail));
        }

        if (string.IsNullOrWhiteSpace(userPassword))
        {
            throw new ArgumentException($"'{nameof(userPassword)}' cannot be null or whitespace.", nameof(userPassword));
        }

        var loginResponse = await httpClient.PostAsJsonAsync("https://members-ng.iracing.com/auth",
                                                             new { email = userEmail, password = userPassword },
                                                             cancellationToken)
                                            .ConfigureAwait(false);
        loginResponse.EnsureSuccessStatusCode();
        IsLoggedIn = true;
        logger.LoginSuccessful(userEmail);
    }

    /// <summary>Retrieves details about the car assets, including image paths and descriptions.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing a dictionary which maps the car identifier to a <see cref="CarAssetDetail"/> object for each car.</returns>
    /// <remarks>All image paths are relative to <c>https://images-static.iracing.com</c>.</remarks>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="Exception">If there's a problem processing the result.</exception>
    public async Task<DataResponse<IReadOnlyDictionary<string, CarAssetDetail>>> GetCarAssetDetailsAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            throw new InvalidOperationException("Must be logged in before requesting data.");
        }

        var carAssetDetailsUrl = new Uri("https://members-ng.iracing.com/data/car/assets");
        return await CreateResponseViaInfoLinkAsync(carAssetDetailsUrl, CarAssetDetailDictionaryContext.Default.IReadOnlyDictionaryStringCarAssetDetail, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Retrieves details about the cars.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="CarInfo"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="Exception">If there's a problem processing the result.</exception>
    public async Task<DataResponse<CarInfo[]>> GetCarsAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            throw new InvalidOperationException("Must be logged in before requesting data.");
        }

        var carInfoUrl = new Uri("https://members-ng.iracing.com/data/car/get");
        return await CreateResponseViaInfoLinkAsync(carInfoUrl, CarInfoArrayContext.Default.CarInfoArray, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Retrieves details about the car classes.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="CarClass"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="Exception">If there's a problem processing the result.</exception>
    public async Task<DataResponse<CarClass[]>> GetCarClassesAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            throw new InvalidOperationException("Must be logged in before requesting data.");
        }

        var carClassUrl = new Uri("https://members-ng.iracing.com/data/carclass/get");
        return await CreateResponseViaInfoLinkAsync(carClassUrl, CarClassArrayContext.Default.CarClassArray, cancellationToken).ConfigureAwait(false);
    }

    // TODO - public async Task<DataResponse<League>> GetLeagueAsync(int leagueId, bool includeLicenses = false, CancellationToken cancellationToken = default)

    /// <summary>Information about reference data defined by the system.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="LookupGroup"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="Exception">If there's a problem processing the result.</exception>
    public async Task<DataResponse<LookupGroup[]>> GetLookupsAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            throw new InvalidOperationException("Must be logged in before requesting data.");
        }

        var lookupsUrl = new Uri("https://members-ng.iracing.com/data/lookup/get?weather=weather_wind_speed_units&weather=weather_wind_speed_max&weather=weather_wind_speed_min&licenselevels=licenselevels");
        return await CreateResponseViaInfoLinkAsync(lookupsUrl, LookupGroupArrayContext.Default.LookupGroupArray, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Information about license levels available in the iRacing system.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="License"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="Exception">If there's a problem processing the result.</exception>
    public async Task<DataResponse<License[]>> GetLicensesAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            throw new InvalidOperationException("Must be logged in before requesting data.");
        }

        var licenseUrl = new Uri("https://members-ng.iracing.com/data/lookup/licenses");
        return await CreateResponseViaInfoLinkAsync(licenseUrl, LicenseArrayContext.Default.LicenseArray, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Retrieve information about one or more other drivers by their customer identifier.</summary>
    /// <param name="customerIds">An array of one or more customer identifiers.</param>
    /// <param name="includeLicenses">Indicates if license information should be included. Either <see langword="true"/> or <see langword="false"/> to exclude for performance purposes.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing an array of <see cref="DriverInfo"/> objects.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    public async Task<DataResponse<DriverInfo[]>> GetDriverInfoAsync(int[] customerIds, bool includeLicenses, CancellationToken cancellationToken = default)
    {
        if (customerIds is not { Length: > 0 })
        {
            throw new ArgumentOutOfRangeException(nameof(customerIds), "Must supply at least one customer identifier value to query.");
        }

        if (!IsLoggedIn)
        {
            throw new InvalidOperationException("Must be logged in before requesting data.");
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

        return CreateResponse(infoLinkResponse.Headers, info.Drivers!, logger);
    }

    /// <summary>Retrieve the <see cref="MemberInfo"/> for the currently authenticated user.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the statistics in a <see cref="MemberInfo"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    public async Task<DataResponse<MemberInfo>> GetMyInfoAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            throw new InvalidOperationException("Must be logged in before requesting data.");
        }
        var memberInfoUrl = new Uri("https://members-ng.iracing.com/data/member/info");
        return await CreateResponseViaInfoLinkAsync(memberInfoUrl, MemberInfoContext.Default.MemberInfo, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Get the results of a subsession, if the authenticated user is authorized to view them.</summary>
    /// <param name="subSessionId">The identifier of the subsession for which results should be returned.</param>
    /// <param name="includeLicenses">Indicates if license information should be included. Either <see langword="true"/> or <see langword="false"/> to exclude for performance purposes.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the result details in a <see cref="SubSessionResult"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    public async Task<DataResponse<SubSessionResult>> GetSubSessionResultAsync(int subSessionId, bool includeLicenses, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            throw new InvalidOperationException("Must be logged in before requesting data.");
        }

        var subSessionResultUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/results/get", new Dictionary<string, string>
        {
            { "subsession_id", subSessionId.ToString(CultureInfo.InvariantCulture) },
            { "include_licenses", includeLicenses ? "true" : "false" }
        });

        return await CreateResponseViaInfoLinkAsync(new Uri(subSessionResultUrl), SubSessionResultContext.Default.SubSessionResult, cancellationToken).ConfigureAwait(false);
    }

    // TODO - "results/seson_results" data

    /// <summary>Retrieve information about the season & series.</summary>
    /// <param name="includeSeries">Indicate if the series details should be included.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the season & optionally series detail in a <see cref="SeasonSeries"/> array.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    public async Task<DataResponse<SeasonSeries[]>> GetSeasonsAsync(bool includeSeries, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            throw new InvalidOperationException("Must be logged in before requesting data.");
        }

        var seasonSeriesUrl = QueryHelpers.AddQueryString("https://members-ng.iracing.com/data/series/seasons", new Dictionary<string, string>
        {
            { "include_series", includeSeries ? "true" : "false" }
        });
        return await CreateResponseViaInfoLinkAsync(new Uri(seasonSeriesUrl), SeasonSeriesArrayContext.Default.SeasonSeriesArray, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Return a summary of statistics for the given customer's career or that or the authenticated user.</summary>
    /// <param name="customerId">iRacing Customer Id for the member to return statistics for, or <see langword="null"/> for the currently authenticated user.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the statistics in a <see cref="MemberCareer"/> object.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    public async Task<DataResponse<MemberCareer>> GetCareerStatisticsAsync(int? customerId, CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            throw new InvalidOperationException("Must be logged in before requesting data.");
        }

        var careerStatisticsUrl = "https://members-ng.iracing.com/data/stats/member_career";
        if (customerId is not null)
        {
            careerStatisticsUrl = QueryHelpers.AddQueryString(careerStatisticsUrl, new Dictionary<string, string>
            {
                { "cust_id", customerId.Value.ToString(CultureInfo.InvariantCulture) }
            });
        }
        return await CreateResponseViaInfoLinkAsync(new Uri(careerStatisticsUrl), MemberCareerContext.Default.MemberCareer, cancellationToken).ConfigureAwait(false);
    }

    // TODO - "stats/member_recent_races"

    // TODO - "stats/member_yearly"

    /// <summary>Retrieve information about the track assets.</summary>
    /// <remarks>Image paths are relative to https://images-static.iracing.com/.</remarks>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the season & optionally series detail in a <see cref="Tracks.Track"/> array.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    public async Task<DataResponse<IReadOnlyDictionary<string, TrackAssets>>> GetTrackAssetsAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            throw new InvalidOperationException("Must be logged in before requesting data.");
        }

        var getTrackUrl = "https://members-ng.iracing.com/data/track/assets";
        return await CreateResponseViaInfoLinkAsync(new Uri(getTrackUrl), TrackAssetsArrayContext.Default.IReadOnlyDictionaryStringTrackAssets, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Retrieve information about the tracks.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="DataResponse{TData}"/> containing the season & optionally series detail in a <see cref="Tracks.Track"/> array.</returns>
    /// <exception cref="InvalidOperationException">If the client is not currently authenticated.</exception>
    /// <exception cref="iRacingDataClientException">If there's a problem processing the result.</exception>
    public async Task<DataResponse<Tracks.Track[]>> GetTracksAsync(CancellationToken cancellationToken = default)
    {
        if (!IsLoggedIn)
        {
            throw new InvalidOperationException("Must be logged in before requesting data.");
        }

        var getTrackUrl = "https://members-ng.iracing.com/data/track/get";
        return await CreateResponseViaInfoLinkAsync(new Uri(getTrackUrl), TrackArrayContext.Default.TrackArray, cancellationToken).ConfigureAwait(false);
    }

    private async Task<DataResponse<TData>> CreateResponseViaInfoLinkAsync<TData>(Uri infoLinkUri, JsonTypeInfo<TData> jsonTypeInfo, CancellationToken cancellationToken)
    {
        var infoLinkResponse = await httpClient.GetAsync(infoLinkUri, cancellationToken).ConfigureAwait(false);
        if (!infoLinkResponse.IsSuccessStatusCode)
        {
            var errorResponse = await infoLinkResponse.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: cancellationToken)
                                                              .ConfigureAwait(false);

            var exception = errorResponse switch {
                { ErrorCode: "Site Maintenance" } => new iRacingInMaintenancePeriodException(errorResponse.ErrorDescription ?? "iRacing services are down for maintenance."),
                _ => null
            };

            if (exception is null)
            {
                logger.ErrorResponseUnknown();
                infoLinkResponse.EnsureSuccessStatusCode();
            }
            else
            {
                logger.ErrorResponse(errorResponse!.ErrorDescription, exception);
                throw exception;
            }
        }

        var infoLink = await infoLinkResponse.Content.ReadFromJsonAsync(LinkResultContext.Default.LinkResult, cancellationToken).ConfigureAwait(false);
        if (infoLink?.Link is null)
        {
            throw new iRacingDataClientException("Unrecognised result.");
        }

        var data = await httpClient.GetFromJsonAsync(infoLink.Link, jsonTypeInfo, cancellationToken: cancellationToken).ConfigureAwait(false);
        if (data is null)
        {
            throw new iRacingDataClientException("Data not found.");
        }

        return CreateResponse(infoLinkResponse.Headers, data, logger)!;
    }

    private static DataResponse<TData> CreateResponse<TData>(HttpResponseHeaders headers, TData data, ILogger logger)
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
