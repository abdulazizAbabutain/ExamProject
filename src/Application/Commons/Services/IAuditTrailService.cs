using Domain.Auditing;
using Domain.Enums;

namespace Application.Commons.Services
{
    public interface IAuditTrailService
    {
        void AddNewEntity<T>(EntityName entityName, Guid entityId, ActionBy actionBy, T entity, int versionNumber, string? comment = null);
        void UpdateEntity<T>(EntityName entityName, Guid entityId, ActionType actionType, ActionBy actionBy, T oldEntity, T newEntity, int versionNumber, string? comment = null);
        void UpdateEntitiesBulk<T>(EntityName entityName, IEnumerable<(Guid EntityId, ActionType ActionType, ActionBy ActionBy, T OldEntity, T NewEntity, int Version, string? Comment)> changes);
        IEnumerable<AuditTrail> GetEntityTrail(Func<AuditTrail, bool> func, int pageNumber, int pageSize, Guid entityId);
        AuditTrail GetEntityTrailDetails(Guid trailId, EntityName entityName, Guid entityId);
        void DeleteEntity<T>(EntityName entityName, Guid entityId, ActionType actionType, ActionBy actionBy, T entity, int versionNumber, string? comment = null);
        int Count(Guid entityId);
        IEnumerable<AuditTrail> GetDeletedEntities(Func<AuditTrail, bool> func, int pageNumber, int pageSize);
        int CountDeletedEntity();
    }
}
