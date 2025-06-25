using Domain.Auditing;
using Domain.Enums;

namespace Application.Commons.Services
{
    public interface IAuditTrailService
    {
        public void AddNewEntity(EntitiesName entityName, Guid entityId, ActionBy actionBy, int versionNumber, string? comment = null);
        public void UpdateEntity<T>(EntitiesName entityName, Guid entityId, ActionType actionType, ActionBy actionBy, T oldEntity, T newEntity, int versionNumber, string? comment = null);
        IEnumerable<AuditTrail> GetEntityTrail(Func<AuditTrail, bool> func, int pageNumber, int pageSize, Guid entityId);
        public int Count(Guid entityId);
    }
}
