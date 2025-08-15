namespace Chronos.Models.Entities;

public class SpentTrafficEntity
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public decimal TrafficVolume { get; set; }
    public DateTime Date {get;set;}
    public DateTime CreatedDate { get; set; }
    
    public ClientEntity? Client { get; set; }
}