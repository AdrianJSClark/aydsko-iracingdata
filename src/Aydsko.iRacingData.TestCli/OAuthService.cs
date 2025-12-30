using System.Buffers.Text;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace Aydsko.iRacingData.TestCli;

internal class OAuthService(HttpClient httpClient, IConfiguration configuration, TimeProvider timeProvider)
    : IOAuthTokenSource
{
#pragma warning disable CA2201 // Do not raise reserved exception types - This is quick & dirty, just throw Exception for problems.
    public async Task<OAuthTokenValue> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        if (configuration["iRacing:ClientId"] is not string clientId) throw new Exception("Missing configuration value \"iRacing:ClientId\".");
        if (configuration["iRacing:ClientSecret"] is not string clientSecret) throw new Exception("Missing configuration value \"iRacing:ClientSecret\".");
        if (configuration["iRacing:RedirectUrl"] is not string redirectUrl) throw new Exception("Missing configuration value \"iRacing:RedirectUrl\".");

        var baseAddress = httpClient.BaseAddress ?? new("https://oauth.iracing.com");

        var savedToken = await TokenStorageService.ReadTokenSaveDataAsync(cancellationToken);

        if (savedToken != null)
        {
            var now = timeProvider.GetUtcNow();
            if (now < savedToken.TokenExpiryInstant)
            {
                return new(savedToken.TokenResponse.AccessToken, savedToken.TokenExpiryInstant);
            }
            else if (now < savedToken.RefreshExpiryInstant)
            {
                var refreshedTokenData = await RequestRefreshTokenGrantAsync(clientId, clientSecret, savedToken.TokenResponse.RefreshToken, baseAddress, cancellationToken);
                await TokenStorageService.WriteTokenSaveDataAsync(refreshedTokenData, cancellationToken);

                Console.WriteLine("--------------- REFRESHED TOKEN START ---------------");
                Console.WriteLine(JsonSerializer.Serialize(refreshedTokenData, JsonSerializerOptions.Web));
                Console.WriteLine("---------------- REFRESHED TOKEN END ----------------");

                return new(refreshedTokenData.TokenResponse.AccessToken, refreshedTokenData.TokenExpiryInstant);
            }
        }

        var pkceCodeVerifier = Base64Url.EncodeToString(RandomNumberGenerator.GetBytes(32));
        var pkceCodeChallenge = Base64Url.EncodeToString(SHA256.HashData(Encoding.ASCII.GetBytes(pkceCodeVerifier)));

        var authorizeParameters = new Dictionary<string, string>
        {
            ["client_id"] = clientId,
            ["redirect_uri"] = redirectUrl,
            ["response_type"] = "code",
            ["code_challenge"] = pkceCodeChallenge,
            ["code_challenge_method"] = "S256",
            ["scope"] = "iracing.auth iracing.profile",
        };

        var authorizeUrl = new Uri(baseAddress, new Uri("/oauth2/authorize", UriKind.Relative)).ToUrlWithQuery(authorizeParameters);

        Console.WriteLine(authorizeUrl.AbsoluteUri);

        using var listener = new HttpListener();
        var listenForUrl = new Uri(redirectUrl, UriKind.Absolute).GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped).TrimEnd('/') + "/";
        listener.Prefixes.Add(listenForUrl);
        listener.Start();

        Process.Start(new ProcessStartInfo()
        {
            UseShellExecute = true,
            FileName = authorizeUrl.AbsoluteUri
        });

        Console.WriteLine("Waiting for callback...");
        var context = await listener.GetContextAsync();

        var authCodeRequestQuery = HttpUtility.ParseQueryString(context.Request.Url?.Query ?? "");

        if (authCodeRequestQuery["code"] is not string { Length: > 0 } code)
        {
            throw new Exception("Missing \"code\" in response from iRacing Authentication.");
        }

        var tokenData = await RequestAuthorizationCodeGrantAsync(clientId, clientSecret, code, redirectUrl, pkceCodeVerifier, baseAddress, cancellationToken);

        await TokenStorageService.WriteTokenSaveDataAsync(tokenData, cancellationToken);

        Console.WriteLine("-------------------- TOKEN START --------------------");
        Console.WriteLine(JsonSerializer.Serialize(tokenData, JsonSerializerOptions.Web));
        Console.WriteLine("--------------------  TOKEN END  --------------------");

        context.Response.StatusCode = 200;
        context.Response.StatusDescription = "OK";
        var response = Encoding.UTF8.GetBytes($"<html><head><title>{typeof(Program).Assembly.GetName().Name} ({typeof(Program).Assembly.GetName().Version})</title><body><h1>Done! Please close this window.</h1></body></html>");
        context.Response.ContentLength64 = response.Length;
        await context.Response.OutputStream.WriteAsync(response, cancellationToken);
        context.Response.OutputStream.Close();
        context.Response.Close();

        listener.Close();

        return new(tokenData.TokenResponse.AccessToken, timeProvider.GetUtcNow().AddSeconds(tokenData.TokenResponse.ExpiresInSeconds));
    }

    internal static string EncodePassword(string username, string password)
    {
#pragma warning disable CA1308 // Normalize strings to uppercase - iRacing API requires lowercase
        var passwordAndEmail = password + (username?.ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

        var hashedPasswordAndEmailBytes = SHA256.HashData(Encoding.UTF8.GetBytes(passwordAndEmail));
        var encodedHash = Convert.ToBase64String(hashedPasswordAndEmailBytes);
        return encodedHash;
    }
#pragma warning restore CA2201 // Do not raise reserved exception types

    private async Task<TokenSaveData> RequestAuthorizationCodeGrantAsync(string clientId,
                                                                         string clientSecret,
                                                                         string code,
                                                                         string redirectUrl,
                                                                         string pkceCodeVerifier,
                                                                         Uri baseAddress,
                                                                         CancellationToken cancellationToken = default)
    {
        var tokenParameters = new Dictionary<string, string>
        {
            ["grant_type"] = "authorization_code",
            ["client_id"] = clientId,
            ["client_secret"] = EncodePassword(clientId, clientSecret),
            ["code"] = code,
            ["redirect_uri"] = redirectUrl,
            ["code_verifier"] = pkceCodeVerifier,
        };

        return await RequestTokenAsync(baseAddress, tokenParameters, cancellationToken);
    }

    private async Task<TokenSaveData> RequestRefreshTokenGrantAsync(string clientId,
                                                                         string clientSecret,
                                                                         string refreshToken,
                                                                         Uri baseAddress,
                                                                         CancellationToken cancellationToken = default)
    {
        var tokenParameters = new Dictionary<string, string>
        {
            ["grant_type"] = "refresh_token",
            ["client_id"] = clientId,
            ["client_secret"] = EncodePassword(clientId, clientSecret),
            ["refresh_token"] = refreshToken,
        };

        return await RequestTokenAsync(baseAddress, tokenParameters, cancellationToken);
    }

    private async Task<TokenSaveData> RequestTokenAsync(Uri baseAddress, Dictionary<string, string> tokenParameters, CancellationToken cancellationToken)
    {
        // Doesn't matter if this is a bit early. That just gives us some buffer when we calculate the token lifetimes.
        var utcNow = timeProvider.GetUtcNow();

        var tokenUrl = new Uri(baseAddress, new Uri("/oauth2/token", UriKind.Relative));
        var tokenResponse = await httpClient.PostAsync(tokenUrl, new FormUrlEncodedContent(tokenParameters), cancellationToken);

        if (!tokenResponse.IsSuccessStatusCode)
        {
            var responseContent = await tokenResponse.Content.ReadAsStringAsync(cancellationToken);
            Console.WriteLine("-------------------- RESPONSE CONTENT --------------------");
            Console.WriteLine(responseContent);
            Console.WriteLine("-------------------- RESPONSE CONTENT --------------------");
            throw new Exception("Token retrieval failed.");
        }

        var tokenDetail = await tokenResponse.Content.ReadFromJsonAsync<OAuthTokenResponse>(cancellationToken)
                          ?? throw new Exception("Failure to parse the token response content.");

        var tokenExpiryInstant = utcNow.AddSeconds(tokenDetail.ExpiresInSeconds);
        DateTimeOffset? refreshExpiryInstant = null;
        if (tokenDetail.RefreshTokenExpiresInSeconds is int refreshExpiryInSeconds)
        {
            refreshExpiryInstant = utcNow.AddSeconds(refreshExpiryInSeconds);
        }

        return new(tokenDetail, tokenExpiryInstant, refreshExpiryInstant);
    }
}

internal record TokenSaveData(OAuthTokenResponse TokenResponse, DateTimeOffset TokenExpiryInstant, DateTimeOffset? RefreshExpiryInstant);
