namespace Chronos.Models;

public class WgClientStat
{
    public string Peer { get; set; }
    public string RemoteIp { get; set; }
    public string RemotePort { get; set; }
    public string WireGuardIp { get; set; }
    public TimeSpan LatestHandshake { get; set; }
    public decimal ReceivedTraffic { get; set; }
    public decimal SentTraffic { get; set; }
}