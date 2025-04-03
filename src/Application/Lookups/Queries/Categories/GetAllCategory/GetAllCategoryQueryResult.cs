using Domain.Extentions;

namespace Application.Lookups.Queries.Categories.GetAllCategory
{
    public class GetAllCategoryQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public int Level { get; set; }
        public bool IsRoot => ParentId.IsNull() && Level == 1;
    }
}
