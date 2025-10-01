// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using System.Net;
using System.Reflection;
using Aydsko.iRacingData.Tracks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Aydsko.iRacingData;

public static class ServicesExtensions
{
    /// <summary>Add required types for iRacing Data API to the service collection.</summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The http client builder for further configuration.</returns>
    /// <exception cref="ArgumentNullException">One of the arguments is <see langword="null"/>.</exception>
    public static IHttpClientBuilder AddIRacingDataApi(this IServiceCollection services)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(services);
#else
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
#endif

        return services.AddIRacingDataApiInternal((_) => { }, false);
    }

    /// <summary>Add required types for iRacing Data API to the service collection.</summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configureOptions">Action to configure the options for the API client.</param>
    /// <returns>The http client builder for further configuration.</returns>
    /// <exception cref="ArgumentNullException">One of the arguments is <see langword="null"/>.</exception>
    public static IHttpClientBuilder AddIRacingDataApi(this IServiceCollection services, Action<iRacingDataClientOptions> configureOptions)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureOptions);
#else
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configureOptions is null)
        {
            throw new ArgumentNullException(nameof(configureOptions));
        }
#endif

        return services.AddIRacingDataApiInternal(configureOptions, false);
    }

    /// <summary>Add required types for iRacing Data API with caching enabled to the service collection.</summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The http client builder for further configuration.</returns>
    /// <exception cref="ArgumentNullException">One of the arguments is <see langword="null"/>.</exception>
    public static IHttpClientBuilder AddIRacingDataApiWithCaching(this IServiceCollection services)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(services);
#else
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
#endif

        return services.AddIRacingDataApiInternal((_) => { }, true);
    }

    /// <summary>Add required types for iRacing Data API with caching enabled to the service collection.</summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configureOptions">Action to configure the options for the API client.</param>
    /// <returns>The http client builder for further configuration.</returns>
    /// <exception cref="ArgumentNullException">One of the arguments is <see langword="null"/>.</exception>
    public static IHttpClientBuilder AddIRacingDataApiWithCaching(this IServiceCollection services, Action<iRacingDataClientOptions> configureOptions)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureOptions);
#else
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configureOptions is null)
        {
            throw new ArgumentNullException(nameof(configureOptions));
        }
#endif

        return services.AddIRacingDataApiInternal(configureOptions, true);
    }

    internal static IHttpClientBuilder AddIRacingDataApiInternal(this IServiceCollection services,
                                                                 Action<iRacingDataClientOptions> configureOptions,
                                                                 bool includeCaching)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(services);
#else
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
#endif

        var options = new iRacingDataClientOptions();
        configureOptions.Invoke(options);
        services.AddSingleton(options);

        var userAgentValue = CreateUserAgentValue(options);

        services.TryAddSingleton(TimeProvider.System);

        if (includeCaching)
        {
            services.AddTransient<IApiClient, CachingApiClient>();
        }
        else
        {
            services.AddTransient<IApiClient, ApiClient>();
        }

        services.AddTransient<IDataClient, DataClient>();

        IHttpClientBuilder httpClientBuilder;

        if (!string.IsNullOrWhiteSpace(options.ClientId) && !string.IsNullOrWhiteSpace(options.ClientSecret))
        {
            httpClientBuilder = services.AddHttpClient<IAuthenticatingHttpClient, PasswordLimitedOAuthAuthenticatingHttpClient>();
        }
        else
        {
            services.TryAddSingleton<CookieContainer>();
            httpClientBuilder = services.AddHttpClient<IAuthenticatingHttpClient, LegacyUsernamePasswordApiClient>()
                                        .ConfigurePrimaryHttpMessageHandler(sp =>
                                        {
                                            var handler = new HttpClientHandler
                                            {
                                                UseCookies = true,
                                                CookieContainer = sp.GetRequiredService<CookieContainer>()
                                            };
                                            return handler;
                                        });
        }

        httpClientBuilder.ConfigureHttpClient(httpClient => httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgentValue));

        return httpClientBuilder;
    }

    private static string CreateUserAgentValue(iRacingDataClientOptions options)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(options);
#else
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }
#endif

        if (options.UserAgentProductName is not string userAgentProductName)
        {
            userAgentProductName = Assembly.GetEntryAssembly()?.GetName().Name ?? "Unknown";
        }

        if (options.UserAgentProductVersion is not Version userAgentProductVersion)
        {
            userAgentProductVersion = Assembly.GetEntryAssembly()?.GetName().Version ?? new Version(0, 0);
        }

        var dataClientVersion = typeof(DataClient).Assembly.GetName()?.Version?.ToString(3) ?? "0.0";
        var userAgentValue = $"{userAgentProductName}/{userAgentProductVersion} Aydsko.iRacingDataClient/{dataClientVersion}";
        return userAgentValue;
    }

    /// <summary>Configure the options to use legacy iRacing username/password authentication.</summary>
    /// <param name="options">The options object to configure.</param>
    /// <param name="userName">iRacing username</param>
    /// <param name="password">iRacing password</param>
    /// <param name="passwordIsEncoded">Indicates that the <paramref name="password"/> value is already encoded for supply to the iRacing Authentication API.</param>
    /// <returns>The options object to allow call chaining.</returns>
    public static iRacingDataClientOptions UseUsernamePasswordAuthentication(this iRacingDataClientOptions options,
                                                                             string userName,
                                                                             string password,
                                                                             bool passwordIsEncoded = false)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(options);
        ArgumentException.ThrowIfNullOrWhiteSpace(userName);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
#else
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentNullException(nameof(userName));
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentNullException(nameof(password));
        }
#endif

        options.Username = userName;
        options.Password = password;
        options.PasswordIsEncoded = passwordIsEncoded;

        return options;
    }

    /// <summary>Configure the options to use "Password Limited" iRacing authentication.</summary>
    /// <param name="options">The options object to configure.</param>
    /// <param name="userName">iRacing username</param>
    /// <param name="password">iRacing password</param>
    /// <param name="clientId">The iRacing-supplied Client ID value.</param>
    /// <param name="clientSecret">The iRacing-supplied Client Secret value.</param>
    /// <param name="passwordIsEncoded">Indicates that the <paramref name="password"/> value is already encoded for supply to the iRacing Authentication API.</param>
    /// <param name="clientSecretIsEncoded">Indicates that the <paramref name="clientSecret"/> value is already encoded for supply to the iRacing Authentication API.</param>
    /// <returns>The options object to allow call chaining.</returns>
    public static iRacingDataClientOptions UsePasswordLimitedOAuth(this iRacingDataClientOptions options,
                                                                            string userName,
                                                                            string password,
                                                                            string clientId,
                                                                            string clientSecret,
                                                                            bool passwordIsEncoded = false,
                                                                            bool clientSecretIsEncoded = false)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(options);
        ArgumentException.ThrowIfNullOrWhiteSpace(userName);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        ArgumentException.ThrowIfNullOrWhiteSpace(clientId);
        ArgumentException.ThrowIfNullOrWhiteSpace(clientSecret);
#else
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentNullException(nameof(userName));
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentNullException(nameof(password));
        }

        if (string.IsNullOrWhiteSpace(clientId))
        {
            throw new ArgumentNullException(nameof(clientId));
        }

        if (string.IsNullOrWhiteSpace(clientSecret))
        {
            throw new ArgumentNullException(nameof(clientSecret));
        }
#endif

        options.Username = userName;
        options.Password = password;
        options.PasswordIsEncoded = passwordIsEncoded;
        options.ClientId = clientId;
        options.ClientSecret = clientSecret;
        options.ClientSecretIsEncoded = clientSecretIsEncoded;

        return options;
    }

    /// <summary>Configure the user agent details to use in requests.</summary>
    /// <param name="options">The options object to configure.</param>
    /// <param name="productName">Name of the client.</param>
    /// <param name="productVersion">Version of the client.</param>
    /// <returns>The options object to allow call chaining.</returns>
    public static iRacingDataClientOptions UseProductUserAgent(this iRacingDataClientOptions options,
                                                               string productName,
                                                               Version productVersion)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(options);
        ArgumentException.ThrowIfNullOrWhiteSpace(productName);
        ArgumentNullException.ThrowIfNull(productVersion);
#else
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new ArgumentNullException(nameof(productName));
        }

        if (productVersion is null)
        {
            throw new ArgumentNullException(nameof(productVersion));
        }
#endif

        options.UserAgentProductName = productName;
        options.UserAgentProductVersion = productVersion;

        return options;
    }

}
