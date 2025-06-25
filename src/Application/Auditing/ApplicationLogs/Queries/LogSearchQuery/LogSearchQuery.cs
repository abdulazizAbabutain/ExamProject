using Application.Commons.Models.Pageination;
using Domain.Auditing;
using MediatR;
using Serilog.Events;

namespace Application.Auditing.ApplicationLogs.Queries.LogSearchQuery
{
    public class LogSearchQuery : PageRequest, IRequest<PageResponse<ApplicationLog>>
    {
        public LogEventLevel? Level { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Message { get; set; }
    }
}
