using Application.Commons.Services;
using Domain.Auditing;
using Domain.Enums;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Audits
{
    public class AuditTrailService : BaseRepository<AuditTrail> , IAuditTrailService
    {
        public AuditTrailService(string databasePath) 
            : base(databasePath, nameof(AuditTrail))
        {
        }


        public void AddNewEntity(EntitiesName entityName, Guid entityId ,ActionBy actionBy, int versionNumber, string? comment = null)
        {
            var auditTrail = new AuditTrail(entityName, entityId, ActionType.Added, actionBy, versionNumber, comment: comment);
            Insert(auditTrail);
        }

        public void UpdateEntity<T>(EntitiesName entityName, Guid entityId,ActionType actionType, ActionBy actionBy, T oldEntity, T newEntity, int versionNumber, string? comment = null)
        {
            var auditTrail = new AuditTrail(entityName, entityId, actionType, actionBy, versionNumber, comment: comment);
            auditTrail.GetChanges(oldEntity,newEntity);
            Insert(auditTrail);
        }

        public IEnumerable<AuditTrail> GetEntityTrail(Func<AuditTrail, bool> func, int pageNumber, int pageSize, Guid entityId)
        {
            return _collection.Find(ad => ad.EntityId == entityId).Where(func)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(e => e.Timestamp);
        }

        

        public int Count(Guid entityId)
        {
            return _collection.Find(ad => ad.EntityId == entityId).Count();
        }

    }
}
