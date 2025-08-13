// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Aydsko.iRacingData.Exceptions;

namespace Aydsko.iRacingData;

public class LegacyUsernamePasswordApiClient(HttpClient httpClient,
                                             iRacingDataClientOptions options,
                                             CookieContainer cookieContainer,
                                             ILogger<LegacyUsernamePasswordApiClient> logger)
    : ApiClientBase(httpClient, options, logger)
{
    private readonly SemaphoreSlim loginSemaphore = new(1, 1);
#pragma warning disable CA1051 // Do not declare visible instance fields - Declared visible for testing purposes.
    protected bool isLoggedIn;
#pragma warning restore CA1051 // Do not declare visible instance fields

    [Obsolete("Configure via the \"AddIRacingDataApi\" extension method on the IServiceCollection which allows you to configure the \"iRacingDataClientOptions\".")]
    public void UseUsernameAndPassword(string username, string password, bool passwordIsEncoded)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw iRacingClientOptionsValueMissingException.Create(nameof(username));
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw iRacingClientOptionsValueMissingException.Create(nameof(password));
        }

        Options.Username = username;
        Options.Password = password;
        Options.PasswordIsEncoded = passwordIsEncoded;

        // If the username & password has been updated likely the authentication needs to run again.
        isLoggedIn = false;
    }

    /// <summary>Will ensure the client is authenticated by checking the <see cref="isLoggedIn"/> property and executing the login process if required.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="Task"/> that resolves when the process is complete.</returns>
    protected internal override async Task EnsureLoggedInAsync(CancellationToken cancellationToken)
    {
        if (!isLoggedIn)
        {
            await loginSemaphore.WaitAsync(cancellationToken)
                                .ConfigureAwait(false);
            try
            {
                if (!isLoggedIn)
                {
                    await LoginInternalAsync(cancellationToken).ConfigureAwait(false);
                }
            }
            finally
            {
                _ = loginSemaphore.Release();
            }
        }
    }

    private async Task LoginInternalAsync(CancellationToken cancellationToken)
    {
        using var activity = AydskoDataClientDiagnostics.ActivitySource.StartActivity("Login");

        if (string.IsNullOrWhiteSpace(Options.Username))
        {
            throw iRacingClientOptionsValueMissingException.Create(nameof(Options.Username));
        }

        if (string.IsNullOrWhiteSpace(Options.Password))
        {
            throw iRacingClientOptionsValueMissingException.Create(nameof(Options.Password));
        }

        try
        {
            if (Options.RestoreCookies is not null
                && Options.RestoreCookies() is CookieCollection savedCookies)
            {
                cookieContainer.Add(savedCookies);
            }

            var cookies = cookieContainer.GetCookies(new Uri("https://members-ng.iracing.com"));
            if (cookies["authtoken_members"] is { Expired: false })
            {
                isLoggedIn = true;
                logger.LoginCookiesRestored(Options.Username!);
                return;
            }

            string? encodedHash = null;

            if (Options.PasswordIsEncoded)
            {
                encodedHash = Options.Password;
            }
            else
            {
#pragma warning disable CA1308 // Normalize strings to uppercase - iRacing API requires lowercase
                var passwordAndEmail = Options.Password + (Options.Username?.ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

#if NET6_0_OR_GREATER
                var hashedPasswordAndEmailBytes = SHA256.HashData(Encoding.UTF8.GetBytes(passwordAndEmail));
#else
                using var sha256 = SHA256.Create();
                var hashedPasswordAndEmailBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordAndEmail));
#endif

                encodedHash = Convert.ToBase64String(hashedPasswordAndEmailBytes);
            }

            var loginResponse = await HttpClient.PostAsJsonAsync("https://members-ng.iracing.com/auth",
                                                                 new
                                                                 {
                                                                     email = Options.Username,
                                                                     password = encodedHash
                                                                 },
                                                                 cancellationToken)
                                                .ConfigureAwait(false);

            if (!loginResponse.IsSuccessStatusCode)
            {
                if (loginResponse.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new iRacingInMaintenancePeriodException("Maintenance assumed because login returned HTTP Error 503 \"Service Unavailable\".");
                }
                else if (loginResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
#if NET6_0_OR_GREATER
                    var content = await loginResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#else
                    var content = await loginResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif
                    var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content);

                    if (errorResponse is not null && errorResponse.ErrorCode == "access_denied")
                    {
                        var errorDescription = errorResponse.ErrorDescription ?? errorResponse.Note ?? errorResponse.Message ?? string.Empty;
                        throw iRacingLoginFailedException.Create($"Access was denied with message \"{errorDescription}\"",
                                                                 false,
                                                                 errorDescription.Equals("legacy authorization refused", StringComparison.OrdinalIgnoreCase));
                    }
                }
                throw new iRacingLoginFailedException($"Login failed with HTTP response \"{loginResponse.StatusCode} {loginResponse.ReasonPhrase}\"");
            }

            var loginResult = await loginResponse.Content.ReadFromJsonAsync(LoginResponseContext.Default.LoginResponse, cancellationToken).ConfigureAwait(false);

            if (loginResult is null || !loginResult.Success)
            {
                var message = loginResult?.Message ?? $"Login failed with HTTP response \"{loginResponse.StatusCode} {loginResponse.ReasonPhrase}\"";
                throw iRacingLoginFailedException.Create(message, loginResult?.VerificationRequired, string.Equals(loginResult?.Message, "Legacy authorization refused.", StringComparison.OrdinalIgnoreCase));
            }

            isLoggedIn = true;
            logger.LoginSuccessful(Options.Username!);

            if (Options.SaveCookies is Action<CookieCollection> saveCredentials)
            {
                saveCredentials(cookieContainer.GetAllCookies());
            }
        }
        catch (Exception ex) when (ex is not iRacingDataClientException)
        {
            throw iRacingLoginFailedException.Create(ex);
        }
    }

    protected internal override void ClearLoggedInState()
    {
        // Unauthorized might just be our session expired
        isLoggedIn = false;

        // Clear any externally saved cookies
        Options.SaveCookies?.Invoke([]);

        // Reset the cookie container so we can re-login
        cookieContainer = new CookieContainer();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            loginSemaphore.Dispose();
        }
        base.Dispose(disposing);
    }
}
