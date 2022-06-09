// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Common;
using Aydsko.iRacingData.Constants;
using Aydsko.iRacingData.Exceptions;
using Aydsko.iRacingData.Results;

namespace Aydsko.iRacingData.UnitTests;

public class CapturedResponseValidationTests : MockedHttpTestBase
{
    // NUnit will ensure that "SetUp" runs before each test so these can all be forced to "null".
    private DataClient sut = null!;

    [SetUp]
    public void SetUp()
    {
        BaseSetUp();
        sut = new DataClient(HttpClient,
                                    new TestLogger<DataClient>(),
                                    new iRacingDataClientOptions() { Username = "test.user@example.com", Password = "SuperSecretPassword" },
                                    new System.Net.CookieContainer());
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetCarAssetDetailsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetCarAssetDetailsSuccessfulAsync)).ConfigureAwait(false);

        var carAssets = await sut.GetCarAssetDetailsAsync().ConfigureAwait(false);

        Assert.That(carAssets, Is.Not.Null);
        Assert.That(carAssets!.Data, Is.Not.Null);

        Assert.That(carAssets.Data, Has.Count.EqualTo(125));
        Assert.That(carAssets.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(carAssets.TotalRateLimit, Is.EqualTo(100));
        Assert.That(carAssets.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetCarsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetCarsSuccessfulAsync)).ConfigureAwait(false);

        var cars = await sut.GetCarsAsync().ConfigureAwait(false);

        Assert.That(cars, Is.Not.Null);
        Assert.That(cars!.Data, Is.Not.Null);

        Assert.That(cars.Data, Has.Length.EqualTo(125));
        Assert.That(cars.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(cars.TotalRateLimit, Is.EqualTo(100));
        Assert.That(cars.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetCarClassesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetCarClassesSuccessfulAsync)).ConfigureAwait(false);

        var carClasses = await sut.GetCarClassesAsync().ConfigureAwait(false);

        Assert.That(carClasses, Is.Not.Null);
        Assert.That(carClasses!.Data, Is.Not.Null);

        Assert.That(carClasses.Data, Has.Length.EqualTo(161));
        Assert.That(carClasses.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(carClasses.TotalRateLimit, Is.EqualTo(100));
        Assert.That(carClasses.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetDivisionsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetDivisionsSuccessfulAsync)).ConfigureAwait(false);

        var divisionsResponse = await sut.GetDivisionsAsync().ConfigureAwait(false);

        Assert.That(divisionsResponse, Is.Not.Null);
        Assert.That(divisionsResponse!.Data, Is.Not.Null);

        Assert.That(divisionsResponse.Data, Has.Length.EqualTo(12));
        Assert.That(divisionsResponse.Data, Has.One.Property(nameof(Division.Label)).EqualTo("ALL")
                                                   .And.Property(nameof(Division.Value)).EqualTo(-1));

        Assert.That(divisionsResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(divisionsResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(divisionsResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetCategoriesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetCategoriesSuccessfulAsync)).ConfigureAwait(false);

        var divisionsResponse = await sut.GetCategoriesAsync().ConfigureAwait(false);

        Assert.That(divisionsResponse, Is.Not.Null);
        Assert.That(divisionsResponse!.Data, Is.Not.Null);

        Assert.That(divisionsResponse.Data, Has.Length.EqualTo(4));
        Assert.That(divisionsResponse.Data, Has.One.Property(nameof(Division.Label)).EqualTo("Oval")
                                                   .And.Property(nameof(Division.Value)).EqualTo(1));
        Assert.That(divisionsResponse.Data, Has.One.Property(nameof(Division.Label)).EqualTo("Road")
                                                   .And.Property(nameof(Division.Value)).EqualTo(2));
        Assert.That(divisionsResponse.Data, Has.One.Property(nameof(Division.Label)).EqualTo("Dirt oval")
                                                   .And.Property(nameof(Division.Value)).EqualTo(3));
        Assert.That(divisionsResponse.Data, Has.One.Property(nameof(Division.Label)).EqualTo("Dirt road")
                                                   .And.Property(nameof(Division.Value)).EqualTo(4));

        Assert.That(divisionsResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(divisionsResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(divisionsResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetEventTypesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetEventTypesSuccessfulAsync)).ConfigureAwait(false);

        var divisionsResponse = await sut.GetEventTypesAsync().ConfigureAwait(false);

        Assert.That(divisionsResponse, Is.Not.Null);
        Assert.That(divisionsResponse!.Data, Is.Not.Null);

        Assert.That(divisionsResponse.Data, Has.Length.EqualTo(4));
        Assert.That(divisionsResponse.Data, Has.One.Property(nameof(Division.Label)).EqualTo("Practice")
                                                   .And.Property(nameof(Division.Value)).EqualTo(2));
        Assert.That(divisionsResponse.Data, Has.One.Property(nameof(Division.Label)).EqualTo("Qualify")
                                                   .And.Property(nameof(Division.Value)).EqualTo(3));
        Assert.That(divisionsResponse.Data, Has.One.Property(nameof(Division.Label)).EqualTo("Time Trial")
                                                   .And.Property(nameof(Division.Value)).EqualTo(4));
        Assert.That(divisionsResponse.Data, Has.One.Property(nameof(Division.Label)).EqualTo("Race")
                                                   .And.Property(nameof(Division.Value)).EqualTo(5));

        Assert.That(divisionsResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(divisionsResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(divisionsResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetLookupsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetLookupsSuccessfulAsync)).ConfigureAwait(false);

        var lookupGroups = await sut.GetLookupsAsync().ConfigureAwait(false);

        Assert.That(lookupGroups, Is.Not.Null);
        Assert.That(lookupGroups!.Data, Is.Not.Null);

        Assert.That(lookupGroups.Data, Has.Length.EqualTo(2));
        Assert.That(lookupGroups.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(lookupGroups.TotalRateLimit, Is.EqualTo(100));
        Assert.That(lookupGroups.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetLicenseLookupsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetLicenseLookupsSuccessfulAsync)).ConfigureAwait(false);

        var carAssets = await sut.GetLicenseLookupsAsync().ConfigureAwait(false);

        Assert.That(carAssets, Is.Not.Null);
        Assert.That(carAssets!.Data, Is.Not.Null);

        Assert.That(carAssets.Data, Has.Length.EqualTo(7));
        Assert.That(carAssets.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(carAssets.TotalRateLimit, Is.EqualTo(100));
        Assert.That(carAssets.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetDriverInfoWithLicensesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetDriverInfoWithLicensesSuccessfulAsync)).ConfigureAwait(false);

        var carAssets = await sut.GetDriverInfoAsync(new[] { 123456 }, true).ConfigureAwait(false);

        Assert.That(carAssets, Is.Not.Null);
        Assert.That(carAssets!.Data, Is.Not.Null);

        Assert.That(carAssets.Data, Has.Length.EqualTo(1));
        Assert.That(carAssets.Data[0].Licenses, Is.Not.Null);
        Assert.That(carAssets.Data[0].Licenses, Has.Length.EqualTo(4));

        Assert.That(carAssets.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(carAssets.TotalRateLimit, Is.EqualTo(100));
        Assert.That(carAssets.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetDriverInfoWithoutLicensesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetDriverInfoWithoutLicensesSuccessfulAsync)).ConfigureAwait(false);

        var carAssets = await sut.GetDriverInfoAsync(new[] { 123456 }, false).ConfigureAwait(false);

        Assert.That(carAssets, Is.Not.Null);
        Assert.That(carAssets!.Data, Is.Not.Null);

        Assert.That(carAssets.Data, Has.Length.EqualTo(1));
        Assert.That(carAssets.Data[0].Licenses, Is.Null);
        Assert.That(carAssets.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(carAssets.TotalRateLimit, Is.EqualTo(100));
        Assert.That(carAssets.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberInfoSucceedsAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberInfoSucceedsAsync)).ConfigureAwait(false);

        var myInfo = await sut.GetMyInfoAsync().ConfigureAwait(false);

        Assert.That(myInfo, Is.Not.Null);
        Assert.That(myInfo!.Data, Is.Not.Null);

        Assert.That(myInfo.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(myInfo.TotalRateLimit, Is.EqualTo(100));
        Assert.That(myInfo.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberInfoDuringMaintenanceThrowsAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberInfoDuringMaintenanceThrowsAsync)).ConfigureAwait(false);

        Assert.ThrowsAsync<iRacingInMaintenancePeriodException>(async () =>
        {
            var myInfo = await sut.GetMyInfoAsync().ConfigureAwait(false);
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetTracksSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetTracksSuccessfulAsync)).ConfigureAwait(false);

        var tracks = await sut.GetTracksAsync().ConfigureAwait(false);

        Assert.That(tracks, Is.Not.Null);
        Assert.That(tracks!.Data, Is.Not.Null);

        Assert.That(tracks.Data, Has.Length.EqualTo(332));
        Assert.That(tracks.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(tracks.TotalRateLimit, Is.EqualTo(100));
        Assert.That(tracks.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonsWithoutSeriesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonsWithoutSeriesSuccessfulAsync)).ConfigureAwait(false);

        var seasons = await sut.GetSeasonsAsync(false).ConfigureAwait(false);

        Assert.That(seasons, Is.Not.Null);
        Assert.That(seasons!.Data, Is.Not.Null);

        Assert.That(seasons.Data, Has.Length.EqualTo(96));
        Assert.That(seasons.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(seasons.TotalRateLimit, Is.EqualTo(100));
        Assert.That(seasons.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));

#if NET6_0_OR_GREATER
        Assert.That(seasons.Data[0].Schedules[0].StartDate, Is.EqualTo(new DateOnly(2022, 02, 15)));
#else
        Assert.That(seasons.Data[0].Schedules[0].StartDate, Is.EqualTo(new DateTime(2022, 02, 15)));
#endif
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonsWithSeriesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonsWithSeriesSuccessfulAsync)).ConfigureAwait(false);

        var seasonsAndSeries = await sut.GetSeasonsAsync(true).ConfigureAwait(false);

        Assert.That(seasonsAndSeries, Is.Not.Null);
        Assert.That(seasonsAndSeries!.Data, Is.Not.Null);

        Assert.That(seasonsAndSeries.Data, Has.Length.EqualTo(96));
        Assert.That(seasonsAndSeries.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(seasonsAndSeries.TotalRateLimit, Is.EqualTo(100));
        Assert.That(seasonsAndSeries.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));

#if NET6_0_OR_GREATER
        Assert.That(seasonsAndSeries.Data[0].Schedules[0].StartDate, Is.EqualTo(new DateOnly(2022, 02, 15)));
#else
        Assert.That(seasonsAndSeries.Data[0].Schedules[0].StartDate, Is.EqualTo(new DateTime(2022, 02, 15)));
#endif
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetStatisticsSeriesSuccesfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetStatisticsSeriesSuccesfulAsync)).ConfigureAwait(false);

        var statsSeriesResponse = await sut.GetStatisticsSeriesAsync(CancellationToken.None).ConfigureAwait(false);

        Assert.That(statsSeriesResponse, Is.Not.Null);
        Assert.That(statsSeriesResponse!.Data, Is.Not.Null);
        Assert.That(statsSeriesResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(statsSeriesResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(statsSeriesResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeriesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeriesSuccessfulAsync)).ConfigureAwait(false);

        var seriesResponse = await sut.GetSeriesAsync(CancellationToken.None).ConfigureAwait(false);

        Assert.That(seriesResponse, Is.Not.Null);
        Assert.That(seriesResponse!.Data, Is.Not.Null);
        Assert.That(seriesResponse!.Data, Has.Length.EqualTo(37));
        Assert.That(seriesResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(seriesResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(seriesResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetTrackAssetsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetTrackAssetsSuccessfulAsync)).ConfigureAwait(false);

        var trackAssets = await sut.GetTrackAssetsAsync().ConfigureAwait(false);

        Assert.That(trackAssets, Is.Not.Null);
        Assert.That(trackAssets!.Data, Is.Not.Null);

        Assert.That(trackAssets.Data, Has.Count.EqualTo(332));
        Assert.That(trackAssets.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(trackAssets.TotalRateLimit, Is.EqualTo(100));
        Assert.That(trackAssets.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonResultsHandlesBadRequestAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonResultsHandlesBadRequestAsync)).ConfigureAwait(false);

        Assert.That(async () =>
        {
            var badRequestResult = await sut.GetSeasonResultsAsync(0, Common.EventType.Race, 0, CancellationToken.None).ConfigureAwait(false);
        }, Throws.Exception.InstanceOf(typeof(HttpRequestException)).And.Message.Contains("400 (Bad Request)"));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberYearlyStatisticsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberYearlyStatisticsSuccessfulAsync)).ConfigureAwait(false);

        var memberStats = await sut.GetMemberYearlyStatisticsAsync().ConfigureAwait(false);

        Assert.That(memberStats, Is.Not.Null);
        Assert.That(memberStats!.Data, Is.Not.Null);

        Assert.That(memberStats.Data.Statistics, Has.Length.EqualTo(2));
        Assert.That(memberStats.Data.CustomerId, Is.EqualTo(123456));
        Assert.That(memberStats.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(memberStats.TotalRateLimit, Is.EqualTo(100));
        Assert.That(memberStats.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberRecentRacesSucceedsAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberRecentRacesSucceedsAsync)).ConfigureAwait(false);

        var memberStats = await sut.GetMemberRecentRacesAsync().ConfigureAwait(false);

        Assert.That(memberStats, Is.Not.Null);
        Assert.That(memberStats!.Data, Is.Not.Null);

        Assert.That(memberStats.Data.Races, Has.Length.EqualTo(10));
        Assert.That(memberStats.Data.CustomerId, Is.EqualTo(123456));
        Assert.That(memberStats.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(memberStats.TotalRateLimit, Is.EqualTo(100));
        Assert.That(memberStats.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberSummarySuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberSummarySuccessfulAsync)).ConfigureAwait(false);

        var memberSummaryResponse = await sut.GetMemberSummaryAsync().ConfigureAwait(false);

        Assert.That(memberSummaryResponse, Is.Not.Null);
        Assert.That(memberSummaryResponse!.Data, Is.Not.Null);

        //Assert.That(memberSummaryResponse.Data.Races, Has.Length.EqualTo(10));
        Assert.That(memberSummaryResponse.Data.CustomerId, Is.EqualTo(123456));
        Assert.That(memberSummaryResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(memberSummaryResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(memberSummaryResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberDivisionSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberDivisionSuccessfulAsync)).ConfigureAwait(false);

        var memberDivisionResponse = await sut.GetMemberDivisionAsync(1234, Common.EventType.Race, CancellationToken.None).ConfigureAwait(false);

        Assert.That(memberDivisionResponse, Is.Not.Null);
        Assert.That(memberDivisionResponse.Data, Is.Not.Null);

        Assert.That(memberDivisionResponse.Data.Success, Is.True);
        Assert.That(memberDivisionResponse.Data.SeasonId, Is.EqualTo(1234));
        Assert.That(memberDivisionResponse.Data.EventType, Is.EqualTo(Common.EventType.Race));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetLeagueWithLicensesSucceedsAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetLeagueWithLicensesSucceedsAsync)).ConfigureAwait(false);

        var memberStats = await sut.GetLeagueAsync(123, true).ConfigureAwait(false);

        Assert.That(memberStats, Is.Not.Null);
        Assert.That(memberStats!.Data, Is.Not.Null);

        Assert.That(memberStats.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(memberStats.TotalRateLimit, Is.EqualTo(100));
        Assert.That(memberStats.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetLeagueWithoutLicensesSucceedsAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetLeagueWithoutLicensesSucceedsAsync)).ConfigureAwait(false);

        var memberStats = await sut.GetLeagueAsync(123, false).ConfigureAwait(false);

        Assert.That(memberStats, Is.Not.Null);
        Assert.That(memberStats!.Data, Is.Not.Null);

        Assert.That(memberStats.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(memberStats.TotalRateLimit, Is.EqualTo(100));
        Assert.That(memberStats.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubSessionLapChartSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSubSessionLapChartSuccessfulAsync)).ConfigureAwait(false);

        var lapChartResponse = await sut.GetSubSessionLapChartAsync(12345, 0).ConfigureAwait(false);

        Assert.That(lapChartResponse, Is.Not.Null);
        Assert.That(lapChartResponse!.Data, Is.Not.Null);

        Assert.That(lapChartResponse.Data.Header, Is.Not.Null);
        Assert.That(lapChartResponse.Data.Header.Success, Is.True);
        Assert.That(lapChartResponse.Data.Header.SessionInfo, Is.Not.Null);
        Assert.That(lapChartResponse.Data.Laps, Has.Length.EqualTo(417));
        Assert.That(lapChartResponse.Data.Laps, Has.ItemAt(0).Property(nameof(SubsessionChartLap.GroupId)).EqualTo(523780)
                                                   .And.ItemAt(0).Property(nameof(SubsessionChartLap.Name)).EqualTo("Jake Dennis")
                                                   .And.ItemAt(0).Property(nameof(SubsessionChartLap.LapPosition)).EqualTo(1));

        Assert.That(lapChartResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(lapChartResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(lapChartResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonDriverStandingsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonDriverStandingsSuccessfulAsync)).ConfigureAwait(false);

        var seasonDriverStandingsResponse = await sut.GetSeasonDriverStandingsAsync(1234, 9, 0, CancellationToken.None).ConfigureAwait(false);

        Assert.That(seasonDriverStandingsResponse, Is.Not.Null);
        Assert.That(seasonDriverStandingsResponse!.Data, Is.Not.Null);

        Assert.That(seasonDriverStandingsResponse.Data.Header, Is.Not.Null);
        Assert.That(seasonDriverStandingsResponse.Data.Header.Success, Is.True);

        Assert.That(seasonDriverStandingsResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(seasonDriverStandingsResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(seasonDriverStandingsResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSingleDriverSubsessionLapsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSingleDriverSubsessionLapsSuccessfulAsync)).ConfigureAwait(false);

        var lapChartResponse = await sut.GetSingleDriverSubsessionLapsAsync(12345, 0, 123456).ConfigureAwait(false);

        Assert.That(lapChartResponse, Is.Not.Null);
        Assert.That(lapChartResponse!.Data, Is.Not.Null);

        Assert.That(lapChartResponse.Data.Header, Is.Not.Null);
        Assert.That(lapChartResponse.Data.Header.Success, Is.True);
        Assert.That(lapChartResponse.Data.Header.SessionInfo, Is.Not.Null);
        Assert.That(lapChartResponse.Data.Laps, Has.Length.EqualTo(16));
        Assert.That(lapChartResponse.Data.Laps, Has.ItemAt(0).Property(nameof(SubsessionLap.GroupId)).EqualTo(341554)
                                                   .And.ItemAt(0).Property(nameof(SubsessionLap.Name)).EqualTo("Adrian Clark")
                                                   .And.ItemAt(0).Property(nameof(SubsessionLap.Incident)).EqualTo(false));

        Assert.That(lapChartResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(lapChartResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(lapChartResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));

    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSingleDriverSubsessionLapsForbiddenThrowsErrorsAsync()
    {
        await MessageHandler.QueueResponsesAsync("ResponseForbidden").ConfigureAwait(false);

        Assert.ThrowsAsync<iRacingForbiddenResponseException>(async () =>
        {
            var lapChartResponse = await sut.GetSingleDriverSubsessionLapsAsync(12345, 0, 123456).ConfigureAwait(false);
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetTeamSubsessionLapsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetTeamSubsessionLapsSuccessfulAsync)).ConfigureAwait(false);

        var lapChartResponse = await sut.GetTeamSubsessionLapsAsync(12345, 0, 123456).ConfigureAwait(false);

        Assert.That(lapChartResponse, Is.Not.Null);
        Assert.That(lapChartResponse!.Data, Is.Not.Null);

        Assert.That(lapChartResponse.Data.Header, Is.Not.Null);
        Assert.That(lapChartResponse.Data.Header.Success, Is.True);
        Assert.That(lapChartResponse.Data.Header.SessionInfo, Is.Not.Null);
        Assert.That(lapChartResponse.Data.Laps, Has.Length.EqualTo(500));
        Assert.That(lapChartResponse.Data.Laps, Has.ItemAt(0).Property(nameof(SubsessionLap.GroupId)).EqualTo(-93376)
                                                   .And.ItemAt(0).Property(nameof(SubsessionLap.Name)).EqualTo("Tempest Motorsports Neon")
                                                   .And.ItemAt(0).Property(nameof(SubsessionLap.Incident)).EqualTo(false));

        Assert.That(lapChartResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(lapChartResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(lapChartResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetTeamSubsessionLapsForbiddenThrowsErrorsAsync()
    {
        await MessageHandler.QueueResponsesAsync("ResponseForbidden").ConfigureAwait(false);

        Assert.ThrowsAsync<iRacingForbiddenResponseException>(async () =>
        {
            var lapChartResponse = await sut.GetTeamSubsessionLapsAsync(12345, 0, 123456).ConfigureAwait(false);
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubSessionResultSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSubSessionResultSuccessfulAsync)).ConfigureAwait(false);

        var subSessionResultResponse = await sut.GetSubSessionResultAsync(12345, false, CancellationToken.None).ConfigureAwait(false);

        Assert.That(subSessionResultResponse, Is.Not.Null);
        Assert.That(subSessionResultResponse!.Data, Is.Not.Null);

        Assert.That(subSessionResultResponse.Data.SeasonId, Is.EqualTo(3620));
        Assert.That(subSessionResultResponse.Data.SeriesName, Is.EqualTo("Global Fanatec Challenge"));
        Assert.That(subSessionResultResponse.Data.SessionResults, Has.Length.EqualTo(2));
        Assert.That(subSessionResultResponse.Data.SessionResults, Has.One.Property(nameof(SessionResults.SimSessionName)).EqualTo("RACE"));

        var raceResults = subSessionResultResponse.Data.SessionResults.Single(r => r.SimSessionName == "RACE");
        Assert.That(raceResults.Results, Has.All.Property(nameof(Result.DriverResults)).Null); // Single-driver events don't have driver results.
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubSessionResultForTeamSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSubSessionResultForTeamSuccessfulAsync)).ConfigureAwait(false);

        var subSessionResultResponse = await sut.GetSubSessionResultAsync(12345, false, CancellationToken.None).ConfigureAwait(false);

        Assert.That(subSessionResultResponse, Is.Not.Null);
        Assert.That(subSessionResultResponse!.Data, Is.Not.Null);

        Assert.That(subSessionResultResponse.Data.SeasonId, Is.EqualTo(3720));
        Assert.That(subSessionResultResponse.Data.SeriesName, Is.EqualTo("Nurburgring Endurance Championship"));
        Assert.That(subSessionResultResponse.Data.SessionResults, Has.Length.EqualTo(3));
        Assert.That(subSessionResultResponse.Data.SessionResults, Has.One.Property(nameof(SessionResults.SimSessionName)).EqualTo("RACE"));

        var raceResults = subSessionResultResponse.Data.SessionResults.Single(r => r.SimSessionName == "RACE");
        Assert.That(raceResults.Results, Has.All.Property(nameof(Result.DriverResults)).Not.Null); // Team events should have driver results.

        var nollerRacing = raceResults.Results.SingleOrDefault(r => r.TeamId == -208016);
        Assert.That(nollerRacing?.DriverResults, Is.Empty.And.Not.Null);

        var racingSociety = raceResults.Results.SingleOrDefault(r => r.TeamId == -261181);
        Assert.That(racingSociety?.DriverResults, Has.Length.EqualTo(2));
        Assert.That(racingSociety?.DriverResults, Has.One.Property(nameof(DriverResult.CustomerId)).EqualTo(696075));
        Assert.That(racingSociety?.DriverResults, Has.One.Property(nameof(DriverResult.CustomerId)).EqualTo(669671));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubSessionResultForbiddenThrowsErrorsAsync()
    {
        await MessageHandler.QueueResponsesAsync("ResponseForbidden").ConfigureAwait(false);

        Assert.ThrowsAsync<iRacingForbiddenResponseException>(async () =>
        {
            var lapChartResponse = await sut.GetSubSessionResultAsync(12345, false).ConfigureAwait(false);
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubsessionEventLogSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSubsessionEventLogSuccessfulAsync)).ConfigureAwait(false);

        var subSessionResultResponse = await sut.GetSubsessionEventLogAsync(12345, 0, CancellationToken.None).ConfigureAwait(false);

        Assert.That(subSessionResultResponse, Is.Not.Null);
        Assert.That(subSessionResultResponse!.Data, Is.Not.Null);
        Assert.That(subSessionResultResponse.Data.Header, Is.Not.Null);
        Assert.That(subSessionResultResponse.Data.LogItems, Is.Not.Null);
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubsessionEventLogForbiddenThrowsErrorsAsync()
    {
        await MessageHandler.QueueResponsesAsync("ResponseForbidden").ConfigureAwait(false);

        Assert.ThrowsAsync<iRacingForbiddenResponseException>(async () =>
        {
            var lapChartResponse = await sut.GetSubsessionEventLogAsync(12345, 0).ConfigureAwait(false);
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonQualifyResultsSuccesfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonQualifyResultsSuccesfulAsync)).ConfigureAwait(false);

        var lapChartResponse = await sut.GetSeasonQualifyResultsAsync(3587, 71, 0).ConfigureAwait(false);

        Assert.That(lapChartResponse, Is.Not.Null);
        Assert.That(lapChartResponse!.Data, Is.Not.Null);

        Assert.That(lapChartResponse.Data.Header, Is.Not.Null);
        Assert.That(lapChartResponse.Data.Header.Success, Is.True);
        Assert.That(lapChartResponse.Data.Header.ChunkInfo, Is.Not.Null);
        Assert.That(lapChartResponse.Data.Results, Has.Length.EqualTo(3078));

        Assert.That(lapChartResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(lapChartResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(lapChartResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonTimeTrialResultsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonTimeTrialResultsSuccessfulAsync)).ConfigureAwait(false);

        var timeTrialResponse = await sut.GetSeasonTimeTrialResultsAsync(3587, 71, 0).ConfigureAwait(false);

        Assert.That(timeTrialResponse, Is.Not.Null);
        Assert.That(timeTrialResponse!.Data, Is.Not.Null);

        Assert.That(timeTrialResponse.Data.Header, Is.Not.Null);
        Assert.That(timeTrialResponse.Data.Header.Success, Is.True);
        Assert.That(timeTrialResponse.Data.Header.ChunkInfo, Is.Not.Null);
        Assert.That(timeTrialResponse.Data.Results, Has.Length.EqualTo(60));

        Assert.That(timeTrialResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(timeTrialResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(timeTrialResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonTimeTrialStandingsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonTimeTrialStandingsSuccessfulAsync)).ConfigureAwait(false);

        var timeTrialResponse = await sut.GetSeasonTimeTrialStandingsAsync(3587, 71, 0, CancellationToken.None).ConfigureAwait(false);

        Assert.That(timeTrialResponse, Is.Not.Null);
        Assert.That(timeTrialResponse!.Data, Is.Not.Null);

        Assert.That(timeTrialResponse.Data.Header, Is.Not.Null);
        Assert.That(timeTrialResponse.Data.Header.Success, Is.True);
        Assert.That(timeTrialResponse.Data.Header.ChunkInfo, Is.Not.Null);
        Assert.That(timeTrialResponse.Data.Standings, Has.Length.EqualTo(60));

        Assert.That(timeTrialResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(timeTrialResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(timeTrialResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonTeamStandingsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonTeamStandingsSuccessfulAsync)).ConfigureAwait(false);

        var timeTrialResponse = await sut.GetSeasonTeamStandingsAsync(3587, 71, 0, CancellationToken.None).ConfigureAwait(false);

        Assert.That(timeTrialResponse, Is.Not.Null);
        Assert.That(timeTrialResponse!.Data, Is.Not.Null);

        Assert.That(timeTrialResponse.Data.Header, Is.Not.Null);
        Assert.That(timeTrialResponse.Data.Header.Success, Is.True);
        Assert.That(timeTrialResponse.Data.Header.ChunkInfo, Is.Not.Null);
        Assert.That(timeTrialResponse.Data.Standings, Has.Length.EqualTo(192));

        Assert.That(timeTrialResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(timeTrialResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(timeTrialResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }
}
