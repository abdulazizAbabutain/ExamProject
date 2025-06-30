using API.Interfaces;
using Application.Commons.Models.Pageination;
using Application.Sources.Commands.AddSource.Requests;
using Application.Sources.Queries.GetAllSources;
using Application.Sources.Queries.GetSourceById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1;
///
/// <summary>
/// 
/// </summary>
/// <param name="mediator"></param>
[Route("api/Source")]
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
        => _resultResponder.FromResult(HttpContext,await _mediator.Send(command));
    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("{id:guid}/reference",Name = nameof(AddReference))]
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}", Name = nameof(GetSourceById))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponse<GetSourceByIdQueryResult>))]
    public async Task<IActionResult> GetSourceById([FromRoute] Guid id)
    {

        return Ok(await _mediator.Send(new GetSourceByIdQuery { Id = id}));
    }
}
