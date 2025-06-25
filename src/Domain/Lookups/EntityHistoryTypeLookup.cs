using Domain.Enums;

namespace Domain.Lookups
{
    public class EntityHistoryTypeLookup
    {
        public ActionType Id { get; set; }
        public string Value
        {
            get
            {
                switch (Id)
                {
                    case ActionType.Added:
                        return nameof(ActionType.Added);

                    case ActionType.Modified:
                        return nameof(ActionType.Modified);

                    case ActionType.Deleted:
                        return nameof(ActionType.Deleted);

                    default:
                        throw new NotImplementedException("no EntityHistoryType has been added");
                }
            }

        }
    }
}
