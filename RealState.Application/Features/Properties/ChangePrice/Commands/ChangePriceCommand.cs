using MediatR;

namespace RealState.Application.Features.Properties.ChangePrice.Commands;

public class ChangePriceCommand : IRequest<BaseCommandResponse<PropertyCommandResponse>>
{
    public Guid Id { get; set; }

    public decimal Price { get; set; }
}
