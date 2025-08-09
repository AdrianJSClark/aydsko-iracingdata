// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Aydsko.iRacingData.UnitTests;

internal sealed class ServicesTests
{
    [Test]
    public async Task LoginAndUserAgentDefaultWorksWhenResolvedFromServicesAsync()
    {
        var cookieContainer = new CookieContainer();
        using var messageHandler = new MockedHttpMessageHandler(cookieContainer);
        await messageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync)).ConfigureAwait(false);

        var services = new ServiceCollection();
        _ = services.AddSingleton(cookieContainer);
        _ = services.AddIRacingDataApiInternal(options =>
        {
            options.Username = "test.user@example.com";
            options.Password = "SuperSecretPassword";
        }, false).ConfigurePrimaryHttpMessageHandler(services => messageHandler);

        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();

        var sut = scope.ServiceProvider.GetRequiredService<IDataClient>();

        var lookups = await sut.GetLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        //Assert.That(testDataClient.IsLoggedIn, Is.True);
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
        var cookieContainer = new CookieContainer();
        using var messageHandler = new MockedHttpMessageHandler(cookieContainer);
        await messageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync)).ConfigureAwait(false);

        var services = new ServiceCollection();
        _ = services.AddSingleton(cookieContainer);
        _ = services.AddIRacingDataApiInternal(options =>
        {
            options.Username = "test.user@example.com";
            options.Password = "SuperSecretPassword";
            options.UserAgentProductName = "UserAgentTest";
            options.UserAgentProductVersion = new(1, 0);
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
