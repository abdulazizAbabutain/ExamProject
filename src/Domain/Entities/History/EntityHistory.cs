using Domain.Entities.Examiner;
using Domain.Enums;
using Domain.Extentions;
using System.Reflection;
using System.Threading.Channels;

namespace Domain.Entities.History
{
    public class EntityHistory
    {


        #region constructor
        public EntityHistory(EntityHistoryType historyType, int versionNumber)
        {
            Id = Guid.NewGuid();
            Type = historyType;
            VersionNumber = versionNumber;
            ActionDate = DateTimeOffset.UtcNow;
        }

        private EntityHistory()
        {

        }

        #endregion
        public Guid Id { get; set; }
        public EntityHistoryType Type { get; set; }
        public int VersionNumber { get; set; }
        public DateTimeOffset ActionDate { get; set; }
        public List<HistoryPropertyChanges> PropertyChanges { get; set; } = new List<HistoryPropertyChanges>();






        public void UpdatePropity(string name, Type dataType, object oldValue, object newValue, Guid? objectId = null)
        {
            PropertyChanges.Add(new HistoryPropertyChanges
            {
                ObjectId = objectId,
                IsNullable = Nullable.GetUnderlyingType(dataType) != null,
                Name = name,
                DataType = dataType.GetFriendlyName(),
                OldValue = oldValue,
                NewValue = newValue
            });
        }

        public void GetPropertyChanges<T>(T entityNew, T? entityOld)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                // Make sure the property is both readable and writable if needed 
                // (otherwise, you can skip or handle read-only properties accordingly)
                if (!prop.CanRead || prop.Name == "Histories") continue;

                var ss = prop.GetType();
                bool isNullableValueType = Nullable.GetUnderlyingType(ss) != null;

                var newValue = prop.GetValue(entityNew, null);

                PropertyChanges.Add(new HistoryPropertyChanges
                {
                    Name = prop.Name,
                    DataType = prop.PropertyType.GetFriendlyName(), // or just prop.PropertyType.Name
                    IsNullable = isNullableValueType,
                    OldValue = null,
                    NewValue = newValue
                });
            }
        }


       
    }
}