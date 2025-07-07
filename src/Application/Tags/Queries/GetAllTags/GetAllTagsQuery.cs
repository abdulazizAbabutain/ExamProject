using Application.Commons.Models.Pageination;
using Application.Commons.Models.Results;
using Domain.Enums;
using MediatR;

namespace Application.Lookups.Queries.Tags.GetAllTags
{
    public class GetAllTagsQuery : PageRequest, IRequest<Result<PageResponse<GetAllTagsQueryResult>>>
    {
        public ColorCategory? BackgroundColorGroup { get; set; }
        public ColorCategory? TextColorGroup { get; set; }
        public string? Search { get; set; }
        public bool? IsArchived { get; set; }
    }
}
