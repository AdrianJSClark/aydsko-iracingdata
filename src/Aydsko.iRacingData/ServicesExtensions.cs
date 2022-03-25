// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;

namespace Aydsko.iRacingData;

public static class ServicesExtensions
{
    public static IServiceCollection AddIRacingDataApi(this IServiceCollection services!!, Action<iRacingDataClientOptions> configureOptions!!)
    {
        services.TryAddSingleton(new CookieContainer());

        var options = new iRacingDataClientOptions();

#pragma warning disable CA1062 // Validate arguments of public methods
        configureOptions(options);
#pragma warning restore CA1062 // Validate arguments of public methods

        if (string.IsNullOrWhiteSpace(options.Username))
        {
            throw iRacingClientOptionsValueMissingException.Create(nameof(options.Username));
        }

        if (string.IsNullOrWhiteSpace(options.Password))
        {
            throw iRacingClientOptionsValueMissingException.Create(nameof(options.Password));
        }

        services.AddHttpClient<IDataClient, DataClient>((HttpClient httpClient, IServiceProvider provider) => new DataClient(httpClient,
                                                                                                                                                  provider.GetRequiredService<ILogger<DataClient>>(),
                                                                                                                                                  options,
                                                                                                                                                  provider.GetRequiredService<CookieContainer>()))
                .ConfigurePrimaryHttpMessageHandler(services => new HttpClientHandler
                {
                    UseCookies = true,
                    CookieContainer = services.GetRequiredService<CookieContainer>()
                });

        return services;
    }
}
