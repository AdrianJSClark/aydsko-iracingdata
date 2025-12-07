using Microsoft.Extensions.DependencyInjection;

namespace Aydsko.iRacingData;

public static class ServicesExtensions
{
    public static IServiceCollection AddIRacingData(this IServiceCollection services, string configurationPath = "iRacing")
    {
        services.AddOptions<iRacingOptions>()
                .BindConfiguration(configurationPath)
                .ValidateDataAnnotations()
                .ValidateOnStart();

        return services;
    }
}
