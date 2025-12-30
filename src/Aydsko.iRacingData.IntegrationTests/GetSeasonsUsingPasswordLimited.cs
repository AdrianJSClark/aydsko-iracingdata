// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.DependencyInjection;

namespace Aydsko.iRacingData.IntegrationTests;

internal sealed class GetSeasonsUsingPasswordLimited
    : ServiceProviderIntegrationFixture
{
    public const int GrandPrixSeriesId = 260;

    [Test]
    public async Task GetSeasonsUsingPasswordLimitedAsync()
    {
        var client = ServiceProvider.GetRequiredService<IDataClient>();

        var seasons = await client.GetSeasonsAsync(true)
                                  .ConfigureAwait(false);

        Assert.That(seasons, Is.Not.Null);
        Assert.That(seasons.Data, Has.Length.GreaterThan(0));
    }

    [Test]
    public async Task GetGrandPrixSeriesWeek12WeatherAsync()
    {
        var client = ServiceProvider.GetRequiredService<IDataClient>();

        var seasons = await client.GetSeasonsAsync(true)
                                  .ConfigureAwait(false);

        Assert.That(seasons?.Data, Is.Not.Null);

        var gpSeries = seasons.Data.Single(s => s.SeriesId == GrandPrixSeriesId);
        var gpSeriesWeek12Weather = gpSeries.Schedules.Single(sch => sch.RaceWeekNumber == 12).Weather;

        Assert.That(gpSeriesWeek12Weather, Is.Not.Null);
        Assert.That(gpSeriesWeek12Weather.WeatherUrl, Is.Not.Null);

        var forecast = await client.GetWeatherForecastFromUrlAsync(new Uri(gpSeriesWeek12Weather.WeatherUrl))
                                   .ConfigureAwait(false);

        Assert.That(forecast, Is.Not.Null);
    }
}
