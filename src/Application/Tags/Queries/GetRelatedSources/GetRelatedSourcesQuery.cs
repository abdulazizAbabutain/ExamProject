using Application.Commons.Models.Pageination;
using Application.Commons.Models.Results;
using Domain.Enums;
using MediatR;

namespace Application.Tags.Queries.GetRelatedSources;

public class GetRelatedSourcesQuery : PageRequest, IRequest<Result<PageResponse<GetRelatedSourcesQueryResult>>>
{
    public Guid TagId { get; set; }
    public SourceType? TypeId { get; set; }
    public string? Title { get; set; }

}
