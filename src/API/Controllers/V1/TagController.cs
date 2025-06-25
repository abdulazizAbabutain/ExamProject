using API.ApiDoc.Tags.Requests;
using Application.Lookups.Queries.Tags.GetAllTags;
using Application.Tags.Commands.AddTag;
using Application.Tags.Commands.ArchiveTag;
using Application.Tags.Commands.DeleteTag;
using Application.Tags.Commands.UnArchiveTag;
using Application.Tags.Commands.UpdateTag;
using Application.Tags.Queries.GetTagTimeline;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers.V1
{
    [Route("api/tag")]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TagController> _logger;

        public TagController(IMediator mediator, ILogger<TagController> logger)
        {
            _mediator = mediator;
            _logger = logger;
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
            _logger
                .LogInformation("test log");
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

        [HttpPost("{id:guid}/archive", Name = nameof(ArchiveTag))]
        [EndpointName(nameof(ArchiveTag))]
        [EndpointSummary("Tag Archive")]
        [EndpointDescription("Description")]
        public async Task<IActionResult> ArchiveTag([FromRoute] ArchiveTagCommand command)
        {
            await _mediator.Send(command);
            return NoContent();

        }


        [HttpPost("{id:guid}/unarchive", Name = nameof(UnarchiveTag))]
        [EndpointName(nameof(UnarchiveTag))]
        [EndpointSummary("Tag Archive")]
        [EndpointDescription("Description")]
        public async Task<IActionResult> UnarchiveTag([FromRoute] UnarchiveTagCommand command)
        {
            await _mediator.Send(command);
            return NoContent();

        }
        [HttpGet("{id:guid}/timeline", Name = nameof(GetTagTimeline))]
        [EndpointName(nameof(GetTagTimeline))]
        [EndpointSummary("Tag Timeline")]
        [EndpointDescription("get the timeline for tag")]
        public async Task<IActionResult> GetTagTimeline([FromRoute] Guid id, [FromQuery] GetTagTimelineQuery query)
        {
            query.Id = id;
            return Ok(await _mediator.Send(query));
        }
    }
}
