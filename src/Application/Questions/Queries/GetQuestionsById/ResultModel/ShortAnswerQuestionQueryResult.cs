namespace Application.Questions.Queries.GetQuestionsById.ResultModel
{
    public class ShortAnswerQuestionQueryResult
    {
        public required string CorrectAnswer { get; set; }
        public List<string>? PossibleAnswers { get; set; }
        public FeedbackQueryResult? Feedback { get; set; }
    }
}
