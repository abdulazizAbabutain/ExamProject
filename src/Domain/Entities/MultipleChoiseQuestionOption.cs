namespace Domain.Entities
{
    public class MultipleChoiseQuestionOption
    {
        public Guid Id { get; set; }
        public required string OptionText { get; set; }
        public required bool IsCorrect { get; set; }
        public required float Weight { get; set; }
        public string? FeedBack { get; set; }
    }
}
