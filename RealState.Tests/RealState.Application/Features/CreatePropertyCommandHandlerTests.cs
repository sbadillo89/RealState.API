using Moq;
using RealState.Application.DTOs.Property;
using RealState.Application.Features.Properties.Create.Commands;
using System.Net;

public class CreatePropertyCommandHandlerTests
{
    private Mock<IRealStateService> _realStateServiceMock;
    private CreatePropertyCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _realStateServiceMock = new Mock<IRealStateService>();

        _handler = new CreatePropertyCommandHandler(_realStateServiceMock.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnBadRequest_WhenValidationFails()
    {
        var command = new CreatePropertyCommand
        {
            Name = "",
            Address = "Some Address",
            OwnerId = Guid.NewGuid(),
            Price = 100000,
            Year = 2020,
            Sold = false
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(result.Errors, Is.Not.Empty);
    }

    [Test]
    public async Task Handle_ShouldReturnCreated_WhenPropertyIsCreatedSuccessfully()
    {
        var command = new CreatePropertyCommand
        {
            Name = "Wall Street",
            Address = "street 123",
            OwnerId = Guid.NewGuid(),
            Price = 200000,
            Year = 2021,
            Sold = false,
            InternalCode = "code"
        };

        var fakeResponse = new PropertyDto
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Address = command.Address,
            OwnerId = command.OwnerId.Value,
            Price = command.Price,
            Year = command.Year,
            Sold = command.Sold.Value,
            InternalCode = "ABC123",
            CreatedDate = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow
        };

        _realStateServiceMock
            .Setup(x => x.CreatePropertyAsync(It.IsAny<CreatePropertyDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(fakeResponse);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(result.Success, Is.True);
        Assert.That(result.Data, Is.Not.Null);
        Assert.That(result.Data.Name, Is.EqualTo(command.Name));
    }

    [Test]
    public async Task Handle_ShouldReturnBadRequest_WhenServiceThrowsException()
    {
        var command = new CreatePropertyCommand
        {
            Name = "White house",
            Address = "avenue 123",
            OwnerId = Guid.NewGuid(),
            Price = 150000,
            Year = 2019,
            Sold = false,
            InternalCode = "my-code"
        };

        _realStateServiceMock
            .Setup(x => x.CreatePropertyAsync(It.IsAny<CreatePropertyDto>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Error"));

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(result.Success, Is.False);
        Assert.That(result.Errors, Contains.Item("Error"));
    }
}
