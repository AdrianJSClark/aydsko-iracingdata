using Aydsko.iRacingData.Tracks;

namespace Aydsko.iRacingData.IntegrationTests.Tracks;

public class GetTracksTests : BaseIntegrationFixture
{
    private Track[] tracksData;

    [SetUp]
    public async Task SetUp()
    {
        var tracksResponse = await Client.GetTracksAsync(CancellationToken.None).ConfigureAwait(false);
        tracksData = tracksResponse.Data;
    }

    [TestCaseSource(nameof(GetTestCases))]
    public decimal? CheckTrackLength(int trackId)
    {
        var track = tracksData.SingleOrDefault(t => t.TrackId == trackId);
        return track?.TrackConfigLengthKm;
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new(390) { ExpectedResult = 4.57M };
        yield return new(391) { ExpectedResult = 3.68M };
        yield return new(197) { ExpectedResult = 1.39M };
        yield return new(152) { ExpectedResult = 4.44M };
        yield return new(151) { ExpectedResult = 1.44M };
        yield return new(95) { ExpectedResult = 5.95M };
        yield return new(50) { ExpectedResult = 6.51M };
        yield return new(341) { ExpectedResult = 5.89M };
    }
}
