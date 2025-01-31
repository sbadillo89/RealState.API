using System.ComponentModel.DataAnnotations.Schema;

namespace RealState.Domain.Entities;

[Table("Property")]
public class Property
{
    [Column("PropertyId")]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string InternalCode { get; set; } = null!;
    public decimal Price { get; set; }
    public int Year { get; set; }
    public bool Sold { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public Guid OwnerId { get; set; }
    public virtual Owner Owner { get; set; } = null!;

}
