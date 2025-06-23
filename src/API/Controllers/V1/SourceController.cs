using Application.Sources.Commands.AddSource.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [Route("api/[controller]")]
    public class SourceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SourceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = nameof(AddSource))]
        public async Task<IActionResult> AddSource([FromBody] AddSourceCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
