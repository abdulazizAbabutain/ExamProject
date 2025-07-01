using MediatR;

namespace Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<GetCategoryByIdQueryResult>
    {
        public Guid Id { get; set; }
    }
}
