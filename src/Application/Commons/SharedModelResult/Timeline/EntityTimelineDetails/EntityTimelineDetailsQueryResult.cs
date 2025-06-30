using Domain.Enums;

namespace Application.Commons.SharedModelResult.Timeline.EntityTimelineDetails
{
    public class EntityTimelineDetailsQueryResult
    {
        public Guid Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public ActionType ActionType { get; set; }
        public ActionBy ActionBy { get; set; }
        public IEnumerable<PropertyChangeResult>? ModifiedProperties { get; set; }
        public string? Comment { get; set; }
        public int VersionNumber { get; set; }
    }
}
