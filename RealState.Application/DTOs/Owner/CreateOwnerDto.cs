namespace RealState.Application.DTOs.Owner;

public record CreateOwnerDto
{
    public string Name { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public DateTime Birthday { get; private set; }
    public bool Active { get; private set; } = true;
}