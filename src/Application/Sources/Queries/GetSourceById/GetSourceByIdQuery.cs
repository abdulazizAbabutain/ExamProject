using Application.Commons.Models.Results;
using MediatR;

namespace Application.Sources.Queries.GetSourceById
{
    public class GetSourceByIdQuery : IRequest<Result<GetSourceByIdQueryResult>>
    {
        public Guid Id { get; set; }
    }
}
