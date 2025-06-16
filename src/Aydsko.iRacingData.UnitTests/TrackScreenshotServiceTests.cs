using Aydsko.iRacingData.Tracks;

namespace Aydsko.iRacingData.UnitTests;

#pragma warning disable CS0618 // Type or member is obsolete
internal sealed class TrackScreenshotServiceTests : MockedHttpTestBase
{
    // NUnit will ensure that "SetUp" runs before each test so these can all be forced to "null".
    private TrackScreenshotService sut = null!;

    [SetUp]
    public async Task SetUpAsync()
    {
        testDataClient = new DataClient(HttpClient,
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

        sut = new TrackScreenshotService(testDataClient);
    }

    [Test]
    public async Task GivenHungaroringTrackId_ThenGetScreenshotsByTrackIdReturnsCorrectResultsAsync()
    {
        const int hungaroringTrackId = 413;
        var hungaroringResults = await sut.GetScreenshotLinksAsync(hungaroringTrackId).ConfigureAwait(false);

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
        var suzukaResults = await sut.GetScreenshotLinksAsync(suzukaTrackId).ConfigureAwait(false);

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
        }
        base.Dispose(disposing);
    }
}
#pragma warning restore CS0618 // Type or member is obsolete
