using RealState.Application.Contracts.Repositories;
using RealState.Domain.Entities;
using RealState.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace RealState.Infrastructure.Repositories;

public class RealStateRepository : IRealStateRepository
{
    private readonly RealStateContext _context;
    public RealStateRepository(RealStateContext context)
    {
        _context = context;
    }

    public async Task<Property> CreatePropertyAsync(Property property)
    {
        var insertedValue = await _context.Properties.AddAsync(property);

        return insertedValue.Entity;
    }

    public async Task<PropertyImage> AddPropertyImageAsync(PropertyImage propertyImage)
    {
        var insertedValue = await _context.PropertyImages.AddAsync(propertyImage);

        return insertedValue.Entity;
    }

    public async Task<Property> GetPropertyById(Guid idProperty, CancellationToken cancellationToken)
    {
        try
        {
            var property = _context.Properties.FirstOrDefault(c => c.Id == idProperty);

            return property;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<List<Property>> GetPropertiesByFilterAsync(Expression<Func<Property, bool>>? filter = null)
    {
        IQueryable<Property> query = _context.Set<Property>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return query.ToList();
    }

    public async Task<Property> UpdatePropertyAsync(Property property)
    {
        var updatedValue = _context.Properties.Update(property);

        return updatedValue.Entity;
    }

    public async Task<Owner> GetOwnerById(Guid idOwner, CancellationToken cancellationToken)
    {
        return _context.Owners.FirstOrDefault(x => x.Id == idOwner);
    }
    
    public async Task<PropertyTrace> GetPropertyTraceById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<PropertyTrace> CreatePropertyTraceAsync(PropertyTrace trace)
    {
        var insertedValue = _context.PropertyTraces.Add(trace);

        return insertedValue.Entity;
    }

    public async Task<PropertyImage> GetByIdProperty(Guid idProperty, CancellationToken cancellationToken)
    {
        return _context.PropertyImages
                        .Where(x => x.PropertyId == idProperty)
                        .OrderByDescending(x => x.CreatedDate)
                        .FirstOrDefault();
    }

    public async Task<PropertyImage> AddImage(PropertyImage propertyImage, CancellationToken cancellationToken)
    {
        var insertedValue = _context.PropertyImages.Add(propertyImage);

        return insertedValue.Entity;
    }
}