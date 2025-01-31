using RealState.Application.DTOs.Property;


public interface IRealStateService
{
    Task<List<PropertyDto>> GetFilteredProperties(PropertyFilterDto propertyFilterDto, CancellationToken cancellationToken);

    Task<PropertyDto> CreatePropertyAsync(CreatePropertyDto property, CancellationToken cancellationToken);

    Task<PropertyImageDto> AddPropertyImageAsync(Guid idProperty, string base64Image, CancellationToken cancellationToken);

    Task<PropertyDto> ChangePropertyPriceAsync(Guid idProperty, decimal price, CancellationToken cancellationToken);

    Task<bool> UpdatePropertyAsync(Guid idProperty, UpdatePropertyDto property, CancellationToken cancellationToken);
}