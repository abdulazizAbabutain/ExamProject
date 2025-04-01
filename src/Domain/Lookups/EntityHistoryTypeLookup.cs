using Domain.Enums;

namespace Domain.Lookups
{
    public class EntityHistoryTypeLookup
    {
        public EntityHistoryType Id { get; set; }
        public string Value
        {
            get
            {
                switch (Id)
                {
                    case EntityHistoryType.Added:
                        return nameof(EntityHistoryType.Added);

                    case EntityHistoryType.Modifyed:
                        return nameof(EntityHistoryType.Modifyed);

                    case EntityHistoryType.Deleted:
                        return nameof(EntityHistoryType.Deleted);

                    default:
                        throw new NotImplementedException("no EntityHistoryType has been added");
                }
            }

        }
    }
}
