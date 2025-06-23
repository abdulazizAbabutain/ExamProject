using MediatR;

namespace Application.Sources.Queries.GetSourceById
{
    public class GetSourceByIdQuery : IRequest<GetSourceByIdQueryResult>
    {
        public Guid Id { get; set; }
    }
}
