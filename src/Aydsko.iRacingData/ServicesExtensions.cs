// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;

namespace Aydsko.iRacingData;

public static class ServicesExtensions
{
    public static IServiceCollection AddIRacingDataApi(this IServiceCollection services)
    {
        services.AddSingleton(new CookieContainer());

        services.AddHttpClient<iRacingDataClient>()
                .ConfigurePrimaryHttpMessageHandler(services => new HttpClientHandler
                {
                    UseCookies = true,
                    CookieContainer = services.GetRequiredService<CookieContainer>()
                });

        return services;
    }
}

