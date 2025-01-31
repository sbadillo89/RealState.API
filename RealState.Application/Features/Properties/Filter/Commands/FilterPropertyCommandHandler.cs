using MediatR;
using RealState.Application.DTOs.Property;

namespace RealState.Application.Features.Properties.Filter.Commands;

public class FilterPropertyCommandHandler : IRequestHandler<FilterPropertyCommand, BaseCommandResponse<List<PropertyCommandResponse>>>
{
    private readonly IRealStateService _realStateService;
    public FilterPropertyCommandHandler(IRealStateService realStateService)
    {
        _realStateService = realStateService;
    }
    public async Task<BaseCommandResponse<List<PropertyCommandResponse>>> Handle(FilterPropertyCommand request, CancellationToken cancellationToken)
    {

        BaseCommandResponse<List<PropertyCommandResponse>> result;

        var property = new PropertyFilterDto
        {
            Name = request.Name,
            OwnerId = request.OwnerId.Value,
            Address = request.Address,
            Price = request.Price,
            Year = request.Year,
            Sold = request.Sold ?? null,
            InternalCode = request.InternalCode
        };

        var response = await _realStateService.GetFilteredProperties(property, cancellationToken);

        result = new BaseCommandResponse<List<PropertyCommandResponse>>
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Success = true,
            Data = response.Select(x => new PropertyCommandResponse
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                Price = x.Price,
                InternalCode = x.InternalCode,
                OwnerId = x.OwnerId,
                Sold = x.Sold,
                Year = x.Year,
                CreatedDate = x.CreatedDate,
                LastUpdated = x.LastUpdated
            }).ToList()
        };

        return result;
    }
}
