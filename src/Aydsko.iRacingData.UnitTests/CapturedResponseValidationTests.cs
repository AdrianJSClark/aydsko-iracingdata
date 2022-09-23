// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Constants;
using Aydsko.iRacingData.Exceptions;
using Aydsko.iRacingData.Leagues;
using Aydsko.iRacingData.Results;
using Aydsko.iRacingData.Searches;
using Aydsko.iRacingData.Series;

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
                             new iRacingDataClientOptions()
                             {
                                 Username = "test.user@example.com",
                                 Password = "SuperSecretPassword",
                                 CurrentDateTimeSource = () => new DateTimeOffset(2022, 04, 05, 0, 0, 0, TimeSpan.Zero)
                             },
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
        Assert.That(carAssets.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(cars.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(carClasses.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(lookupGroups.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(carAssets.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetClubHistoryLookupsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetClubHistoryLookupsSuccessfulAsync)).ConfigureAwait(false);

        var clubHistoryLookups = await sut.GetClubHistoryLookupsAsync(2022, 1).ConfigureAwait(false);

        Assert.That(clubHistoryLookups, Is.Not.Null);
        Assert.That(clubHistoryLookups!.Data, Is.Not.Null);

        Assert.That(clubHistoryLookups.Data, Has.Length.EqualTo(42));
        Assert.That(clubHistoryLookups.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(clubHistoryLookups.TotalRateLimit, Is.EqualTo(100));
        Assert.That(clubHistoryLookups.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        Assert.That(clubHistoryLookups.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(carAssets.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(carAssets.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(myInfo.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberProfileSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberProfileSuccessfulAsync)).ConfigureAwait(false);

        var memberProfileResponse = await sut.GetMemberProfileAsync().ConfigureAwait(false);

        Assert.That(memberProfileResponse, Is.Not.Null);
        Assert.That(memberProfileResponse!.Data, Is.Not.Null);

        Assert.That(memberProfileResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(memberProfileResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(memberProfileResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        Assert.That(memberProfileResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task SearchDriversSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(SearchDriversSuccessfulAsync)).ConfigureAwait(false);

        var memberProfileResponse = await sut.SearchDriversAsync("123456").ConfigureAwait(false);

        Assert.That(memberProfileResponse, Is.Not.Null);
        Assert.That(memberProfileResponse!.Data, Is.Not.Null);
        Assert.That(memberProfileResponse.Data, Has.Length.EqualTo(1));

        Assert.That(memberProfileResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(memberProfileResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(memberProfileResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        Assert.That(memberProfileResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(tracks.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(seasons.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));

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
        Assert.That(seasonsAndSeries.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));

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
        Assert.That(statsSeriesResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(seriesResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeriesAssetsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeriesAssetsSuccessfulAsync)).ConfigureAwait(false);

        var seriesResponse = await sut.GetSeriesAssetsAsync(CancellationToken.None).ConfigureAwait(false);

        Assert.That(seriesResponse, Is.Not.Null);
        Assert.That(seriesResponse!.Data, Is.Not.Null);
        Assert.That(seriesResponse!.Data, Has.Count.EqualTo(37));
        Assert.That(seriesResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(seriesResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(seriesResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        Assert.That(seriesResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetTrackAssetsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetTrackAssetsSuccessfulAsync)).ConfigureAwait(false);

        var trackAssets = await sut.GetTrackAssetsAsync().ConfigureAwait(false);

        Assert.That(trackAssets, Is.Not.Null);
        Assert.That(trackAssets!.Data, Is.Not.Null);

        Assert.That(trackAssets.Data, Has.Count.EqualTo(340));
        Assert.That(trackAssets.Data.ContainsKey("1"), Is.True);

        var limeRockPark = trackAssets.Data["1"];

        Assert.That(limeRockPark, Is.Not.Null);
        Assert.That(limeRockPark.Coordinates, Is.EqualTo("41.9282105,-73.3839642"));
        Assert.That(limeRockPark.TrackMap, Is.EqualTo("https://dqfp1ltauszrc.cloudfront.net/public/track-maps/tracks_limerock/1-limerock-full/"));
        Assert.That(limeRockPark.TrackMapLayers, Is.Not.Null);

        Assert.That(limeRockPark.TrackMapLayers.Background, Is.EqualTo("background.svg"));
        Assert.That(limeRockPark.TrackMapLayers.Inactive, Is.EqualTo("inactive.svg"));
        Assert.That(limeRockPark.TrackMapLayers.Active, Is.EqualTo("active.svg"));
        Assert.That(limeRockPark.TrackMapLayers.PitRoad, Is.EqualTo("pitroad.svg"));
        Assert.That(limeRockPark.TrackMapLayers.StartFinish, Is.EqualTo("start-finish.svg"));
        Assert.That(limeRockPark.TrackMapLayers.Turns, Is.EqualTo("turns.svg"));

        Assert.That(trackAssets.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(trackAssets.TotalRateLimit, Is.EqualTo(100));
        Assert.That(trackAssets.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        Assert.That(trackAssets.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(memberStats.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(memberStats.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(memberSummaryResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(memberDivisionResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(memberStats.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(memberStats.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(lapChartResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonDriverStandingsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonDriverStandingsSuccessfulAsync)).ConfigureAwait(false);

        var seasonDriverStandingsResponse = await sut.GetSeasonDriverStandingsAsync(1234, 9, 0, cancellationToken: CancellationToken.None).ConfigureAwait(false);

        Assert.That(seasonDriverStandingsResponse, Is.Not.Null);
        Assert.That(seasonDriverStandingsResponse!.Data, Is.Not.Null);

        Assert.That(seasonDriverStandingsResponse.Data.Header, Is.Not.Null);
        Assert.That(seasonDriverStandingsResponse.Data.Header.Success, Is.True);

        Assert.That(seasonDriverStandingsResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(seasonDriverStandingsResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(seasonDriverStandingsResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        Assert.That(seasonDriverStandingsResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(lapChartResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(lapChartResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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

        Assert.That(subSessionResultResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(subSessionResultResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(subSessionResultResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        Assert.That(subSessionResultResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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

        Assert.That(subSessionResultResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(subSessionResultResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(subSessionResultResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        Assert.That(subSessionResultResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubSessionResultForLeagueSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSubSessionResultForLeagueSuccessfulAsync)).ConfigureAwait(false);

        var subSessionResultResponse = await sut.GetSubSessionResultAsync(12345, false, CancellationToken.None).ConfigureAwait(false);

        Assert.That(subSessionResultResponse, Is.Not.Null);
        Assert.That(subSessionResultResponse!.Data, Is.Not.Null);

        Assert.That(subSessionResultResponse.Data.SeasonId, Is.EqualTo(0));
        Assert.That(subSessionResultResponse.Data.SeriesName, Is.EqualTo("Hosted iRacing"));
        Assert.That(subSessionResultResponse.Data.SessionResults, Has.Length.EqualTo(3));
        Assert.That(subSessionResultResponse.Data.SessionResults, Has.One.Property(nameof(SessionResults.SimSessionName)).EqualTo("RACE"));

        Assert.That(subSessionResultResponse.Data.LeagueId, Is.EqualTo(5453));
        Assert.That(subSessionResultResponse.Data.LeagueName, Is.EqualTo("Snail Speed Racing - GT3 League"));
        Assert.That(subSessionResultResponse.Data.LeagueSeasonName, Is.EqualTo("2022-S2"));
        Assert.That(subSessionResultResponse.Data.HostId, Is.EqualTo(411093));

        Assert.That(subSessionResultResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(subSessionResultResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(subSessionResultResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        Assert.That(subSessionResultResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
    public async Task GetSubSessionResultUnauthorizedThrowsErrorsAsync()
    {
        await MessageHandler.QueueResponsesAsync("ResponseUnauthorized").ConfigureAwait(false);

        Assert.ThrowsAsync<iRacingUnauthorizedResponseException>(async () =>
        {
            var lapChartResponse = await sut.GetSubSessionResultAsync(12345, false).ConfigureAwait(false);
        });

        Assert.False(sut.IsLoggedIn);
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

        Assert.That(subSessionResultResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(subSessionResultResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(subSessionResultResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        Assert.That(subSessionResultResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(lapChartResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(timeTrialResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonTimeTrialStandingsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonTimeTrialStandingsSuccessfulAsync)).ConfigureAwait(false);

        var timeTrialResponse = await sut.GetSeasonTimeTrialStandingsAsync(3587, 71, 0, cancellationToken: CancellationToken.None).ConfigureAwait(false);

        Assert.That(timeTrialResponse, Is.Not.Null);
        Assert.That(timeTrialResponse!.Data, Is.Not.Null);

        Assert.That(timeTrialResponse.Data.Header, Is.Not.Null);
        Assert.That(timeTrialResponse.Data.Header.Success, Is.True);
        Assert.That(timeTrialResponse.Data.Header.ChunkInfo, Is.Not.Null);
        Assert.That(timeTrialResponse.Data.Standings, Has.Length.EqualTo(60));

        Assert.That(timeTrialResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(timeTrialResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(timeTrialResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        Assert.That(timeTrialResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
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
        Assert.That(timeTrialResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task SearchHostedResultsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(SearchHostedResultsSuccessfulAsync)).ConfigureAwait(false);

        var searchParams = new HostedSearchParameters
        {
            StartRangeBegin = new(2022, 03, 30),
            SessionName = "Missed Apex"
        };
        var searchHostedResponse = await sut.SearchHostedResultsAsync(searchParams, CancellationToken.None).ConfigureAwait(false);

        Assert.That(searchHostedResponse, Is.Not.Null);
        Assert.That(searchHostedResponse!.Data, Is.Not.Null);

        Assert.That(searchHostedResponse.Data.Header, Is.Not.Null);
        Assert.That(searchHostedResponse.Data.Header.Data, Is.Not.Null);
        Assert.That(searchHostedResponse.Data.Header.Data.Success, Is.True);
        Assert.That(searchHostedResponse.Data.Header.Data.ChunkInfo, Is.Not.Null);
        Assert.That(searchHostedResponse.Data.Items, Has.Length.EqualTo(53));

        Assert.That(searchHostedResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(searchHostedResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(searchHostedResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task SearchOfficialResultsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(SearchOfficialResultsSuccessfulAsync)).ConfigureAwait(false);

        var searchParams = new OfficialSearchParameters
        {
            SeasonYear = 2022,
            SeasonQuarter = 3,
            SeriesId = 260
        };
        var searchHostedResponse = await sut.SearchOfficialResultsAsync(searchParams, CancellationToken.None).ConfigureAwait(false);

        Assert.That(searchHostedResponse, Is.Not.Null);
        Assert.That(searchHostedResponse!.Data, Is.Not.Null);

        Assert.That(searchHostedResponse.Data.Header, Is.Not.Null);
        Assert.That(searchHostedResponse.Data.Header.Data, Is.Not.Null);
        Assert.That(searchHostedResponse.Data.Header.Data.Success, Is.True);
        Assert.That(searchHostedResponse.Data.Header.Data.ChunkInfo, Is.Not.Null);
        Assert.That(searchHostedResponse.Data.Items, Has.Length.EqualTo(333));

        Assert.That(searchHostedResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(searchHostedResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(searchHostedResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberChartDataSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberChartDataSuccessfulAsync)).ConfigureAwait(false);

        var memberChartResponse = await sut.GetMemberChartData(341554, 2, Member.MemberChartType.IRating, CancellationToken.None).ConfigureAwait(false);

        Assert.That(memberChartResponse, Is.Not.Null);
        Assert.That(memberChartResponse.Data, Is.Not.Null);

        Assert.That(memberChartResponse.Data.Success, Is.True);
        Assert.That(memberChartResponse.Data.Points, Has.Length.EqualTo(104));
        Assert.That(memberChartResponse.Data.ChartType, Is.EqualTo(Member.MemberChartType.IRating));
        Assert.That(memberChartResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task SearchLeagueDirectorySuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(SearchLeagueDirectorySuccessfulAsync)).ConfigureAwait(false);

        var searchParams = new SearchLeagueDirectoryParameters();
        var leagueDirectoryResponse = await sut.SearchLeagueDirectoryAsync(searchParams, CancellationToken.None).ConfigureAwait(false);

        Assert.That(leagueDirectoryResponse, Is.Not.Null);
        Assert.That(leagueDirectoryResponse.Data, Is.Not.Null);

        Assert.That(leagueDirectoryResponse.Data.Success, Is.True);

        Assert.That(leagueDirectoryResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(leagueDirectoryResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(leagueDirectoryResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        Assert.That(leagueDirectoryResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task ListSeasonsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(ListSeasonsSuccessfulAsync)).ConfigureAwait(false);

        var listSeasonsResponse = await sut.ListSeasonsAsync(2022, 1, CancellationToken.None).ConfigureAwait(false);

        Assert.That(listSeasonsResponse, Is.Not.Null);
        Assert.That(listSeasonsResponse.Data, Is.Not.Null);

        Assert.That(listSeasonsResponse.Data, Has.Property(nameof(ListOfSeasons.SeasonYear)).EqualTo(2022));
        Assert.That(listSeasonsResponse.Data, Has.Property(nameof(ListOfSeasons.SeasonQuarter)).EqualTo(1));
        Assert.That(listSeasonsResponse.Data, Has.Property(nameof(ListOfSeasons.Seasons)).Not.Null.Or.Empty);
        Assert.That(listSeasonsResponse.Data.Seasons, Has.Length.EqualTo(126));

        Assert.That(listSeasonsResponse.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(listSeasonsResponse.TotalRateLimit, Is.EqualTo(100));
        Assert.That(listSeasonsResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        Assert.That(listSeasonsResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
    }
}
