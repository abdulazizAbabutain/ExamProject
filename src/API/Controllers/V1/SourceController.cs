using Application.Commons.Models.Pageination;
using Application.Sources.Commands.AddSource.Requests;
using Application.Sources.Queries.GetAllSources;
using Application.Sources.Queries.GetSourceById;
using Application.Sources.Queries.GetSourceById.ResultModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [Route("api/Source")]
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

        [HttpPost("{id:guid}/reference",Name = nameof(AddReference))]
        public async Task<IActionResult> AddReference([FromBody] AddSourceCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet(Name = nameof(GetAllSources))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<GetAllSourceQueryResult>))]
        public async Task<IActionResult> GetAllSources([FromQuery] GetAllSourceQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id:guid}", Name = nameof(GetSourceById))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<GetSourceByIdQueryResult>))]
        public async Task<IActionResult> GetSourceById([FromRoute] Guid id)
        {

            return Ok(await _mediator.Send(new GetSourceByIdQuery { Id = id}));
        }
    }
}
