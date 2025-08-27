using Chronos.Models.Entities;
using Chronos.Repositories.Interfaces;
using Chronos.Services.Interfaces;

namespace Chronos.Services.Implementation;

public class ChronosService(
    ISecureShellService secureShellService,
    IParsingService parsingService,
    IClientsRepository clientsRepository,
    ISpentTrafficRepository spentTrafficRepository) : IChronosService
{
    private const string SplitStr = "\n\n";

    public async Task SaveWgStatisticAsync()
    {
        var rawStat = await secureShellService.GetWireGuardStatisticsAsync();

        var parsStats = parsingService.GetClientStats(rawStat.Split(SplitStr));

        var clients = await clientsRepository.GetAllAsync();

        var now = DateTime.Now;

        var lastTraffic = await spentTrafficRepository.GetLastRecAsync();
        var averageTraffic = await spentTrafficRepository.GetAverageLastMonthAsync();
        var uptime = now - DateTime.Parse(await secureShellService.GetUpTimeAsync());

        var clientStats = clients.Join(parsStats, entity => entity.IpAddress, stat => stat.WireGuardIp, (entity, stat) =>
            {
                lastTraffic.TryGetValue(entity.Id, out var lastStat);
                averageTraffic.TryGetValue(entity.Id, out var averageStat); 
                
                return new SpentTrafficEntity
                {
                    ClientId = entity.Id,
                    SentVolumeAll = stat.SentTraffic,
                    SentVolumePerDay = CalcStat(stat.SentTraffic, lastStat?.SentVolumeAll, averageStat.Sent, uptime),
                    ReceivedVolumeAll = stat.ReceivedTraffic,
                    ReceivedVolumePerDay = CalcStat(stat.ReceivedTraffic, lastStat?.ReceivedVolumeAll, averageStat.Received, uptime),
                    Date = new DateTime(now.Year, now.Month, now.Day),
                    CreatedDate = now,
                    Client = entity
                };
            });

        await spentTrafficRepository.AddAsync(clientStats);

        await clientsRepository.UpdateHandshakeAsync(parsStats.Where(x => x.LatestHandshake != TimeSpan.Zero).ToDictionary(x => x.WireGuardIp,
            y => now - y.LatestHandshake));
    }

    public async Task CreateClientsAsync()
    {
        var rawClients = await secureShellService.GetClientsAsync();

        var parsClient = parsingService.GetClients(rawClients);

        var utcNow = DateTime.Now;

        await clientsRepository.UpsertAsync(parsClient.Select(x => new ClientEntity()
        {
            ClientName = x.ClientName,
            IpAddress = x.Address,
            CreatedDate = utcNow,
        }));
    }

    private decimal CalcStat(decimal current, decimal? prev, decimal average, TimeSpan upTime)
    {
        return current < prev 
            ? average / 24 * (24 - upTime.Hours) + current 
            : current - (prev ?? 0);
    }
}