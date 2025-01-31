using System.ComponentModel.DataAnnotations.Schema;

namespace RealState.Domain.Entities;

[Table("PropertyTrace")]
public class PropertyTrace
{
    [Column("IdPropertyTrace")]
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public virtual Property Property { get; set; } = null!;
    public DateTime DateSale { get; set; }
    public decimal Value { get; set; }
    public decimal Tax { get; set; }
    public string? TransactionType { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
