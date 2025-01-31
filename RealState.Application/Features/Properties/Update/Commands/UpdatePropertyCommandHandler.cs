using MediatR;
using RealState.Application.DTOs.Property;

namespace RealState.Application.Features.Properties.Update.Commands;

public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, BaseCommandResponse<PropertyCommandResponse>>
{
    private readonly IRealStateService _realStateService;
    public UpdatePropertyCommandHandler(IRealStateService realStateService)
    {
        _realStateService = realStateService;
    }
    public async Task<BaseCommandResponse<PropertyCommandResponse>> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
        BaseCommandResponse<PropertyCommandResponse> result;

        var property = new UpdatePropertyDto
        {
            Name = request.Name,
            OwnerId = request.OwnerId,
            Address = request.Address!,
            Price = request.Price,
            Year = request.Year,
            Sold = request.Sold,
            InternalCode = request.InternalCode
        };

        try
        {
            var response = await _realStateService.UpdatePropertyAsync(request.Id, property, cancellationToken);

            result = new BaseCommandResponse<PropertyCommandResponse>
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Success = true
            };
        }
        catch (Exception ex)
        {
            result = new BaseCommandResponse<PropertyCommandResponse>
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Success = false,
                Errors = [ex.Message]
            };
        }

        return result;
    }
}

/*
 * 
{
  "name": "",
  "address": "",
  "internalCode": "",
  "price": 99999,
  "year": 2024,
  "sold": true
}

 */