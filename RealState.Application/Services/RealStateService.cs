
using RealState.Application.Contracts;
using RealState.Application.DTOs.Property;
using RealState.Domain.Entities;
using System.Linq.Expressions;

namespace RealState.Application.Services;

public class RealStateService : IRealStateService
{
    private readonly IUnitOfWork _unitOfWork;

    public RealStateService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PropertyImageDto> AddPropertyImageAsync(Guid idProperty, string base64Image, CancellationToken cancellationToken)
    {
        try
        {
            var propertyDB = _unitOfWork.RealStateRepository
                                   .GetPropertiesByFilterAsync(p => p.Id == idProperty)
                                   .Result.FirstOrDefault();
            if (propertyDB == null)
                throw new Exception("Property does not exists");

            // add image
            var propertyImage = new PropertyImage
            {
                Id = Guid.NewGuid(),
                PropertyId = idProperty,
                Enabled = true,
                File = Convert.FromBase64String(base64Image),
                CreatedDate = DateTime.UtcNow
            };

            var imageInserted = await _unitOfWork.RealStateRepository.AddPropertyImageAsync(propertyImage);

            var success = await _unitOfWork.SaveChangesAsync(cancellationToken);
            var imageDto = new PropertyImageDto
            {
                Id = imageInserted.Id,
                PropertyId = idProperty,
                Enabled = imageInserted.Enabled,
                CreatedDate = imageInserted.CreatedDate,
                File = imageInserted.File
            };
            return Convert.ToBoolean(success) ? imageDto : default;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PropertyDto> ChangePropertyPriceAsync(Guid idProperty, decimal price, CancellationToken cancellationToken)
    {
        var propertyDB = _unitOfWork.RealStateRepository
                                   .GetPropertiesByFilterAsync(p => p.Id == idProperty)
                                   .Result.FirstOrDefault();
        if (propertyDB == null)
            throw new Exception("Property does not exists");

        propertyDB.Price = price;

        var propertyUpdated = await _unitOfWork.RealStateRepository.UpdatePropertyAsync(propertyDB);

        // add trace
        var propertyTrace = new PropertyTrace
        {
            Id = Guid.NewGuid(),
            PropertyId = propertyUpdated.Id,
            TransactionType = Domain.Enums.TransactionTypes.PriceChange.ToString(),
            Value = price,
            CreatedDate = DateTime.UtcNow
        };

        var traceInserted = await _unitOfWork.RealStateRepository.CreatePropertyTraceAsync(propertyTrace);

        var success = await _unitOfWork.SaveChangesAsync(cancellationToken);

        var propertyDto = new PropertyDto
        {
            Id = propertyUpdated.Id,
            Name = propertyUpdated.Name,
            Address = propertyUpdated.Address,
            InternalCode = propertyUpdated.InternalCode,
            OwnerId = propertyUpdated.OwnerId,
            Price = propertyUpdated.Price,
            Sold = propertyUpdated.Sold,
            Year = propertyUpdated.Year,
            CreatedDate = propertyUpdated.CreatedDate,
            LastUpdated = propertyUpdated.LastUpdated,
        };

        return Convert.ToBoolean(success) ? propertyDto : default;
    }

    public async Task<PropertyDto> CreatePropertyAsync(CreatePropertyDto property, CancellationToken cancellationToken)
    {
        try
        {
            var entityProperty = property.ToEntity();

            // Validate if property exists
            var propertyDB = _unitOfWork.RealStateRepository
                                   .GetPropertiesByFilterAsync(p => p.Id == entityProperty.Id || p.InternalCode == entityProperty.InternalCode)
                                   .Result.FirstOrDefault();
            if (propertyDB != null)
                throw new Exception("Property already exists");

            // Validate if owner exits
            var ownerDB = await _unitOfWork.RealStateRepository.GetOwnerById(entityProperty.OwnerId, cancellationToken);
            if (ownerDB == null)
                throw new Exception($"Owner with Id: {entityProperty.OwnerId} does not exist.");

            entityProperty.Id = Guid.NewGuid();
            var propertyInserted = await _unitOfWork.RealStateRepository.CreatePropertyAsync(entityProperty);

            // add trace
            var propertyTrace = new PropertyTrace
            {
                Id = Guid.NewGuid(),
                PropertyId = propertyInserted.Id,
                TransactionType = Domain.Enums.TransactionTypes.Creation.ToString(),
                Value = entityProperty.Price,
                CreatedDate = DateTime.UtcNow
            };

            var traceInserted = await _unitOfWork.RealStateRepository.CreatePropertyTraceAsync(propertyTrace);

            var success = await _unitOfWork.SaveChangesAsync(cancellationToken);

            var propertyDto = new PropertyDto
            {
                Id = propertyInserted.Id,
                Name = propertyInserted.Name,
                Address = propertyInserted.Address,
                InternalCode = propertyInserted.InternalCode,
                OwnerId = propertyInserted.OwnerId,
                Price = propertyInserted.Price,
                Sold = propertyInserted.Sold,
                Year = propertyInserted.Year,
                CreatedDate = propertyInserted.CreatedDate,
                LastUpdated = propertyInserted.LastUpdated,
            };

            return Convert.ToBoolean(success) ? propertyDto : default;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<PropertyDto>> GetFilteredProperties(PropertyFilterDto propertyFilterDto, CancellationToken cancellationToken)
    {
        Expression<Func<Property, bool>> filter = p => true;

        if (!string.IsNullOrEmpty(propertyFilterDto.InternalCode))
            filter = CombineExpressions(filter, p => p.InternalCode.Contains(propertyFilterDto.InternalCode));

        if (!string.IsNullOrEmpty(propertyFilterDto.Address))
            filter = CombineExpressions(filter, p => p.Address.Contains(propertyFilterDto.Address));

        if (propertyFilterDto.Year > 0)
            filter = CombineExpressions(filter, p => p.Year == propertyFilterDto.Year);

        if (propertyFilterDto.Price > 0)
            filter = CombineExpressions(filter, p => p.Price == propertyFilterDto.Price);

        if (propertyFilterDto.Sold.HasValue)
            filter = CombineExpressions(filter, p => p.Sold == propertyFilterDto.Sold);

        if (Guid.TryParse(propertyFilterDto.OwnerId.ToString(), out Guid parsedGuid) && propertyFilterDto.OwnerId != default)
            filter = CombineExpressions(filter, p => p.OwnerId == propertyFilterDto.OwnerId);

        var properties = await _unitOfWork.RealStateRepository.GetPropertiesByFilterAsync(filter);

        return properties.Select(x => new PropertyDto
        {
            Name = x.Name,
            Address = x.Address,
            Id = x.Id,
            InternalCode = x.InternalCode,
            OwnerId = x.OwnerId,
            Price = x.Price,
            Sold = x.Sold,
            Year = x.Year,
            CreatedDate = x.CreatedDate,
            LastUpdated = x.LastUpdated,
        }).ToList();
    }

    public async Task<bool> UpdatePropertyAsync(Guid idProperty, UpdatePropertyDto property, CancellationToken cancellationToken)
    {
        var existingProperty = _unitOfWork.RealStateRepository
                                 .GetPropertiesByFilterAsync(p => p.Id == idProperty)
                                 .Result.FirstOrDefault();

        if (existingProperty == null)
            throw new Exception("Property does not exists");

        existingProperty.Name = !string.IsNullOrWhiteSpace(property.Name) ? property.Name : existingProperty.Name;
        existingProperty.Address = !string.IsNullOrWhiteSpace(property.Address) ? property.Address : existingProperty.Address;
        existingProperty.Price = property.Price > 0 ? property.Price : existingProperty.Price;
        existingProperty.InternalCode = !string.IsNullOrWhiteSpace(property.InternalCode) ? property.InternalCode : existingProperty.InternalCode;
        existingProperty.Year = property.Year > 0 ? property.Year : existingProperty.Year;
        existingProperty.Sold = property.Sold.HasValue ? property.Sold.Value : existingProperty.Sold;

        var propertyUpdated = await _unitOfWork.RealStateRepository.UpdatePropertyAsync(existingProperty);

        // add trace
        var propertyTrace = new PropertyTrace
        {
            Id = Guid.NewGuid(),
            PropertyId = existingProperty.Id,
            TransactionType = Domain.Enums.TransactionTypes.Update.ToString(),
            Value = propertyUpdated.Price,
            CreatedDate = DateTime.UtcNow
        };

        var traceInserted = await _unitOfWork.RealStateRepository.CreatePropertyTraceAsync(propertyTrace);

        var success = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Convert.ToBoolean(success);
    }

    private static Expression<Func<T, bool>> CombineExpressions<T>(
    Expression<Func<T, bool>> first,
    Expression<Func<T, bool>> second)
    {
        var parameter = Expression.Parameter(typeof(T));

        var combined = Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(
                Expression.Invoke(first, parameter),
                Expression.Invoke(second, parameter)
            ), parameter);

        return combined;
    }
}
