using RealState.Application.Features.Properties.Filter.Commands;

namespace RealState.Application.DTOs.Property;

public class PropertyFilterDto
{
    public string? Name { get; set; } = null!;
    public string? Address { get; set; } = null!;
    public string? InternalCode { get; set; } = null!;
    public decimal Price { get; set; }
    public int Year { get; set; }
    public bool? Sold { get; set; }
    public Guid OwnerId { get; set; }

    public FilterPropertyCommand ToCommand()
    {
        return new FilterPropertyCommand
        {
            OwnerId = this.OwnerId,
            Address = this.Address,
            Year = this.Year,
            InternalCode = this.InternalCode,
            Name = this.Name,
            Price = this.Price,
            Sold = this.Sold,
        };
    }
}
