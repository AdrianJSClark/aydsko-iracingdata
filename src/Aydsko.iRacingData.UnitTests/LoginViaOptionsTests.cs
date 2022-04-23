// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.UnitTests;

public class LoginViaOptionsTests : MockedHttpTestBase
{
    [SetUp]
    public void SetUp()
    {
        BaseSetUp();
    }

    [Test]
    public async Task GivenOptionsWithUsernameAndPasswordWhenAMethodIsCalledThenItWillSucceedAsync()
    {
        var options = new iRacingDataClientOptions
        {
            Username = "test.user@example.com",
            Password = "SuperSecretPassword",
            RestoreCookies = null,
            SaveCookies = null,
        };

        var sut = new DataClient(HttpClient,
                                        new TestLogger<DataClient>(),
                                        options,
                                        CookieContainer);

        await MessageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync)).ConfigureAwait(false);
        var lookups = await sut.GetLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        Assert.That(sut.IsLoggedIn, Is.True);
        Assert.That(lookups, Is.Not.Null);
        Assert.That(lookups.Data, Is.Not.Null.Or.Empty);
    }
}
