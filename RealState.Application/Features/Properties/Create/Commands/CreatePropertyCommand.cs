using MediatR;

namespace RealState.Application.Features.Properties.Create.Commands;

public class CreatePropertyCommand : IRequest<BaseCommandResponse<PropertyCommandResponse>>
{
    public string? Name { get; set; } = null!;
    public string? Address { get; set; } = null!;
    public string? InternalCode { get; set; } = null!;
    public decimal Price { get; set; }
    public int Year { get; set; }
    public bool? Sold { get; set; }
    public Guid? OwnerId { get; set; }
}
