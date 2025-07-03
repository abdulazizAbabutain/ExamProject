using Application.Commons.Managers;
using Application.Commons.Models.Pageination;
using Application.Commons.Models.Results;
using Domain.Auditing;
using Domain.Extentions;
using LinqKit;
using MapsterMapper;
using MediatR;

namespace Application.Auditing.EntitiesLog.Queries.GetDeletedEntities;

public class GetDeletedEntitiesQueryHandler(IAuditManager auditManager,IMapper mapper) : IRequestHandler<GetDeletedEntitiesQuery, Result<PageResponse<GetDeletedEntitiesQueryResult>>>
{
    private readonly IAuditManager _auditManager = auditManager;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<PageResponse<GetDeletedEntitiesQueryResult>>> Handle(GetDeletedEntitiesQuery request, CancellationToken cancellationToken)
    {
        var query = PredicateBuilder.New<AuditTrail>(true);

        if (request.StartDate.HasValue)
            query = query.And(e => request.StartDate >= e.Timestamp);
        if (request.EndDate.HasValue)
            query = query.And(e => request.EndDate <= e.Timestamp);
        if (request.ActionBy.HasValue)
            query = query.And(e => request.ActionBy == e.ChangedBy);
        if (request.EntityName.HasValue)
            query = query.And(e => request.EntityName == e.EntityName);
        if(!string.IsNullOrWhiteSpace(request.Comment))
            query = query.And(e => e.Comment.IsNotNull() && e.Comment.ToLower().Contains(request.Comment.ToLower()));

        var deletedEntities = _auditManager.AuditTrailService.GetDeletedEntities(query, request.PageNumber, request.PageSize);
        var deletedEntitiesCount = _auditManager.AuditTrailService.CountDeletedEntity();

        return Result<PageResponse<GetDeletedEntitiesQueryResult>>.Success(new PageResponse<GetDeletedEntitiesQueryResult>(_mapper.Map<IEnumerable<GetDeletedEntitiesQueryResult>>(deletedEntities),request.PageNumber, request.PageSize, deletedEntitiesCount));
    }
}
