// © 2023-2024 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests.Member;

internal sealed class CachingMemberInfoTest : CachingIntegrationFixture
{
    [Test]
    public async Task TestMemberInfoAsync()
    {
        if (Configuration["iRacingData:CustomerId"] is not string customerIdValue || !int.TryParse(customerIdValue, out var iRacingCustomerId))
        {
            throw new InvalidOperationException("iRacing Customer Id value not found in configuration.");
        }

        var memberInfo = await Client.GetMyInfoAsync().ConfigureAwait(false);
        var memberInfo2 = await Client.GetMyInfoAsync().ConfigureAwait(false);

        var stats = MemoryCache.GetCurrentStatistics();

        Assert.Multiple(() =>
        {
            Assert.That(memberInfo, Is.Not.Null);
            Assert.That(memberInfo.Data, Is.Not.Null);

            Assert.That(memberInfo.Data.CustomerId, Is.EqualTo(iRacingCustomerId));

            Assert.That(memberInfo2, Is.Not.Null);
            Assert.That(memberInfo2.Data, Is.Not.Null);

            Assert.That(memberInfo2.Data.CustomerId, Is.EqualTo(iRacingCustomerId));

            Assert.That(stats?.TotalHits, Is.Not.Null.And.EqualTo(1), "TotalHits didn't match.");
            Assert.That(stats?.TotalMisses, Is.Not.Null.And.EqualTo(1), "TotalMisses didn't match.");
        });
    }
}
