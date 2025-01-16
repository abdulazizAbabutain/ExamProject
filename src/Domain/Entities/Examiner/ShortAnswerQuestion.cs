namespace Domain.Entities.Examiner
{
    public class ShortAnswerQuestion
    {
        public Guid Id { get; set; }
        public required List<string> PossibleAnswers { get; set; }
        public string? WrongAnswerFeedBack { get; set; }
        public string? CorrertAnswerFeedBack { get; set; }
    }
}
