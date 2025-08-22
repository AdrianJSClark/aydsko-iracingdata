// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Aydsko.iRacingData.Exceptions;

namespace Aydsko.iRacingData;

public class LegacyUsernamePasswordApiClient(HttpClient httpClient,
                                             iRacingDataClientOptions options,
                                             CookieContainer cookieContainer,
                                             ILogger<LegacyUsernamePasswordApiClient> logger)
    : IAuthenticatingHttpClient, IDisposable
{
    private readonly SemaphoreSlim loginSemaphore = new(1, 1);
#pragma warning disable CA1051 // Do not declare visible instance fields - Declared visible for testing purposes.
    protected bool isLoggedIn;
    private bool disposedValue;
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

        options.Username = username;
        options.Password = password;
        options.PasswordIsEncoded = passwordIsEncoded;

        // If the username & password has been updated likely the authentication needs to run again.
        isLoggedIn = false;
    }

    /// <summary>Will ensure the client is authenticated by checking the <see cref="isLoggedIn"/> property and executing the login process if required.</summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="Task"/> that resolves when the process is complete.</returns>
    protected internal async Task EnsureLoggedInAsync(CancellationToken cancellationToken)
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

        if (string.IsNullOrWhiteSpace(options.Username))
        {
            throw iRacingClientOptionsValueMissingException.Create(nameof(options.Username));
        }

        if (string.IsNullOrWhiteSpace(options.Password))
        {
            throw iRacingClientOptionsValueMissingException.Create(nameof(options.Password));
        }

        try
        {
            if (options.RestoreCookies is not null
                && options.RestoreCookies() is CookieCollection savedCookies)
            {
                cookieContainer.Add(savedCookies);
            }

            var cookies = cookieContainer.GetCookies(new Uri("https://members-ng.iracing.com"));
            if (cookies["authtoken_members"] is { Expired: false })
            {
                isLoggedIn = true;
                logger.LoginCookiesRestored(options.Username!);
                return;
            }

            var encodedHash = options.PasswordIsEncoded ? options.Password : ApiClientBase.EncodePassword(options.Username!, options.Password!);
            var loginResponse = await httpClient.PostAsJsonAsync("https://members-ng.iracing.com/auth",
                                                                 new
                                                                 {
                                                                     email = options.Username,
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
            logger.LoginSuccessful(options.Username!);

            if (options.SaveCookies is Action<CookieCollection> saveCredentials)
            {
                saveCredentials(cookieContainer.GetAllCookies());
            }
        }
        catch (Exception ex) when (ex is not iRacingDataClientException)
        {
            throw iRacingLoginFailedException.Create(ex);
        }
    }

    public void ClearLoggedInState()
    {
        // Unauthorized might just be our session expired
        isLoggedIn = false;

        // Clear any externally saved cookies
        options.SaveCookies?.Invoke([]);

        // Reset the cookie container so we can re-login
        cookieContainer = new CookieContainer();
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                     HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
                                                     CancellationToken cancellationToken = default)
    {
        var response = await httpClient.SendAsync(request, completionOption, cancellationToken)
                                       .ConfigureAwait(false);
        return response;
    }

    public async Task<HttpResponseMessage> SendAuthenticatedRequestAsync(HttpRequestMessage request,
                                                                         HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
                                                                         CancellationToken cancellationToken = default)
    {
        await EnsureLoggedInAsync(cancellationToken).ConfigureAwait(false);
        return await SendAsync(request, completionOption, cancellationToken).ConfigureAwait(false);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                loginSemaphore.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~LegacyUsernamePasswordApiClient()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
