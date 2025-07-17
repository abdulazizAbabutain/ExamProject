using Application.Commons.Models.Pageination;
using Application.Commons.Models.Results;
using Domain.Enums;
using MediatR;

namespace Application.Lookups.Queries.Tags.GetAllTags
{
    public class GetAllTagsQuery : PageRequest, IRequest<Result<PageResponse<GetAllTagsQueryResult>>>
    {
        public string? Search { get; set; }
        public bool? IsArchived { get; set; }
        public bool? NeedReview { get; set; }
        public EntityLanguage? Language { get; set; }
    }
}
