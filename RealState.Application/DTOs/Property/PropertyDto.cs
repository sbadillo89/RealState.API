using entities = RealState.Domain.Entities;

namespace RealState.Application.DTOs.Property;

public class PropertyDto
{
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

    public entities.Property ToEntity()
    {
        return new entities.Property
        {
            Id = this.Id,
            Name = this.Name,
            Address = this.Address,
            InternalCode = this.InternalCode,
            CreatedDate = this.CreatedDate,
            LastUpdated = this.LastUpdated,
            OwnerId = this.OwnerId,
            Price = this.Price,
            Sold = this.Sold,
            Year = this.Year
        };
    }
}
