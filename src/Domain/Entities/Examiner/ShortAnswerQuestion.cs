namespace Domain.Entities.Examiner
{
    public class ShortAnswerQuestion
    {
        public ShortAnswerQuestion(string correctAnswer,List<string>? possibleAnswers, string? wrongAnswerFeedBack, string? correctAnswerFeedBack)
        {
            Id = Guid.CreateVersion7();
            CorrectAnswer = correctAnswer;
            PossibleAnswers = possibleAnswers;
            WrongAnswerFeedBack = wrongAnswerFeedBack;
            CorrectAnswerFeedBack = correctAnswerFeedBack;
        }

        public Guid Id { get; set; }
        public string CorrectAnswer { get; private set; }
        public List<string>? PossibleAnswers { get; private set; }
        public string? WrongAnswerFeedBack { get; private set; }
        public string? CorrectAnswerFeedBack { get; private set; }
    }
}
