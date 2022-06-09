// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json;
using System.Text.Json.Serialization;

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

        var loginRequest = MessageHandler.Requests.Peek();
        Assert.That(loginRequest, Is.Not.Null);

        var contentStreamTask = loginRequest.Content?.ReadAsStreamAsync() ?? Task.FromResult(Stream.Null);
        using var requestContentStream = await contentStreamTask.ConfigureAwait(false);
        Assert.That(requestContentStream, Is.Not.Null.Or.Empty);

        var loginDto = await JsonSerializer.DeserializeAsync<TestLoginDto>(requestContentStream).ConfigureAwait(false);
        Assert.That(loginDto, Is.Not.Null);

        Assert.That(loginDto!.Email, Is.EqualTo("test.user@example.com"));
        Assert.That(loginDto!.Password, Is.EqualTo("SuperSecretPassword"));

        Assert.That(sut.IsLoggedIn, Is.True);
        Assert.That(lookups, Is.Not.Null);
        Assert.That(lookups.Data, Is.Not.Null.Or.Empty);
    }

    [Test]
    public async Task GivenOptionsWithUsernameAndPasswordAndGiven22S3ModeWhenAMethodIsCalledThenItWillSucceedAsync()
    {
        var options = new iRacingDataClientOptions
        {
            Username = "CLunky@iracing.Com",
            Password = "MyPassWord",
            RestoreCookies = null,
            SaveCookies = null,
            Use2022Season3Login = true,
        };

        var sut = new DataClient(HttpClient,
                                 new TestLogger<DataClient>(),
                                 options,
                                 CookieContainer);

        await MessageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync)).ConfigureAwait(false);
        var lookups = await sut.GetLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        var loginRequest = MessageHandler.Requests.Peek();
        Assert.That(loginRequest, Is.Not.Null);

        var contentStreamTask = loginRequest.Content?.ReadAsStreamAsync() ?? Task.FromResult(Stream.Null);
        using var requestContentStream = await contentStreamTask.ConfigureAwait(false);
        Assert.That(requestContentStream, Is.Not.Null);

        var loginDto = await JsonSerializer.DeserializeAsync<TestLoginDto>(requestContentStream).ConfigureAwait(false);
        Assert.That(loginDto, Is.Not.Null);

        Assert.That(loginDto!.Email, Is.EqualTo("CLunky@iracing.Com"));
        Assert.That(loginDto!.Password, Is.EqualTo("xGKecAR27ALXNuMLsGaG0v5Q9pSs2tZTZRKNgmHMg+Q="));

        Assert.That(sut.IsLoggedIn, Is.True);
        Assert.That(lookups, Is.Not.Null);
        Assert.That(lookups.Data, Is.Not.Null.Or.Empty);
    }

    private class TestLoginDto
    {
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}
