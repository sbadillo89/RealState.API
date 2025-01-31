
using MediatR;

namespace RealState.Application.Features.Properties.Update.Commands;

public class UpdatePropertyCommand : IRequest<BaseCommandResponse<PropertyCommandResponse>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = null!;
    public string? Address { get; set; } = null!;
    public string? InternalCode { get; set; } = null!;
    public decimal Price { get; set; }
    public int Year { get; set; }
    public bool? Sold { get; set; }
    public Guid? OwnerId { get; set; }
}
