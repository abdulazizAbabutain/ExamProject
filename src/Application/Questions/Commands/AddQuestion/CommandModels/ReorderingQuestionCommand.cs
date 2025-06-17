namespace Application.Questions.Commands.AddQuestion.CommandModels
{
    public class ReorderingQuestionCommand
    {
        public string Value { get; set; }
        public int Order { get; set; }
        public FeedbackCommand? Feedback { get; set; }
    }
}
