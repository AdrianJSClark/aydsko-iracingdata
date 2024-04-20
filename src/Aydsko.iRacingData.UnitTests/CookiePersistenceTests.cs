// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Net;

namespace Aydsko.iRacingData.UnitTests;

public class CookiePersistenceTests : MockedHttpTestBase
{
    [SetUp]
    public void SetUp()
    {
        BaseSetUp();
    }

    [Test]
    public async Task GivenOptionsWithNullDelegateValuesWhenAMethodIsCalledThenItWillSucceedAsync()
    {
        var options = new iRacingDataClientOptions
        {
            RestoreCookies = null,
            SaveCookies = null,
            Username = "fake.for.unit.tests@example.com",
            Password = "obviously.fake.password.value",
        };

        using var sut = new DataClient(HttpClient,
                                       new TestLogger<DataClient>(),
                                       options,
                                       CookieContainer);

        await MessageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync)).ConfigureAwait(false);
        await sut.GetLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        Assert.That(sut.IsLoggedIn, Is.True);
    }

    [Test]
    public async Task GivenOptionsWithASaveActionTheSaveActionIsCalledWithTheCookiesAsync()
    {
        CookieCollection? savedCookies = null;
        var options = new iRacingDataClientOptions
        {
            RestoreCookies = null,
            SaveCookies = (cookies) => savedCookies = cookies,
            Username = "fake.for.unit.tests@example.com",
            Password = "obviously.fake.password.value",
        };

        using var sut = new DataClient(HttpClient,
                                       new TestLogger<DataClient>(),
                                       options,
                                       CookieContainer);

        await MessageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync)).ConfigureAwait(false);
        await sut.GetLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        Assert.That(sut.IsLoggedIn, Is.True);
        Assert.That(savedCookies, Is.Not.Null);
        Assert.That(savedCookies, Has.Count.EqualTo(2));
        Assert.That(savedCookies, Has.One.Property(nameof(Cookie.Name)).EqualTo("irsso_members"));
        Assert.That(savedCookies, Has.One.Property(nameof(Cookie.Name)).EqualTo("authtoken_members"));
    }

    [Test]
    public async Task GivenOptionsWithARestoreFuncTheFuncIsCalledToGetTheCookiesAsync()
    {
        var savedCookies = new CookieCollection
        {
            new Cookie("first", "one", "/", "example.com"),
            new Cookie("second", "two", "/", "example.com")
        };

        var options = new iRacingDataClientOptions
        {
            Username = "test.user@example.com",
            Password = "SuperSecretPassword",
            RestoreCookies = () => savedCookies,
            SaveCookies = null,
        };

        using var sut = new DataClient(HttpClient,
                                       new TestLogger<DataClient>(),
                                       options,
                                       CookieContainer);

        await MessageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync)).ConfigureAwait(false);
        await sut.GetLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        Assert.That(sut.IsLoggedIn, Is.True);

        var cookies = CookieContainer.GetAllCookies();

        Assert.That(cookies, Has.Count.EqualTo(4));
        Assert.That(cookies, Has.One.Property(nameof(Cookie.Name)).EqualTo("first"));
        Assert.That(cookies, Has.One.Property(nameof(Cookie.Name)).EqualTo("second"));
        Assert.That(cookies, Has.One.Property(nameof(Cookie.Name)).EqualTo("irsso_members"));
        Assert.That(cookies, Has.One.Property(nameof(Cookie.Name)).EqualTo("authtoken_members"));
    }

    [Test]
    public async Task GivenACookieContainerWithCookiesAndNoRestoreOrSaveFunctionsThenTheCookiesAreUsedAsync()
    {
        CookieContainer.Add(new Cookie("irsso_members", "one", "/", "members-ng.iracing.com"));
        CookieContainer.Add(new Cookie("authtoken_members", "two", "/", "members-ng.iracing.com"));

        var options = new iRacingDataClientOptions
        {
            Username = "test.user@example.com",
            Password = "SuperSecretPassword",
            RestoreCookies = null,
            SaveCookies = null,
        };

        using var sut = new DataClient(HttpClient,
                                       new TestLogger<DataClient>(),
                                       options,
                                       CookieContainer);

        await MessageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync), false).ConfigureAwait(false);
        await sut.GetLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        Assert.That(sut.IsLoggedIn, Is.True);

        var cookies = CookieContainer.GetAllCookies();

        Assert.That(cookies, Has.Count.EqualTo(2));
        Assert.That(cookies, Has.One.Property(nameof(Cookie.Name)).EqualTo("irsso_members"));
        Assert.That(cookies, Has.One.Property(nameof(Cookie.Name)).EqualTo("authtoken_members"));
    }
}
