// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Aydsko.iRacingData.UnitTests;

/// <summary>Tests that validate the <see cref="ServicesExtensions.AddIRacingDataApi(IServiceCollection, Action{Aydsko.iRacingData.iRacingDataClientOptions})"/> operates as intended.</summary>
public class ServicesExtensionsTests
{
    /// <summary>Ensure tha the login and default user agent value works.</summary>
    /// <returns>A <see cref="Task"/> that resolves when the method completes.</returns>
    [Test]
    public async Task LoginAndUserAgentDefaultWorksWhenResolvedFromServicesAsync()
    {
        var cookieContainer = new CookieContainer();
        using var messageHandler = new MockedHttpMessageHandler(cookieContainer);
        await messageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync)).ConfigureAwait(false);

        var services = new ServiceCollection();
        services.AddSingleton(cookieContainer);
        services.AddIRacingDataApiInternal(options =>
        {
            options.Username = "test.user@example.com";
            options.Password = "SuperSecretPassword";
        }).ConfigurePrimaryHttpMessageHandler(services => messageHandler);

        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();

        var sut = scope.ServiceProvider.GetRequiredService<IDataClient>();

        var lookups = await sut.GetLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        //Assert.That(sut.IsLoggedIn, Is.True);
        Assert.That(lookups, Is.Not.Null);
        Assert.That(lookups.Data, Is.Not.Null.Or.Empty);
        foreach (var request in messageHandler.Requests)
        {
            Assert.That(request.Headers.UserAgent.ToString(), Is.Not.Null);
        }
    }

    /// <summary>Ensure the login and a supplied user agent value.</summary>
    /// <returns>A <see cref="Task"/> that resolves when the method completes.</returns>
    [Test]
    public async Task LoginAndUserAgentWorksWhenResolvedFromServicesAsync()
    {
        var cookieContainer = new CookieContainer();
        using var messageHandler = new MockedHttpMessageHandler(cookieContainer);
        await messageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync)).ConfigureAwait(false);

        var services = new ServiceCollection();
        services.AddSingleton(cookieContainer);
        services.AddIRacingDataApiInternal(options =>
        {
            options.Username = "test.user@example.com";
            options.Password = "SuperSecretPassword";
            options.UserAgentProductName = "UserAgentTest";
            options.UserAgentProductVersion = new(1, 0);
        }).ConfigurePrimaryHttpMessageHandler(services => messageHandler);

        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();

        var sut = scope.ServiceProvider.GetRequiredService<IDataClient>();

        var lookups = await sut.GetLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        Assert.That(lookups, Is.Not.Null);
        Assert.That(lookups.Data, Is.Not.Null.Or.Empty);

        foreach (var request in messageHandler.Requests)
        {
            Assert.That(request.Headers.UserAgent.ToString(), Is.EqualTo($"UserAgentTest/1.0 Aydsko.iRacingDataClient/{typeof(IDataClient).Assembly.GetName().Version?.ToString(3)}"));
        }
    }
}
