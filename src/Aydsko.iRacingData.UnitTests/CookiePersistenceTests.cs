// © 2022 Adrian Clark
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
    public async Task GivenOptionsWithNullDelegateValuesWhenLoginIsCalledThenItWillSucceedAsync()
    {
        var options = new iRacingDataClientOptions
        {
            RestoreCookies = null,
            SaveCookies = null,
        };

        var sut = new iRacingDataClient(HttpClient,
                                        new TestLogger<iRacingDataClient>(),
                                        options,
                                        CookieContainer);

        await MessageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.ValidCredentialsIsSuccessfulAsync)).ConfigureAwait(false);
        await sut.LoginAsync("test.user@example.com", "SuperSecretPassword", CancellationToken.None).ConfigureAwait(false);

        Assert.That(sut.IsLoggedIn, Is.True);
    }

    [Test]
    public async Task GivenOptionsWithASaveActionTheSaveActionIsCalledWithTheCookies()
    {
        CookieCollection? savedCookies = null;
        var options = new iRacingDataClientOptions
        {
            RestoreCookies = null,
            SaveCookies = (cookies) => savedCookies = cookies,
        };

        var sut = new iRacingDataClient(HttpClient,
                                        new TestLogger<iRacingDataClient>(),
                                        options,
                                        CookieContainer);

        await MessageHandler.QueueResponsesAsync(nameof(GivenOptionsWithASaveActionTheSaveActionIsCalledWithTheCookies)).ConfigureAwait(false);
        await sut.LoginAsync("test.user@example.com", "SuperSecretPassword", CancellationToken.None).ConfigureAwait(false);

        Assert.That(sut.IsLoggedIn, Is.True);
        Assert.That(savedCookies, Is.Not.Null);
        Assert.That(savedCookies, Has.Count.EqualTo(2));
        Assert.That(savedCookies, Has.One.Property(nameof(Cookie.Name)).EqualTo("irsso_members"));
        Assert.That(savedCookies, Has.One.Property(nameof(Cookie.Name)).EqualTo("authtoken_members"));
    }

    [Test]
    public async Task GivenOptionsWithARestoreFuncTheFuncIsCalledToGetTheCookies()
    {
        var savedCookies = new CookieCollection
        {
            new Cookie("first", "one", "/", "example.com"),
            new Cookie("second", "two", "/", "example.com")
        };

        var options = new iRacingDataClientOptions
        {
            RestoreCookies = () => savedCookies,
            SaveCookies = null,
        };

        var sut = new iRacingDataClient(HttpClient,
                                        new TestLogger<iRacingDataClient>(),
                                        options,
                                        CookieContainer);

        await MessageHandler.QueueResponsesAsync(nameof(GivenOptionsWithASaveActionTheSaveActionIsCalledWithTheCookies)).ConfigureAwait(false);
        await sut.LoginAsync("test.user@example.com", "SuperSecretPassword", CancellationToken.None).ConfigureAwait(false);

        Assert.That(sut.IsLoggedIn, Is.True);

        var cookies = CookieContainer.GetAllCookies();

        Assert.That(cookies, Has.Count.EqualTo(4));
        Assert.That(cookies, Has.One.Property(nameof(Cookie.Name)).EqualTo("first"));
        Assert.That(cookies, Has.One.Property(nameof(Cookie.Name)).EqualTo("second"));
    }
}
