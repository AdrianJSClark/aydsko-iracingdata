// © 2023-2024 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Globalization;
using System.Net;
using Aydsko.iRacingData.Constants;
using Aydsko.iRacingData.Exceptions;
using Aydsko.iRacingData.Leagues;
using Aydsko.iRacingData.Results;
using Aydsko.iRacingData.Searches;
using Aydsko.iRacingData.Series;
using Aydsko.iRacingData.Stats;
using Aydsko.iRacingData.TimeAttack;

namespace Aydsko.iRacingData.UnitTests;

internal sealed class CapturedResponseValidationTests : MockedHttpTestBase
{
    [Test(TestOf = typeof(DataClient))]
    public async Task GetCarAssetDetailsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetCarAssetDetailsSuccessfulAsync)).ConfigureAwait(false);

        var carAssets = await testDataClient.GetCarAssetDetailsAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(carAssets, Is.Not.Null);
            Assert.That(carAssets!.Data, Is.Not.Null);

            Assert.That(carAssets.Data, Has.Count.EqualTo(159));

            Assert.That(carAssets.Data, Contains.Key("161"));
            var mercedesAmgW13 = carAssets.Data["161"];
            Assert.That(mercedesAmgW13.CarId, Is.EqualTo(161));
            Assert.That(mercedesAmgW13.LogoUri, Is.EqualTo(new Uri("https://images-static.iracing.com/img/logos/partners/mercedes-logo.png")));
            Assert.That(mercedesAmgW13.LargeImageUri, Is.EqualTo(new Uri("https://images-static.iracing.com/img/cars/mercedesw13/mercedesw13-large.jpg")));
            Assert.That(mercedesAmgW13.SmallImageUri, Is.EqualTo(new Uri("https://images-static.iracing.com/img/cars/mercedesw13/mercedesw13-small.jpg")));

            Assert.That(carAssets.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(carAssets.TotalRateLimit, Is.EqualTo(100));
            Assert.That(carAssets.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(carAssets.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetCarsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetCarsSuccessfulAsync)).ConfigureAwait(false);

        var cars = await testDataClient.GetCarsAsync().ConfigureAwait(false);

        Assert.That(cars, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(cars!.Data, Is.Not.Null);

            Assert.That(cars.Data, Has.Length.EqualTo(159));
            Assert.That(cars.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(cars.TotalRateLimit, Is.EqualTo(100));
            Assert.That(cars.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(cars.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetCarClassesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetCarClassesSuccessfulAsync)).ConfigureAwait(false);

        var carClasses = await testDataClient.GetCarClassesAsync().ConfigureAwait(false);

        Assert.That(carClasses, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(carClasses!.Data, Is.Not.Null);

            Assert.That(carClasses.Data, Has.Length.EqualTo(223));
            Assert.That(carClasses.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(carClasses.TotalRateLimit, Is.EqualTo(100));
            Assert.That(carClasses.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(carClasses.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetDivisionsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetDivisionsSuccessfulAsync)).ConfigureAwait(false);

        var divisionsResponse = await testDataClient.GetDivisionsAsync().ConfigureAwait(false);

        Assert.That(divisionsResponse, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(divisionsResponse!.Data, Is.Not.Null);

            Assert.That(divisionsResponse.Data, Has.Length.EqualTo(12));
        });
        Assert.Multiple(() =>
        {
            Assert.That(divisionsResponse.Data, Has.One.Property(nameof(Division.Label)).EqualTo("ALL")
                                                           .And.Property(nameof(Division.Value)).EqualTo(-1));

            Assert.That(divisionsResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(divisionsResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(divisionsResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetCategoriesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetCategoriesSuccessfulAsync)).ConfigureAwait(false);

        var divisionsResponse = await testDataClient.GetCategoriesAsync().ConfigureAwait(false);

        Assert.That(divisionsResponse, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(divisionsResponse!.Data, Is.Not.Null);

            Assert.That(divisionsResponse.Data, Has.Length.EqualTo(4));
        });
        Assert.That(divisionsResponse.Data, Has.One.Property(nameof(Division.Label)).EqualTo("Oval")
                                                   .And.Property(nameof(Division.Value)).EqualTo(1));
        Assert.That(divisionsResponse.Data, Has.One.Property(nameof(Division.Label)).EqualTo("Road")
                                                   .And.Property(nameof(Division.Value)).EqualTo(2));
        Assert.That(divisionsResponse.Data, Has.One.Property(nameof(Division.Label)).EqualTo("Dirt oval")
                                                   .And.Property(nameof(Division.Value)).EqualTo(3));
        Assert.Multiple(() =>
        {
            Assert.That(divisionsResponse.Data, Has.One.Property(nameof(Division.Label)).EqualTo("Dirt road")
                                                           .And.Property(nameof(Division.Value)).EqualTo(4));

            Assert.That(divisionsResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(divisionsResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(divisionsResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetEventTypesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetEventTypesSuccessfulAsync)).ConfigureAwait(false);

        var divisionsResponse = await testDataClient.GetEventTypesAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
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
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetLookupsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetLookupsSuccessfulAsync)).ConfigureAwait(false);

        var lookupGroups = await testDataClient.GetLookupsAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(lookupGroups, Is.Not.Null);
            Assert.That(lookupGroups!.Data, Is.Not.Null);

            Assert.That(lookupGroups.Data, Has.Length.EqualTo(2));
            Assert.That(lookupGroups.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(lookupGroups.TotalRateLimit, Is.EqualTo(100));
            Assert.That(lookupGroups.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(lookupGroups.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetLicenseLookupsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetLicenseLookupsSuccessfulAsync)).ConfigureAwait(false);

        var carAssets = await testDataClient.GetLicenseLookupsAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(carAssets, Is.Not.Null);
            Assert.That(carAssets!.Data, Is.Not.Null);

            Assert.That(carAssets.Data, Has.Length.EqualTo(7));
            Assert.That(carAssets.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(carAssets.TotalRateLimit, Is.EqualTo(100));
            Assert.That(carAssets.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(carAssets.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetDriverInfoWithLicensesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetDriverInfoWithLicensesSuccessfulAsync)).ConfigureAwait(false);

        var carAssets = await testDataClient.GetDriverInfoAsync(TestCustomerIds, true).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(carAssets, Is.Not.Null);
            Assert.That(carAssets!.Data, Is.Not.Null);

            Assert.That(carAssets.Data, Has.Length.EqualTo(1));
            Assert.That(carAssets.Data[0].Licenses, Is.Not.Null);
            Assert.That(carAssets.Data[0].Licenses, Has.Length.EqualTo(4));

            Assert.That(carAssets.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(carAssets.TotalRateLimit, Is.EqualTo(100));
            Assert.That(carAssets.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(carAssets.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetDriverInfoWithoutLicensesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetDriverInfoWithoutLicensesSuccessfulAsync)).ConfigureAwait(false);

        var carAssets = await testDataClient.GetDriverInfoAsync(TestCustomerIds, false).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(carAssets, Is.Not.Null);
            Assert.That(carAssets!.Data, Is.Not.Null);

            Assert.That(carAssets.Data, Has.Length.EqualTo(1));
            Assert.That(carAssets.Data[0].Licenses, Is.Null);
            Assert.That(carAssets.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(carAssets.TotalRateLimit, Is.EqualTo(100));
            Assert.That(carAssets.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(carAssets.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberInfoSucceedsAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberInfoSucceedsAsync)).ConfigureAwait(false);

        var myInfo = await testDataClient.GetMyInfoAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(myInfo, Is.Not.Null);
            Assert.That(myInfo!.Data, Is.Not.Null);

            Assert.That(myInfo.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(myInfo.TotalRateLimit, Is.EqualTo(100));
            Assert.That(myInfo.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(myInfo.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberProfileSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberProfileSuccessfulAsync)).ConfigureAwait(false);

        var memberProfileResponse = await testDataClient.GetMemberProfileAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(memberProfileResponse, Is.Not.Null);
            Assert.That(memberProfileResponse!.Data, Is.Not.Null);

            Assert.That(memberProfileResponse.Data.CustomerId, Is.EqualTo(341554));
            Assert.That(memberProfileResponse.Data.RecentEvents, Has.Length.EqualTo(5));

            var sampleEvent = memberProfileResponse.Data.RecentEvents.FirstOrDefault(re => re.SubsessionId == 67375848);
            Assert.That(sampleEvent, Is.Not.Null);
            Assert.That(sampleEvent!.EventType, Is.EqualTo("RACE"));
            Assert.That(sampleEvent.PercentRank, Is.EqualTo(0.8369153));
            Assert.That(sampleEvent.BestLapTime, Is.EqualTo(TimeSpan.FromSeconds(76.8907)));

            Assert.That(memberProfileResponse.RateLimitRemaining, Is.EqualTo(239));
            Assert.That(memberProfileResponse.TotalRateLimit, Is.EqualTo(240));
            Assert.That(memberProfileResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2024, 3, 17, 11, 15, 55, TimeSpan.Zero)));
            Assert.That(memberProfileResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2024, 3, 17, 11, 29, 55, 769, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task SearchDriversSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(SearchDriversSuccessfulAsync)).ConfigureAwait(false);

        var memberProfileResponse = await testDataClient.SearchDriversAsync("123456").ConfigureAwait(false);

        Assert.That(memberProfileResponse, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(memberProfileResponse!.Data, Is.Not.Null);
            Assert.That(memberProfileResponse.Data, Has.Length.EqualTo(1));

            Assert.That(memberProfileResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(memberProfileResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(memberProfileResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(memberProfileResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberInfoDuringMaintenanceThrowsAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberInfoDuringMaintenanceThrowsAsync), false).ConfigureAwait(false);

        _ = Assert.ThrowsAsync<iRacingInMaintenancePeriodException>(async () =>
        {
            var myInfo = await testDataClient.GetMyInfoAsync().ConfigureAwait(false);
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetTracksSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetTracksSuccessfulAsync)).ConfigureAwait(false);

        var tracks = await testDataClient.GetTracksAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(tracks, Is.Not.Null);
            Assert.That(tracks!.Data, Is.Not.Null);

            Assert.That(tracks.Data, Has.Length.EqualTo(332));

            Assert.That(tracks.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(tracks.TotalRateLimit, Is.EqualTo(100));
            Assert.That(tracks.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(tracks.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonsWithoutSeriesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonsWithoutSeriesSuccessfulAsync)).ConfigureAwait(false);

        var seasons = await testDataClient.GetSeasonsAsync(false).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
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
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonsWithSeriesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonsWithSeriesSuccessfulAsync)).ConfigureAwait(false);

        var seasonsAndSeries = await testDataClient.GetSeasonsAsync(true).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(seasonsAndSeries, Is.Not.Null);
            Assert.That(seasonsAndSeries!.Data, Is.Not.Null);

            Assert.That(seasonsAndSeries.Data, Has.Length.EqualTo(134));
            Assert.That(seasonsAndSeries.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(seasonsAndSeries.TotalRateLimit, Is.EqualTo(100));
            Assert.That(seasonsAndSeries.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(seasonsAndSeries.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });

#if NET6_0_OR_GREATER
        Assert.That(seasonsAndSeries.Data[0].Schedules[0].StartDate, Is.EqualTo(new DateOnly(2024, 04, 09)));
#else
        Assert.That(seasonsAndSeries.Data[0].Schedules[0].StartDate, Is.EqualTo(new DateTime(2024, 04, 09)));
#endif
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetStatisticsSeriesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetStatisticsSeriesSuccessfulAsync)).ConfigureAwait(false);

        var statsSeriesResponse = await testDataClient.GetStatisticsSeriesAsync(CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(statsSeriesResponse, Is.Not.Null);
            Assert.That(statsSeriesResponse!.Data, Is.Not.Null);
            Assert.That(statsSeriesResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(statsSeriesResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(statsSeriesResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(statsSeriesResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeriesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeriesSuccessfulAsync)).ConfigureAwait(false);

        var seriesResponse = await testDataClient.GetSeriesAsync(CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(seriesResponse, Is.Not.Null);
            Assert.That(seriesResponse!.Data, Is.Not.Null);
            Assert.That(seriesResponse!.Data, Has.Length.EqualTo(37));
            Assert.That(seriesResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(seriesResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(seriesResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(seriesResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeriesAssetsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeriesAssetsSuccessfulAsync)).ConfigureAwait(false);

        var seriesResponse = await testDataClient.GetSeriesAssetsAsync(CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(seriesResponse, Is.Not.Null);
            Assert.That(seriesResponse!.Data, Is.Not.Null);
            Assert.That(seriesResponse!.Data, Has.Count.EqualTo(37));
            Assert.That(seriesResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(seriesResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(seriesResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(seriesResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetTrackAssetsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetTrackAssetsSuccessfulAsync)).ConfigureAwait(false);

        var trackAssets = await testDataClient.GetTrackAssetsAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(trackAssets, Is.Not.Null);
            Assert.That(trackAssets!.Data, Is.Not.Null);

            Assert.That(trackAssets.Data, Has.Count.EqualTo(340));
            Assert.That(trackAssets.Data.ContainsKey("1"), Is.True);
        });

        var limeRockPark = trackAssets.Data["1"];

        Assert.Multiple(() =>
        {
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
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonResultsHandlesBadRequestAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonResultsHandlesBadRequestAsync)).ConfigureAwait(false);

        Assert.That(async () =>
        {
            var badRequestResult = await testDataClient.GetSeasonResultsAsync(0, Common.EventType.Race, 0, CancellationToken.None).ConfigureAwait(false);
        }, Throws.Exception.InstanceOf<System.Net.Http.HttpRequestException>().And.Message.Contains("400 (Bad Request)"));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberYearlyStatisticsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberYearlyStatisticsSuccessfulAsync)).ConfigureAwait(false);

        var memberStats = await testDataClient.GetMemberYearlyStatisticsAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(memberStats, Is.Not.Null);
            Assert.That(memberStats!.Data, Is.Not.Null);

            Assert.That(memberStats.Data.Statistics, Has.Length.EqualTo(2));
            Assert.That(memberStats.Data.CustomerId, Is.EqualTo(123456));
            Assert.That(memberStats.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(memberStats.TotalRateLimit, Is.EqualTo(100));
            Assert.That(memberStats.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(memberStats.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberRecentRacesSucceedsAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberRecentRacesSucceedsAsync)).ConfigureAwait(false);

        var memberStats = await testDataClient.GetMemberRecentRacesAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(memberStats, Is.Not.Null);
            Assert.That(memberStats!.Data, Is.Not.Null);

            Assert.That(memberStats.Data.Races, Has.Length.EqualTo(10));
            Assert.That(memberStats.Data.CustomerId, Is.EqualTo(123456));
            Assert.That(memberStats.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(memberStats.TotalRateLimit, Is.EqualTo(100));
            Assert.That(memberStats.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(memberStats.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberSummarySuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberSummarySuccessfulAsync)).ConfigureAwait(false);

        var memberSummaryResponse = await testDataClient.GetMemberSummaryAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(memberSummaryResponse, Is.Not.Null);
            Assert.That(memberSummaryResponse!.Data, Is.Not.Null);

            //Assert.That(memberSummaryResponse.MemberAwardResultData.Races, Has.Length.EqualTo(10));
            Assert.That(memberSummaryResponse.Data.CustomerId, Is.EqualTo(123456));
            Assert.That(memberSummaryResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(memberSummaryResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(memberSummaryResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(memberSummaryResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberDivisionSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberDivisionSuccessfulAsync)).ConfigureAwait(false);

        var memberDivisionResponse = await testDataClient.GetMemberDivisionAsync(1234, Common.EventType.Race, CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(memberDivisionResponse, Is.Not.Null);
            Assert.That(memberDivisionResponse.Data, Is.Not.Null);

            Assert.That(memberDivisionResponse.Data.Success, Is.True);
            Assert.That(memberDivisionResponse.Data.SeasonId, Is.EqualTo(1234));
            Assert.That(memberDivisionResponse.Data.EventType, Is.EqualTo(Common.EventType.Race));
            Assert.That(memberDivisionResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetLeagueWithLicensesSucceedsAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetLeagueWithLicensesSucceedsAsync)).ConfigureAwait(false);

        var memberStats = await testDataClient.GetLeagueAsync(123, true).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(memberStats, Is.Not.Null);
            Assert.That(memberStats!.Data, Is.Not.Null);
            Assert.That(memberStats.Data.LeagueName, Is.EqualTo("Missed Apex iRacing"));
            Assert.That(memberStats.Data.Roster, Has.Length.EqualTo(60));
            Assert.That(memberStats.Data.RosterCount, Is.EqualTo(60));

            Assert.That(memberStats.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(memberStats.TotalRateLimit, Is.EqualTo(100));
            Assert.That(memberStats.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(memberStats.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetLeagueWithoutLicensesSucceedsAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetLeagueWithoutLicensesSucceedsAsync)).ConfigureAwait(false);

        var memberStats = await testDataClient.GetLeagueAsync(123, false).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(memberStats, Is.Not.Null);
            Assert.That(memberStats!.Data, Is.Not.Null);

            Assert.That(memberStats.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(memberStats.TotalRateLimit, Is.EqualTo(100));
            Assert.That(memberStats.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(memberStats.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubSessionLapChartSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSubSessionLapChartSuccessfulAsync)).ConfigureAwait(false);

        var lapChartResponse = await testDataClient.GetSubSessionLapChartAsync(12345, 0).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(lapChartResponse, Is.Not.Null);

            Assert.That(lapChartResponse.Data.Header, Is.Not.Null);
            Assert.That(lapChartResponse.Data.Header.Success, Is.True);
            Assert.That(lapChartResponse.Data.Header.SessionInfo, Is.Not.Null);
            Assert.That(lapChartResponse.Data.Laps, Has.Length.EqualTo(417));

            Assert.That(lapChartResponse.Data.Laps, Has.ItemAt(0).Property(nameof(SubsessionChartLap.GroupId)).EqualTo(523780)
                                                           .And.ItemAt(0).Property(nameof(SubsessionChartLap.Name)).EqualTo("Jake Dennis")
                                                           .And.ItemAt(0).Property(nameof(SubsessionChartLap.LapPosition)).EqualTo(1)
                                                           .And.ItemAt(0).Property(nameof(SubsessionLap.Flags)).EqualTo(LapFlags.None));

            Assert.That(lapChartResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(lapChartResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(lapChartResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(lapChartResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonDriverStandingsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonDriverStandingsSuccessfulAsync)).ConfigureAwait(false);

        var seasonDriverStandingsResponse = await testDataClient.GetSeasonDriverStandingsAsync(1234, 9, 0, cancellationToken: CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(seasonDriverStandingsResponse, Is.Not.Null);

            Assert.That(seasonDriverStandingsResponse.Data.Header, Is.Not.Null);
            Assert.That(seasonDriverStandingsResponse.Data.Header.Success, Is.True);

            Assert.That(seasonDriverStandingsResponse.RateLimitRemaining, Is.EqualTo(239));
            Assert.That(seasonDriverStandingsResponse.TotalRateLimit, Is.EqualTo(240));
            Assert.That(seasonDriverStandingsResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2024, 9, 29, 2, 20, 40, TimeSpan.Zero)));
            Assert.That(seasonDriverStandingsResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2024, 9, 29, 2, 32, 29, 935, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSingleDriverSubsessionLapsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSingleDriverSubsessionLapsSuccessfulAsync)).ConfigureAwait(false);

        var lapChartResponse = await testDataClient.GetSingleDriverSubsessionLapsAsync(12345, 0, 123456).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(lapChartResponse, Is.Not.Null);

            Assert.That(lapChartResponse.Data.Header, Is.Not.Null);
            Assert.That(lapChartResponse.Data.Header.Success, Is.True);
            Assert.That(lapChartResponse.Data.Header.SessionInfo, Is.Not.Null);
            Assert.That(lapChartResponse.Data.Laps, Has.Length.EqualTo(16));

            Assert.That(lapChartResponse.Data.Laps, Has.ItemAt(0).Property(nameof(SubsessionLap.GroupId)).EqualTo(341554)
                                                           .And.ItemAt(0).Property(nameof(SubsessionLap.Name)).EqualTo("Adrian Clark")
                                                           .And.ItemAt(0).Property(nameof(SubsessionLap.Incident)).EqualTo(false)
                                                           .And.ItemAt(0).Property(nameof(SubsessionLap.Flags)).EqualTo(LapFlags.Invalid | LapFlags.Pitted | LapFlags.Tow));

            Assert.That(lapChartResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(lapChartResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(lapChartResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(lapChartResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSingleDriverSubsessionLapsForbiddenThrowsErrorsAsync()
    {
        await MessageHandler.QueueResponsesAsync("ResponseForbidden", false).ConfigureAwait(false);

        _ = Assert.ThrowsAsync<iRacingForbiddenResponseException>(async () =>
        {
            var lapChartResponse = await testDataClient.GetSingleDriverSubsessionLapsAsync(12345, 0, 123456).ConfigureAwait(false);
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetTeamSubsessionLapsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetTeamSubsessionLapsSuccessfulAsync)).ConfigureAwait(false);

        var lapChartResponse = await testDataClient.GetTeamSubsessionLapsAsync(12345, 0, 123456).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(lapChartResponse, Is.Not.Null);

            Assert.That(lapChartResponse.Data.Header, Is.Not.Null);
            Assert.That(lapChartResponse.Data.Header.Success, Is.True);
            Assert.That(lapChartResponse.Data.Header.SessionInfo, Is.Not.Null);
            Assert.That(lapChartResponse.Data.Laps, Has.Length.EqualTo(500));
            Assert.That(lapChartResponse.Data.Laps, Has.ItemAt(0).Property(nameof(SubsessionLap.GroupId)).EqualTo(-93376)
                                                           .And.ItemAt(0).Property(nameof(SubsessionLap.Name)).EqualTo("Tempest Motorsports Neon")
                                                           .And.ItemAt(0).Property(nameof(SubsessionLap.Incident)).EqualTo(false)
                                                           .And.ItemAt(0).Property(nameof(SubsessionLap.Flags)).EqualTo(LapFlags.None));

            Assert.That(lapChartResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(lapChartResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(lapChartResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(lapChartResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetTeamSubsessionLapsForbiddenThrowsErrorsAsync()
    {
        await MessageHandler.QueueResponsesAsync("ResponseForbidden", false).ConfigureAwait(false);

        _ = Assert.ThrowsAsync<iRacingForbiddenResponseException>(async () =>
        {
            var lapChartResponse = await testDataClient.GetTeamSubsessionLapsAsync(12345, 0, 123456).ConfigureAwait(false);
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubSessionResultSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSubSessionResultSuccessfulAsync)).ConfigureAwait(false);

        var subSessionResultResponse = await testDataClient.GetSubSessionResultAsync(12345, false, CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(subSessionResultResponse, Is.Not.Null);
            Assert.That(subSessionResultResponse!.Data, Is.Not.Null);

            var subSessionResult = subSessionResultResponse.Data;
            Assert.That(subSessionResult.SeasonId, Is.EqualTo(3620));
            Assert.That(subSessionResult.SeriesName, Is.EqualTo("Global Fanatec Challenge"));
            Assert.That(subSessionResult.SessionResults, Has.Length.EqualTo(2));
            Assert.That(subSessionResult.SessionResults, Has.One.Property(nameof(SessionResults.SimSessionName)).EqualTo("RACE"));
            Assert.That(subSessionResult.NumberOfDrivers, Is.EqualTo(19));
            Assert.That(subSessionResult.EventAverageLap, Is.EqualTo(TimeSpan.FromSeconds(407.8532)));
            Assert.That(subSessionResult.EventBestLapTime, Is.EqualTo(TimeSpan.FromSeconds(408.2271)));

            var raceResults = subSessionResult.SessionResults.Single(r => r.SimSessionName == "RACE");
            Assert.That(raceResults.Results, Has.All.Property(nameof(Result.DriverResults)).Null); // Single-driver events don't have driver results.

            var sampleDriver = raceResults.Results.FirstOrDefault(r => r.Position == 0);
            Assert.That(sampleDriver, Is.Not.Null);
            Assert.That(sampleDriver!.CarClassName, Is.EqualTo("Cadillac CTS-VR"));
            Assert.That(sampleDriver.CarClassShortName, Is.EqualTo("Cadillac CTS-VR"));
            Assert.That(sampleDriver.CarName, Is.EqualTo("Cadillac CTS-V Racecar"));
            Assert.That(sampleDriver.DivisionName, Is.EqualTo("Division 1"));
            Assert.That(sampleDriver.CountryCode, Is.EqualTo("AU"));

            Assert.That(subSessionResult.Weather, Is.Not.Null);

            var weather = subSessionResult.Weather;
            Assert.That(weather.SimulatedStart, Is.EqualTo(new DateTime(2022, 04, 02, 18, 25, 00)));
            Assert.That(weather.AllowFog, Is.EqualTo(false));
            Assert.That(weather.PrecipitationOption, Is.EqualTo(0));

            Assert.That(subSessionResult.SessionSplits, Has.Length.EqualTo(2));
            Assert.That(subSessionResult.SessionSplits, Contains.Item(new SessionSplit { SubSessionId = 45243121, EventStrengthOfField = 1683 }));
            Assert.That(subSessionResult.SessionSplits, Contains.Item(new SessionSplit { SubSessionId = 45243122, EventStrengthOfField = 1143 }));

            Assert.That(subSessionResultResponse.RateLimitRemaining, Is.EqualTo(239));
            Assert.That(subSessionResultResponse.TotalRateLimit, Is.EqualTo(240));
            Assert.That(subSessionResultResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2024, 10, 10, 13, 50, 14, TimeSpan.Zero)));
            Assert.That(subSessionResultResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2024, 10, 10, 14, 4, 15, 56, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubSessionResultWithWeatherSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSubSessionResultWithWeatherSuccessfulAsync)).ConfigureAwait(false);

        var subSessionResultResponse = await testDataClient.GetSubSessionResultAsync(12345, false, CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(subSessionResultResponse, Is.Not.Null);
            Assert.That(subSessionResultResponse!.Data, Is.Not.Null);

            Assert.That(subSessionResultResponse.Data.SeasonId, Is.EqualTo(4780));
            Assert.That(subSessionResultResponse.Data.SeriesName, Is.EqualTo("Formula B - Super Formula IMSIM Series - Fixed"));
            Assert.That(subSessionResultResponse.Data.SessionResults, Has.Length.EqualTo(3));
            Assert.That(subSessionResultResponse.Data.SessionResults, Has.One.Property(nameof(SessionResults.SimSessionName)).EqualTo("RACE"));

            Assert.That(subSessionResultResponse.RateLimitRemaining, Is.EqualTo(239));
            Assert.That(subSessionResultResponse.TotalRateLimit, Is.EqualTo(240));
            Assert.That(subSessionResultResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2024, 5, 1, 11, 54, 8, TimeSpan.Zero)));
            Assert.That(subSessionResultResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2024, 5, 1, 12, 8, 8, 231, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubSessionResultForTeamSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSubSessionResultForTeamSuccessfulAsync)).ConfigureAwait(false);

        var subSessionResultResponse = await testDataClient.GetSubSessionResultAsync(12345, false, CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
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

            var sampleDriverResult = racingSociety?.DriverResults?.FirstOrDefault(d => d.CustomerId == 696075);
            Assert.That(sampleDriverResult, Is.Not.Null);
            Assert.That(sampleDriverResult!.DisplayName, Is.EqualTo("Fabian Albrecht"));
            Assert.That(sampleDriverResult!.CountryCode, Is.EqualTo("DE"));

            var sampleDriverResult2 = racingSociety?.DriverResults?.FirstOrDefault(d => d.CustomerId == 669671);
            Assert.That(sampleDriverResult2, Is.Not.Null);
            Assert.That(sampleDriverResult2!.DisplayName, Is.EqualTo("Mike Honda"));
            Assert.That(sampleDriverResult2!.CountryCode, Is.EqualTo("DE"));

            Assert.That(subSessionResultResponse.RateLimitRemaining, Is.EqualTo(239));
            Assert.That(subSessionResultResponse.TotalRateLimit, Is.EqualTo(240));
            Assert.That(subSessionResultResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2024, 10, 10, 12, 26, 11, TimeSpan.Zero)));
            Assert.That(subSessionResultResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2024, 10, 10, 12, 40, 12, 320, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubSessionResultForLeagueSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSubSessionResultForLeagueSuccessfulAsync)).ConfigureAwait(false);

        var subSessionResultResponse = await testDataClient.GetSubSessionResultAsync(12345, false, CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
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
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubSessionResultForbiddenThrowsErrorsAsync()
    {
        await MessageHandler.QueueResponsesAsync("ResponseForbidden", false).ConfigureAwait(false);

        _ = Assert.ThrowsAsync<iRacingForbiddenResponseException>(async () =>
        {
            var lapChartResponse = await testDataClient.GetSubSessionResultAsync(12345, false).ConfigureAwait(false);
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubSessionResultUnauthorizedThrowsErrorsAsync()
    {
        // We queue this 3 times because we now have a retry mechanism in place.
        await MessageHandler.QueueResponsesAsync("ResponseUnauthorized", false).ConfigureAwait(false);
        await MessageHandler.QueueResponsesAsync("ResponseUnauthorized", false).ConfigureAwait(false);
        await MessageHandler.QueueResponsesAsync("ResponseUnauthorized", false).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _ = Assert.ThrowsAsync<iRacingUnauthorizedResponseException>(async () =>
            {
                var lapChartResponse = await testDataClient.GetSubSessionResultAsync(12345, false).ConfigureAwait(false);
            });

            Assert.That(testDataClient.IsLoggedIn, Is.False);
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubSessionResultUnauthorizedDueToLegacyAuthenticationSettingThrowsErrorsAsync()
    {
        await MessageHandler.QueueResponsesAsync("ResponseUnauthorizedLegacyRequired", false).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            var loginFailedException = Assert.ThrowsAsync<iRacingLoginFailedException>(async () =>
            {
                var lapChartResponse = await testDataClient.GetSubSessionResultAsync(12345, false).ConfigureAwait(false);
            });

            if (loginFailedException != null)
            {
                Assert.That(loginFailedException.LegacyAuthenticationRequired, Is.True);
            }

            Assert.That(testDataClient.IsLoggedIn, Is.False);
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubsessionEventLogSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSubsessionEventLogSuccessfulAsync)).ConfigureAwait(false);

        var subSessionResultResponse = await testDataClient.GetSubsessionEventLogAsync(12345, 0, CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(subSessionResultResponse, Is.Not.Null);
            Assert.That(subSessionResultResponse.Data.Header, Is.Not.Null);
            Assert.That(subSessionResultResponse.Data.LogItems, Is.Not.Null);

            Assert.That(subSessionResultResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(subSessionResultResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(subSessionResultResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(subSessionResultResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSubsessionEventLogForbiddenThrowsErrorsAsync()
    {
        await MessageHandler.QueueResponsesAsync("ResponseForbidden", false).ConfigureAwait(false);

        _ = Assert.ThrowsAsync<iRacingForbiddenResponseException>(async () =>
        {
            var lapChartResponse = await testDataClient.GetSubsessionEventLogAsync(12345, 0).ConfigureAwait(false);
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonQualifyResultsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonQualifyResultsSuccessfulAsync)).ConfigureAwait(false);

        var lapChartResponse = await testDataClient.GetSeasonQualifyResultsAsync(3587, 71, 0).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(lapChartResponse, Is.Not.Null);

            Assert.That(lapChartResponse.Data.Header, Is.Not.Null);
            Assert.That(lapChartResponse.Data.Header.Success, Is.True);
            Assert.That(lapChartResponse.Data.Header.ChunkInfo, Is.Not.Null);
            Assert.That(lapChartResponse.Data.Results, Has.Length.EqualTo(3078));

            Assert.That(lapChartResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(lapChartResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(lapChartResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(lapChartResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonTimeTrialResultsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonTimeTrialResultsSuccessfulAsync)).ConfigureAwait(false);

        var timeTrialResponse = await testDataClient.GetSeasonTimeTrialResultsAsync(3587, 71, 0).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(timeTrialResponse, Is.Not.Null);

            Assert.That(timeTrialResponse.Data.Header, Is.Not.Null);
            Assert.That(timeTrialResponse.Data.Header.Success, Is.True);
            Assert.That(timeTrialResponse.Data.Header.ChunkInfo, Is.Not.Null);
            Assert.That(timeTrialResponse.Data.Results, Has.Length.EqualTo(60));

            Assert.That(timeTrialResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(timeTrialResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(timeTrialResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(timeTrialResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonTimeTrialStandingsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonTimeTrialStandingsSuccessfulAsync)).ConfigureAwait(false);

        var timeTrialResponse = await testDataClient.GetSeasonTimeTrialStandingsAsync(3587, 71, 0, cancellationToken: CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(timeTrialResponse, Is.Not.Null);

            Assert.That(timeTrialResponse.Data.Header, Is.Not.Null);
            Assert.That(timeTrialResponse.Data.Header.Success, Is.True);
            Assert.That(timeTrialResponse.Data.Header.ChunkInfo, Is.Not.Null);
            Assert.That(timeTrialResponse.Data.Standings, Has.Length.EqualTo(60));

            Assert.That(timeTrialResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(timeTrialResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(timeTrialResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(timeTrialResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonTeamStandingsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonTeamStandingsSuccessfulAsync)).ConfigureAwait(false);

        var timeTrialResponse = await testDataClient.GetSeasonTeamStandingsAsync(3587, 71, 0, CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(timeTrialResponse, Is.Not.Null);

            Assert.That(timeTrialResponse.Data.Header, Is.Not.Null);
            Assert.That(timeTrialResponse.Data.Header.Success, Is.True);
            Assert.That(timeTrialResponse.Data.Header.ChunkInfo, Is.Not.Null);
            Assert.That(timeTrialResponse.Data.Standings, Has.Length.EqualTo(192));

            Assert.That(timeTrialResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(timeTrialResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(timeTrialResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(timeTrialResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
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
        var searchHostedResponse = await testDataClient.SearchHostedResultsAsync(searchParams, CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(searchHostedResponse, Is.Not.Null);

            Assert.That(searchHostedResponse.Data.Header, Is.Not.Null);
            Assert.That(searchHostedResponse.Data.Header.Data, Is.Not.Null);
            Assert.That(searchHostedResponse.Data.Header.Data.Success, Is.True);
            Assert.That(searchHostedResponse.Data.Header.Data.ChunkInfo, Is.Not.Null);
            Assert.That(searchHostedResponse.Data.Items, Has.Length.EqualTo(53));

            Assert.That(searchHostedResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(searchHostedResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(searchHostedResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        });
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
        var searchHostedResponse = await testDataClient.SearchOfficialResultsAsync(searchParams, CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(searchHostedResponse, Is.Not.Null);

            Assert.That(searchHostedResponse.Data.Header, Is.Not.Null);
            Assert.That(searchHostedResponse.Data.Header.Data, Is.Not.Null);
            Assert.That(searchHostedResponse.Data.Header.Data.Success, Is.True);
            Assert.That(searchHostedResponse.Data.Header.Data.ChunkInfo, Is.Not.Null);
            Assert.That(searchHostedResponse.Data.Items, Has.Length.EqualTo(333));

            Assert.That(searchHostedResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(searchHostedResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(searchHostedResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberChartDataSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberChartDataSuccessfulAsync)).ConfigureAwait(false);

        var memberChartResponse = await testDataClient.GetMemberChartDataAsync(341554, 2, Member.MemberChartType.IRating, CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(memberChartResponse, Is.Not.Null);
            Assert.That(memberChartResponse.Data, Is.Not.Null);

            Assert.That(memberChartResponse.Data.Success, Is.True);
            Assert.That(memberChartResponse.Data.Points, Has.Length.EqualTo(104));
            Assert.That(memberChartResponse.Data.ChartType, Is.EqualTo(Member.MemberChartType.IRating));
            Assert.That(memberChartResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task SearchLeagueDirectorySuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(SearchLeagueDirectorySuccessfulAsync)).ConfigureAwait(false);

        var searchParams = new SearchLeagueDirectoryParameters();
        var leagueDirectoryResponse = await testDataClient.SearchLeagueDirectoryAsync(searchParams, CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(leagueDirectoryResponse, Is.Not.Null);
            Assert.That(leagueDirectoryResponse.Data, Is.Not.Null);

            Assert.That(leagueDirectoryResponse.Data.Success, Is.True);

            Assert.That(leagueDirectoryResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(leagueDirectoryResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(leagueDirectoryResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(leagueDirectoryResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task ListSeasonsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(ListSeasonsSuccessfulAsync)).ConfigureAwait(false);

        var listSeasonsResponse = await testDataClient.ListSeasonsAsync(2022, 1, CancellationToken.None).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
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
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetRaceGuideSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetRaceGuideSuccessfulAsync)).ConfigureAwait(false);

        var raceGuideResponse = await testDataClient.GetRaceGuideAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(raceGuideResponse, Is.Not.Null);
            Assert.That(raceGuideResponse!.Data, Is.Not.Null);

            Assert.That(raceGuideResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(raceGuideResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(raceGuideResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(raceGuideResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetLeagueSeasonsAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetLeagueSeasonsAsync)).ConfigureAwait(false);

        var leagueSeasons = await testDataClient.GetLeagueSeasonsAsync(123456).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(leagueSeasons, Is.Not.Null);
            Assert.That(leagueSeasons!.Data, Is.Not.Null);
            Assert.That(leagueSeasons.Data.Success, Is.True);
            Assert.That(leagueSeasons.Data.LeagueId, Is.EqualTo(10651));
            Assert.That(leagueSeasons.Data.Seasons, Has.Length.EqualTo(2));

            Assert.That(leagueSeasons.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(leagueSeasons.TotalRateLimit, Is.EqualTo(100));
            Assert.That(leagueSeasons.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(leagueSeasons.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetCustomerLeagueSessionsAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetCustomerLeagueSessionsAsync)).ConfigureAwait(false);

        var countryResponse = await testDataClient.GetCustomerLeagueSessionsAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(countryResponse, Is.Not.Null);
            Assert.That(countryResponse!.Data, Is.Not.Null);

            Assert.That(countryResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(countryResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(countryResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(countryResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2023, 4, 8, 20, 28, 49, 471, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetCountriesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetCountriesSuccessfulAsync)).ConfigureAwait(false);

        var countryResponse = await testDataClient.GetCountriesAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(countryResponse, Is.Not.Null);
            Assert.That(countryResponse!.Data, Is.Not.Null);

            Assert.That(countryResponse.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(countryResponse.TotalRateLimit, Is.EqualTo(100));
            Assert.That(countryResponse.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(countryResponse.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetDriverAwardsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetDriverAwardsSuccessfulAsync)).ConfigureAwait(false);

        var response = await testDataClient.GetDriverAwardsAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response!.Data, Is.Not.Null);
            Assert.That(response.Data, Has.Length.EqualTo(185));

            Assert.That(response.RateLimitRemaining, Is.EqualTo(239));
            Assert.That(response.TotalRateLimit, Is.EqualTo(240));
            Assert.That(response.RateLimitReset, Is.EqualTo(new DateTimeOffset(2025, 3, 12, 14, 32, 0, TimeSpan.Zero)));
            Assert.That(response.DataExpires, Is.Null);
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetDriverAwardInstanceSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetDriverAwardInstanceSuccessfulAsync)).ConfigureAwait(false);

        var response = await testDataClient.GetDriverAwardInstanceAsync(0).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response!.Data, Is.Not.Null);
            Assert.That(response.Data.AwardId, Is.EqualTo(1451));
            Assert.That(response.Data.Awards, Has.Length.EqualTo(1));

            Assert.That(response.RateLimitRemaining, Is.EqualTo(239));
            Assert.That(response.TotalRateLimit, Is.EqualTo(240));
            Assert.That(response.RateLimitReset, Is.EqualTo(new DateTimeOffset(2025, 3, 12, 14, 32, 0, TimeSpan.Zero)));
            Assert.That(response.DataExpires, Is.Null);
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetBestLapStatisticsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetBestLapStatisticsSuccessfulAsync)).ConfigureAwait(false);

        var response = await testDataClient.GetBestLapStatisticsAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response!.Data, Is.Not.Null);

            Assert.That(response.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(response.TotalRateLimit, Is.EqualTo(100));
            Assert.That(response.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(response.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetHostedSessionsCombinedSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetHostedSessionsCombinedSuccessfulAsync)).ConfigureAwait(false);

        var response = await testDataClient.ListHostedSessionsCombinedAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response!.Data, Is.Not.Null);

            Assert.That(response.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(response.TotalRateLimit, Is.EqualTo(100));
            Assert.That(response.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(response.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetHostedSessionsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetHostedSessionsSuccessfulAsync)).ConfigureAwait(false);

        var response = await testDataClient.ListHostedSessionsAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response!.Data, Is.Not.Null);

            Assert.That(response.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(response.TotalRateLimit, Is.EqualTo(100));
            Assert.That(response.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(response.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetWorldRecordStatisticsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetWorldRecordStatisticsSuccessfulAsync)).ConfigureAwait(false);

        var response = await testDataClient.GetWorldRecordsAsync(145, 341).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Data.Header, Is.Not.Null);
            Assert.That(response.Data.Entries, Is.Not.Null);

            Assert.That(response.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(response.TotalRateLimit, Is.EqualTo(100));
            Assert.That(response.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(response.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetTeamSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetTeamSuccessfulAsync)).ConfigureAwait(false);

        var response = await testDataClient.GetTeamAsync(259167).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response!.Data, Is.Not.Null);

            Assert.That(response.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(response.TotalRateLimit, Is.EqualTo(100));
            Assert.That(response.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(response.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetLeaguePointsSystemsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetLeaguePointsSystemsSuccessfulAsync)).ConfigureAwait(false);

        var response = await testDataClient.GetLeaguePointsSystemsAsync(259167).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response!.Data, Is.Not.Null);

            Assert.That(response.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(response.TotalRateLimit, Is.EqualTo(100));
            Assert.That(response.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(response.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberParticipationCreditsSucceedsAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberParticipationCreditsSucceedsAsync)).ConfigureAwait(false);

        var response = await testDataClient.GetMemberParticipationCreditsAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response!.Data, Is.Not.Null);
            Assert.That(response.Data, Has.Length.EqualTo(3));

            Assert.That(response.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(response.TotalRateLimit, Is.EqualTo(100));
            Assert.That(response.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(response.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetPastSeasonsForSeriesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetPastSeasonsForSeriesSuccessfulAsync)).ConfigureAwait(false);

        var response = await testDataClient.GetPastSeasonsForSeriesAsync(260).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response!.Data, Is.Not.Null);

            Assert.That(response.Data.SeriesName, Is.EqualTo("Formula A - Grand Prix Series"));
            Assert.That(response.Data.SearchFilters, Is.EqualTo("openwheel,road"));
            Assert.That(response.Data.FixedSetup, Is.False);
            Assert.That(response.Data.Logo, Is.EqualTo("formulaagrandprixseries-logo.png"));
            Assert.That(response.Data.LogoUri, Is.EqualTo(new Uri("https://images-static.iracing.com/img/logos/series/formulaagrandprixseries-logo.png")));

            Assert.That(response.Data.Seasons, Has.Length.EqualTo(29));

            Assert.That(response.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(response.TotalRateLimit, Is.EqualTo(100));
            Assert.That(response.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(response.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetServiceStatusSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetServiceStatusSuccessfulAsync), false).ConfigureAwait(false);

        var response = await testDataClient.GetServiceStatusAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
#if NET8_0_OR_GREATER
            Assert.That(response.Timestamp, Is.EqualTo(new DateTimeOffset(2023, 1, 27, 12, 29, 57, 713, 258, TimeSpan.Zero)));
#else
            Assert.That(response.Timestamp, Is.EqualTo(new DateTimeOffset(2023, 1, 27, 12, 29, 57, 713, TimeSpan.Zero)));
#endif
            Assert.That(response.MaintenanceMessages, Has.Length.EqualTo(1));
            Assert.That(response.MaintenanceMessages[0], Is.EqualTo("Downtime has been scheduled for the 2023 Season 1 Patch 3 Hotfix 1 Release on January 27th at 1100 EST / 1600 GMT. Please see Staff Announcements on the iRacing Forums for more details."));

            Assert.That(response.Tests, Is.Not.Null);

            Assert.That(response.Tests.ConfigurationFlags, Is.Not.Null);

            Assert.That(response.Tests.ConfigurationFlags.iRacingUIinMaintenanceMode, Is.Not.Null);
            Assert.That(response.Tests.ConfigurationFlags.iRacingUIinMaintenanceMode.Description, Is.EqualTo("Result is a 0 or a 1. 0 means BetaUI is NOT in maintenance mode."));
            Assert.That(response.Tests.ConfigurationFlags.iRacingUIinMaintenanceMode.Result, Is.Not.Null.And.Length.EqualTo(241));
            Assert.That(response.Tests.ConfigurationFlags.iRacingUIinMaintenanceMode.SummaryLabel, Is.EqualTo("No"));
            Assert.That(response.Tests.ConfigurationFlags.iRacingUIinMaintenanceMode.SummaryLevel, Is.EqualTo(0));

            Assert.That(response.Tests.ConfigurationFlags.TestDriveAvailable, Is.Not.Null);
            Assert.That(response.Tests.ConfigurationFlags.TestDriveAvailable.Description, Is.EqualTo("Result is a 0 or a 1. 0 means TestDrive is disabled."));
            Assert.That(response.Tests.ConfigurationFlags.TestDriveAvailable.Result, Is.Not.Null.And.Length.EqualTo(241));
            Assert.That(response.Tests.ConfigurationFlags.TestDriveAvailable.SummaryLabel, Is.EqualTo("Yes"));
            Assert.That(response.Tests.ConfigurationFlags.TestDriveAvailable.SummaryLevel, Is.EqualTo(2));

            Assert.That(response.Tests.ConfigurationFlags.ClassicMemberSiteInMaintenanceMode, Is.Not.Null);
            Assert.That(response.Tests.ConfigurationFlags.ClassicMemberSiteInMaintenanceMode.Description, Is.EqualTo("Result is a 0 or a 1. 0 means the classic website is NOT in maintenance mode."));
            Assert.That(response.Tests.ConfigurationFlags.ClassicMemberSiteInMaintenanceMode.Result, Is.Not.Null.And.Length.EqualTo(241));
            Assert.That(response.Tests.ConfigurationFlags.ClassicMemberSiteInMaintenanceMode.SummaryLabel, Is.EqualTo("No"));
            Assert.That(response.Tests.ConfigurationFlags.ClassicMemberSiteInMaintenanceMode.SummaryLevel, Is.EqualTo(0));

            Assert.That(response.Tests.Websites, Is.Not.Null);
            Assert.That(response.Tests.Websites.PublicSiteWwwiRacingCom, Is.Not.Null);
            Assert.That(response.Tests.Websites.PublicSiteWwwiRacingCom.Description, Is.EqualTo("Result is a 0 or a 1. 0 means the website returned something other than HTTP/200 or has the wrong content."));
            Assert.That(response.Tests.Websites.PublicSiteWwwiRacingCom.Result, Is.Not.Null.And.Length.EqualTo(241));
            Assert.That(response.Tests.Websites.PublicSiteWwwiRacingCom.SummaryLabel, Is.EqualTo("Okay"));
            Assert.That(response.Tests.Websites.PublicSiteWwwiRacingCom.SummaryLevel, Is.EqualTo(2));

            Assert.That(response.Tests.Websites.Forums, Is.Not.Null);
            Assert.That(response.Tests.Websites.Forums.Description, Is.EqualTo("Result is a 0 or 1. 0 means high response error rates."));
            Assert.That(response.Tests.Websites.Forums.Result, Is.Not.Null.And.Length.EqualTo(241));
            Assert.That(response.Tests.Websites.Forums.SummaryLabel, Is.EqualTo("Okay"));
            Assert.That(response.Tests.Websites.Forums.SummaryLevel, Is.EqualTo(2));

            Assert.That(response.Tests.Websites.Downloads, Is.Not.Null);
            Assert.That(response.Tests.Websites.Downloads.Description, Is.EqualTo("Result is a 0 or a 1. 0 means the website returned something other than HTTP/200 or has the wrong content."));
            Assert.That(response.Tests.Websites.Downloads.Result, Is.Not.Null.And.Length.EqualTo(241));
            Assert.That(response.Tests.Websites.Downloads.SummaryLabel, Is.EqualTo("Okay"));
            Assert.That(response.Tests.Websites.Downloads.SummaryLevel, Is.EqualTo(2));

            Assert.That(response.Tests.Websites.LegacyForums, Is.Not.Null);
            Assert.That(response.Tests.Websites.LegacyForums.Description, Is.EqualTo("Result is a 0 or 1. 0 means high response error rates."));
            Assert.That(response.Tests.Websites.LegacyForums.Result, Is.Not.Null.And.Length.EqualTo(241));
            Assert.That(response.Tests.Websites.LegacyForums.SummaryLabel, Is.EqualTo("Okay"));
            Assert.That(response.Tests.Websites.LegacyForums.SummaryLevel, Is.EqualTo(2));

            Assert.That(response.Tests.RaceServerNetwork, Is.Not.Null);
            Assert.That(response.Tests.RaceServerNetwork.RaceServerConnectivitySydney.Description, Is.EqualTo("Result is a 0 or a 1. 0 means the farm is experiencing connectivity issues."));
            Assert.That(response.Tests.RaceServerNetwork.RaceServerConnectivitySydney.Result, Is.Not.Null.And.Length.EqualTo(241));
            Assert.That(response.Tests.RaceServerNetwork.RaceServerConnectivitySydney.SummaryLabel, Is.EqualTo("Okay"));
            Assert.That(response.Tests.RaceServerNetwork.RaceServerConnectivitySydney.SummaryLevel, Is.EqualTo(2));

            Assert.That(response.Tests.RaceServerNetwork.RaceServerConnectivityUSWestCoast.Description, Is.EqualTo("Result is a 0 or a 1. 0 means the farm is experiencing connectivity issues."));
            Assert.That(response.Tests.RaceServerNetwork.RaceServerConnectivityUSWestCoast.Result, Is.Not.Null.And.Length.EqualTo(241));
            Assert.That(response.Tests.RaceServerNetwork.RaceServerConnectivityUSWestCoast.SummaryLabel, Is.EqualTo("Okay"));
            Assert.That(response.Tests.RaceServerNetwork.RaceServerConnectivityUSWestCoast.SummaryLevel, Is.EqualTo(2));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetTimeAttackSeriesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetTimeAttackSeriesSuccessfulAsync), false).ConfigureAwait(false);

        var response = await testDataClient.GetTimeAttackSeasonsAsync().ConfigureAwait(false);

        Assert.That(response, Is.Not.Null);
        Assert.That(response, Has.Length.EqualTo(51));

        var competition1001 = response.SingleOrDefault(c => c.CompetitionId == 1001);

        Assert.Multiple(() =>
        {
            Assert.That(competition1001, Is.Not.Null);

#if NET6_0_OR_GREATER
            Assert.That(competition1001!.StartDate, Is.EqualTo(new DateOnly(2022, 12, 13)));
            Assert.That(competition1001.EndDate, Is.EqualTo(new DateOnly(2023, 3, 5)));
#else
            Assert.That(competition1001.StartDate, Is.EqualTo(new DateTime(2022, 12, 13)));
            Assert.That(competition1001.EndDate, Is.EqualTo(new DateTime(2023, 3, 5)));
#endif
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetTimeAttackMemberSeasonResultsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetTimeAttackMemberSeasonResultsSuccessfulAsync)).ConfigureAwait(false);

        var response = await testDataClient.GetTimeAttackMemberSeasonResultsAsync(3212).ConfigureAwait(false);

        Assert.That(response, Is.Not.Null);
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data, Has.Length.EqualTo(2));

        var itemZero = response.Data[0];
        Assert.That(itemZero, Is.Not.Null);
        Assert.That(itemZero, Has.Property(nameof(TimeAttackMemberSeasonResult.CustomerId)).EqualTo(341554)
                                 .And.Property(nameof(TimeAttackMemberSeasonResult.TrackId)).EqualTo(218)
                                 .And.Property(nameof(TimeAttackMemberSeasonResult.CompetitionSeasonId)).EqualTo(3212)
                                 .And.Property(nameof(TimeAttackMemberSeasonResult.BestLapTime)).EqualTo(TimeSpan.FromSeconds(76.2649)));

        var itemOne = response.Data[1];
        Assert.That(itemOne, Has.Property(nameof(TimeAttackMemberSeasonResult.CustomerId)).EqualTo(341554)
                                .And.Property(nameof(TimeAttackMemberSeasonResult.TrackId)).EqualTo(341)
                                .And.Property(nameof(TimeAttackMemberSeasonResult.CompetitionSeasonId)).EqualTo(3212)
                                .And.Property(nameof(TimeAttackMemberSeasonResult.BestLapTime)).EqualTo(TimeSpan.FromSeconds(90.8513)));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberRecapSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberRecapSuccessfulAsync)).ConfigureAwait(false);

        var response = await testDataClient.GetMemberRecapAsync().ConfigureAwait(false);

        Assert.That(response, Is.Not.Null);
        Assert.That(response.Data, Is.Not.Null);

        Assert.That(response.Data, Has.Property(nameof(MemberRecap.CustomerId)).EqualTo(341554)
                                      .And.Property(nameof(MemberRecap.Success)).EqualTo(true)
                                      .And.Property(nameof(MemberRecap.Season)).Null
                                      .And.Property(nameof(MemberRecap.Year)).EqualTo(2023));

        Assert.That(response.Data.Statistics, Is.Not.Null);

        Assert.That(response.Data.Statistics, Has.Property(nameof(RecapStatistics.NumberOfStarts)).EqualTo(34)
                                                 .And.Property(nameof(RecapStatistics.TotalLapsLed)).EqualTo(40)
                                                 .And.Property(nameof(RecapStatistics.FavoriteCar)).Not.Null
                                                 .And.Property(nameof(RecapStatistics.FavoriteTrack)).Not.Null);
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSpectatorSubsessionIdentifiersAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSpectatorSubsessionIdentifiersAsync)).ConfigureAwait(false);

        var response = await testDataClient.GetSpectatorSubsessionIdentifiersAsync().ConfigureAwait(false);

        Assert.That(response, Is.Not.Null);
        Assert.That(response.Data, Is.Not.Null);

        Assert.That(response.Data, Has.Property(nameof(SpectatorSubsessionIds.EventTypes)).EqualTo(new[] { Common.EventType.Qualify, Common.EventType.Practice, Common.EventType.TimeTrial, Common.EventType.Race })
                                      .And.Property(nameof(SpectatorSubsessionIds.Success)).EqualTo(true)
                                      .And.Property(nameof(SpectatorSubsessionIds.SubsessionIdentifiers)).Length.EqualTo(192));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSpectatorSubsessionDetailsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSpectatorSubsessionDetailsSuccessfulAsync)).ConfigureAwait(false);

        var response = await testDataClient.GetSpectatorSubsessionDetailsAsync().ConfigureAwait(false);

        Assert.That(response, Is.Not.Null);
        Assert.That(response.Data, Is.Not.Null);

        Assert.That(response.Data, Has.Property(nameof(SpectatorDetails.EventTypes)).EqualTo(new[] { Common.EventType.TimeTrial, Common.EventType.Qualify, Common.EventType.Practice, Common.EventType.Race })
                                      .And.Property(nameof(SpectatorDetails.Success)).EqualTo(true)
                                      .And.Property(nameof(SpectatorDetails.Subsessions)).Length.EqualTo(300));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetWeatherForecastAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetWeatherForecastAsync), false).ConfigureAwait(false);

        var response = await testDataClient.GetWeatherForecastFromUrlAsync(new Uri("http://example.com")).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.Not.Empty);
            Assert.That(response.Count(), Is.EqualTo(8));

            Assert.That(response,
                Has.All.Property(nameof(WeatherForecast.TimeOffset)).Not.Zero
                    .And.Property(nameof(WeatherForecast.RawAirTemperature)).Not.Zero
                    .And.Property(nameof(WeatherForecast.PrecipitationChance)).Not.Default
                    .And.Property(nameof(WeatherForecast.Index)).Not.Default
                    .And.Property(nameof(WeatherForecast.IsSunUp)).Not.Default
                    .And.Property(nameof(WeatherForecast.Pressure)).Not.Default
                    .And.Property(nameof(WeatherForecast.WindDirectionDegrees)).Not.Default
                    .And.Property(nameof(WeatherForecast.AirTemperature)).Not.Default
                    .And.Property(nameof(WeatherForecast.ValidStatistics)).Not.Default
                    .And.Property(nameof(WeatherForecast.AffectsSession)).Not.Default
                    .And.Property(nameof(WeatherForecast.CloudCoverPercentage)).Not.Default
                    .And.Property(nameof(WeatherForecast.RelativeHumidity)).Not.Default
                    .And.Property(nameof(WeatherForecast.WindSpeed)).Not.Default
                    .And.Property(nameof(WeatherForecast.AllowPrecipitation)).Not.Null
                    .And.Property(nameof(WeatherForecast.PrecipitationAmount)).Not.Null
                    .And.Property(nameof(WeatherForecast.Timestamp)).Not.Null);

            var forecast = response.First();
            Assert.That(forecast.TimeOffset, Is.EqualTo(TimeSpan.FromMinutes(-385)));
            Assert.That(forecast.RawAirTemperature, Is.EqualTo(7.72m));
            Assert.That(forecast.PrecipitationChance, Is.EqualTo(100m));
            Assert.That(forecast.Index, Is.EqualTo(0));
            Assert.That(forecast.IsSunUp, Is.EqualTo(true));
            Assert.That(forecast.Pressure, Is.EqualTo(967.1m));
            Assert.That(forecast.WindDirectionDegrees, Is.EqualTo(239));
            Assert.That(forecast.WindDirection, Is.EqualTo(WindDirection.SouthWest));
            Assert.That(forecast.AirTemperature, Is.EqualTo(18.63m));
            Assert.That(forecast.ValidStatistics, Is.EqualTo(true));
            Assert.That(forecast.AffectsSession, Is.EqualTo(false));
            Assert.That(forecast.CloudCoverPercentage, Is.EqualTo(76.6m));
            Assert.That(forecast.RelativeHumidity, Is.EqualTo(99.99m));
            Assert.That(forecast.WindSpeed, Is.EqualTo(6.07m));
            Assert.That(forecast.AllowPrecipitation, Is.EqualTo(true));
            Assert.That(forecast.PrecipitationAmount, Is.EqualTo(4.2m));
            Assert.That(forecast.Timestamp, Is.EqualTo(new DateTime(2024, 04, 13, 12, 0, 0, DateTimeKind.Utc)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberProfileFailsWithGatewayTimeoutAsync()
    {
        await MessageHandler.QueueResponsesAsync("ResponseUnknown504", true).ConfigureAwait(false);

        Assert.That(async () => await testDataClient.GetMemberProfileAsync(341554).ConfigureAwait(false),
                    Throws.InstanceOf<iRacingUnknownResponseException>()
                          .And.Property(nameof(iRacingUnknownResponseException.ResponseHttpStatusCode)).EqualTo(HttpStatusCode.GatewayTimeout));
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetMemberProfileFailsOnLoginWithGatewayTimeoutAsync()
    {
        await MessageHandler.QueueResponsesAsync("ResponseUnknown504", false).ConfigureAwait(false);

        Assert.That(async () => await testDataClient.GetMemberProfileAsync(341554).ConfigureAwait(false),
                    Throws.InstanceOf<iRacingLoginFailedException>());
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetLookupWithExpiredAuthWorksAsync()
    {
        var responseResourceNames = new string[] {
            "Aydsko.iRacingData.UnitTests.Responses.SuccessfulLogin.json",
            "Aydsko.iRacingData.UnitTests.Responses.GetLookupWithExpiredAuthWorksAsync.1.json", // Link to result
            "Aydsko.iRacingData.UnitTests.Responses.GetLookupWithExpiredAuthWorksAsync.2.json", // Result
            "Aydsko.iRacingData.UnitTests.Responses.GetLookupWithExpiredAuthWorksAsync.3.json", // Unauthorised
            "Aydsko.iRacingData.UnitTests.Responses.SuccessfulLogin.json",
            "Aydsko.iRacingData.UnitTests.Responses.GetLookupWithExpiredAuthWorksAsync.4.json",
            "Aydsko.iRacingData.UnitTests.Responses.GetLookupWithExpiredAuthWorksAsync.5.json"
        };

        foreach (var resourceName in responseResourceNames)
        {
            await MessageHandler.QueueResponseFromManifestResourceAsync(resourceName).ConfigureAwait(false);
        }

        var lookupGroups = await testDataClient.GetLookupsAsync().ConfigureAwait(false);
        var lookupGroups2 = await testDataClient.GetLookupsAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(lookupGroups, Is.Not.Null);
            Assert.That(lookupGroups!.Data, Is.Not.Null);

            Assert.That(lookupGroups.Data, Has.Length.EqualTo(2));

            Assert.That(lookupGroups2, Is.Not.Null);
            Assert.That(lookupGroups2!.Data, Is.Not.Null);

            Assert.That(lookupGroups2.Data, Has.Length.EqualTo(2));

            // TODO: Check that the second authentication call was actually made.
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task ListHostedSessionsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(ListHostedSessionsSuccessfulAsync)).ConfigureAwait(false);

        var response = await testDataClient.ListHostedSessionsAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response!.Data, Is.Not.Null);

            Assert.That(response.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(response.TotalRateLimit, Is.EqualTo(100));
            Assert.That(response.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(response.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task ListHostedSessionsCombinedSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(ListHostedSessionsCombinedSuccessfulAsync)).ConfigureAwait(false);

        var response = await testDataClient.ListHostedSessionsCombinedAsync().ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response!.Data, Is.Not.Null);

            Assert.That(response.RateLimitRemaining, Is.EqualTo(99));
            Assert.That(response.TotalRateLimit, Is.EqualTo(100));
            Assert.That(response.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
            Assert.That(response.DataExpires, Is.EqualTo(new DateTimeOffset(2022, 8, 27, 11, 23, 19, 507, TimeSpan.Zero)));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GetSeasonSuperSessionStandingsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetSeasonSuperSessionStandingsSuccessfulAsync)).ConfigureAwait(false);

        var response = await testDataClient.GetSeasonSuperSessionStandingsAsync(1, 1).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response!.Data.Header, Is.Not.Null);
            Assert.That(response!.Data.Results, Is.Not.Null.And.Not.Empty);

            var header = response.Data.Header;

            Assert.That(header.SeriesName, Is.EqualTo("iRacing Chili Bowl Sim-Motion Nationals"));
            Assert.That(header.SeasonName, Is.EqualTo("2023 iRacing Chili Bowl Sim-Motion Nationals"));
            Assert.That(header.LastUpdated, Is.EqualTo(DateTimeOffset.ParseExact("2024-10-19T04:12:41.7826237Z", "yyyy-MM-ddTHH:mm:ss.fffffffZ", CultureInfo.InvariantCulture)));

            var results = response.Data.Results;

            Assert.That(results, Has.Length.EqualTo(header.ChunkInfo.Rows));

            var exampleResult = results.SingleOrDefault(r => r.Rank == 10);
            Assert.That(exampleResult, Is.Not.Null);
            Assert.That(exampleResult!.CustomerId, Is.EqualTo(511237));
            Assert.That(exampleResult.DisplayName, Is.EqualTo("Colt Currie"));

            Assert.That(response.RateLimitRemaining, Is.EqualTo(239));
            Assert.That(response.TotalRateLimit, Is.EqualTo(240));
            Assert.That(response.RateLimitReset, Is.EqualTo(new DateTimeOffset(2024, 10, 19, 04, 13, 41, TimeSpan.Zero)));
            Assert.That(response.DataExpires, Is.Null);
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            testDataClient?.Dispose();
        }
        base.Dispose(disposing);
    }
}
