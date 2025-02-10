namespace Application.Questions.Commands.AddQuestion.CommandModels
{
    public class MultipleChoiseQuestionCommand
    {
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public float Weight { get; set; } 
        public string? FeedBack { get; set; }
    }
}
