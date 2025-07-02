using Application.Commons.Managers;
using Application.Commons.Models.Results;
using Application.Commons.SharedModelResult.Timeline.EntityTimelineDetails;
using Domain.Extentions;
using MapsterMapper;
using MediatR;

namespace Application.EntitlesTimeline.Queries.EntityTimelineDetails
{
    public class EntityTimelineDetailsQueryHandler(IAuditManager auditManager, IMapper mapper) : IRequestHandler<EntityTimelineDetailsQuery, Result<EntityTimelineDetailsQueryResult>>
    {
        private readonly IAuditManager _auditManager = auditManager;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<EntityTimelineDetailsQueryResult>> Handle(EntityTimelineDetailsQuery request, CancellationToken cancellationToken)
        {
            var result = _auditManager.AuditTrailService.GetEntityTrailDetails(request.TimelineId, request.EntityName, request.EntityId);
            if (result.IsNull())
                return Result<EntityTimelineDetailsQueryResult>.NotFoundFailure($"entity train with id {request.TimelineId} was not found");

            return Result<EntityTimelineDetailsQueryResult>.Success(_mapper.Map<EntityTimelineDetailsQueryResult>(result));
        }
    }
}
