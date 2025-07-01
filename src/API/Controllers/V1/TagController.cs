using API.ApiDoc.Tags.Requests;
using API.Interfaces;
using Application.EntitlesTimeline.Queries.EntityTimeline;
using Application.EntitlesTimeline.Queries.EntityTimelineDetails;
using Application.Lookups.Queries.Tags.GetAllTags;
using Application.Tags.Commands.AddTag;
using Application.Tags.Commands.ArchiveAllTags;
using Application.Tags.Commands.ArchiveTag;
using Application.Tags.Commands.DeleteTag;
using Application.Tags.Commands.UnArchiveTag;
using Application.Tags.Commands.UpdateTag;
using Application.Tags.Queries.GetTagDetails;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace API.Controllers.V1;

[Route("api/tag")]
public class TagController(IMediator mediator, IHttpResultResponder resultResponder) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IHttpResultResponder _resultResponder = resultResponder;

    [HttpPost(Name = nameof(AddTag))]
    [SwaggerRequestExample(typeof(AddTagCommand), typeof(AddTagCommandRequestExample))]
    public async Task<IActionResult> AddTag([FromBody] AddTagCommand command)
    {
        var result = await _mediator.Send(command);
        return _resultResponder.FromResult(HttpContext, result);
    }


    [HttpGet(Name = nameof(GetAllTags))]
    public async Task<IActionResult> GetAllTags([FromQuery] GetAllTagsQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("{id:guid}", Name = nameof(TagDetails))]
    [EndpointName(nameof(TagDetails))]
    [EndpointSummary("Tag Details")]
    [EndpointDescription("Tag Details")]
    public async Task<IActionResult> TagDetails([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new GetTagDetailsQuery() { Id = id }));
    }

    [HttpPut(Name = nameof(UpdateTag))]
    public async Task<IActionResult> UpdateTag([FromBody] UpdateTagCommand command)
    {
        var result = await _mediator.Send(command);
        return _resultResponder.FromResult(HttpContext, result);

    }

    [HttpDelete("{id:guid}", Name = nameof(DeleteTag))]
    public async Task<IActionResult> DeleteTag([FromRoute] DeleteTagCommand command)
    {
        return _resultResponder.FromResult(HttpContext, await _mediator.Send(command));

    }

    [HttpPost("archive", Name = nameof(ArchiveAllTag))]
    [EndpointName(nameof(ArchiveAllTag))]
    [EndpointSummary("Archive all tags")]
    [EndpointDescription("Archive all tags")]
    [Obsolete("will be removed")]
    public async Task<IActionResult> ArchiveAllTag([FromRoute] ArchiveAllTagsCommand command)
    {
        var result = await _mediator.Send(command);
        return _resultResponder.FromResult(HttpContext, result);

    }

    [HttpPost("{id:guid}/archive", Name = nameof(ArchiveTag))]
    [EndpointName(nameof(ArchiveTag))]
    [EndpointSummary("Tag Archive")]
    [EndpointDescription("Description")]
    public async Task ArchiveTag([FromRoute] ArchiveTagCommand command)
        => _resultResponder.FromResult(HttpContext, await _mediator.Send(command));

    [HttpPost("{id:guid}/unarchive", Name = nameof(UnarchiveTag))]
    [EndpointName(nameof(UnarchiveTag))]
    [EndpointSummary("Tag Unarchive")]
    [EndpointDescription("Description")]
    public async Task<IActionResult> UnarchiveTag([FromRoute] UnarchiveTagCommand command)
        => _resultResponder.FromResult(HttpContext, await _mediator.Send(command));
    
    [HttpGet("{id:guid}/timeline", Name = nameof(GetTagTimeline))]
    [EndpointName(nameof(GetTagTimeline))]
    [EndpointSummary("Tag Timeline")]
    [EndpointDescription("get the timeline for tag")]
    public async Task<IActionResult> GetTagTimeline([FromRoute] Guid id, [FromQuery] GetEntityTimelineQuery query)
    {
        query.Id = id;
        query.EntityName = EntitiesName.Tag;
        return Ok(await _mediator.Send(query));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timelineId"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{tagId:guid}/timeline/{timelineId:guid}", Name = nameof(GetTagTimelineDetails))]
    [EndpointName(nameof(GetTagTimelineDetails))]
    [EndpointSummary("Tag Timeline details")]
    [EndpointDescription("get the timeline for tag")]
    public async Task<IActionResult> GetTagTimelineDetails([FromRoute(Name = "TagId")] Guid tagId, [FromRoute(Name = "TimelineId")] Guid timelineId)
    {
        var query = new EntityTimelineDetailsQuery()
        {
            Id = timelineId,
            EntityName = EntitiesName.Tag,
            EntityId = tagId
        };
        return Ok(await _mediator.Send(query));
    }
}
