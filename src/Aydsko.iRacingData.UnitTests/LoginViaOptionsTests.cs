// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.UnitTests;

internal sealed class PasswordEncodingTests
    : MockedHttpTestBase
{
    [TestCaseSource(nameof(GetTestCases))]
    public async Task ValidateLoginRequestViaOptionsAsync(string username, string password, bool passwordIsEncoded, string expectedEncodedPassword)
    {
        var options = new iRacingDataClientOptions
        {
            Username = username,
            Password = password,
            PasswordIsEncoded = passwordIsEncoded,
            RestoreCookies = null,
            SaveCookies = null,
        };

        using var client = new TestLegacyUsernamePasswordApiClient(HttpClient, options, CookieContainer, new TestLogger<LegacyUsernamePasswordApiClient>());
        using var apiClient = new ApiClient(client, options, new TestLogger<ApiClient>());
        var sut = new DataClient(apiClient, options, new TestLogger<DataClient>(), FakeTimeProvider);

        await MessageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync)).ConfigureAwait(false);
        var lookups = await sut.GetLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(MessageHandler.RequestContent, Has.Count.GreaterThanOrEqualTo(1));

            var request = MessageHandler.RequestContent.Dequeue();
            Assert.That(request, Is.Not.Null);

            Assert.That(request.ContentStream, Is.Not.Null.Or.Empty);

            var loginDto = await JsonSerializer.DeserializeAsync<TestLoginDto>(request.ContentStream).ConfigureAwait(false);
            Assert.That(loginDto, Is.Not.Null);

            Assert.That(loginDto!.Email, Is.EqualTo(username));
            Assert.That(loginDto!.Password, Is.EqualTo(expectedEncodedPassword));

            Assert.That(client.IsLoggedIn, Is.True);
            Assert.That(lookups, Is.Not.Null);
            Assert.That(lookups.Data, Is.Not.Null.Or.Empty);
        }
    }

    [TestCaseSource(nameof(GetTestCases))]
    [Obsolete]
    public async Task ValidateLoginRequestViaMethodWithPasswordIsEncodedParamAsync(string username, string password, bool passwordIsEncoded, string expectedEncodedPassword)
    {
        var options = new iRacingDataClientOptions { RestoreCookies = null, SaveCookies = null, };

        await MessageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync)).ConfigureAwait(false);

        using var client = new TestLegacyUsernamePasswordApiClient(HttpClient, options, CookieContainer, new TestLogger<LegacyUsernamePasswordApiClient>());
        using var apiClient = new ApiClient(client, options, new TestLogger<ApiClient>());
        var sut = new DataClient(apiClient, options, new TestLogger<DataClient>(), FakeTimeProvider);

        sut.UseUsernameAndPassword(username, password, passwordIsEncoded);

        var lookups = await sut.GetLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        var request = MessageHandler.RequestContent.Peek();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(request, Is.Not.Null);
            Assert.That(request.ContentStream, Is.Not.Null.Or.Empty);

            var loginDto = await JsonSerializer.DeserializeAsync<TestLoginDto>(request.ContentStream).ConfigureAwait(false);
            Assert.That(loginDto, Is.Not.Null);

            Assert.That(loginDto!.Email, Is.EqualTo(username));
            Assert.That(loginDto!.Password, Is.EqualTo(expectedEncodedPassword));

            Assert.That(client.IsLoggedIn, Is.True);
            Assert.That(lookups, Is.Not.Null);
            Assert.That(lookups.Data, Is.Not.Null.Or.Empty);
        }
    }

    [TestCaseSource(nameof(GetTestCasesWithUnencodedPasswords))]
    [Obsolete]
    public async Task ValidateLoginRequestViaMethodAsync(string username, string password, string expectedEncodedPassword)
    {
        var options = new iRacingDataClientOptions { RestoreCookies = null, SaveCookies = null, };

        await MessageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync)).ConfigureAwait(false);

        using var client = new TestLegacyUsernamePasswordApiClient(HttpClient, options, CookieContainer, new TestLogger<LegacyUsernamePasswordApiClient>());
        using var apiClient = new ApiClient(client, options, new TestLogger<ApiClient>());
        var sut = new DataClient(apiClient, options, new TestLogger<DataClient>(), FakeTimeProvider);

        sut.UseUsernameAndPassword(username, password);

        var lookups = await sut.GetLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        var request = MessageHandler.RequestContent.Dequeue();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(request, Is.Not.Null);
            Assert.That(request.ContentStream, Is.Not.Null.Or.Empty);

            var loginDto = await JsonSerializer.DeserializeAsync<TestLoginDto>(request.ContentStream).ConfigureAwait(false);
            Assert.That(loginDto, Is.Not.Null);

            Assert.That(loginDto!.Email, Is.EqualTo(username));
            Assert.That(loginDto!.Password, Is.EqualTo(expectedEncodedPassword));

            Assert.That(client.IsLoggedIn, Is.True);
            Assert.That(lookups, Is.Not.Null);
            Assert.That(lookups.Data, Is.Not.Null.Or.Empty);
        }
    }

    [TestCaseSource(nameof(GetTestCasesWithUnencodedPasswords))]
    [Obsolete]
    public async Task LoginIsNotCalledIfCookiesAreSuccessfullyRestoredAsync(string username, string password, string expectedEncodedPassword)
    {
        var restoreCookiesWasCalled = false;
        var saveCookiesWasCalled = false;

        var cookieContainer = new CookieContainer();
        cookieContainer.Add(new Cookie("authtoken_members", "%7B%22authtoken%22%3A%7B%22authcode%22%3A%22AbC123%22%2C%22email%22%3A%22test.user%40example.com%22%7D%7D", "/", ".iracing.com"));
        cookieContainer.Add(new Cookie("irsso_members", "ABC123DEF456", "/", ".iracing.com"));
        cookieContainer.Add(new Cookie("r_members", "", "/", ".iracing.com"));

        var cookieCollection = cookieContainer.GetCookies(new Uri("https://members-ng.iracing.com"));

        var options = new iRacingDataClientOptions
        {
            RestoreCookies = () =>
            {
                restoreCookiesWasCalled = true;
                return cookieCollection;
            },
            SaveCookies = (newCollection) =>
            {
                cookieCollection = newCollection;
                saveCookiesWasCalled = true;
            },
        };

        await MessageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync), false).ConfigureAwait(false);

        using var client = new TestLegacyUsernamePasswordApiClient(HttpClient, options, CookieContainer, new TestLogger<LegacyUsernamePasswordApiClient>());
        using var apiClient = new ApiClient(client, options, new TestLogger<ApiClient>());
        var sut = new DataClient(apiClient, options, new TestLogger<DataClient>(), FakeTimeProvider);

        sut.UseUsernameAndPassword(username, password);

        var lookups = await sut.GetLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(restoreCookiesWasCalled, Is.True);
            Assert.That(saveCookiesWasCalled, Is.False);
            Assert.That(MessageHandler.RequestContent, Has.Count.EqualTo(2));
        }
    }

    [TestCaseSource(nameof(GetTestCasesWithUnencodedPasswords))]
    [Obsolete]
    public async Task LoginIsCalledIfCookiesAreExpiredAsync(string username, string password, string expectedEncodedPassword)
    {
        var restoreCookiesWasCalled = false;
        var saveCookiesWasCalled = false;

        Cookie[] cookies =
        [
            new Cookie("authtoken_members", "%7B%22authtoken%22%3A%7B%22authcode%22%3A%22AbC123%22%2C%22email%22%3A%22test.user%40example.com%22%7D%7D", "/", ".iracing.com"),
            new Cookie("irsso_members", "ABC123DEF456", "/", ".iracing.com"),
            new Cookie("r_members", "", "/", ".iracing.com")
        ];

        var cookieContainer = new CookieContainer();
        foreach (var cookie in cookies)
        {
            cookie.Expires = DateTime.Now - TimeSpan.FromDays(1);
            cookieContainer.Add(cookie);
        }

        var cookieCollection = cookieContainer.GetCookies(new Uri("https://members-ng.iracing.com"));

        var options = new iRacingDataClientOptions
        {
            RestoreCookies = () =>
            {
                restoreCookiesWasCalled = true;
                return cookieCollection;
            },
            SaveCookies = (newCollection) =>
            {
                cookieCollection = newCollection;
                saveCookiesWasCalled = true;
            },
        };

        await MessageHandler.QueueResponsesAsync(nameof(CapturedResponseValidationTests.GetLookupsSuccessfulAsync)).ConfigureAwait(false);

        using var client = new TestLegacyUsernamePasswordApiClient(HttpClient, options, CookieContainer, new TestLogger<LegacyUsernamePasswordApiClient>());
        using var apiClient = new ApiClient(client, options, new TestLogger<ApiClient>());
        var sut = new DataClient(apiClient, options, new TestLogger<DataClient>(), FakeTimeProvider);

        sut.UseUsernameAndPassword(username, password);

        var lookups = await sut.GetLookupsAsync(CancellationToken.None).ConfigureAwait(false);

        var request = MessageHandler.RequestContent.Dequeue();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(request, Is.Not.Null);
            Assert.That(request.ContentStream, Is.Not.Null.Or.Empty);

            var loginDto = await JsonSerializer.DeserializeAsync<TestLoginDto>(request.ContentStream).ConfigureAwait(false);
            Assert.That(loginDto, Is.Not.Null);

            Assert.That(loginDto!.Email, Is.EqualTo(username));
            Assert.That(loginDto!.Password, Is.EqualTo(expectedEncodedPassword));

            Assert.That(client.IsLoggedIn, Is.True);
            Assert.That(lookups, Is.Not.Null);
            Assert.That(lookups.Data, Is.Not.Null.Or.Empty);

            Assert.That(restoreCookiesWasCalled, Is.True);
            Assert.That(saveCookiesWasCalled, Is.True);
            Assert.That(MessageHandler.RequestContent, Has.Count.EqualTo(2));
        }
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new("test.user@example.com", "SuperSecretPassword", false, "nXmEFCdpHheD1R3XBVkm6VQavR7ZLbW7SRmzo/MfFso=");
        yield return new("CLunky@iracing.Com", "MyPassWord", false, "xGKecAR27ALXNuMLsGaG0v5Q9pSs2tZTZRKNgmHMg+Q=");

        yield return new("test.user@example.com", "nXmEFCdpHheD1R3XBVkm6VQavR7ZLbW7SRmzo/MfFso=", true, "nXmEFCdpHheD1R3XBVkm6VQavR7ZLbW7SRmzo/MfFso=");
        yield return new("CLunky@iracing.Com", "xGKecAR27ALXNuMLsGaG0v5Q9pSs2tZTZRKNgmHMg+Q=", true, "xGKecAR27ALXNuMLsGaG0v5Q9pSs2tZTZRKNgmHMg+Q=");
    }

    private static IEnumerable<TestCaseData> GetTestCasesWithUnencodedPasswords()
    {
        yield return new("test.user@example.com", "SuperSecretPassword", "nXmEFCdpHheD1R3XBVkm6VQavR7ZLbW7SRmzo/MfFso=");
        yield return new("CLunky@iracing.Com", "MyPassWord", "xGKecAR27ALXNuMLsGaG0v5Q9pSs2tZTZRKNgmHMg+Q=");
    }

    private sealed class TestLoginDto
    {
        [JsonPropertyName("email")] public string? Email { get; set; }
        [JsonPropertyName("password")] public string? Password { get; set; }
    }
}
