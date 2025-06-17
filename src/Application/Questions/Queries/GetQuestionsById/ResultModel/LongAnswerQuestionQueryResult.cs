namespace Application.Questions.Queries.GetQuestionsById.ResultModel
{
    public class LongAnswerQuestionQueryResult
    {
        public int? MaximinWords { get; set; }
        public int? MinimanWords { get; set; }
        public string Answer { get; set; }
        public string? GeneralFeedback { get; set; }
    }
}
