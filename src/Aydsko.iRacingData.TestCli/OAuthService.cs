using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Aydsko.iRacingData.TestCli;

internal class OAuthService(HttpClient httpClient, IConfiguration configuration)
    : IOAuthTokenSource
{
    public async Task<OAuthTokenValue> GetTokenAsync(CancellationToken cancellationToken = default)
    {
#pragma warning disable CA2201 // Do not raise reserved exception types
        if (configuration["iRacing:ClientId"] is not string clientId) throw new Exception("Missing configuration value \"iRacing:ClientId\".");
        if (configuration["iRacing:ClientSecret"] is not string clientSecret) throw new Exception("Missing configuration value \"iRacing:ClientSecret\".");
        if (configuration["iRacing:RedirectUrl"] is not string redirectUrl) throw new Exception("Missing configuration value \"iRacing:RedirectUrl\".");
#pragma warning restore CA2201 // Do not raise reserved exception types

        var pkceCodeVerifier = Base64Url.EncodeToString(RandomNumberGenerator.GetBytes(32));
        var pkceCodeChallenge = Base64Url.EncodeToString(SHA256.HashData(Encoding.ASCII.GetBytes(pkceCodeVerifier)));

        var parameters = new Dictionary<string, string>
        {
            ["client_id"] = clientId,
            ["redirect_uri"] = redirectUrl,
            ["response_type"] = "code",
            ["code_challenge"] = pkceCodeChallenge,
            ["code_challenge_method"] = "S256",
            ["scope"] = "iracing.auth iracing.profile",
        };

        var baseAddress = httpClient.BaseAddress ?? new("https://oauth.iracing.com/oauth2");
        var authorizeUrl = new Uri(baseAddress, new Uri("/oauth2/authorize", UriKind.Relative)).ToUrlWithQuery(parameters);

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

        var authCodeRequest = context.Request;

        context.Response.StatusCode = 200;
        context.Response.StatusDescription = "OK";
        context.Response.Close();

        listener.Close();

        return new(string.Empty, DateTimeOffset.MinValue);
    }
}
