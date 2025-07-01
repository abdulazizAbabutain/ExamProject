namespace Application.Categories.Commands.AddCategory
{
    public class AddCategoryCommandResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentId { get; set; }
    }
}
