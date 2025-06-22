namespace Domain.Entities.Examiner
{
    public class TrueFalseQuestion
    {
        public Guid Id { get; set; }
        public bool IsCorrect { get; set; }
        public string? WrongAnswerFeedBack { get; set; }
        public string? AnswerFeedBack { get; set; }
        public bool RequireJustification { get; set; } = false;
        public string? Justification { get; set; }


        public TrueFalseQuestion(bool isCorrect, string? wrongAnswerFeedBack ,string? answerFeedBack) 
        {
            Id = Guid.NewGuid();
            IsCorrect = isCorrect;
            WrongAnswerFeedBack = wrongAnswerFeedBack;
            AnswerFeedBack = answerFeedBack;
        }
    }
}
