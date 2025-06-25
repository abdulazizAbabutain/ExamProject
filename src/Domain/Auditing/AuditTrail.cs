using Domain.Enums;

namespace Domain.Auditing
{
    public class AuditTrail
    {
        public Guid Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public EntitiesName EntityName { get; set; }
        public Guid EntityId { get; set; }
        public EntityLogAction Operation { get; set; }
        public string? ChangedBy { get; set; }
        public Dictionary<string, object>? Changes { get; set; }
        public string? Comment { get; set; }

        public Dictionary<string, object> GetChanges<T>(T oldObj, T newObj)
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
            return changes;
        }
    }
}
