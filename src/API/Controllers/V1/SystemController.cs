using API.Interfaces;
using Application.Auditing.ApplicationLogs.Queries.LogSearchQuery;
using Application.Auditing.EntitiesLog.Queries.GetDeletedEntities;
using Application.Commons.Extensions;
using Application.Commons.Managers;
using Application.Commons.Models.SystemModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog.Events;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace API.Controllers.V1
{
    [Route("api/system")]
    public class SystemController(ISystemManager systemManager, IMediator mediator, IHttpResultResponder resultResponder) : ControllerBase
    {
        private readonly ISystemManager _SystemManager = systemManager;
        private readonly IMediator _mediator = mediator;
        private readonly IHttpResultResponder _resultResponder = resultResponder;

        [HttpGet("status")]
        public async Task<IActionResult> GetSystemStatus()
        {
            var sys = new AppStatusResponseModel
            {
                AppVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown",
                Uptime = DateTimeOffset.Now - Process.GetCurrentProcess().StartTime,
                OSVersion = Environment.OSVersion.ToString(),
                CpuArchitecture = RuntimeInformation.OSArchitecture.ToString(),
                MemoryUsageMB = StringExtension.ToReadableSize(GC.GetTotalMemory(false)),
                DatabaseSizeBytes = StringExtension.ToReadableSize(_SystemManager.SystemService.GetDatabaseFileSizeBytes()),
                Collections = _SystemManager.SystemService.AnalyzeCollections()
            };

            return Ok(sys);
        }
        [HttpGet("logs")]
        public async Task<IActionResult> GetLogs([FromQuery] LogSearchQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost("logs/enable")]
        public IActionResult Enable()
        {
            LoggingLevelController.LevelSwitch.MinimumLevel = LogEventLevel.Information;
            return Ok("Logging enabled.");
        }

        [HttpPost("logs/disable")]
        public IActionResult Disable()
        {
            LoggingLevelController.LevelSwitch.MinimumLevel = LogEventLevel.Error;
            return Ok("logging disabled.");
        }
        [HttpPost("logs/debug")]
        public IActionResult Debug()
        {
            LoggingLevelController.LevelSwitch.MinimumLevel = LogEventLevel.Debug;
            return Ok("debugging enabled.");
        }

        [HttpGet("logs/status")]
        public IActionResult Status()
        {
            return Ok(new { CurrentLevel = LoggingLevelController.LevelSwitch.MinimumLevel.ToString() });
        }
        [HttpGet("deletedEntity")]
        public async Task<IActionResult> GetDeletedEntities([FromQuery] GetDeletedEntitiesQuery query)
        {
            return _resultResponder.FromResult(HttpContext,await _mediator.Send(query));
        }


    }
}
