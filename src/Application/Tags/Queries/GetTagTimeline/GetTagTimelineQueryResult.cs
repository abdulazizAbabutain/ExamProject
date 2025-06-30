using Domain.Enums;

namespace Application.Tags.Queries.GetTagTimeline
{
    public class GetTagTimelineQueryResult
    {
        public DateTimeOffset Timestamp { get; set; }
        public ActionType ActionType { get; set; }
        public ActionBy ActionBy { get; set; }
        public IEnumerable<PropertyChangeResult>? ModifiedProperties { get; set; }
        public string? Comment { get; set; }
        public int VersionNumber { get; set; }
    }
}
