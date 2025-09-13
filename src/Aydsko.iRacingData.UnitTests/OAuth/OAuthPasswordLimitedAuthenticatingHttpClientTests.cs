using System.Net;
using System.Text;
using System.Web;
using System.Net.Http.Headers;


#if NETFRAMEWORK
using System.Net.Http;
#endif

namespace Aydsko.iRacingData.UnitTests.OAuth;

internal class OAuthPasswordLimitedAuthenticatingHttpClientTests
{
    [Test]
    public async Task GivenAnUnauthenticatedClientWhenARequestIsMadeThenTheTokenIsRequestedAsync()
    {
        var validResponse = """
                            {
                              "access_token": "fake_access_token",
                              "token_type": "Bearer",
                              "expires_in": 600,
                              "refresh_token": "fake_refresh_token",
                              "refresh_token_expires_in": 3600,
                              "scope": "iracing.auth"
                            }
                            """;
        using var fakeHandler = new FakeHttpHandler();
        fakeHandler.AddJsonResponse(HttpStatusCode.OK, validResponse);
        fakeHandler.AddJsonResponse(HttpStatusCode.OK, "{\"value\":true}");

        var options = new iRacingDataClientOptions().UsePasswordLimitedAuthentication("test.user@example.com",
                                                                                      "SuperSecretPassword",
                                                                                      "UnitTestApp",
                                                                                      "Secret-Client-Password");

        using var passwordLimitedClient = new OAuthPasswordLimitedAuthenticatingHttpClient(fakeHandler.GetClient(), options);

        using var testRequest = new HttpRequestMessage(HttpMethod.Get, new Uri("https://example.com/test-request"));
        var result = await passwordLimitedClient.SendAuthenticatedRequestAsync(testRequest).ConfigureAwait(false);

        using (Assert.EnterMultipleScope())
        {
            // Make sure we got a request.
            Assert.That(fakeHandler.Requests, Is.Not.Null);

            var requestContent = Encoding.UTF8.GetString(fakeHandler.Requests[0]);

            var requestContentValues = new Dictionary<string, string>();
            var formValues = HttpUtility.ParseQueryString(requestContent);

            foreach (var key in formValues.AllKeys)
            {
                if (key == null)
                {
                    continue;
                }

                if (formValues[key] is string value)
                {
                    requestContentValues.Add(key, value);
                }
            }

            // Ensure the request was for the correct thing.
            Assert.That(requestContentValues, Contains.Key("grant_type").WithValue("password_limited"));
            Assert.That(requestContentValues, Contains.Key("username").WithValue("test.user@example.com"));
            Assert.That(requestContentValues, Contains.Key("password").WithValue("nXmEFCdpHheD1R3XBVkm6VQavR7ZLbW7SRmzo/MfFso="));
            Assert.That(requestContentValues, Contains.Key("client_id").WithValue("UnitTestApp"));
            Assert.That(requestContentValues, Contains.Key("client_secret").WithValue("D2GfmYK/nC8zeQPkpV2uGkk13hL0/LTQH44hiJKawCo="));
            Assert.That(requestContentValues, Contains.Key("scope").WithValue("iracing.auth iracing.profile"));

            // Check that there a response that indicated success.
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Property(nameof(result.StatusCode)).EqualTo(HttpStatusCode.OK));

            // Validate that it had the content we expected.
            var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.That(resultContent, Is.EqualTo(validResponse));
        }
    }
}
