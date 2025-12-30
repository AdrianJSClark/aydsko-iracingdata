// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.DependencyInjection;

namespace Aydsko.iRacingData.UnitTests;

internal sealed class ServicesTests
{
    [Test]
    public async Task LoginAndUserAgentDefaultWorksWhenResolvedFromServicesAsync()
    {
        using var messageHandler = new MockedHttpMessageHandler();
        await messageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync)).ConfigureAwait(false);

        var services = new ServiceCollection();
        _ = services.AddIRacingDataApiInternal(options =>
        {
            options.UsePasswordLimitedOAuth("test.user@example.com", "SuperSecretPassword", "ClientIdValue", "ClientSecretValue");
        }, false).ConfigurePrimaryHttpMessageHandler(services => messageHandler);

        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();

        var sut = scope.ServiceProvider.GetRequiredService<IDataClient>();

        var lookups = await sut.GetLookupsAsync(CancellationToken.None)
                               .ConfigureAwait(false);

        Assert.That(lookups, Is.Not.Null);
        Assert.That(lookups.Data, Is.Not.Null.Or.Empty);

        foreach (var request in messageHandler.RequestContent)
        {
            Assert.That(string.Join(" ", request.Headers.Where(h => h.Key == "User-Agent").SelectMany(h => h.Value)), Is.Not.Null.Or.Empty);
        }
    }

    [Test]
    public async Task LoginAndUserAgentWorksWhenResolvedFromServicesAsync()
    {
        using var messageHandler = new MockedHttpMessageHandler();
        await messageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync)).ConfigureAwait(false);

        var services = new ServiceCollection();
        _ = services.AddIRacingDataApiInternal(options =>
        {
            options.UseProductUserAgent("UserAgentTest", new(1, 0));
            options.UsePasswordLimitedOAuth("test.user@example.com", "SuperSecretPassword", "ClientIdValue", "ClientSecretValue");
        }, false).ConfigurePrimaryHttpMessageHandler(services => messageHandler);

        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();

        var sut = scope.ServiceProvider.GetRequiredService<IDataClient>();

        var lookups = await sut.GetLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        Assert.That(lookups, Is.Not.Null);
        Assert.That(lookups.Data, Is.Not.Null.Or.Empty);
        foreach (var request in messageHandler.RequestContent)
        {
            Assert.That(string.Join(" ", request.Headers.Where(h => h.Key == "User-Agent").SelectMany(h => h.Value)), Is.EqualTo($"UserAgentTest/1.0 Aydsko.iRacingDataClient/{typeof(IDataClient).Assembly.GetName().Version?.ToString(3)}"));
        }
    }
}
