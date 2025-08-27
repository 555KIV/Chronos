namespace Chronos.Services.Interfaces;

public interface ISecureShellService
{
    Task<string> GetWireGuardStatisticsAsync(CancellationToken cancellationToken = default);
    Task<Dictionary<string, string>> GetClientsAsync(CancellationToken cancellationToken = default);
    Task<string> GetUpTimeAsync(CancellationToken cancellationToken = default);
}