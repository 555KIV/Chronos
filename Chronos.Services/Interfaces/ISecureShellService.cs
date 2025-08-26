namespace Chronos.Services.Interfaces;

public interface ISecureShellService
{
    Task<string> GetWireGuardStatistics(CancellationToken cancellationToken = default);
    Task<Dictionary<string, string>> GetClients(CancellationToken cancellationToken = default);
}