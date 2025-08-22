using System.Globalization;
using System.Text.RegularExpressions;
using Chronos.Models;
using Chronos.Services.Interfaces;

namespace Chronos.Services.Implementation;

public class ParsingService : IParsingService
{
    private readonly Regex _peerRegex = new (@"peer: (?<peer>.*)");
    private readonly Regex _remoteAddressRegex = new (@"endpoint: (?<ip>\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}):(?<port>\d{1,5})");
    private readonly Regex _wireGuardIpRegex = new(@"allowed ips: (?<ip>\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3})");
    private readonly Regex _handshakeRegex = new (@"latest handshake: (?:(?<day>\d*) days?)?\W*?(?:(?<hour>\d*) hours?)?\W*?(?:(?<minute>\d*) minutes?)?\W*?(?:(?<seconds>\d*) seconds? ago)");
    private readonly Regex _trafficRegex = new (@"transfer: (?<received_vol>\d*\.\d*) (?<received_type>(?:M|G)iB) received, (?<sent_vol>\d*\.\d*) (?<sent_type>(?:M|G)iB)");
    
    public IEnumerable<WgClientStat> GetClientStats(IEnumerable<string> clientData)
    {
        var listStats = new List<WgClientStat>();

        foreach (var client in clientData)
        {
            var peer = _peerRegex.Match(client).Groups["peer"].Value;
            var remoteIp = _remoteAddressRegex.Match(client).Groups["ip"].Value;
            var remotePort = _remoteAddressRegex.Match(client).Groups["port"].Value;
            var wireGuard = _wireGuardIpRegex.Match(client).Groups["ip"].Value;
            var handshake = GetLatestHandshake(client);
            var (received, sent) = GetLatestTraffic(client);
            
            listStats.Add(new WgClientStat()
            {
                Peer = peer,
                RemoteIp = remoteIp,
                RemotePort = remotePort,
                WireGuardIp = wireGuard,
                LatestHandshake = handshake,
                ReceivedTraffic = received,
                SentTraffic = sent,
            });
        }
        
        return listStats;
    }

    private TimeSpan GetLatestHandshake(string str)
    {
        var match = _handshakeRegex.Match(str);
        
        int.TryParse(match.Groups["day"].Value, out var day);
        int.TryParse(match.Groups["hour"].Value, out var hour);
        int.TryParse(match.Groups["minute"].Value, out var minute);
        int.TryParse(match.Groups["seconds"].Value, out var seconds);

        return TimeSpan.FromDays(day, hour, minute, seconds);
    }

    private (decimal ReceivedTraffic, decimal SentTraffic) GetLatestTraffic(string str)
    {
        var match = _trafficRegex.Match(str);
        
        decimal.TryParse(match.Groups["received_vol"].Value, CultureInfo.InvariantCulture, out var receivedVol);
        var receivedType = match.Groups["received_type"].Value;
        
        decimal.TryParse(match.Groups["sent_vol"].Value, CultureInfo.InvariantCulture, out var sentVol);
        var sentType = match.Groups["sent_type"].Value;

        if (string.Equals(receivedType, "mib", StringComparison.OrdinalIgnoreCase))
        {
            receivedVol /= 1024;
        }
        
        if (string.Equals(sentType, "mib", StringComparison.OrdinalIgnoreCase))
        {
            sentVol /= 1024;
        }
        
        return (receivedVol, sentVol);
    }
}