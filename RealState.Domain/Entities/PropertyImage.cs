using System.ComponentModel.DataAnnotations.Schema;

namespace RealState.Domain.Entities;

[Table("PropertyImage")]
public class PropertyImage
{
    [Column("IdPropertyImage")]
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public virtual Property Property { get; set; } = null!;
    public byte[] File { get; set; } = null!;
    public bool Enabled { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}