using Application.Commons.Services;
using Domain.Auditing;
using Domain.Enums;
using Infrastructure.Repositories.BaseRepository;
using System.Security.Principal;
using System.Threading.Channels;

namespace Infrastructure.Audits;
public class AuditTrailService : BaseRepository<AuditTrail>, IAuditTrailService
{
    private readonly Channel<AuditTrail> _auditQueue;

    public AuditTrailService(string databasePath)
        : base(databasePath, nameof(AuditTrail))
    {
        _auditQueue = Channel.CreateUnbounded<AuditTrail>();
        StartBackgroundAuditWriter();
    }

    public void AddNewEntity<T>(EntityName entityName, Guid entityId, ActionBy actionBy,T entity ,int versionNumber, string? comment = null)
    {
        var auditTrail = new AuditTrail(entityName, entityId, ActionType.Added, actionBy, versionNumber, comment);
        auditTrail.GetChanges(entity, false);
        EnqueueAudit(auditTrail);
    }

    public void UpdateEntity<T>(EntityName entityName, Guid entityId, ActionType actionType, ActionBy actionBy, T oldEntity, T newEntity, int versionNumber, string? comment = null)
    {
        var auditTrail = new AuditTrail(entityName, entityId, actionType, actionBy, versionNumber, comment);
        auditTrail.GetChanges(oldEntity, newEntity);
        EnqueueAudit(auditTrail);
    }

    public void AddEntitiesBulk(IEnumerable<AuditTrail> auditTrails)
    {
        if (auditTrails == null || !auditTrails.Any())
            return;

        _collection.InsertBulk(auditTrails);
    }

    public void DeleteEntity<T>(EntityName entityName, Guid entityId, ActionType actionType, ActionBy actionBy,T entity ,int versionNumber, string? comment = null)
    {
        var auditTrail = new AuditTrail(entityName, entityId, actionType, actionBy, versionNumber, comment);
        auditTrail.GetChanges(entity, true);
        EnqueueAudit(auditTrail);
    }

    public void UpdateEntitiesBulk<T>(
        EntityName entityName,
        IEnumerable<(Guid EntityId, ActionType ActionType, ActionBy ActionBy, T OldEntity, T NewEntity, int Version, string? Comment)> changes)
    {
        var auditTrails = new List<AuditTrail>();

        foreach (var change in changes)
        {
            var audit = new AuditTrail(entityName, change.EntityId, change.ActionType, change.ActionBy, change.Version, change.Comment);
            audit.GetChanges(change.OldEntity, change.NewEntity);
            auditTrails.Add(audit);
        }

        AddEntitiesBulk(auditTrails);
    }

    public IEnumerable<AuditTrail> GetEntityTrail(Func<AuditTrail, bool> func, int pageNumber, int pageSize, Guid entityId)
    {
        return _collection.Find(ad => ad.EntityId == entityId)
            .Where(func)
            .OrderBy(e => e.Timestamp)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public IEnumerable<AuditTrail> GetDeletedEntities(Func<AuditTrail, bool> func, int pageNumber, int pageSize)
    {
        return _collection.FindAll()
            .Where(func)
            .Where(e => e.Operation == ActionType.Deleted)
            .OrderBy(e => e.Timestamp)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }


    public int Count(Guid entityId)
    {
        return _collection.Find(ad => ad.EntityId == entityId).Count();
    }



    public int CountDeletedEntity()
    {
        return _collection.Find(ad => ad.Operation == ActionType.Deleted).Count();
    }

    private void StartBackgroundAuditWriter()
    {
        Task.Run(async () =>
        {
            var buffer = new List<AuditTrail>();

            while (await _auditQueue.Reader.WaitToReadAsync())
            {
                while (_auditQueue.Reader.TryRead(out var audit))
                {
                    buffer.Add(audit);
                    if (buffer.Count >= 50)
                    {
                        _collection.InsertBulk(buffer);
                        buffer.Clear();
                    }
                }

                if (buffer.Any())
                {
                    _collection.InsertBulk(buffer);
                    buffer.Clear();
                }
            }
        });
    }

    public void EnqueueAudit(AuditTrail audit)
    {
        _auditQueue.Writer.TryWrite(audit);
    }

    public AuditTrail GetEntityTrailDetails(Guid trailId, EntityName entityName, Guid entityId)
    {
        return GetCollection().Find(e => e.Id == trailId && e.EntityName == entityName && e.EntityId == entityId).FirstOrDefault();
    }
}