namespace Application.Commons.SharedModelResult.Timeline.EntityTimelineDetails
{
    public class PropertyChangeResult
    {
        public string PropertyName { get; set; } = default!;
        public object? OldValue { get; set; }
        public object? NewValue { get; set; }
        public string PropertyType { get; set; } = default!;
    }
}
