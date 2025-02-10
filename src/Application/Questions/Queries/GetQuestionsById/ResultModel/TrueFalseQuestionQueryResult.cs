namespace Application.Questions.Queries.GetQuestionsById.ResultModel
{
    public class TrueFalseQuestionQueryResult
    {
        public Guid Id { get; set; }
        public bool IsCorrect { get; set; }
        public string? WrongFeedBack { get; set; }
        public string? AnswerFeedBack { get; set; }
    }
}
