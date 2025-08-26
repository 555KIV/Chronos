using Chronos.Services.Extensions;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;

namespace Chronos.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddChronosServices(configuration);
        
        services.AddTickerQ(options =>
        {
            options.AddDashboard(x => x.BasePath = "/dashboard");
            options.SetInstanceIdentifier("MyAppTickerQ");
        });
        
        services.AddOpenApi();
        
        return services;
    }
}