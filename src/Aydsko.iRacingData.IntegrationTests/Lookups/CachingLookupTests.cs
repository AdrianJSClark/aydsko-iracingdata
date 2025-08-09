// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests.Lookups;

internal sealed class CachingLookupTests : CachingIntegrationFixture
{
    [Test(TestOf = typeof(DataClient))]
    public async Task TestLicenseLookupsAreCachedAsync()
    {
        var license = await Client.GetLicenseLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        Assert.That(license, Is.Not.Null);
        Assert.That(license.Data, Is.Not.Null);

        Assert.That(license.Data, Has.Length.EqualTo(7));

        var license2 = await Client.GetLicenseLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        Assert.That(license2, Is.Not.Null);
        Assert.That(license2.Data, Is.Not.Null);

        Assert.That(license2.Data, Has.Length.EqualTo(7));

        var stats = MemoryCache.GetCurrentStatistics();
        Assert.Multiple(() =>
        {
            Assert.That(stats?.TotalHits, Is.Not.Null.And.EqualTo(1), "TotalHits didn't match.");
            Assert.That(stats?.TotalMisses, Is.Not.Null.And.EqualTo(1), "TotalMisses didn't match.");
        });
    }
}
