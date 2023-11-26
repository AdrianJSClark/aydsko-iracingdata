// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests.Results;

internal sealed class ResultsGetTest : DataClientIntegrationFixture
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
    }
}
