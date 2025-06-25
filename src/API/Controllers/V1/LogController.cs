using Application.Auditing.ApplicationLogs.Queries.LogSearchQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [Route("api/log")]
    public class LogController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetLogs([FromQuery] LogSearchQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
