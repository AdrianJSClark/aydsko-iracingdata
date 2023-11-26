// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests.Tracks;

internal sealed class CachingGetTracksTests : CachingIntegrationFixture
{
    [Test]
    public async Task GetTracksTestAsync()
    {
        var tracksResponse = await Client.GetTracksAsync(CancellationToken.None).ConfigureAwait(false);
        Assert.That(tracksResponse.Data, Is.Not.Null.Or.Empty);

        var tracksResponse2 = await Client.GetTracksAsync(CancellationToken.None).ConfigureAwait(false);
        Assert.That(tracksResponse2.Data, Is.Not.Null.Or.Empty);

        var stats = MemoryCache.GetCurrentStatistics();
        Assert.Multiple(() =>
        {
            Assert.That(stats?.TotalHits, Is.Not.Null.And.EqualTo(1), "TotalHits didn't match.");
            Assert.That(stats?.TotalMisses, Is.Not.Null.And.EqualTo(1), "TotalMisses didn't match.");
        });
    }
}
