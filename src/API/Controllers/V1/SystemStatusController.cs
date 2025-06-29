using Application.Commons.Extensions;
using Application.Commons.Managers;
using Application.Commons.Models.SystemModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace API.Controllers.V1
{
    [Route("api/system/status")]
    public class SystemStatusController(ISystemManager systemManager) : ControllerBase
    {
        private readonly ISystemManager _SystemManager = systemManager;

        [HttpGet]
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
    }
}
