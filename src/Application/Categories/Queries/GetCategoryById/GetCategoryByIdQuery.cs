using Application.Commons.Models.Results;
using MediatR;

namespace Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<Result<GetCategoryByIdQueryResult>>
    {
        public Guid Id { get; set; }
    }
}
