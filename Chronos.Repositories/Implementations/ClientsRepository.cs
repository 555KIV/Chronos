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

    public async Task UpdateHandshakeAsync(Dictionary<string, DateTime> updates)
    {
        foreach (var (ip, dt) in updates)
        {
            var entity = await context.Clients.FirstOrDefaultAsync(x=> x.IpAddress == ip);
            if (entity != null)
            {
                entity.LastHandshake = dt;
            }
        }
        
        await context.SaveChangesAsync();
    }

    public async Task UpsertAsync(IEnumerable<ClientEntity> clients)
    {
        foreach (var client in clients)
        {
            var entity = await context.Clients.FirstOrDefaultAsync(x => x.ClientName == client.ClientName);
            if (entity != null)
            {
                entity.IpAddress = client.IpAddress;
            }
            else
            {
                context.Clients.Add(client);
            }
        }
        
        await context.SaveChangesAsync();
    }
}