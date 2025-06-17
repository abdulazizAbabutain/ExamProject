namespace Application.Questions.Commands.AddQuestion.CommandModels
{
    public class LongAnswerQuestionCommand
    {
        public int? MaximinWords { get; set; }
        public int? MinimanWords { get; set; }
        public string Answer { get; set; }
        public string? GeneralFeedback { get; set; }
    }
}
