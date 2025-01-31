using MediatR;
using RealState.Application.Validators;

namespace RealState.Application.Features.Properties.ChangePrice.Commands;

public class ChangePriceCommandHandler : IRequestHandler<ChangePriceCommand, BaseCommandResponse<PropertyCommandResponse>>
{
    private readonly IRealStateService _realStateService;

    public ChangePriceCommandHandler(IRealStateService realStateService)
    {
        _realStateService = realStateService;
    }
    public async Task<BaseCommandResponse<PropertyCommandResponse>> Handle(ChangePriceCommand request, CancellationToken cancellationToken)
    {
        var result = new BaseCommandResponse<PropertyCommandResponse>();
        string[] errors;
        try
        {
            var validator = new ChangePricePropertyValidator();
            var validateErrors = validator.Validate(request);

            if (!validateErrors.IsValid)
            {
                errors = validateErrors.Errors.Select(e => e.ErrorMessage).ToArray();

                result.Errors = errors;
                result.StatusCode = System.Net.HttpStatusCode.BadRequest;

                return result;
            }

            var response = await _realStateService.ChangePropertyPriceAsync(request.Id, request.Price, cancellationToken);

            result = new BaseCommandResponse<PropertyCommandResponse>
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Success = true,
                Data = new PropertyCommandResponse
                {
                    Id = response.Id,
                    Name = response.Name,
                    Address = response.Address,
                    Price = response.Price,
                    InternalCode = response.InternalCode,
                    OwnerId = response.OwnerId,
                    Sold = response.Sold,
                    Year = response.Year,
                    CreatedDate = response.CreatedDate,
                    LastUpdated = response.LastUpdated
                }
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
