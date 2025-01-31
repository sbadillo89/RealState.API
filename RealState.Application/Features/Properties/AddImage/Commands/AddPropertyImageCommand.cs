using MediatR;

namespace RealState.Application.Features.Properties.AddImage.Commands;

public class AddPropertyImageCommand : IRequest<BaseCommandResponse<AddPropertyImageCommandResponse>>
{
    public Guid Id { get; set; }
    public string? Image { get; set; }
}
