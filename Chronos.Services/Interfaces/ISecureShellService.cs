namespace Chronos.Services.Interfaces;

public interface ISecureShellService
{
    Task<string> GetWireGuardStatistics(CancellationToken cancellationToken = default);
}