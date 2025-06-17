namespace Application.Questions.Commands.AddQuestion.CommandModels
{
    public class ShortAnswerQuestionCommand
    {
        public required string CorrectAnswer { get; set; }
        public List<string>? PossibleAnswers { get; set; }
        public FeedbackCommand? Feedback { get; set; }
    }
}
