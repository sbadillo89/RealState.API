using RealState.Application.Features.Properties.Update.Commands;

namespace RealState.Application.DTOs.Property;

public class UpdatePropertyDto
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? InternalCode { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public bool? Sold { get; set; }
    public Guid? OwnerId { get; set; }

    public UpdatePropertyCommand ToCommand()
    {
        return new UpdatePropertyCommand
        {
            Address = this.Address,
            OwnerId = (this.OwnerId.HasValue ? this.OwnerId.Value : null),
            Name = this.Name,
            InternalCode = this.InternalCode,
            Price = this.Price,
            Year = this.Year,
            Sold = (this.Sold ?? null)
        };
    }
}
