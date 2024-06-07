// © 2023-2024 Adrian Clark
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

        services.TryAddSingleton(new CookieContainer());
#pragma warning disable CS0618 // Type or member is obsolete
        services.TryAddTransient<TrackScreenshotService>();
#pragma warning restore CS0618 // Type or member is obsolete

        var options = new iRacingDataClientOptions();
        configureOptions.Invoke(options);
        services.AddSingleton(options);

        var userAgentValue = CreateUserAgentValue(options);

        var httpClientBuilder = (includeCaching ? services.AddHttpClient<IDataClient, CachingDataClient>() : services.AddHttpClient<IDataClient, DataClient>())
                                .ConfigureHttpClient(httpClient => httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgentValue))
                                .ConfigurePrimaryHttpMessageHandler(() =>
                                {
                                    var handler = new HttpClientHandler
                                    {
                                        UseCookies = true,
                                        CookieContainer = services.BuildServiceProvider().GetRequiredService<CookieContainer>()
                                    };
                                    return handler;
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
