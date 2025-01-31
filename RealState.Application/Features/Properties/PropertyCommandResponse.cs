using RealState.Domain.Entities;

namespace RealState.Application.Features.Properties
{
    public class PropertyCommandResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string InternalCode { get; set; } = null!;
        public decimal Price { get; set; }
        public int Year { get; set; }
        public bool Sold { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public Guid OwnerId { get; set; }
    }
}
