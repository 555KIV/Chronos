using System.ComponentModel.DataAnnotations.Schema;

namespace Chronos.Models.Entities;

[Table("clients")]
public class ClientEntity
{
    public int Id { get; set; }
    public string ClientName { get; set; }
    public string IpAddress { get; set; }
    public int FamilyId { get; set; }
    public DateTime CreatedDate { get; set; }
    
    
    public FamilyEntity? Family { get; set; }
}