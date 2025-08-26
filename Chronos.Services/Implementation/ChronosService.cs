using Chronos.Models.Entities;
using Chronos.Repositories.Interfaces;
using Chronos.Services.Interfaces;

namespace Chronos.Services.Implementation;

public class ChronosService(
    ISecureShellService secureShellService,
    IParsingService parsingService,
    IClientsRepository clientsRepository, ISpentTrafficRepository spentTrafficRepository) : IChronosService
{
    private const string SplitStr = "\n\n";

    public async Task SaveWgStatistic()
    {
        var rawStat = await secureShellService.GetWireGuardStatistics();

        var parsStats = parsingService.GetClientStats(rawStat.Split(SplitStr));

        var clients = await clientsRepository.GetAllAsync();

        var utcNow = DateTime.UtcNow;
        
        var clientStats = clients.Join(parsStats, entity => entity.IpAddress, stat => stat.WireGuardIp, (entity, stat) =>
            new SpentTrafficEntity()
            {
                ClientId = entity.Id,
                SentVolume = stat.SentTraffic,
                ReceivedVolume = stat.ReceivedTraffic,
                Date = utcNow,
                CreatedDate = utcNow,
                Client = entity
            });

        await spentTrafficRepository.AddAsync(clientStats);
    }

    public async Task CreateClients()
    {
        var rawClients = await secureShellService.GetClients();
        
        var parsClient = parsingService.GetClients(rawClients);
        
        var utcNow = DateTime.UtcNow;

        await clientsRepository.AddAsync(parsClient.Select(x => new ClientEntity()
        {
            ClientName = x.ClientName,
            IpAddress = x.Address,
            CreatedDate = utcNow,
        }));
    }
}