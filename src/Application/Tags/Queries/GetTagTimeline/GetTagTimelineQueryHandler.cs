using Application.Commons.Managers;
using Application.Commons.Models.Pageination;
using Application.Commons.Models.Results;
using Domain.Auditing;
using Domain.Entities.EntityLookup;
using LinqKit;
using MediatR;

namespace Application.Tags.Queries.GetTagTimeline
{
    public class GetTagTimelineQueryHandler(IAuditManager auditManager) : IRequestHandler<GetTagTimelineQuery, Result<PageResponse<GetTagTimelineQueryResult>>>
    {
        private readonly IAuditManager _auditManager = auditManager;

        public async Task<Result<PageResponse<GetTagTimelineQueryResult>>> Handle(GetTagTimelineQuery request, CancellationToken cancellationToken)
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

            var result = _auditManager.AuditTrailService.GetEntityTrail(query, request.PageNumber, request.PageSize, request.Id).Select(e => new GetTagTimelineQueryResult
            {
                ActionBy = e.ChangedBy,
                ActionType = e.Operation,
                Comment = e.Comment,
                ModifiedProperties = e.Changes,
                Timestamp = e.Timestamp,
                VersionNumber = e.VersionNumber
            });
            var count = _auditManager.AuditTrailService.Count(request.Id);

            return Result<PageResponse<GetTagTimelineQueryResult>>.Success(new PageResponse<GetTagTimelineQueryResult>(result, request.PageNumber, request.PageSize, count));
        }
    }
}
