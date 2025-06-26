using Application.Commons.Models.Pageination;
using Domain.Auditing;
using Domain.Enums;
using MediatR;
using Serilog.Events;

namespace Application.Auditing.ApplicationLogs.Queries.LogSearchQuery
{
    public class LogSearchQuery : PageRequest, IRequest<PageResponse<ApplicationLog>>
    {
        public LogEventLevel? Level { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string? Message { get; set; }
        //public OrderType? Order { get; set; }
    }
}
