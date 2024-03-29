﻿// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests.Lookups;

internal sealed class LookupTests : DataClientIntegrationFixture
{
    [Test]
    public async Task TestClubHistoryLookupsAsync()
    {
        var clubHistory = await Client.GetClubHistoryLookupsAsync(2022, 1).ConfigureAwait(false);

        Assert.That(clubHistory, Is.Not.Null);
        Assert.That(clubHistory.Data, Is.Not.Null);

        Assert.That(clubHistory.Data, Has.Length.EqualTo(42));
    }
}
