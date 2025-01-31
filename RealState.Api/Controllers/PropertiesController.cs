using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealState.Application.DTOs.Property;
using RealState.Application.Features;
using RealState.Application.Features.Properties;
using RealState.Application.Features.Properties.AddImage.Commands;
using RealState.Application.Features.Properties.ChangePrice.Commands;
using RealState.Infrastructure.Persistence;

namespace RealState.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RealStateContext realStateContext;

        public PropertiesController(IMediator mediator, RealStateContext context)
        {
            _mediator = mediator;
            realStateContext = context;
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse<PropertyCommandResponse>>> Post([FromBody] CreatePropertyDto propertyDto)
        {
            var command = propertyDto.ToCommand();

            var response = await _mediator.Send(command, CancellationToken.None);

            return StatusCode((int)response.StatusCode, response);

        }

        [HttpPost("filter")]
        public async Task<ActionResult<BaseCommandResponse<List<PropertyCommandResponse>>>> Post([FromBody] PropertyFilterDto filter)
        {
            var command = filter.ToCommand();

            var response = await _mediator.Send(command, CancellationToken.None);

            return StatusCode((int)response.StatusCode, response);

        }

        [HttpPost("{id}/add-image")]
        public async Task<ActionResult<BaseCommandResponse<PropertyImageDto>>> Post(Guid id, [FromBody] AddPropertyImageDto propertyImageDto)
        {
            var command = new AddPropertyImageCommand
            {
                Id = id,
                Image = propertyImageDto.Base64Image
            };

            var response = await _mediator.Send(command, CancellationToken.None);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UpdatePropertyDto property)
        {
            var command = property.ToCommand();
            command.Id = id;

            var response = await _mediator.Send(command, CancellationToken.None);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("{id}/change-price")]
        public async Task<ActionResult<BaseCommandResponse<PropertyCommandResponse>>> Put(Guid id, [FromBody] ChangePricePropertyDto pricePropertyDto)
        {
            var command = new ChangePriceCommand
            {
                Price = pricePropertyDto.Price,
                Id = id,
            };

            var respone = await _mediator.Send(command, CancellationToken.None);

            return StatusCode((int)respone.StatusCode, respone);
        }

    }
}
