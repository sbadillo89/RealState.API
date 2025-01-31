using MediatR;
using RealState.Application.DTOs.Property;
using RealState.Application.Validators;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RealState.Application.Features.Properties.Create.Commands;

public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, BaseCommandResponse<PropertyCommandResponse>>
{
    private readonly IRealStateService _realStateService;
    public CreatePropertyCommandHandler(IRealStateService realStateService)
    {
        _realStateService = realStateService;
    }
    public async Task<BaseCommandResponse<PropertyCommandResponse>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var result = new BaseCommandResponse<PropertyCommandResponse>();
        string[] errors;

        var validator = new CreatePropertyValidator();
        var validateErrors = validator.Validate(request);

        if (!validateErrors.IsValid)
        {
            errors = validateErrors.Errors.Select(e => e.ErrorMessage).ToArray();

            result.Errors = errors;
            result.StatusCode = System.Net.HttpStatusCode.BadRequest;

            return result;
        }

        var property = new CreatePropertyDto
        {
            Name = request.Name,
            OwnerId = request.OwnerId.Value,
            Address = request.Address!,
            Price = request.Price,
            Year = request.Year,
            Sold = request.Sold.Value,
            InternalCode = request.InternalCode
        };

        try
        {
            var response = await _realStateService.CreatePropertyAsync(property, cancellationToken);
            result = new BaseCommandResponse<PropertyCommandResponse>
            {
                StatusCode = System.Net.HttpStatusCode.Created,
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
