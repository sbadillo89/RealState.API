using RealState.Domain.Entities;
using System.Linq.Expressions;

namespace RealState.Application.Contracts.Repositories;

public interface IRealStateRepository
{
    Task<List<Property>> GetPropertiesByFilterAsync(Expression<Func<Property, bool>>? filter = null);
    Task<Property> GetPropertyById(Guid idProperty, CancellationToken cancellationToken);
    Task<Property> CreatePropertyAsync(Property property);
    Task<PropertyImage> AddPropertyImageAsync(PropertyImage propertyImage);
    Task<Property> UpdatePropertyAsync(Property property);

    #region "Owner"
    Task<Owner> GetOwnerById(Guid idOwner, CancellationToken cancellationToken);
    #endregion "Owner"

    #region "Trace"
    Task<PropertyTrace> GetPropertyTraceById(Guid id);
    Task<PropertyTrace> CreatePropertyTraceAsync(PropertyTrace trace);
    #endregion "Trace"


    #region "Property Image"
    Task<PropertyImage> GetByIdProperty(Guid idProperty, CancellationToken cancellationToken);
    Task<PropertyImage> AddImage(PropertyImage propertyImage, CancellationToken cancellationToken);
    #endregion "Property Image"
}
