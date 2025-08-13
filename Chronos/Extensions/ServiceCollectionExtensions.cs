using Chronos.Services.Extensions;

namespace Chronos.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddChronosServices(configuration);
        
        services.AddOpenApi();
        
        return services;
    }
}