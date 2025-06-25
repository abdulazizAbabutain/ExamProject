using Application.Commons.Managers;
using Application.Commons.Models.Pageination;
using Domain.Auditing;
using LinqKit;
using MediatR;

namespace Application.Auditing.ApplicationLogs.Queries.LogSearchQuery
{
    public class LogSearchQueryHandler(IAuditManager auditManager) : IRequestHandler<LogSearchQuery, PageResponse<ApplicationLog>>
    {
        private readonly IAuditManager _auditManager = auditManager;

        public async Task<PageResponse<ApplicationLog>> Handle(LogSearchQuery request, CancellationToken cancellationToken)
        {
            var query = PredicateBuilder.New<ApplicationLog>(true);


            if (request.StartDate.HasValue)
                query = query.And(e => request.StartDate >= e.Timestamp);

            if (request.EndDate.HasValue)
                query = query.And(e => request.EndDate <= e.Timestamp);

            if (request.Level.HasValue)
                query = query.And(e => request.Level == e.Level);

            if (!string.IsNullOrWhiteSpace(request.Message))
                query = query.And(e => e.Message.ToLower().Contains(request.Message.ToLower()));

            var logs = _auditManager.ApplicationLogRepository.GetAll(query, request.PageNumber, request.PageSize);
            var count = _auditManager.ApplicationLogRepository.Count();

            return new PageResponse<ApplicationLog>(logs, request.PageNumber, request.PageSize, count);
        }
    }
}
