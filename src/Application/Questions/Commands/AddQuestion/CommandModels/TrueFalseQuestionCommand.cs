namespace Application.Questions.Commands.AddQuestion.CommandModels
{
    public class TrueFalseQuestionCommand
    {
        public bool IsCorrect { get; set; }
        public string? WrongFeedBack { get; set; }
        public string? AnswerFeedBack { get; set; }
    }
}
