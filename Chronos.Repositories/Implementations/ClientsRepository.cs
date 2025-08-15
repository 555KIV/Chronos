using Chronos.Models.Entities;
using Chronos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Repositories.Implementations;

public class ClientsRepository(ChronosDbContext context) : IClientsRepository
{
    public async Task AddAsync(ClientEntity client)
    {
        await context.Clients.AddAsync(client);
        await context.SaveChangesAsync();
    }

    public async Task AddAsync(IEnumerable<ClientEntity> clients)
    {
        await context.Clients.AddRangeAsync(clients);
        await context.SaveChangesAsync();
    }

    public async Task<ClientEntity?> GetAsync(int id)
    {
        return await context.Clients.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<ClientEntity>> GetAllAsync()
    {
        return await context.Clients.ToListAsync();
    }
}