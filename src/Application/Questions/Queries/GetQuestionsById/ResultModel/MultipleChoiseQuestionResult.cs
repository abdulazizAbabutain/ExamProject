namespace Application.Questions.Queries.GetQuestionsById.ResultModel
{
    public class MultipleChoiseQuestionResult
    {
        public Guid Id { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public float Weight { get; set; }
        public string? FeedBack { get; set; }
    }
}
