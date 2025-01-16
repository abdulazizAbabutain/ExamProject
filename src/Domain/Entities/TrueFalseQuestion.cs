namespace Domain.Entities
{
    public class TrueFalseQuestion
    {
        public Guid Id { get; set; }
        public required bool IsCorrect { get; set; }
        public string? FeedBack { get; set; }
    }
}
