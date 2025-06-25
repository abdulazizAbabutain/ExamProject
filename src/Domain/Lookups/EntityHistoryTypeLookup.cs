using Domain.Enums;

namespace Domain.Lookups
{
    public class EntityHistoryTypeLookup
    {
        public EntityLogAction Id { get; set; }
        public string Value
        {
            get
            {
                switch (Id)
                {
                    case EntityLogAction.Added:
                        return nameof(EntityLogAction.Added);

                    case EntityLogAction.Modifyed:
                        return nameof(EntityLogAction.Modifyed);

                    case EntityLogAction.Deleted:
                        return nameof(EntityLogAction.Deleted);

                    default:
                        throw new NotImplementedException("no EntityHistoryType has been added");
                }
            }

        }
    }
}
