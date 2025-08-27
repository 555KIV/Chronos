using Chronos.Models.Entities;

namespace Chronos.Repositories.Interfaces;

public interface ISpentTrafficRepository
{
    Task AddAsync(IEnumerable<SpentTrafficEntity> entities);
    Task<IEnumerable<SpentTrafficEntity>> GetByClientIdAsync(int clientId);
    Task<IEnumerable<SpentTrafficEntity>> GetByFamilyIdAsync(int familyId);
    Task<Dictionary<int, SpentTrafficEntity?>> GetLastRecAsync();
    Task<Dictionary<int, (decimal Sent, decimal Received)>> GetAverageLastMonthAsync();
}