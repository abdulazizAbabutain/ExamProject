namespace Application.Questions.Queries.GetQuestionsById.ResultModel
{
    public class ReorderingQuestionQueryResult
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public FeedbackQueryResult? Feedback { get; set; }
    }
}
