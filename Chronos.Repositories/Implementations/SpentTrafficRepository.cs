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

    public async Task<Dictionary<int, SpentTrafficEntity?>> GetLastRecAsync()
    {
        return await context.SpentTraffic
            .GroupBy(x => x.ClientId)
            .Select(x => x
                .OrderByDescending(y => y.Date)
                .LastOrDefault())
            .ToDictionaryAsync(x => x.ClientId, y => y);
    }

    public async Task<IEnumerable<SpentTrafficEntity>> GetByClientIdAsync(int clientId)
    {
        return await context.SpentTraffic.Where(x => x.ClientId == clientId).ToListAsync();
    }

    public async Task<IEnumerable<SpentTrafficEntity>> GetByFamilyIdAsync(int familyId)
    {
        return await context.SpentTraffic
            .Include(x => x.Client)
            .Where(x => x.Client!.FamilyId == familyId)
            .ToListAsync();
    }

    public async Task<Dictionary<int, (decimal Sent, decimal Received)>> GetAverageLastMonthAsync()
    {
        var oneMonthAgo = DateTime.Now.AddMonths(-1);

        return await context.SpentTraffic
            .Where(x => x.Date >= oneMonthAgo)
            .GroupBy(x => x.ClientId)
            .Select(g => new
            {
                ClientId = g.Key,
                Sent = g.Average(y => y.SentVolumePerDay),
                Received = g.Average(y => y.ReceivedVolumePerDay)
            })
            .ToDictionaryAsync(
                x => x.ClientId,
                x => (x.Sent, x.Received)
            );
    }
}