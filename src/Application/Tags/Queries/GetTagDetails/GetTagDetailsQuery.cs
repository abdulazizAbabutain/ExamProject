using MediatR;

namespace Application.Tags.Queries.GetTagDetails;

public class GetTagDetailsQuery : IRequest<GetTagDetailsQueryResult>
{
    public Guid Id { get; set; }
}