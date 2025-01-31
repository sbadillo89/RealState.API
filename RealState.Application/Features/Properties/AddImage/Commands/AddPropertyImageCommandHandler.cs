using MediatR;
using RealState.Application.DTOs.Property;
using RealState.Application.Validators;

namespace RealState.Application.Features.Properties.AddImage.Commands;

public class AddPropertyImageCommandHandler : IRequestHandler<AddPropertyImageCommand, BaseCommandResponse<AddPropertyImageCommandResponse>>
{
    private readonly IRealStateService _realStateService;

    public AddPropertyImageCommandHandler(IRealStateService realStateService)
    {
        _realStateService = realStateService;
    }
    public async Task<BaseCommandResponse<AddPropertyImageCommandResponse>> Handle(AddPropertyImageCommand request, CancellationToken cancellationToken)
    {
        var result = new BaseCommandResponse<AddPropertyImageCommandResponse>();
        string[] errors;

        try
        {
            var validator = new AddPropertyImageValidator();
            var validateErrors = validator.Validate(request);

            if (!validateErrors.IsValid)
            {
                errors = validateErrors.Errors.Select(e => e.ErrorMessage).ToArray();

                result.Errors = errors;
                result.StatusCode = System.Net.HttpStatusCode.BadRequest;

                return result;
            }

            var response = await _realStateService.AddPropertyImageAsync(request.Id, request.Image!, cancellationToken);

            result = new BaseCommandResponse<AddPropertyImageCommandResponse>
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Success = true,
                Data = new AddPropertyImageCommandResponse
                {
                    Id = response.Id,
                    Enabled = response.Enabled,
                    File = response.File,
                    PropertyId = response.PropertyId,
                    CreatedDate = response.CreatedDate,
                }
            };

        }
        catch (Exception ex)
        {
            result = new BaseCommandResponse<AddPropertyImageCommandResponse>
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Success = false,
                Errors = [ex.Message]
            };
        }

        return result;
    }
}
