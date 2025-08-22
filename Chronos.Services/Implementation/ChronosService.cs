using Chronos.Services.Interfaces;

namespace Chronos.Services.Implementation;

public class ChronosService(ISecureShellService secureShellService, IParsingService parsingService) : IChronosService
{
    private const string SplitStr = "\n\n";
    public async Task SaveWgStatistic()
    {
        var rawStat = await secureShellService.GetWireGuardStatistics();

        var parsStats = parsingService.GetClientStats(rawStat.Split(SplitStr));

        Console.WriteLine(parsStats.Sum(x => x.SentTraffic + x.ReceivedTraffic));
    }
}