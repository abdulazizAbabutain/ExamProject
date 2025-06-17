using API.ApiDoc.Tags.Requests;
using Application.Lookups.Commands.Tags.AddTag;
using Application.Lookups.Commands.Tags.DeleteTag;
using Application.Lookups.Commands.Tags.UpdateTag;
using Application.Lookups.Queries.Tags.GetAllTags;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace API.Controllers.V1
{
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = nameof(AddTag))]
        [SwaggerRequestExample(typeof(AddTagCommand), typeof(AddTagCommandRequestExample))]
        public async Task<IActionResult> AddTag([FromBody] AddTagCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpGet(Name = nameof(GetAllTags))]
        public async Task<IActionResult> GetAllTags([FromQuery] GetAllTagsQuery query)
        {
            return Ok(await _mediator.Send(query));

        }

        [HttpPut(Name = nameof(UpdateTag))]
        public async Task<IActionResult> UpdateTag([FromBody] UpdateTagCommand command)
        {
            await _mediator.Send(command);
            return NoContent();

        }

        [HttpDelete("{id:guid}", Name = nameof(DeleteTag))]
        public async Task<IActionResult> DeleteTag([FromRoute] DeleteTagCommand command)
        {
            await _mediator.Send(command);
            return NoContent();

        }
    }
}
