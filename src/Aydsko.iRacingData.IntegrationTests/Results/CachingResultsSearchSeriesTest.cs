// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.IntegrationTests.Results;

internal sealed class CachingResultsSearchSeriesTest : CachingIntegrationFixture
{
    [Test(TestOf = typeof(DataClient))]
    public async Task GivenValidSearchParametersTheCorrectResultIsReturnedAsync()
    {
        var searchParameters = new Searches.OfficialSearchParameters
        {
            StartRangeBegin = new DateTime(2022, 6, 1, 0, 0, 0),
            StartRangeEnd = new DateTime(2022, 6, 30, 23, 59, 59, 999),
            EventTypes = new[] { 5 },
            OfficialOnly = true,
            ParticipantCustomerId = 341554
        };

        var searchResults = await Client.SearchOfficialResultsAsync(searchParameters).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(searchResults, Is.Not.Null);
            Assert.That(searchResults.Data.Header, Is.Not.Null);
            Assert.That(searchResults.Data.Items, Is.Not.Null.Or.Empty);
            Assert.That(searchResults.Data.Items, Has.Length.EqualTo(10));
        });

        var searchResults2 = await Client.SearchOfficialResultsAsync(searchParameters).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(searchResults2, Is.Not.Null);
            Assert.That(searchResults2.Data.Header, Is.Not.Null);
            Assert.That(searchResults2.Data.Items, Is.Not.Null.Or.Empty);
            Assert.That(searchResults2.Data.Items, Has.Length.EqualTo(10));
        });

        var stats = MemoryCache.GetCurrentStatistics();
        Assert.Multiple(() =>
        {
            Assert.That(stats?.TotalHits, Is.Not.Null.And.EqualTo(1));
            Assert.That(stats?.TotalMisses, Is.Not.Null.And.EqualTo(1));
        });
    }

    [Test(TestOf = typeof(DataClient))]
    public async Task GivenSearchParametersThatResultInZeroResultsTheCorrectResultIsReturnedAsync()
    {
        var searchParameters = new Searches.OfficialSearchParameters
        {
            StartRangeBegin = new DateTime(2022, 6, 1, 0, 0, 0),
            StartRangeEnd = new DateTime(2022, 6, 30, 23, 59, 59, 999),
            EventTypes = new[] { 5 },
            OfficialOnly = true,
            ParticipantCustomerId = 341554,
            CategoryIds = new[] { 1 }
        };

        var searchResults = await Client.SearchOfficialResultsAsync(searchParameters).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(searchResults, Is.Not.Null);
            Assert.That(searchResults.Data.Header, Is.Not.Null);
            Assert.That(searchResults.Data.Header.Data.Success, Is.True);

            Assert.That(searchResults.Data.Items, Is.Not.Null);
            Assert.That(searchResults.Data.Items, Has.Length.EqualTo(0));
        });

        var searchResults2 = await Client.SearchOfficialResultsAsync(searchParameters).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(searchResults2, Is.Not.Null);
            Assert.That(searchResults2.Data.Header, Is.Not.Null);
            Assert.That(searchResults2.Data.Header.Data.Success, Is.True);

            Assert.That(searchResults2.Data.Items, Is.Not.Null);
            Assert.That(searchResults2.Data.Items, Has.Length.EqualTo(0));
        });

        var stats = MemoryCache.GetCurrentStatistics();
        Assert.Multiple(() =>
        {
            Assert.That(stats?.TotalHits, Is.Not.Null.And.EqualTo(1));
            Assert.That(stats?.TotalMisses, Is.Not.Null.And.EqualTo(1));
        });
    }
}
