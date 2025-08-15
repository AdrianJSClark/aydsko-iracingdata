// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.UnitTests;

internal sealed class DataClientTrackAssetScreenshotUrisTests : MockedHttpTestBase
{
    [SetUp]
    public async Task SetUpAsync()
    {
        var options = new iRacingDataClientOptions()
        {
            Username = "test.user@example.com",
            Password = "SuperSecretPassword",
            CurrentDateTimeSource = () => new DateTimeOffset(2022, 04, 05, 0, 0, 0, TimeSpan.Zero)
        };

        var client = new TestLegacyUsernamePasswordApiClient(HttpClient,
                                                             options,
                                                             CookieContainer,
                                                             new TestLogger<LegacyUsernamePasswordApiClient>());
        var sut = new DataClient(client, options, new TestLogger<DataClient>());

        // Make use of our captured responses.
        await MessageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetTracksSuccessfulAsync))
                            .ConfigureAwait(false);
        await MessageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetTrackAssetsSuccessfulAsync), false)
                            .ConfigureAwait(false);

        apiClient = client;
        testDataClient = sut;
    }

    [Test]
    public async Task GivenHungaroringTrackId_ThenGetTrackAssetScreenshotUrisAsyncReturnsCorrectResultsAsync()
    {
        const int hungaroringTrackId = 413;
        var hungaroringResults = await testDataClient!.GetTrackAssetScreenshotUrisAsync(hungaroringTrackId).ConfigureAwait(false);

        Assert.That(hungaroringResults?.Count(), Is.EqualTo(11));

        using (Assert.EnterMultipleScope())
        {
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/01.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/02.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/03.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/04.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/05.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/06.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/07.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/08.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/09.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/10.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/11.jpg")));
        }
    }

    [Test]
    public async Task GivenSuzukaTrackId_ThenGetScreenshotsByTrackIdReturnsCorrectResultsAsync()
    {
        const int suzukaTrackId = 168;
        var suzukaResults = await testDataClient!.GetTrackAssetScreenshotUrisAsync(suzukaTrackId).ConfigureAwait(false);

        Assert.That(suzukaResults?.Count(), Is.EqualTo(4));

        using (Assert.EnterMultipleScope())
        {
            Assert.That(suzukaResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/114_screenshots/01.jpg")));
            Assert.That(suzukaResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/114_screenshots/02.jpg")));
            Assert.That(suzukaResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/114_screenshots/03.jpg")));
            Assert.That(suzukaResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/114_screenshots/04.jpg")));
        }
    }

    [Test]
    public async Task GivenHungaroringTrack_ThenGetTrackAssetScreenshotUrisReturnsCorrectResultsAsync()
    {
        var hungaroringTrack = (await testDataClient!.GetTracksAsync().ConfigureAwait(false)).Data.Single(t => t.TrackId == 413);
        var hungaroringTrackAssets = (await testDataClient.GetTrackAssetsAsync().ConfigureAwait(false)).Data["413"];

        var hungaroringResults = testDataClient.GetTrackAssetScreenshotUris(hungaroringTrack, hungaroringTrackAssets);

        Assert.That(hungaroringResults?.Count(), Is.EqualTo(11));

        using (Assert.EnterMultipleScope())
        {
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/01.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/02.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/03.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/04.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/05.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/06.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/07.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/08.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/09.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/10.jpg")));
            Assert.That(hungaroringResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/370_screenshots/11.jpg")));
        }
    }

    [Test]
    public async Task GivenSuzukaTrack_ThenGetTrackAssetScreenshotUrisReturnsCorrectResultsAsync()
    {
        var suzukaTrack = (await testDataClient!.GetTracksAsync().ConfigureAwait(false)).Data.Single(t => t.TrackId == 168);
        var suzukaTrackAssets = (await testDataClient.GetTrackAssetsAsync().ConfigureAwait(false)).Data["168"];

        var suzukaResults = testDataClient.GetTrackAssetScreenshotUris(suzukaTrack, suzukaTrackAssets);

        Assert.That(suzukaResults?.Count(), Is.EqualTo(4));

        using (Assert.EnterMultipleScope())
        {
            Assert.That(suzukaResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/114_screenshots/01.jpg")));
            Assert.That(suzukaResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/114_screenshots/02.jpg")));
            Assert.That(suzukaResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/114_screenshots/03.jpg")));
            Assert.That(suzukaResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/114_screenshots/04.jpg")));
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            apiClient?.Dispose();
        }
        base.Dispose(disposing);
    }
}
