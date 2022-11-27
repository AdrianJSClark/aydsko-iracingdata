using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aydsko.iRacingData.IntegrationTests.Results;
public class ResultsGetTest : BaseIntegrationFixture
{
    [Test]
    public async Task GivenAValidSubsessionIdThenAResultIsReturned()
    {
        var results = await Client.GetSubSessionResultAsync(50033865, true);

        Assert.Multiple(() =>
        {
            Assert.That(results, Is.Not.Null);
            Assert.That(results.Data, Is.Not.Null);
        });
    }
}
