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

    [TestCaseSource(nameof(GetTestCases))]
    public async Task ValidateLoginRequestViaOptions(string username, string password, string expectedEncodedPassword)
    {
        var options = new iRacingDataClientOptions
        {
            Username = username,
            Password = password,
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

        Assert.That(loginDto!.Email, Is.EqualTo(username));
        Assert.That(loginDto!.Password, Is.EqualTo(expectedEncodedPassword));

        Assert.That(sut.IsLoggedIn, Is.True);
        Assert.That(lookups, Is.Not.Null);
        Assert.That(lookups.Data, Is.Not.Null.Or.Empty);
    }

    public static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new("test.user@example.com", "SuperSecretPassword", "nXmEFCdpHheD1R3XBVkm6VQavR7ZLbW7SRmzo/MfFso=");
        yield return new("CLunky@iracing.Com", "MyPassWord", "xGKecAR27ALXNuMLsGaG0v5Q9pSs2tZTZRKNgmHMg+Q=");
    }

    private class TestLoginDto
    {
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}
