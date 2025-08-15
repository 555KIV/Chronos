using Chronos.Models.Entities;
using Chronos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Repositories.Implementations;

public class FamilyRepository(ChronosDbContext context) : IFamilyRepository
{
    public async Task AddAsync(FamilyEntity family)
    {
        await context.Families.AddAsync(family);
        await context.SaveChangesAsync();
    }

    public async Task<FamilyEntity?> GetAsync(int id)
    {
        return await context.Families.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<FamilyEntity>> GetAllAsync()
    {
        return await context.Families.ToListAsync();
    }
}