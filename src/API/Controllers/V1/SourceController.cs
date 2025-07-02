using API.Interfaces;
using Application.Commons.Models.Pageination;
using Application.EntitlesTimeline.Queries.EntityTimeline;
using Application.EntitlesTimeline.Queries.EntityTimelineDetails;
using Application.Sources.Commands.AddBulkTagsToSource;
using Application.Sources.Commands.AddSource.Requests;
using Application.Sources.Commands.AddTagToSource;
using Application.Sources.Commands.RemoveBulkTagFromSource;
using Application.Sources.Commands.RemoveTagFromSource;
using Application.Sources.Queries.GetAllSources;
using Application.Sources.Queries.GetSourceById;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1;
///
/// <summary>
/// 
/// </summary>
/// <param name="mediator"></param>
[Route("api/source")]
public class SourceController(IMediator mediator, IHttpResultResponder resultResponder) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IHttpResultResponder _resultResponder = resultResponder;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost(Name = nameof(AddSource))]
    public async Task<IActionResult> AddSource([FromBody] AddSourceCommand command)
        => _resultResponder.FromResult(HttpContext, await _mediator.Send(command));
    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("{id:guid}/reference", Name = nameof(AddReference))]
    public async Task<IActionResult> AddReference([FromBody] AddSourceCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet(Name = nameof(GetAllSources))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<GetAllSourceQueryResult>))]
    public async Task<IActionResult> GetAllSources([FromQuery] GetAllSourceQuery query)
    {
        return Ok(await _mediator.Send(query));
    }


    [HttpPost("{sourceId:guid}/tag/{tagId:guid}")]
    public async Task<IActionResult> AddNewTag([FromRoute] Guid sourceId, [FromRoute] Guid tagId)
    {
        return _resultResponder.FromResult(HttpContext, await _mediator.Send(new AddTagsToSourceCommand { SourceId = sourceId, TagId = tagId}));
    }

    [HttpPost("{sourceId:guid}/tag/bulk")]
    public async Task<IActionResult> AddBulkNewTags([FromRoute] Guid sourceId, [FromBody] AddBulkTagsToSourceCommand command)
    {
        command.SourceId = sourceId;
        return _resultResponder.FromResult(HttpContext, await _mediator.Send(command));
    }


    [HttpDelete("{sourceId:guid}/tag/{tagId:guid}")]
    public async Task<IActionResult> RemoveTag([FromRoute] RemoveTagFromSourceCommand command)
    {
        return _resultResponder.FromResult(HttpContext, await _mediator.Send(command));
    }



    [HttpDelete("{sourceId:guid}/tag/bulk")]
    public async Task<IActionResult> RemoveTag([FromRoute] Guid sourceId, [FromQuery] RemoveBulkTagFromSourceCommand command)
    {
        command.SourceId = sourceId;
        return _resultResponder.FromResult(HttpContext, await _mediator.Send(command));
    }




    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}", Name = nameof(GetSourceById))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<GetSourceByIdQueryResult>))]
    public async Task<IActionResult> GetSourceById([FromRoute] Guid id)
    {

        return Ok(await _mediator.Send(new GetSourceByIdQuery { Id = id }));
    }

    [HttpGet("{id:guid}/timeline", Name = nameof(GetSourceTimeline))]
    [EndpointName(nameof(GetSourceTimeline))]
    [EndpointSummary("Tag Timeline")]
    [EndpointDescription("get the timeline for tag")]
    public async Task<IActionResult> GetSourceTimeline([FromRoute] Guid id, [FromQuery] GetEntityTimelineQuery query)
    {
        query.Id = id;
        query.EntityName = EntitiesName.Source;
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("{id:guid}/timeline/{timelineId:guid}", Name = nameof(GetSourceTimelineDetails))]
    [EndpointName(nameof(GetSourceTimelineDetails))]
    [EndpointSummary("Tag Timeline details")]
    [EndpointDescription("get the timeline for tag")]
    public async Task<IActionResult> GetSourceTimelineDetails([FromRoute] Guid id, [FromRoute] Guid timelineId, [FromQuery] EntityTimelineDetailsQuery query)
    {
        query.Id = timelineId;
        query.EntityName = EntitiesName.Source;
        query.EntityId = id;
        return Ok(await _mediator.Send(query));
    }
}
