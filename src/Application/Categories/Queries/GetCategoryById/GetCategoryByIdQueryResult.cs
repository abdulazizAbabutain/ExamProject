using Domain.Extentions;

namespace Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public IEnumerable<GetCategoryByIdQueryResult> SubCategory { get; set; }
    }
}
