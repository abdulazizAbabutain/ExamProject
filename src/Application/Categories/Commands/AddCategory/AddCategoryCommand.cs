using Application.Commons.Models.Results;
using MediatR;

namespace Application.Categories.Commands.AddCategory
{
    public class AddCategoryCommand : IRequest<Result<AddCategoryCommandResult>>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentId { get; set; }
    }
}
