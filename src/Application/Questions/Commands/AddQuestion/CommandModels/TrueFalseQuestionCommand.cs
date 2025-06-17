namespace Application.Questions.Commands.AddQuestion.CommandModels
{
    public class TrueFalseQuestionCommand
    {
        public bool IsCorrect { get; set; }
        public FeedbackCommand? Feedback { get; set; }
    }
}
