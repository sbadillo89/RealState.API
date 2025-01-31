namespace RealState.Application.Features.Properties.AddImage.Commands;

public class AddPropertyImageCommandResponse
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public byte[] File { get; set; } = null!;
    public bool Enabled { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
