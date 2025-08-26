using Chronos.Repositories.Implementations;
using Chronos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chronos.Repositories.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddDbContext<ChronosDbContext>(x => x.UseSqlite("Data Source=Chronos.db"));
        services.AddScoped<IClientsRepository, ClientsRepository>();
        services.AddScoped<ISpentTrafficRepository, SpentTrafficRepository>();
        services.AddScoped<IFamilyRepository, FamilyRepository>();
        
        return services;
    }
}