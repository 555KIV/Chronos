namespace Chronos.Models.Entities;

public class SpentTrafficEntity
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public decimal TrafficVolume => ReceivedVolume + SentVolume;
    public decimal ReceivedVolume { get; set; }
    public decimal SentVolume { get; set; }
    public DateTime Date {get;set;}
    public DateTime CreatedDate { get; set; }
    
    public ClientEntity? Client { get; set; }
}