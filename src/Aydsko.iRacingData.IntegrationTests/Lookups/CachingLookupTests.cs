﻿// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests.Lookups;

internal sealed class CachingLookupTests : CachingIntegrationFixture
{
    [Test(TestOf = typeof(DataClient))]
    public async Task TestClubHistoryLookupsAreCachedAsync()
    {
        var clubHistory = await Client.GetClubHistoryLookupsAsync(2022, 1).ConfigureAwait(false);

        Assert.That(clubHistory, Is.Not.Null);
        Assert.That(clubHistory.Data, Is.Not.Null);

        Assert.That(clubHistory.Data, Has.Length.EqualTo(42));

        var clubHistory2 = await Client.GetClubHistoryLookupsAsync(2022, 1).ConfigureAwait(false);

        Assert.That(clubHistory2, Is.Not.Null);
        Assert.That(clubHistory2.Data, Is.Not.Null);

        Assert.That(clubHistory2.Data, Has.Length.EqualTo(42));

        var stats = MemoryCache.GetCurrentStatistics();
        Assert.Multiple(() =>
        {
            Assert.That(stats?.TotalHits, Is.Not.Null.And.EqualTo(1), "TotalHits didn't match.");
            Assert.That(stats?.TotalMisses, Is.Not.Null.And.EqualTo(1), "TotalMisses didn't match.");
        });
    }
}
