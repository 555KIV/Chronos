using Chronos.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Chronos.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddChronosServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSettings();
        
        return services;
    }
    
    private static IServiceCollection AddSettings(this IServiceCollection services)
    {
        services.AddOptions<SecureShellOptions>()
            .BindConfiguration(nameof(SecureShellOptions));
        
        return services;
    }
}