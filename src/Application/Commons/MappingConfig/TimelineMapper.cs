using Application.Auditing.EntitiesLog.Queries.GetDeletedEntities;
using Application.Commons.SharedModelResult.Timeline.EntityTimeline;
using Application.Commons.SharedModelResult.Timeline.EntityTimelineDetails;
using Domain.Auditing;
using Mapster;

namespace Application.Commons.MappingConfig
{
    internal class TimelineMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuditTrail, TimelineQueryResult>()
                .Map(dest => dest.ActionBy, src => src.ChangedBy)
                .Map(dest => dest.ActionType, src => src.Operation);

            config.NewConfig<AuditTrail, EntityTimelineDetailsQueryResult>()
                .Map(dest => dest.ActionBy, src => src.ChangedBy)
                .Map(dest => dest.ActionType, src => src.Operation)
                .Map(dest => dest.ModifiedProperties, src => src.Changes);


            config.NewConfig<AuditTrail, GetDeletedEntitiesQueryResult>()
                .Map(dest => dest.ActionBy, src => src.ChangedBy)
                .Map(dest => dest.EntityId, src => src.EntityId)
                .Map(dest => dest.ActionDate, src => src.Timestamp);

                


            config.NewConfig<PropertyChange, PropertyChangeResult>();
        }
    }
}
