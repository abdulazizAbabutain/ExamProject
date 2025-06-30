using Domain.Auditing;
using Domain.Enums;

namespace Application.Commons.Services
{
    public interface IAuditTrailService
    {
        void AddNewEntity<T>(EntitiesName entityName, Guid entityId, ActionBy actionBy, T entity, int versionNumber, string? comment = null);
        void UpdateEntity<T>(EntitiesName entityName, Guid entityId, ActionType actionType, ActionBy actionBy, T oldEntity, T newEntity, int versionNumber, string? comment = null);
        void UpdateEntitiesBulk<T>(EntitiesName entityName, IEnumerable<(Guid EntityId, ActionType ActionType, ActionBy ActionBy, T OldEntity, T NewEntity, int Version, string? Comment)> changes);
        IEnumerable<AuditTrail> GetEntityTrail(Func<AuditTrail, bool> func, int pageNumber, int pageSize, Guid entityId);
        void DeleteEntity<T>(EntitiesName entityName, Guid entityId, ActionType actionType, ActionBy actionBy, T entity, int versionNumber, string? comment = null);
        int Count(Guid entityId);
    }
}
