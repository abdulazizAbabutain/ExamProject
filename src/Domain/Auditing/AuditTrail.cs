using Domain.Enums;
using Newtonsoft.Json;
using System.Collections;
using System.Reflection;

namespace Domain.Auditing;

public class AuditTrail
{
    public AuditTrail(EntityName entityName, Guid entityId, ActionType operation, ActionBy changedBy, int versionNumber, string? comment = null)
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
    public EntityName EntityName { get; set; }
    public Guid EntityId { get; set; }
    public ActionType Operation { get; set; }
    public ActionBy ChangedBy { get; set; }
    public int VersionNumber { get; set; }
    public List<PropertyChange> Changes { get; set; }
    public string? Comment { get; set; }

    public void GetChanges<T>(T oldObj, T newObj)
    {
        var changes = new List<PropertyChange>();

        foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var oldVal = prop.GetValue(oldObj);
            var newVal = prop.GetValue(newObj);

            if (IsEnumerableButNotString(prop.PropertyType))
            {
                var oldJson = JsonConvert.SerializeObject(oldVal);
                var newJson = JsonConvert.SerializeObject(newVal);

                if (oldJson != newJson)
                {
                    changes.Add(new PropertyChange
                    {
                        PropertyName = prop.Name,
                        OldValue = oldVal,
                        NewValue = newVal,
                        PropertyType = prop.PropertyType.Name
                    });
                }
            }
            else
            {
                bool valuesDiffer = oldVal is null
                    ? newVal is not null
                    : !oldVal.Equals(newVal);

                if (valuesDiffer)
                {
                    changes.Add(new PropertyChange
                    {
                        PropertyName = prop.Name,
                        OldValue = oldVal,
                        NewValue = newVal,
                        PropertyType = prop.PropertyType.Name
                    });
                }
            }
        }

        Changes = changes;
    }

    public void GetChanges<T>(T obj, bool isDelete)
    {
        var changes = new List<PropertyChange>();

        foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var val = prop.GetValue(obj);
            var type = prop.PropertyType;


            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = Nullable.GetUnderlyingType(type)!;
            }

            if (isDelete)
                changes.Add(new PropertyChange
                {
                    PropertyName = prop.Name,
                    OldValue = val,
                    NewValue = null,
                    PropertyType = type.Name
                });
            else
                changes.Add(new PropertyChange
                {
                    PropertyName = prop.Name,
                    OldValue = null,
                    NewValue = val,
                    PropertyType = type.Name
                });
        }

        Changes = changes;
    }

    private bool IsEnumerableButNotString(Type type)
    {
        return typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string);
    }
}
