// © 2023-2024 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests.Member;

internal sealed class MemberInfoTest : DataClientIntegrationFixture
{
    [Test]
    public async Task TestMemberInfoAsync()
    {
        var iRacingUsername = Configuration["iRacingData:Username"] ?? throw new InvalidOperationException("iRacing Username not found in configuration.");

        var memberInfo = await Client.GetMyInfoAsync().ConfigureAwait(false);

        Assert.That(memberInfo, Is.Not.Null);
        Assert.That(memberInfo.Data, Is.Not.Null);

        Assert.That(memberInfo.Data.Username, Is.EqualTo(Security.ObfuscateUsernameOrEmail(iRacingUsername)));
    }
}
