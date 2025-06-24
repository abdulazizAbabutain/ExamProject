using Application.Commons.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [Route("api/log")]
    public class LogController : ControllerBase
    {
        private readonly ILoggingService _loggingService;

        public LogController(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        [HttpGet()]
        public IActionResult GetLogs()
        {
            var logs = _loggingService.GetAllLogs();
            return Ok(logs);
        }
    }
}
