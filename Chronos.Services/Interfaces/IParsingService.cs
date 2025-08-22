using Chronos.Models;

namespace Chronos.Services.Interfaces;

public interface IParsingService
{
    IEnumerable<WgClientStat> GetClientStats(IEnumerable<string> clientData);
}