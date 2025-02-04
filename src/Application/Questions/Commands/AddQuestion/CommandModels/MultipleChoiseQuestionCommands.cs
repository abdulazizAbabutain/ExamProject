namespace Application.Questions.Commands.AddQuestion.CommandModels
{
    public class MultipleChoiseQuestionCommand
    {
        public required string OptionText { get; set; }
        public required bool IsCorrect { get; set; }
        public float Weight { get; set; } 
        public string? FeedBack { get; set; }
    }
}
