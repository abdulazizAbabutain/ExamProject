using Domain.Extentions;

namespace Application.Categories.Commands.AddCategory
{
    public class AddCategoryCommandResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentId { get; set; }
        public int Level { get; set; }
        public bool HasChildren { get; set; }
        public bool IsRoot => ParentId.IsNull() && Level == 1;
    }
}
