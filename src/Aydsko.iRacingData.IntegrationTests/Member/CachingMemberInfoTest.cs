// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests.Member;

internal class CachingMemberInfoTest : CachingIntegrationFixture
{
    [Test]
    public async Task TestMemberInfoAsync()
    {
        var memberInfo = await Client.GetMyInfoAsync().ConfigureAwait(false);

        Assert.That(memberInfo, Is.Not.Null);
        Assert.That(memberInfo.Data, Is.Not.Null);

        Assert.That(memberInfo.Data.Username, Is.EqualTo(Configuration["iRacingData:Username"]));

        var memberInfo2 = await Client.GetMyInfoAsync().ConfigureAwait(false);

        Assert.That(memberInfo2, Is.Not.Null);
        Assert.That(memberInfo2.Data, Is.Not.Null);

        Assert.That(memberInfo2.Data.Username, Is.EqualTo(Configuration["iRacingData:Username"]));

        var stats = MemoryCache.GetCurrentStatistics();
        Assert.Multiple(() =>
        {
            Assert.That(stats?.TotalHits, Is.Not.Null.And.EqualTo(1), "TotalHits didn't match.");
            Assert.That(stats?.TotalMisses, Is.Not.Null.And.EqualTo(1), "TotalMisses didn't match.");
        });
    }
}
