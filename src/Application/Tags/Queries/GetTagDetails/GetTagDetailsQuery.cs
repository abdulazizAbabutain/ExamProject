using Application.Commons.Models.Results;
using MediatR;

namespace Application.Tags.Queries.GetTagDetails;

public class GetTagDetailsQuery : IRequest<Result<GetTagDetailsQueryResult>>
{
    public Guid Id { get; set; }
}