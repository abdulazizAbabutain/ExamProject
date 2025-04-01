namespace Application.Histories.Queries.GetQuestionHistory.Models
{
    public class PropertyChangesQueryResult
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public bool IsNullable { get; set; }
        public object? OldValue { get; set; }
        public object? NewValue { get; set; }
    }
}
