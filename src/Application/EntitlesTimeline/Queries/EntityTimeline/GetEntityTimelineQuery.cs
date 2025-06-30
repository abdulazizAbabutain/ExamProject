using Application.Commons.Models.Pageination;
using Application.Commons.Models.Results;
using Application.Commons.SharedModelResult.Timeline.EntityTimeline;
using Domain.Enums;
using MediatR;
using Newtonsoft.Json;

namespace Application.EntitlesTimeline.Queries.EntityTimeline
{
    public class GetEntityTimelineQuery : PageRequest, IRequest<Result<PageResponse<TimelineQueryResult>>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public EntitiesName EntityName { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public ActionType? ActionType { get; set; }
        public ActionBy? ActionBy { get; set; }
        public string? Comment { get; set; }
    }
}
