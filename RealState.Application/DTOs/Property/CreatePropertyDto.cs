using RealState.Application.Features.Properties.Create.Commands;
using entities = RealState.Domain.Entities;

namespace RealState.Application.DTOs.Property;

public class CreatePropertyDto
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? InternalCode { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public bool Sold { get; set; }
    public Guid OwnerId { get; set; }

    public CreatePropertyCommand ToCommand()
    {
        return new CreatePropertyCommand
        {
            Address = this.Address,
            OwnerId = (this.OwnerId),
            Name = this.Name,
            InternalCode = this.InternalCode,
            Price = this.Price,
            Year = this.Year,
            Sold = (this.Sold)
        };
    }
    public entities.Property ToEntity()
    {
        return new entities.Property
        {
            Name = this.Name,
            Address = this.Address,
            InternalCode = this.InternalCode,
            OwnerId = this.OwnerId,
            Price = this.Price,
            Sold = this.Sold,
            Year = this.Year
        };
    }
}