using MediatR;

namespace Application.Lookups.Commands.Categories.AddCategory
{
    public class AddCategoryCommand : IRequest
    {
        public string name {  get; set; }
        public Guid? ParentId { get; set; }
    }
}
