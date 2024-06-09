namespace Aydsko.iRacingData.IntegrationTests.Stats;

internal sealed class DriverStatisticsByCategoryCsvTests : DataClientIntegrationFixture
{
    [Test]
    public async Task TestDriverStatisticsByCategoryCsvAsync()
    {
        var driverStats = await Client.GetDriverStatisticsByCategoryCsvAsync(4).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(driverStats, Is.Not.Null);
            Assert.That(driverStats.FileName, Is.Not.Null.Or.Empty);
            Assert.That(driverStats.FileName, Is.EqualTo("Dirt_Road_driver_stats.csv"));
            Assert.That(driverStats.ContentBytes, Is.Not.Null);
            Assert.That(driverStats.ContentBytes, Is.Not.Empty);
        });
    }
}
