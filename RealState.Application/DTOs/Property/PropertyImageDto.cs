using RealState.Domain.Entities;

namespace RealState.Application.DTOs.Property;

public class PropertyImageDto
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public byte[] File { get; set; } = null!;
    public bool Enabled { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public static PropertyImage ToEntity(PropertyImageDto dto)
    {
        return new PropertyImage
        {
            Id = dto.Id,
            PropertyId = dto.PropertyId,
            File = dto.File,
            Enabled = dto.Enabled,
            CreatedDate = dto.CreatedDate
        };
    }
}