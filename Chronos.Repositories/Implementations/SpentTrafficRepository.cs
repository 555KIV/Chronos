using Chronos.Models.Entities;
using Chronos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Repositories.Implementations;

public class SpentTrafficRepository(ChronosDbContext context) : ISpentTrafficRepository
{
    public async Task AddAsync(IEnumerable<SpentTrafficEntity> entities)
    {
        await context.SpentTraffic.AddRangeAsync(entities);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<SpentTrafficEntity>> GetByClientIdAsync(int clientId)
    {
        return await context.SpentTraffic.Where(x => x.ClientId == clientId).ToListAsync();
    }

    public async Task<IEnumerable<SpentTrafficEntity>> GetByFamilyIdAsync(int familyId)
    {
        return await context.SpentTraffic.Include(x => x.Client).Where(x => x.Client!.FamilyId == familyId).ToListAsync();
    }
}