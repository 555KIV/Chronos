using System.ComponentModel.DataAnnotations.Schema;

namespace Chronos.Models.Entities;

[Table("families")]
public class FamilyEntity
{
    public int Id { get; set; }
    public string FamilyName { get; set; }
    public DateTime CreatedDate { get; set; }
    
    public IEnumerable<ClientEntity> Clients { get; set; }
}