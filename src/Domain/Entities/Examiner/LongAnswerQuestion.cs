namespace Domain.Entities.Examiner
{
    public class LongAnswerQuestion
    {
        public Guid Id { get; set; }
        public required string Answer { get; set; }
    }
}
