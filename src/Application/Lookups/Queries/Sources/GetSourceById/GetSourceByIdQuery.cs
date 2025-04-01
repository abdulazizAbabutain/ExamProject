using MediatR;

namespace Application.Lookups.Queries.Sources.GetSourceById
{
    public class GetSourceByIdQuery : IRequest<GetSourceByIdQueryResult>
    {
        public Guid Id { get; set; }
    }
}
