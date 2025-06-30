using Application.Commons.Managers;
using Application.Commons.Models.Pageination;
using Application.Commons.Models.Results;
using Application.Commons.SharedModelResult.Timeline.EntityTimeline;
using Domain.Auditing;
using LinqKit;
using MapsterMapper;
using MediatR;

namespace Application.EntitlesTimeline.Queries.EntityTimeline
{
    internal class GetEntityTimelineQueryHandler(IAuditManager auditManager, IMapper mapper) : IRequestHandler<GetEntityTimelineQuery, Result<PageResponse<TimelineQueryResult>>>
    {
        private readonly IAuditManager _auditManager = auditManager;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<PageResponse<TimelineQueryResult>>> Handle(GetEntityTimelineQuery request, CancellationToken cancellationToken)
        {
            var query = PredicateBuilder.New<AuditTrail>(true);

            if (request.StartDate.HasValue)
                query = query.And(e => request.StartDate >= e.Timestamp);

            if (request.EndDate.HasValue)
                query = query.And(e => request.EndDate <= e.Timestamp);

            if (request.ActionType.HasValue)
                query = query.And(e => request.ActionType == e.Operation);

            if (request.ActionBy.HasValue)
                query = query.And(e => request.ActionBy == e.ChangedBy);

            if (!string.IsNullOrWhiteSpace(request.Comment))
                query = query.And(e => request.Comment.ToLower().Contains(e.Comment.ToLower()));

            query = query.And(e => request.EntityName == e.EntityName);

            var entityTrailResult = _auditManager.AuditTrailService.GetEntityTrail(query, request.PageNumber, request.PageSize, request.Id).ToList();
            var entityTimelineMapped = _mapper.Map<IEnumerable<TimelineQueryResult>>(entityTrailResult);
            var count = _auditManager.AuditTrailService.Count(request.Id);

            return Result<PageResponse<TimelineQueryResult>>.Success(new PageResponse<TimelineQueryResult>(entityTimelineMapped, request.PageNumber, request.PageSize, count));
        }
    }
}
