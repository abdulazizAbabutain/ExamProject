namespace Domain.Entities.History
{
    public class HistoryPropertyChanges
    {
        public Guid? ObjectId { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public bool IsNullable { get; set; }
        public object? OldValue { get; set; }
        public object? NewValue { get; set; }
    }
}
