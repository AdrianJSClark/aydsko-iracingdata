// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using System.Reflection;
using Aydsko.iRacingData.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Aydsko.iRacingData;

public static class ServicesExtensions
{
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

        if (!Uri.TryCreate(options.ApiBaseUrl, UriKind.Absolute, out var _))
        {
            throw new iRacingDataClientException($"Invalid or missing \"{nameof(iRacingDataClientOptions.ApiBaseUrl)}\" value. Must be populated with an absolute URL.");
        }

        if (!Uri.TryCreate(options.AuthServiceBaseUrl, UriKind.Absolute, out var _))
        {
            throw new iRacingDataClientException($"Invalid or missing \"{nameof(iRacingDataClientOptions.AuthServiceBaseUrl)}\" value. Must be populated with an absolute URL.");
        }

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

        if (options.OAuthTokenResponseCallback is not null)
        {
            httpClientBuilder = services.AddHttpClient<IAuthenticatingHttpClient, OAuthCallbackAuthenticatingApiClient>();
        }
        else if (!string.IsNullOrWhiteSpace(options.ClientId) && !string.IsNullOrWhiteSpace(options.ClientSecret))
        {
            httpClientBuilder = services.AddHttpClient<IAuthenticatingHttpClient, PasswordLimitedOAuthAuthenticatingHttpClient>();
        }
        else if (options.TokenSourceFactory is not null)
        {
            services.AddTransient(options.TokenSourceFactory);
            httpClientBuilder = services.AddHttpClient<IAuthenticatingHttpClient, OAuthTokenSourceApiClient>();
        }
        else
        {
            throw new iRacingDataClientException("Invalid configuration for iRacing authentication. You must configure OAuth authentication using the \"UseOAuthTokenSource\" method on the options object.");
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
}
