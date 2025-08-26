using Chronos.Models.Options;
using Chronos.Services.Interfaces;
using Microsoft.Extensions.Options;
using Renci.SshNet;

namespace Chronos.Services.Implementation;

public class SecureShellService : ISecureShellService
{
    private readonly SecureShellOptions _options;

    public SecureShellService(IOptions<SecureShellOptions> options)
    {
        _options = options.Value;
    }

    public async Task<string> GetWireGuardStatistics(CancellationToken cancellationToken = default) => await RunCommandAsync("wg show all", cancellationToken);
    
    public async Task<Dictionary<string, string>> GetClients(CancellationToken cancellationToken = default)
    {
        var clientDictionary = new Dictionary<string, string>();
        
        using var client = new SshClient(_options.IpAddress, _options.Port, _options.Username, _options.Password);
        
        await client.ConnectAsync(cancellationToken);
        
        var configFiles = ExecuteSshCommand(client,"find -name \"*.conf\" -type f");

        foreach (var file in configFiles.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            clientDictionary[file] = ExecuteSshCommand(client, $"cat {file}");
        };
        
        client.Disconnect();
        
        return clientDictionary;
    }

    private async Task<string> RunCommandAsync(string command, CancellationToken cancellationToken = default)
    {
        using var client = new SshClient(_options.IpAddress, _options.Port, _options.Username, _options.Password);
        
        await client.ConnectAsync(cancellationToken);

        using var cmd = client.RunCommand(command);

        client.Disconnect();
        
        return cmd.Result;
    }
    
    private string ExecuteSshCommand(SshClient client, string command)
    {
        using var cmd = client.CreateCommand(command);
        
        return cmd.Execute();
    }
}