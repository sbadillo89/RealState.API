using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealState.Api.Controllers;
using RealState.Application.DTOs.Property;
using RealState.Application.Features;
using RealState.Application.Features.Properties;
using RealState.Application.Features.Properties.AddImage.Commands;
using RealState.Application.Features.Properties.ChangePrice.Commands;
using RealState.Infrastructure.Persistence;

[TestFixture]
public class PropertiesControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<RealStateContext> _contextMock;
    private PropertiesController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _contextMock = new Mock<RealStateContext>();
        _controller = new PropertiesController(_mediatorMock.Object, _contextMock.Object);
    }

    [Test]
    public async Task Post_ShouldReturnStatusCodeFromMediatorResponse()
    {
        var propertyDto = new CreatePropertyDto();
        var commandResponse = new BaseCommandResponse<PropertyCommandResponse> { StatusCode = System.Net.HttpStatusCode.Created };

        _mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<BaseCommandResponse<PropertyCommandResponse>>>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(commandResponse);

        var result = await _controller.Post(propertyDto);
        var statusCodeResult = result.Result as ObjectResult;

        Assert.NotNull(statusCodeResult);
        Assert.AreEqual(201, statusCodeResult.StatusCode);
    }

    [Test]
    public async Task Post_Filter_ShouldReturnStatusCodeFromMediatorResponse()
    {
        var filterDto = new PropertyFilterDto();
        var commandResponse = new BaseCommandResponse<List<PropertyCommandResponse>> { StatusCode = System.Net.HttpStatusCode.OK };

        _mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<BaseCommandResponse<List<PropertyCommandResponse>>>>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(commandResponse);

        var result = await _controller.Post(filterDto);
        var statusCodeResult = result.Result as ObjectResult;

        Assert.NotNull(statusCodeResult);
        Assert.AreEqual(200, statusCodeResult.StatusCode);
    }

    [Test]
    public async Task Put_ShouldReturnStatusCodeFromMediatorResponse()
    {
        var updateDto = new UpdatePropertyDto();
        var commandResponse = new BaseCommandResponse<PropertyCommandResponse> { StatusCode = System.Net.HttpStatusCode.OK };

        _mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<BaseCommandResponse<PropertyCommandResponse>>>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(commandResponse);

        var result = await _controller.Put(Guid.NewGuid(), updateDto);
        var statusCodeResult = result as ObjectResult;

        Assert.NotNull(statusCodeResult);
        Assert.AreEqual(200, statusCodeResult.StatusCode);
    }

    [Test]
    public async Task Put_ChangePrice_ShouldReturnStatusCodeFromMediatorResponse()
    {
        var priceDto = new ChangePricePropertyDto { Price = 1000 };
        var commandResponse = new BaseCommandResponse<PropertyCommandResponse> { StatusCode = System.Net.HttpStatusCode.OK };

        _mediatorMock.Setup(m => m.Send(It.IsAny<ChangePriceCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(commandResponse);

        var result = await _controller.Put(Guid.NewGuid(), priceDto);
        var statusCodeResult = result.Result as ObjectResult;

        Assert.NotNull(statusCodeResult);
        Assert.AreEqual(200, statusCodeResult.StatusCode);
    }
}
