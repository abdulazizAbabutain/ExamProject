using Application.Commons.Models.Pageination;
using Domain.Enums;
using MediatR;

namespace Application.Lookups.Queries.Tags.GetAllTags
{
    public class GetAllTagsQuery : PageRequest, IRequest<PageResponse<GetAllTagsQueryResult>>
    {
        public ColorCategory? ColorCategory { get; set; }
        public string? Search { get; set; }
    }
}
