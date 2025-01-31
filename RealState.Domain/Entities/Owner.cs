using System.ComponentModel.DataAnnotations.Schema;

namespace RealState.Domain.Entities;

[Table("Owner")]
public class Owner
{
    [Column("OwnerId")]
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public DateTime Birthday { get; private set; }
    public string? Photo { get; set; }
    public bool Active { get; private set; } = true;
    public DateTime CreatedDate { get; set; }
}