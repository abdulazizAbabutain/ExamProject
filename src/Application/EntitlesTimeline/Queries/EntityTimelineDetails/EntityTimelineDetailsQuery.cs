using Application.Commons.Models.Results;
using Application.Commons.SharedModelResult.Timeline.EntityTimelineDetails;
using Domain.Enums;
using MediatR;

namespace Application.EntitlesTimeline.Queries.EntityTimelineDetails
{
    public class EntityTimelineDetailsQuery : IRequest<Result<EntityTimelineDetailsQueryResult>>
    {
        public Guid TimelineId { get; set; }
        public EntitiesName EntityName { get; set; }
        public Guid EntityId { get; set; }
    }
}
