namespace Domain.Entities.Examiner
{
    public class ReorderingQuestion
    {
        public ReorderingQuestion(string value, int order, string? wrongAnswerFeedBack, string? correctAnswerFeedBack)
        {
            Id = Guid.CreateVersion7();
            Value = value;
            Order = order;
            WrongAnswerFeedBack = wrongAnswerFeedBack;
            CorrectAnswerFeedBack = correctAnswerFeedBack;
        }
        public Guid Id { get; private set; }
        public string Value { get; private set; }
        public int Order { get; private set; }
        public string? WrongAnswerFeedBack { get; private set; }
        public string? CorrectAnswerFeedBack { get; private set; }
    }
}
