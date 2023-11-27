namespace Aydsko.iRacingData.UnitTests;

internal sealed class DataClientTrackAssetScreenshotUrisTests : MockedHttpTestBase
{
    // NUnit will ensure that "SetUp" runs before each test so these can all be forced to "null".
    private DataClient? sut = null!;

    [SetUp]
    public async Task SetUpAsync()
    {
        BaseSetUp();
        var dataClient = new DataClient(HttpClient,
                                        new TestLogger<DataClient>(),
                                        new iRacingDataClientOptions()
                                        {
                                            Username = "test.user@example.com",
                                            Password = "SuperSecretPassword",
                                            CurrentDateTimeSource = () => new DateTimeOffset(2022, 04, 05, 0, 0, 0, TimeSpan.Zero)
                                        },
                                        new System.Net.CookieContainer());

        // Make use of our captured responses.
        await MessageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetTracksSuccessfulAsync)).ConfigureAwait(false);
        await MessageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetTrackAssetsSuccessfulAsync), false).ConfigureAwait(false);

        sut = dataClient;
    }

    [Test]
    public async Task GivenHungaroringTrackId_ThenGetTrackAssetScreenshotUrisAsyncReturnsCorrectResultsAsync()
    {
        const int hungaroringTrackId = 413;
        var hungaroringResults = await sut!.GetTrackAssetScreenshotUrisAsync(hungaroringTrackId).ConfigureAwait(false);

        Assert.That(hungaroringResults?.Count(), Is.EqualTo(11));
        Assert.Multiple(() =>
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
        });
    }

    [Test]
    public async Task GivenSuzukaTrackId_ThenGetScreenshotsByTrackIdReturnsCorrectResultsAsync()
    {
        const int suzukaTrackId = 168;
        var suzukaResults = await sut!.GetTrackAssetScreenshotUrisAsync(suzukaTrackId).ConfigureAwait(false);

        Assert.That(suzukaResults?.Count(), Is.EqualTo(4));
        Assert.Multiple(() =>
        {
            Assert.That(suzukaResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/114_screenshots/01.jpg")));
            Assert.That(suzukaResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/114_screenshots/02.jpg")));
            Assert.That(suzukaResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/114_screenshots/03.jpg")));
            Assert.That(suzukaResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/114_screenshots/04.jpg")));
        });
    }

    [Test]
    public async Task GivenHungaroringTrack_ThenGetTrackAssetScreenshotUrisReturnsCorrectResultsAsync()
    {
        var hungaroringTrack = (await sut!.GetTracksAsync().ConfigureAwait(false)).Data.Single(t => t.TrackId == 413);
        var hungaroringTrackAssets = (await sut.GetTrackAssetsAsync().ConfigureAwait(false)).Data["413"];

        var hungaroringResults = sut.GetTrackAssetScreenshotUris(hungaroringTrack, hungaroringTrackAssets);

        Assert.That(hungaroringResults?.Count(), Is.EqualTo(11));
        Assert.Multiple(() =>
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
        });
    }

    [Test]
    public async Task GivenSuzukaTrack_ThenGetTrackAssetScreenshotUrisReturnsCorrectResultsAsync()
    {
        var suzukaTrack = (await sut!.GetTracksAsync().ConfigureAwait(false)).Data.Single(t => t.TrackId == 168);
        var suzukaTrackAssets = (await sut.GetTrackAssetsAsync().ConfigureAwait(false)).Data["168"];

        var suzukaResults = sut.GetTrackAssetScreenshotUris(suzukaTrack, suzukaTrackAssets);

        Assert.That(suzukaResults?.Count(), Is.EqualTo(4));
        Assert.Multiple(() =>
        {
            Assert.That(suzukaResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/114_screenshots/01.jpg")));
            Assert.That(suzukaResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/114_screenshots/02.jpg")));
            Assert.That(suzukaResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/114_screenshots/03.jpg")));
            Assert.That(suzukaResults, Contains.Item(new Uri("https://dqfp1ltauszrc.cloudfront.net/public/track-maps-screenshots/114_screenshots/04.jpg")));
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            sut?.Dispose();
        }
        base.Dispose(disposing);
    }
}
