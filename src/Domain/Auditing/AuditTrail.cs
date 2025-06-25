using Domain.Enums;
using System.Threading.Channels;

namespace Domain.Auditing
{
    public class AuditTrail
    {
        public AuditTrail(EntitiesName entityName, Guid entityId, ActionType operation, ActionBy changedBy,int versionNumber, string? comment = null)
        {
            EntityName = entityName;
            EntityId = entityId;
            Operation = operation;
            ChangedBy = changedBy;
            Comment = comment;
            VersionNumber = versionNumber;
            Id = Guid.CreateVersion7();
            Timestamp = DateTimeOffset.Now;
        }

        public Guid Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public EntitiesName EntityName { get; set; }
        public Guid EntityId { get; set; }
        public ActionType Operation { get; set; }
        public ActionBy ChangedBy { get; set; }
        public int VersionNumber { get; set; }
        public Dictionary<string, object>? Changes { get; set; }
        public string? Comment { get; set; }

        public void GetChanges<T>(T oldObj, T newObj)
        {
            var changes = new Dictionary<string, object>();
            foreach (var prop in typeof(T).GetProperties())
            {
                var oldVal = prop.GetValue(oldObj);
                var newVal = prop.GetValue(newObj);
                if (!Equals(oldVal, newVal))
                {
                    changes[prop.Name] = new { Old = oldVal, New = newVal };
                }
            }
            Changes = changes;
        }
    }
}
