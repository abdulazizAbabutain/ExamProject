using Domain.Enums;

namespace Application.Auditing.EntitiesLog.Queries.GetDeletedEntities;

public class GetDeletedEntitiesQueryResult
{
    public Guid EntityId { get; set; }
    public EntityName EntityName { get; set; }
    public DateTimeOffset ActionDate { get; set; }
    public ActionBy ActionBy { get; set; }
    public int VersionNumber { get; set; }
}
