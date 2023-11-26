// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests.Results;

internal sealed class CachingResultsGetTest : CachingIntegrationFixture
{
    [Test]
    public async Task GivenAValidSubsessionIdThenAResultIsReturnedAsync()
    {
        var results = await Client.GetSubSessionResultAsync(50033865, true).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(results, Is.Not.Null);
            Assert.That(results.Data, Is.Not.Null);
        });

        var results2 = await Client.GetSubSessionResultAsync(50033865, true).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(results2, Is.Not.Null);
            Assert.That(results2.Data, Is.Not.Null);
        });

        var stats = MemoryCache.GetCurrentStatistics();
        Assert.Multiple(() =>
        {
            Assert.That(stats?.TotalHits, Is.Not.Null.And.EqualTo(1), "TotalHits didn't match.");
            Assert.That(stats?.TotalMisses, Is.Not.Null.And.EqualTo(1), "TotalMisses didn't match.");
        });
    }
}
