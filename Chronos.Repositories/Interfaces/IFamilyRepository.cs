using Chronos.Models.Entities;

namespace Chronos.Repositories.Interfaces;

public interface IFamilyRepository
{
    Task AddAsync(FamilyEntity entity);
    Task<FamilyEntity?> GetAsync(int id);
    Task<IEnumerable<FamilyEntity>> GetAllAsync();
}