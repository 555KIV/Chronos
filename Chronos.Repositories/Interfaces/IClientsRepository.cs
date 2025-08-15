using Chronos.Models.Entities;

namespace Chronos.Repositories.Interfaces;

public interface IClientsRepository
{
    Task AddAsync(IEnumerable<ClientEntity> entities);
    Task AddAsync(ClientEntity entity);
    Task<ClientEntity?> GetAsync(int id);
    Task<IEnumerable<ClientEntity>> GetAllAsync();
}