// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests.Member;

public class MemberInfoTest : BaseIntegrationFixture
{
    [Test]
    public async Task TestMemberInfoAsync()
    {
        var memberInfo = await Client.GetMyInfoAsync().ConfigureAwait(false);

        Assert.That(memberInfo, Is.Not.Null);
        Assert.That(memberInfo.Data, Is.Not.Null);

        Assert.That(memberInfo.Data.Username, Is.EqualTo(Configuration["iRacingData:Username"]));
    }
}
