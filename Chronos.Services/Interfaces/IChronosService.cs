namespace Chronos.Services.Interfaces;

public interface IChronosService
{
    Task SaveWgStatisticAsync();
    Task CreateClientsAsync();
}