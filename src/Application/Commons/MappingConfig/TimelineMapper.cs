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

            config.NewConfig<PropertyChange, PropertyChangeResult>();
        }
    }
}
