using Application.Commons.Models.Pageination;
using Application.Commons.Models.Results;
using Domain.Enums;
using MediatR;

namespace Application.Auditing.EntitiesLog.Queries.GetDeletedEntities;

public class GetDeletedEntitiesQuery : PageRequest, IRequest<Result<PageResponse<GetDeletedEntitiesQueryResult>>>
{
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public EntityName? EntityName { get; set; }
    public ActionBy? ActionBy { get; set; }
    public string? Comment{ get; set; }
}
