// © 2023 Adrian Clark
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
    /// <returns>The service collection for further configuration.</returns>
    /// <exception cref="ArgumentNullException">One of the arguments is <see langword="null"/>.</exception>
    public static IServiceCollection AddIRacingDataApi(this IServiceCollection services)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(services);
#else
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
#endif

        services.AddIRacingDataApiInternal((_) => { }, false);
        return services;
    }

    /// <summary>Add required types for iRacing Data API to the service collection.</summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configureOptions">Action to configure the options for the API client.</param>
    /// <returns>The service collection for further configuration.</returns>
    /// <exception cref="ArgumentNullException">One of the arguments is <see langword="null"/>.</exception>
    public static IServiceCollection AddIRacingDataApi(this IServiceCollection services, Action<iRacingDataClientOptions> configureOptions)
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

        services.AddIRacingDataApiInternal(configureOptions, false);
        return services;
    }

    /// <summary>Add required types for iRacing Data API with caching enabled to the service collection.</summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The service collection for further configuration.</returns>
    /// <exception cref="ArgumentNullException">One of the arguments is <see langword="null"/>.</exception>
    public static IServiceCollection AddIRacingDataApiWithCaching(this IServiceCollection services)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(services);
#else
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
#endif

        services.AddIRacingDataApiInternal((_) => { }, true);
        return services;
    }

    /// <summary>Add required types for iRacing Data API with caching enabled to the service collection.</summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configureOptions">Action to configure the options for the API client.</param>
    /// <returns>The service collection for further configuration.</returns>
    /// <exception cref="ArgumentNullException">One of the arguments is <see langword="null"/>.</exception>
    public static IServiceCollection AddIRacingDataApiWithCaching(this IServiceCollection services, Action<iRacingDataClientOptions> configureOptions)
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

        services.AddIRacingDataApiInternal(configureOptions, true);
        return services;
    }

    static internal IHttpClientBuilder AddIRacingDataApiInternal(this IServiceCollection services,
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

        services.TryAddSingleton(new CookieContainer());
        services.TryAddTransient<TrackScreenshotService>();

        var options = new iRacingDataClientOptions();
        configureOptions.Invoke(options);
        services.AddSingleton(options);

        var userAgentValue = CreateUserAgentValue(options);

        var httpClientBuilder = (includeCaching ? services.AddHttpClient<IDataClient, CachingDataClient>() : services.AddHttpClient<IDataClient, DataClient>())
                                .ConfigureHttpClient(httpClient => httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgentValue))
                                .ConfigureHttpMessageHandlerBuilder(msgHandlerBuilder =>
                                {
                                    if (msgHandlerBuilder.PrimaryHandler is HttpClientHandler httpClientHandler)
                                    {
                                        httpClientHandler.UseCookies = true;
                                        httpClientHandler.CookieContainer = msgHandlerBuilder.Services.GetRequiredService<CookieContainer>();
                                    }
                                });

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
