using Application.Auditing.ApplicationLogs.Queries.LogSearchQuery;
using Application.Commons.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog.Events;

namespace API.Controllers.V1
{
    [Route("api/system/log")]
    public class LogController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetLogs([FromQuery] LogSearchQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost("enable")]
        public IActionResult Enable()
        {
            LoggingLevelController.LevelSwitch.MinimumLevel = LogEventLevel.Information;
            return Ok("Logging enabled.");
        }

        [HttpPost("disable")]
        public IActionResult Disable()
        {
            LoggingLevelController.LevelSwitch.MinimumLevel = LogEventLevel.Error; 
            return Ok("logging disabled.");
        }
        [HttpPost("debug")]
        public IActionResult Debug()
        {
            LoggingLevelController.LevelSwitch.MinimumLevel = LogEventLevel.Debug;
            return Ok("debugging enabled.");
        }

        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok(new { CurrentLevel = LoggingLevelController.LevelSwitch.MinimumLevel.ToString() });
        }
    }
}
