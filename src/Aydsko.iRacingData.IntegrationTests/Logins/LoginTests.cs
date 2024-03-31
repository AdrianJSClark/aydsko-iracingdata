// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Net;
using Aydsko.iRacingData.Exceptions;

namespace Aydsko.iRacingData.IntegrationTests.Logins;

internal sealed class LoginTests : BaseIntegrationFixture<DataClient>
{
    private CookieCollection? _cookiesToRestore;
    private CookieCollection? _cookiesFromSave;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var options = BaseSetUp();
        options.RestoreCookies = () => _cookiesToRestore ?? [];
        options.SaveCookies = (cookies) => _cookiesFromSave = cookies;

        Client = new DataClient(HttpClient, new TestLogger<DataClient>(), options, CookieContainer);
    }

    [Test]
    public void TestFailedLoginFromBadCookieRestore()
    {
        _cookiesToRestore = new CookieCollection();
        _cookiesToRestore.Add(new Cookie("test", "test", "/", ".iracing.com"));

        _cookiesFromSave = new CookieCollection();
        _cookiesFromSave.Add(new Cookie("test", "test", "/", "localhost"));

        Assert.Multiple(() =>
        {
            Assert.That(async () => await Client.GetClubHistoryLookupsAsync(2022, 1).ConfigureAwait(false),
                        Throws.Exception.InstanceOf<iRacingUnauthorizedResponseException>());

            Assert.That(_cookiesFromSave, Is.Empty);
            Assert.That(Client.IsLoggedIn, Is.False);
        });
    }
}
