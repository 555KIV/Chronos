using System.ComponentModel.DataAnnotations.Schema;

namespace Chronos.Models.Entities;

[Table("spent_traffic")]
public class SpentTrafficEntity
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    
    public decimal TrafficVolumeAll => ReceivedVolumeAll + SentVolumeAll;
    public decimal ReceivedVolumeAll { get; set; }
    public decimal SentVolumeAll { get; set; }
    
    public decimal ReceivedVolumePerDay { get; set; }
    public decimal SentVolumePerDay { get; set; }
    public decimal TrafficVolumePerDay => ReceivedVolumePerDay + SentVolumePerDay;
    
    public DateTime Date {get;set;}
    public DateTime CreatedDate { get; set; }
    
    public ClientEntity? Client { get; set; }
}