using Application.Commons.Models.Pageination;
using Application.Commons.Models.Results;
using Domain.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Tags.Queries.GetTagTimeline
{
    public class GetTagTimelineQuery : PageRequest,IRequest<Result<PageResponse<GetTagTimelineQueryResult>>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public ActionType? ActionType { get; set; }
        public ActionBy? ActionBy { get; set; }
        public string? Comment { get; set; }
    }
}
