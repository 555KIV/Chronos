using Chronos.Models.Options;
using Chronos.Services.Implementation;
using Chronos.Services.Interfaces;
using Chronos.Repositories.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chronos.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddChronosServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ISecureShellService, SecureShellService>();

        services.AddRepositories();
        services.AddSettings();
        
        return services;
    }
    
    private static IServiceCollection AddSettings(this IServiceCollection services)
    {
        services.AddOptions<SecureShellOptions>()
            .BindConfiguration(nameof(SecureShellOptions));

        services.AddOptions<TariffPlanOptions>()
            .BindConfiguration(nameof(TariffPlanOptions));
        
        return services;
    }
}