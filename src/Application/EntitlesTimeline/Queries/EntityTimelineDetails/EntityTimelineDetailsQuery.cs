using Application.Commons.Models.Results;
using Application.Commons.SharedModelResult.Timeline.EntityTimelineDetails;
using Domain.Enums;
using MediatR;
using Newtonsoft.Json;

namespace Application.EntitlesTimeline.Queries.EntityTimelineDetails
{
    public class EntityTimelineDetailsQuery : IRequest<Result<EntityTimelineDetailsQueryResult>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public EntitiesName EntityName { get; set; }
        [JsonIgnore]
        public Guid EntityId { get; set; }
    }
}
