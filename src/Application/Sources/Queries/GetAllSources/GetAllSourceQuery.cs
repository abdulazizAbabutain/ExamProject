using Application.Commons.Models.Pageination;
using Application.Commons.Models.Results;
using Domain.Enums;
using MediatR;

namespace Application.Sources.Queries.GetAllSources
{
    public class GetAllSourceQuery : PageRequest, IRequest<Result<PageResponse<GetAllSourceQueryResult>>>
    {
        public SourceType? TypeId { get; set; }
        public Guid? Tags { get; set; }
        public string? Title { get; set; }
    }
}
