// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Net;
using System.Reflection;
using Aydsko.iRacingData.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Aydsko.iRacingData;

public static class ServicesExtensions
{
    public static IServiceCollection AddIRacingDataApi(this IServiceCollection services, Action<iRacingDataClientOptions> configureOptions)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configureOptions is null)
        {
            throw new ArgumentNullException(nameof(configureOptions));
        }

        services.AddIRacingDataApiInternal(configureOptions);
        return services;
    }

    static internal IHttpClientBuilder AddIRacingDataApiInternal(this IServiceCollection services, Action<iRacingDataClientOptions> configureOptions)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        configureOptions ??= opt => { };

        services.TryAddSingleton(new CookieContainer());

        var options = new iRacingDataClientOptions();
        configureOptions(options);

        var userAgentValue = CreateUserAgentValue(options);

        var httpClientBuilder = services.AddHttpClient<IDataClient, DataClient>((HttpClient httpClient, IServiceProvider provider) => new DataClient(httpClient,
                                                                                                                                                     provider.GetRequiredService<ILogger<DataClient>>(),
                                                                                                                                                     options,
                                                                                                                                                     provider.GetRequiredService<CookieContainer>()))
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
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

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
