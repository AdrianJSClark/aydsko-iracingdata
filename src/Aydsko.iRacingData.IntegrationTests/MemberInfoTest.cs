// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests;

internal sealed class MemberInfoTest
    : DataClientIntegrationFixture
{
    [Test]
    public async Task TestMemberInfoAsync()
    {
        if (Configuration["iRacingData:CustomerId"] is not string customerIdValue
            || !int.TryParse(customerIdValue, out var iRacingCustomerId))
        {
            throw new InvalidOperationException("iRacing Customer Id value not found in configuration.");
        }

        var memberInfo = await Client.GetMyInfoAsync().ConfigureAwait(false);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(memberInfo, Is.Not.Null);
            Assert.That(memberInfo.Data, Is.Not.Null);

            Assert.That(memberInfo.Data.CustomerId, Is.EqualTo(iRacingCustomerId));
        }
    }
}
