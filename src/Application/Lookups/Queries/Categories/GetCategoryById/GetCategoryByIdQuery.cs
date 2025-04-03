using MediatR;

namespace Application.Lookups.Queries.Categories.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<GetCategoryByIdQueryResult>
    {
        public Guid Id { get; set; }
    }
}
